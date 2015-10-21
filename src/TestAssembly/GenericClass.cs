using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAssembly
{
    public class GenericClass<TSource>
        where TSource : IFoo, new()
    {
        private readonly TSource foo;

        public GenericClass()
        {
            foo = new TSource();
        }
    }
}
