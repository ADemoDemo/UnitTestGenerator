using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator;
#pragma warning disable RECS0026 // Possible unassigned object created by 'new'
#pragma warning disable RECS0001 // Class is declared partial but has only one part
        namespace FooBarLibrary.Tests
        {
            [TestClass]
            public partial class FooTests_Reuse
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat_StrANullValueGiven_ShouldThrowArgumentNullException()
                {
                    fooInstance.Concat(null, Value.Create<string>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat_StrBNullValueGiven_ShouldThrowArgumentNullException()
                {
                    fooInstance.Concat(Value.Create<string>(), null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat2_StrANullValueGiven_ShouldThrowArgumentNullException()
                {
                    fooInstance.Concat2(null, Value.Create<string>(), Value.Create<string>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat2_StrBNullValueGiven_ShouldThrowArgumentNullException()
                {
                    fooInstance.Concat2(Value.Create<string>(), null, Value.Create<string>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Concat2_StrCNullValueGiven_ShouldThrowArgumentNullException()
                {
                    fooInstance.Concat2(Value.Create<string>(), Value.Create<string>(), null);		 
                }

            }
        }
#pragma warning restore RECS0026 // Possible unassigned object created by 'new'
#pragma warning restore RECS0001 // Class is declared partial but has only one part
