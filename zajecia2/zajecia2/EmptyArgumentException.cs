using System;
using System.Runtime.Serialization;

namespace zajecia2
{
    [Serializable]
    internal class EmptyArgumentException : Exception
    {

        public EmptyArgumentException()
        {
        }

        public EmptyArgumentException(string message) : base(message)
        {
        }

        public EmptyArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptyArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}