using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGenerator.DynamicProxy
{
    [Serializable]
    public class TypeAccessiblityException : ProxyGeneratorException
    {
        public TypeAccessiblityException() { }
        public TypeAccessiblityException(string message) : base(message) { }
        public TypeAccessiblityException(string message, Exception inner) : base(message, inner) { }
        protected TypeAccessiblityException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
