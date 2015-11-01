using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAssembly
{
    public class MultipleConstructors
    {
        public MultipleConstructors(PublicClass internalClass)
        {

        }

        public MultipleConstructors(PublicClass internalClass, string name)
        {

        }
    }
}
