using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAEnerys
{
    public interface IFFWriteable
    {
        void Write(IFFWriter iff);
    }

    public class IFFWriter : BinaryWriter
    {
        private IFFChunkStack ChunkStack;

        public IFFWriter(Stream output) : base(output)
        {
            ChunkStack = new IFFChunkStack(this);
        }

        public IFFWriter(string filename)
            : this(new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Write))
        { }

        public void Push(string name, IFFChunkType type = IFFChunkType.Default, uint version = 0)
        {
            ChunkStack.Push(name, type, version);
        }

        public string Pop()
        {
            return ChunkStack.Pop().Name;
        }

        public void Write(IFFWriteable obj)
        {
            obj.Write(this);
        }

        public override void Write(string value)
        {
            Write(value.Length);

            foreach (char ch in value)
                Write((byte)(ch & 0xff));
        }

        public void Write(string value, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length");
            if (length == 0) return;

            byte[] str = new byte[length];
            if (value != null)
            {
                int realLength = Math.Min(value.Length, length);
                for (int i = 0; i < realLength; ++i)
                    str[i] = (byte)(value[i] & 0xff);
            }

            Write(str);
        }

        public void WriteCString(string value)
        {
            foreach (char ch in value)
                Write((byte)(ch & 0xff));
            Write((byte)0);
        }
    }
}
