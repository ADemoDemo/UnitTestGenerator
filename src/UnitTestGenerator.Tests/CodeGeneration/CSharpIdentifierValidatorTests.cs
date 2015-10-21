using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace UnitTestGenerator.Tests.CodeGeneration
{
    [TestClass]
    public class CSharpIdentifierValidatorTests
    {
        [TestMethod]
        public void IsValidIdentifier_ValidStringGiven_ShouldReturnTrue()
        {
            bool? result = false;
            using (var testee = new CSharpIdentifierValidator())
            {
                result = testee.IsValidIdentifier("variable");
            }
            result.Should().Be(true);
        }
    }
}
