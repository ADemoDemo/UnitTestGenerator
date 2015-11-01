using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooBarLibrary.Tests
{
    public partial class FooTests_Reuse
    {
        private Foo fooInstance;

        [TestInitialize]
        public void Initialize()
        {
            fooInstance = new Foo();
        }
    }
}
