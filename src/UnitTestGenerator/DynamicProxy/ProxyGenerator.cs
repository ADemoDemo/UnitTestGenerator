using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGenerator.DynamicProxy
{
    public static class ProxyGenerator
    {
        static CastleProxyGenerator generator = new CastleProxyGenerator();

        public static object CreateProxy(Type type)
        {
            return generator.CreateClassProxy(type);
        }

        public static TType CreateProxy<TType>()
            where TType : class
        {
            return generator.CreateClassProxy<TType>();
        }
    }
}
