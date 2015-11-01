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
                public void Concat30edf6bb()
                {
                    var foo = new Foo();		 
                    foo.Concat(null, Value.Create<string>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat2e66ca90()
                {
                    var foo = new Foo();		 
                    foo.Concat(Value.Create<string>(), null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concatfe340ef0()
                {
                    var foo = new Foo();		 
                    foo.Concat(null, Value.Create<string>(), Value.Create<string>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat353424e8()
                {
                    var foo = new Foo();		 
                    foo.Concat(Value.Create<string>(), null, Value.Create<string>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat8187436f()
                {
                    var foo = new Foo();		 
                    foo.Concat(Value.Create<string>(), Value.Create<string>(), null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Lowercase76b63e4a()
                {
                    var foo = new Foo();		 
                    foo.Lowercase(null);		 
                }

            }
        }
#pragma warning restore RECS0026 // Possible unassigned object created by 'new'
#pragma warning restore RECS0001 // Class is declared partial but has only one part
