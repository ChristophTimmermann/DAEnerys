using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAEnerys
{
    public interface IFFReadable
    {
        void Read(IFFReader iff);
    }

    public delegate void IFFChunkHandler(IFFReader reader, IFFChunk chunk);

    internal class IFFHandlerNode
    {
        public string Name;
        public IFFChunkType Type;
        public uint Version;
        public IFFChunkHandler Handler;

        public IFFHandlerNode(string name, IFFChunkType type, uint version, IFFChunkHandler handler)
        {
            Name = name;
            Type = type;
            Version = version;
            Handler = handler;
        }

        internal bool Equals(string name, IFFChunkType type, uint version)
        {
            return Name == name &&
                Type == type &&
                (Type != IFFChunkType.Normal || Version == version);
        }
    }

    public class IFFReader : BinaryReader
    {
        private List<IFFChunk> ChunkList;
        private List<IFFHandlerNode> Handlers;
        public IFFChunkHandler DefaultHandler { get; set; }

        public IFFReader(Stream input) : base(input)
        {
            ChunkList = new List<IFFChunk>();
            Handlers = new List<IFFHandlerNode>();
            DefaultHandler = __DefaultChunkHandler;
        }

        public IFFReader(string filename)
            : this(new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
        { }

        public void Parse(bool fromBeginning = false)
        {
            ChunkList.Clear();
            if (fromBeginning && BaseStream.Position != 0)
                BaseStream.Position = 0;

            while (BaseStream.Position < BaseStream.Length)
                ChunkList.Add(new IFFChunk(this));
        }

        public string ReadCString()
        {
            string str = "";
            byte b;
            while ((b = ReadByte()) != 0)
                str += (char)b;
            return str;
        }

        public string ReadString(int length)
        {
            string str = "";
            for (int i = 0; i < length; ++i)
                str += (char)ReadByte();
            return str;
        }

        public override string ReadString()
        {
            int length = ReadInt32();
            if (length > 512)
                Trace.TraceWarning("Input string may be invalid.");
            return ReadString();
        }

        public void AddHandler(string name, IFFChunkType type, IFFChunkHandler handler, uint version = 0)
        {
            if (handler == null) throw new ArgumentNullException("handler");
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (name.Length != 4) throw new ArgumentException("name.Length != 4");

            if (Handlers.Exists((h) => h.Equals(name, type, version)))
                Handlers.RemoveAll((h) => h.Equals(name, type, version));

            Handlers.Add(new IFFHandlerNode(name, type, version, handler));
        }

        public IFFChunkHandler FindHandler(string name, IFFChunkType type, uint version)
        {
            if (Handlers.Exists((h) => h.Equals(name, type, version)))
                return Handlers.Find((h) => h.Equals(name, type, version)).Handler;
            return DefaultHandler;
        }

        private void __DefaultChunkHandler(IFFReader reader, IFFChunk chunk)
        {
            Trace.TraceInformation(
                "Skipping '" + chunk.Type.ToString().ToUpper() + "' chunk '" + chunk.Name + "'" +
                (chunk.Type == IFFChunkType.Normal ? ", version " + chunk.Version : ""));
        }
    }
}
