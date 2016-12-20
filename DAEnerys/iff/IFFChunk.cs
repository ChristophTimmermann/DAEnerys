using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAEnerys
{
    public enum IFFChunkType
    {
        /// <summary>No special header.</summary>
        Default,

        /// <summary>Chunk in a 'NRML' header.</summary>
        Normal,

        /// <summary>Chunk in a 'FORM' header.</summary>
        Form
    }

    public class IFFChunk
    {
        public string Name;
        public IFFChunkType Type;
        public uint Version;
        public long StartPos;
        public int Size;

        public IFFChunk(string name, IFFChunkType type, uint version)
        {
            Name = name;
            Type = type;
            Version = version;
        }

        public IFFChunk(IFFReader reader)
        {
            StartPos = reader.BaseStream.Position;
            Name = reader.ReadString(4);
            Size = Utilities.SwapEndian(reader.ReadInt32());

            if (Name == "NRML")
            {
                Type = IFFChunkType.Normal;
                Name = reader.ReadString(4); // get the real ID
                Version = Utilities.SwapEndian(reader.ReadUInt32());
                Size -= 8; // subtract size of 'NRML' head and version
            }
            else if (Name == "FORM")
            {
                Type = IFFChunkType.Form;
                Name = reader.ReadString(4); // get the real ID
                Size -= 4; // subtract size of 'FORM' head
            }
            else
            {
                Type = IFFChunkType.Default;
            }

            if (reader.BaseStream.Position + Size > reader.BaseStream.Length)
                throw new Exception("invalid chunk " + Name + " at offset " + StartPos);

            IFFChunkHandler handler = reader.FindHandler(Name, Type, Version);
            if (handler == null)
            {
                reader.BaseStream.Position += Size;
            }
            else
            {
                MemoryStream ChunkStream = new MemoryStream(reader.ReadBytes((int)Size));
                IFFReader ChunkReader = new IFFReader(ChunkStream);
                handler.Invoke(ChunkReader, this);
                ChunkReader.Close();
                ChunkStream.Close();
            }
        }
    }

    public class IFFChunkStack
    {
        private IFFWriter writer;
        private Stack<IFFChunk> stack;

        public IFFChunkStack(IFFWriter writer)
        {
            this.writer = writer;
            stack = new Stack<IFFChunk>();
        }

        public void Push(string name, IFFChunkType type, uint version)
        {
            IFFChunk chunk = new IFFChunk(name, type, version);
            chunk.StartPos = (uint)writer.BaseStream.Position + 8;
            stack.Push(chunk);

            switch (type)
            {
                case IFFChunkType.Normal:
                    writer.Write("NRML", 4);
                    writer.Write((uint)0);
                    writer.Write(name, 4);
                    writer.Write(Utilities.SwapEndian(version));
                    break;
                case IFFChunkType.Form:
                    writer.Write("FORM", 4);
                    writer.Write((uint)0);
                    writer.Write(name, 4);
                    break;
                case IFFChunkType.Default:
                    writer.Write(name, 4);
                    writer.Write((uint)0);
                    break;
            }
        }

        public IFFChunk Pop()
        {
            if (stack.Count == 0) throw new InvalidOperationException("No chunk to pop.");

            IFFChunk chunk = stack.Pop();
            long curpos = writer.BaseStream.Position;
            uint size = (uint)(curpos - chunk.StartPos);
            writer.BaseStream.Position = chunk.StartPos - 4;
            writer.Write(Utilities.SwapEndian(size));
            writer.BaseStream.Position = curpos;

            return chunk;
        }
    }
}
