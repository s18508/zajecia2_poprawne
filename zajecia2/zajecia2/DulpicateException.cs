using System;
using System.Runtime.Serialization;

namespace zajecia2
{
    [Serializable]
    internal class DulpicateException : Exception
    {
        public DulpicateException()
        {
        }

        public DulpicateException(string message) : base(message)
        {
        }

        public DulpicateException(string message, string trescBledow) : base(message)
        {
            trescBledow+= message;
        }

        public DulpicateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DulpicateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}