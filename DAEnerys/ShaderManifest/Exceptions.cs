using System;

namespace NewShaderManifest
{
    [Serializable]
    internal class ConfigParseException : Exception
    {
        public ConfigParseException(string message) : base(message)
        {
        }
    }

    [Serializable]
    internal class ManifestNotInitializedException : Exception
    {
        public ManifestNotInitializedException() : base("The shader manifest has not yet been initialized.") { }
    }

    [Serializable]
    internal class ManifestParseException : Exception
    {
        public ManifestParseException(string message) : base(message)
        {
        }

        public ManifestParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    [Serializable]
    internal class ProgramNotAvailableException : Exception
    {
        public ProgramNotAvailableException(string name) : base("Program " + name + " is not available.")
        {
        }
    }

    [Serializable]
    internal class ProgramParseException : Exception
    {
        public ProgramParseException(string message) : base(message)
        {
        }
    }

    [Serializable]
    internal class SurfaceDrawException : Exception
    {
        public SurfaceDrawException(string message) : base(message)
        {
        }
    }

    [Serializable]
    internal class SurfaceNotAvailableException : Exception
    {
        public SurfaceNotAvailableException(string name) : base("Surface " + name + " is not available.")
        {
        }
    }

    [Serializable]
    internal class SurfaceParseException : Exception
    {
        public SurfaceParseException(string message) : base(message)
        {
        }
    }
}