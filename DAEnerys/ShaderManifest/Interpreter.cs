using System;
using System.Collections.Generic;

namespace NewShaderManifest
{
    class Interpreter
    {
        public delegate int FunctionCallback(int nargs, Node args);
        public delegate bool IfConditionCallback(BinOpr op, string left, string right);

        private Dictionary<string, FunctionCallback> funcs = new Dictionary<string, FunctionCallback>();
        private IfConditionCallback IfCondCB = null;

        public bool HasError { get; private set; }
        public string ErrorMessage { get; private set; }

        public Interpreter(IfConditionCallback cb)
        {
            IfCondCB = cb;
        }

        public void RegisterFunction(string name, FunctionCallback cb)
        {
            funcs[name] = cb;
        }

        public bool Run(Node root)
        {
            if (root == null) return true;
            Node curnode = root;

            while (curnode != null)
            {
                if (curnode is EndOfLineNode)
                {
                    //ignore
                }
                else if (curnode is FuncNode)
                    do_func((FuncNode)curnode);
                else if (curnode is IfNode)
                    do_if((IfNode)curnode);
                else if (curnode is IdentNode)
                {
                    string identifier = ((IdentNode)curnode).identifier;
                    if (funcs.ContainsKey(identifier))
                        curnode = curnode[funcs[identifier](-1, curnode.Next)];
                    else
                        throw new Exception("unexpected identifier: " + identifier);
                }
                else
                {
                    throw new Exception("unexpected statement");
                }
                if (HasError) return false;
                curnode = curnode.Next;
            }
            return true;
        }

        void error(string message)
        {
            // Make sure execution stops here
            ErrorMessage = message;
            HasError = true;
        }

        void do_func(FuncNode node)
        {
            string funcname = node.funcname;
            int nargs = node.arguments.Length;
            if (funcs.ContainsKey(funcname))
                funcs[funcname](nargs, node.arguments);
            else
                throw new Exception("undefined function: " + funcname);
        }

        void do_if(IfNode ifnode)
        {
            if (IfCondCB == null) throw new Exception("IfConditionCallback must be set.");

            ComparisionNode cond = ifnode.condition;

            bool result;
            string left = cond.left.str;
            string right = cond.right.str;
            result = IfCondCB(cond.op, left, right);

            ifnode.Insert(result ? ifnode.true_branch : ifnode.false_branch);
        }
    }
}
