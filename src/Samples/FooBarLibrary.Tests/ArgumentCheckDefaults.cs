using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator;
#pragma warning disable RECS0026 // Possible unassigned object created by 'new'
#pragma warning disable RECS0001 // Class is declared partial but has only one part
        namespace FooBarLibrary.Tests
        {
            [TestClass]
            public partial class FooTestsDefaults
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat_StrANullValueGiven_ShouldThrowArgumentNullException()
                {
                    var foo = new Foo();		 
                    foo.Concat(null, "abc");		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat_StrBNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var foo = new Foo();		 
                    foo.Concat("abc", null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat2_StrANullValueGiven_ShouldThrowArgumentNullException()
                {
                    var foo = new Foo();		 
                    foo.Concat2(null, "abc", "abc");		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat2_StrBNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var foo = new Foo();		 
                    foo.Concat2("abc", null, "abc");		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat2_StrCNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var foo = new Foo();		 
                    foo.Concat2("abc", "abc", null);		 
                }

            }
        }
#pragma warning restore RECS0026 // Possible unassigned object created by 'new'
#pragma warning restore RECS0001 // Class is declared partial but has only one part
