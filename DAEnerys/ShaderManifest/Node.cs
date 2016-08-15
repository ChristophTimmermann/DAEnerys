using System;

namespace NewShaderManifest
{
    internal class Node
    {
        public Node Previous { get; set; }
        public Node Next { get; set; }
        public Node First
        {
            get
            {
                Node node = this;
                while (node.Previous != null) node = node.Previous;
                return node;
            }
        }
        public Node Last
        {
            get
            {
                Node node = this;
                while (node.Next != null) node = node.Next;
                return node;
            }
        }
        public void PushFront(Node node)
        {
            Node first = First;
            first.Previous = node;
            node.Next = first;
        }
        public void PushBack(Node node)
        {
            Node last = Last;
            last.Next = node;
            node.Previous = last;
        }
        public void In(Node node)
        {
            Node last = Last;
            last.Next = node;
            node.Previous = last;
        }

        // inserts a node (or series of nodes) between the current node and the next node
        public void Insert(Node node)
        {
            if (node == null) return;
            Node lastnode = node.Last;

            if (Next != null)
                Next.Previous = lastnode;
            lastnode.Next = Next;
            Next = node;
            node.Previous = this;
        }

        public int Length
        {
            get
            {
                int count = 0;
                Node node = First;
                while (node != null)
                {
                    count++;
                    node = node.Next;
                }
                return count;
            }
        }

        public int LengthToEnd
        {
            get
            {
                int count = 0;
                Node node = this;
                while (node != null)
                {
                    count++;
                    node = node.Next;
                }
                return count;
            }
        }

        public Node this[int index]
        {
            get
            {
                if (index == 0)
                    return this;
                else if (index > 0)
                {
                    Node node = this;
                    for (int i = 0; i < index; i++)
                    {
                        if (node == null)
                            throw new IndexOutOfRangeException();
                        node = node.Next;
                    }
                    return node;
                }
                else if (index < 0)
                {
                    Node node = this;
                    for (int i = 0; i > index; i--)
                    {
                        if (node == null)
                            throw new IndexOutOfRangeException();
                        node = node.Previous;
                    }
                    return node;
                }
                return this;
            }
        }
    }

    internal class EndOfLineNode : Node { }

    internal enum ValueType
    {
        Number,
        String
    }

    internal class TextNode : Node
    {
        public ValueType type;
        public string str;

        public TextNode(string value)
        {
            type = ValueType.String;
            str = value;
        }

        public TextNode(double value)
        {
            type = ValueType.Number;
            str = value.ToString();
        }
    }

    internal class IdentNode : TextNode
    {
        public string identifier
        {
            get
            {
                return str;
            }
            set
            {
                str = value;
            }
        }
        public IdentNode(string id) : base(id) { }
    }

    internal class FuncNode : Node
    {
        public string funcname;
        public Node arguments;
    }

    internal enum BinOpr
    {
        /// <summary>"Not Equal"</summary>
        OP_NE = 0,
        /// <summary>"Equal"</summary>
        OP_EQ = 1,
        /// <summary>"Less Than"</summary>
        OP_LT = 2,
        /// <summary>"Less Than or Equal"</summary>
        OP_LE = 3,
        /// <summary>"Greater Than"</summary>
        OP_GT = 4,
        /// <summary>"Greater Than or Equal"</summary>
        OP_GE = 5,
        /// <summary>"String Comparison"</summary>
        OP_STR = 6,
        /// <summary>"Is Defined"</summary>
        OP_DEF = 7
    }

    internal class ComparisionNode : Node
    {
        public TextNode left;
        public BinOpr op;
        public TextNode right;
    }


    internal class IfNode : Node
    {
        public ComparisionNode condition;
        public Node true_branch;
        public Node false_branch;
    }
}
