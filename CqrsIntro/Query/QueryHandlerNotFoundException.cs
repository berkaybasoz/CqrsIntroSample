using System;
using System.Runtime.Serialization;

namespace CqrsIntro.Query
{
    [Serializable]
    internal class QueryHandlerNotFoundException : Exception
    {
        private Type type;

        public QueryHandlerNotFoundException()
        {
        }

        public QueryHandlerNotFoundException(string message) : base(message)
        {
        }

        public QueryHandlerNotFoundException(Type type)
        {
            this.type = type;
        }

        public QueryHandlerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected QueryHandlerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}