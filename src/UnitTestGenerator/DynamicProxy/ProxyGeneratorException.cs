using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGenerator.DynamicProxy
{
    [Serializable]
    public class ProxyGeneratorException : Exception
    {
        public ProxyGeneratorException() { }
        public ProxyGeneratorException(string message) : base(message) { }
        public ProxyGeneratorException(string message, Exception inner) : base(message, inner) { }
        protected ProxyGeneratorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
