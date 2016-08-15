using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace NewShaderManifest
{
    class Compiler
    {
        private static string DECIMAL = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator; // ugly, dirty, stupid HACK
        private static Regex rgxIdentifier = new Regex("[a-zA-Z_][a-zA-Z_0-9]*");
        private static Regex rgxNumber = new Regex(@"^[+-]?([0-9]*" + DECIMAL + @"?[0-9]+|[0-9]+" + DECIMAL + "?[0-9]*)([eE][+-]?[0-9]+)?$");

        private enum Symbol
        {
            None,
            PoundDef,
            PoundUndef,
            PoundIf,
            PoundElse,
            PoundFi,
            PoundImport,
            PoundDefUser,
            PoundDefProg,
            OpStr,
            OpEq,
            OpNeq,
            OpGt,
            OpGte,
            OpLt,
            OpLte,
            OpDef,
            Number,
            Identifier,
            EndOfLine,
            EndOfFile,
            Unknown,
            Error
        };

        private class token
        {
            public Symbol sym;
            public string str;
            public int line;

            public token()
            {
                sym = Symbol.None;
                str = "";
                line = 0;
            }

            public token(Symbol sym, string str, int line)
            {
                this.sym = sym;
                this.str = str;
                this.line = line;
            }
        };

        private StreamReader input;
        private int curline = 0;
        private List<token> tokens;
        private token curtok;
        private int tokpos;

        bool getline(out string line)
        {
            line = "";
            try
            {
                line = input.ReadLine();
                ++curline;
            }
            catch (Exception e)
            {
                if (input.EndOfStream)
                    return false;
                Log.WriteLine("Error reading file: " + e.Message);
                return false;
            }
            return true;
        }

        Symbol symbolize(string s)
        {
            if (s == "#def") return Symbol.PoundDef;
            if (s == "#undef") return Symbol.PoundUndef;
            if (s == "#if") return Symbol.PoundIf;
            if (s == "#else") return Symbol.PoundElse;
            if (s == "#fi") return Symbol.PoundFi;
            if (s == "#import") return Symbol.PoundImport;
            if (s == "#defuser") return Symbol.PoundDef;
            if (s == "#defprog") return Symbol.PoundDef;
            if (s == "str") return Symbol.OpStr;
            if (s == "def") return Symbol.OpDef;
            if (s == "eq") return Symbol.OpEq;
            if (s == "neq") return Symbol.OpNeq;
            if (s == "gt") return Symbol.OpGt;
            if (s == "gte") return Symbol.OpGte;
            if (s == "lt") return Symbol.OpLt;
            if (s == "lte") return Symbol.OpLte;

            Match m = rgxNumber.Match(s);
            if (m.Success && m.Index == 0 && m.Length == s.Length)
                return Symbol.Number;
            else
            {
                m = rgxIdentifier.Match(s);
                if (m.Success && m.Index == 0 && m.Length == s.Length)
                    return Symbol.Identifier;
                else if (s == "global")
                    return Symbol.Unknown;
                else
                    return Symbol.Unknown;
            }

        }

        void lex(FileStream file)
        {
            input = new StreamReader(file);
            tokens = new List<token>();
            string line = "";
            while (!input.EndOfStream)
            {
                getline(out line);
                // kill leading and trailing whitespace
                line = line.Trim();

                // ignore comments
                int comment = (int)line.IndexOf("//");
                if (comment >= 0)
                    line = line.Substring(0, comment);

                // split the line
                string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    tokens.Add(new token(symbolize(part), part, curline));
                }
                tokens.Add(new token(Symbol.EndOfLine, "", curline));
            }
            tokens.Add(new token(Symbol.EndOfFile, "", curline));
        }

        void next()
        {
            curtok = tokens[++tokpos];
        }

        private void error(string message)
        {
            // Make sure parsing stops here
            Log.WriteLine(string.Format("{0}\n  at line {1} in {2}", message, curtok.line, ((FileStream)input.BaseStream).Name));
            //curtok = new token(Symbol.Error, curtok.str, curtok.line);
            return;
        }

        private bool accept(Symbol sym)
        {
            if (curtok.sym == sym)
            {
                next();
                return true;
            }
            return false;
        }

        private bool accept(Symbol[] symbols)
        {
            foreach (Symbol sym in symbols)
                if (accept(sym))
                    return true;
            return false;
        }

        private bool expect(Symbol sym)
        {
            if (accept(sym))
                return true;
            error(string.Format("syntax error: `{0}' found, expected `{1}'", curtok.sym, sym));
            return false;
        }

        private bool expect(Symbol[] symbols, string name)
        {
            foreach (Symbol sym in symbols)
                if (accept(sym))
                    return true;
            error(string.Format("syntax error: `{0}' found, expected {1}", curtok.sym, name));
            return false;
        }

        private ComparisionNode cond()
        {
            ComparisionNode node = null;
            Symbol sym = curtok.sym;
            if (expect(new Symbol[] {
                    Symbol.OpStr,
                    Symbol.OpEq,
                    Symbol.OpNeq,
                    Symbol.OpGt,
                    Symbol.OpGte,
                    Symbol.OpLt,
                    Symbol.OpLte,
                    Symbol.OpDef
                }, "operator"))
            {
                string left = curtok.str;
                expect(Symbol.Identifier);
                string right = curtok.str;
                if (accept(Symbol.Identifier) || accept(Symbol.Number)) { }
                node = new ComparisionNode();
                node.left = new TextNode(left);
                node.op = getbinop(sym);
                node.right = new TextNode(right);
            }
            return node;
        }

        private BinOpr getbinop(Symbol sym)
        {
            switch (sym)
            {
                case Symbol.OpStr: return BinOpr.OP_STR;
                case Symbol.OpEq: return BinOpr.OP_EQ;
                case Symbol.OpNeq: return BinOpr.OP_NE;
                case Symbol.OpGt: return BinOpr.OP_GT;
                case Symbol.OpGte: return BinOpr.OP_GE;
                case Symbol.OpLt: return BinOpr.OP_LT;
                case Symbol.OpLte: return BinOpr.OP_LE;
                case Symbol.OpDef: return BinOpr.OP_DEF;
            }
            throw new Exception(); // should never reach this point
        }

        private Node elsestat()
        {
            IfNode elsenode = new IfNode();

            expect(Symbol.PoundElse);
            if (curtok.sym == Symbol.OpStr ||
                curtok.sym == Symbol.OpEq ||
                curtok.sym == Symbol.OpNeq ||
                curtok.sym == Symbol.OpGt ||
                curtok.sym == Symbol.OpGte ||
                curtok.sym == Symbol.OpLt ||
                curtok.sym == Symbol.OpLte ||
                curtok.sym == Symbol.OpDef)
            {
                elsenode.condition = cond();
                expect(Symbol.EndOfLine);
                elsenode.true_branch = block();

                if (curtok.sym == Symbol.PoundElse)
                    elsenode.false_branch = elsestat();

                return elsenode;
            }
            else
            {
                expect(Symbol.EndOfLine);
                return block();
            }
        }

        private IfNode ifstat()
        {
            IfNode ifnode = new IfNode();

            expect(Symbol.PoundIf);
            ifnode.condition = cond();
            expect(Symbol.EndOfLine);
            ifnode.true_branch = block();

            if (curtok.sym == Symbol.PoundElse)
                ifnode.false_branch = elsestat();

            expect(Symbol.PoundFi);
            expect(Symbol.EndOfLine);
            return ifnode;
        }

        private Node block()
        {
            Node root = null;
            Node curnode = root;
            while (curtok.sym != Symbol.EndOfFile)
            {
                Node node = null;
                switch (curtok.sym)
                {
                    case Symbol.None:
                        error("wtf?");
                        return null;
                    case Symbol.PoundIf:
                        node = ifstat();
                        break;
                    case Symbol.PoundElse:
                        return root;
                    case Symbol.PoundFi:
                        return root;
                    case Symbol.PoundDef:
                        {
                            string funcname = curtok.str;
                            expect(Symbol.PoundDef);
                            string defname = curtok.str;
                            expect(Symbol.Identifier);
                            string defval = curtok.str;
                            bool b = accept(Symbol.Number) || accept(Symbol.Identifier);
                            expect(Symbol.EndOfLine);

                            FuncNode f = new FuncNode();
                            f.funcname = funcname;
                            f.arguments = new TextNode(defname);
                            if (b)
                                f.arguments.PushBack(new TextNode(defval));
                            node = f;
                        }
                        break;
                    case Symbol.PoundUndef:
                        {
                            string funcname = curtok.str;
                            expect(Symbol.PoundUndef);
                            string defname = curtok.str;
                            expect(Symbol.Identifier);
                            expect(Symbol.EndOfLine);

                            FuncNode f = new FuncNode();
                            f.funcname = funcname;
                            f.arguments = new TextNode(defname);
                            f.arguments.PushBack(new TextNode(0));
                            node = f;
                        }
                        break;
                    case Symbol.PoundImport:
                        {
                            string funcname = curtok.str;
                            expect(Symbol.PoundImport);
                            string importfile = "";
                            while (curtok.sym != Symbol.EndOfLine)
                            {
                                importfile += curtok.str + " ";
                                next();
                            }
                            expect(Symbol.EndOfLine);

                            FuncNode f = new FuncNode();
                            f.funcname = funcname;
                            f.arguments = new TextNode(importfile);
                            node = f;
                        }
                        break;
                    case Symbol.Identifier:
                        {
                            string identifier = curtok.str;
                            expect(Symbol.Identifier);
                            node = new IdentNode(identifier);
                        }
                        break;
                    case Symbol.Number:
                        {
                            string number = curtok.str;
                            expect(Symbol.Number);
                            node = new TextNode(double.Parse(number, CultureInfo.InvariantCulture));
                        }
                        break;
                    case Symbol.Unknown:
                        {
                            string str = curtok.str;
                            expect(Symbol.Unknown);
                            node = new TextNode(str);
                        }
                        break;
                    case Symbol.EndOfLine:
                        node = new EndOfLineNode();
                        next();
                        break;
                    case Symbol.EndOfFile:
                        break;
                    case Symbol.Error:
                        error("unidentified symbol");
                        return null;
                    default:
                        error("wtf?");
                        return null;
                }
                if (node != null)
                {
                    if (root == null)
                    {
                        root = node;
                        curnode = root;
                    }
                    else
                    {
                        curnode.Next = node;
                        node.Previous = curnode;
                        curnode = node;
                    }
                }
            }
            return root;
        }

        private Node __compile(FileStream input)
        {
            lex(input);
            for (int i = tokens.Count - 2; i >= 0; i--)
            {
                if (tokens[i + 1].sym == Symbol.EndOfLine && tokens[i].sym == Symbol.EndOfLine)
                {
                    tokens.RemoveAt(i + 1);
                }
            }
            curtok = tokens[0];
            return block();
        }

        internal static Node CompileSource(FileStream input)
        {
            try
            {
                return new Compiler().__compile(input);
            }
            finally
            {
                if (input != null)
                    input.Close();
            }
        }
    }
}
