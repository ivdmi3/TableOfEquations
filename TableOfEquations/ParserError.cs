using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMatrix
{
    [System.Serializable]
    public class ParserError : Exception
    {
        public ParserError() { }
        public ParserError(string message) : base(message) { }
        public ParserError(string message, Exception inner) : base(message, inner) { }
        protected ParserError(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
