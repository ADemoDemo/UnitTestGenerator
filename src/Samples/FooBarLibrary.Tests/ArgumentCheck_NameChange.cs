using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator;
#pragma warning disable RECS0026 // Possible unassigned object created by 'new'
#pragma warning disable RECS0001 // Class is declared partial but has only one part
        namespace FooBarLibrary.Tests
        {
            [TestClass]
            public partial class FooTestsNameChange
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concatb0c5332e()
                {
                    var foo = new Foo();		 
                    foo.Concat(null, Value.Create<string>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat9847491d()
                {
                    var foo = new Foo();		 
                    foo.Concat(Value.Create<string>(), null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat21fe1e0cb()
                {
                    var foo = new Foo();		 
                    foo.Concat2(null, Value.Create<string>(), Value.Create<string>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat2c6dcbdc6()
                {
                    var foo = new Foo();		 
                    foo.Concat2(Value.Create<string>(), null, Value.Create<string>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat212e800f5()
                {
                    var foo = new Foo();		 
                    foo.Concat2(Value.Create<string>(), Value.Create<string>(), null);		 
                }

            }
        }
#pragma warning restore RECS0026 // Possible unassigned object created by 'new'
#pragma warning restore RECS0001 // Class is declared partial but has only one part
