using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGenerator
{
    public static class Value
    {
        private static Fixture fixture = new Fixture();

        public static T Create<T>()
        {
            return fixture.Create<T>();
        }
    }
}
