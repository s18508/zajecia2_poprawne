using System;
using System.Runtime.Serialization;

namespace zajecia2
{
    [Serializable]
    internal class ArgumentNot9Exception : Exception
    {
        public ArgumentNot9Exception()
        {
        }

        public ArgumentNot9Exception(string message) : base(message)
        {
        }

        public ArgumentNot9Exception(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ArgumentNot9Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}