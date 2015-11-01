using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator;
#pragma warning disable RECS0026 // Possible unassigned object created by 'new'
#pragma warning disable RECS0001 // Class is declared partial but has only one part
        namespace FooBarLibrary.Tests
        {
            [TestClass]
            public partial class FooTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat_StringString_StrANullValueGiven_ShouldThrowArgumentNullException()
                {
                    var foo = new Foo();		 
                    foo.Concat(null, Value.Create<string>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat_StringString_StrBNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var foo = new Foo();		 
                    foo.Concat(Value.Create<string>(), null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat_StringStringString_StrANullValueGiven_ShouldThrowArgumentNullException()
                {
                    var foo = new Foo();		 
                    foo.Concat(null, Value.Create<string>(), Value.Create<string>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat_StringStringString_StrBNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var foo = new Foo();		 
                    foo.Concat(Value.Create<string>(), null, Value.Create<string>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat_StringStringString_StrCNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var foo = new Foo();		 
                    foo.Concat(Value.Create<string>(), Value.Create<string>(), null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Lowercase_ValueNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var foo = new Foo();		 
                    foo.Lowercase(null);		 
                }

            }
        }
#pragma warning restore RECS0026 // Possible unassigned object created by 'new'
#pragma warning restore RECS0001 // Class is declared partial but has only one part
