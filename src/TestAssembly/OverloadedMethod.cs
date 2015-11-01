using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAssembly
{
    public class OverloadedMethods
    {
        public OverloadedMethods(string name)
        {
        }

        public OverloadedMethods(IEnumerable<string> names)
        {

        }

        public void OverloadedMethod(IEnumerable<string> strings)
        {

        }


        public void OverloadedMethod(IEnumerable<int> ints)
        {

        }

        public string SameName(string name1, string name2)
        {
            return name1 + name2;
        }

        public string SameName(string name1, string name2, string name3)
        {
            return name1 + name2 + name3;
        }
    }
}
