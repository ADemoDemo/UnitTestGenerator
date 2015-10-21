using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace UnitTestGenerator.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void ToCamelCase_NullGiven_ShouldReturnNull()
        {
            string input = null;

            string result = StringExtensions.ToCamelCase(input);

            result.Should().BeNull();
        }

        [TestMethod()]
        public void ToCamelCase_StringOfSizeOneGiven_ShouldReturnLowercaseFirstChar()
        {
            string input = "A";
            string expected = "a";

            string result = StringExtensions.ToCamelCase(input);

            result.Should().Be(expected);
        }

        [TestMethod()]
        public void ToCamelCase_StringGiven_ShouldReturnLowercaseFirstChar()
        {
            string input = "HelloWorld";
            string expected = "helloWorld";

            string result = StringExtensions.ToCamelCase(input);

            result.Should().Be(expected);
        }

        [TestMethod()]
        public void Capitalize_NullGiven_ShouldReturnNull()
        {
            string input = null;

            string result = StringExtensions.Capitalize(input);

            result.Should().BeNull();
        }

        [TestMethod()]
        public void Capitalize_StringOfSizeOneGiven_ShouldReturnUppercaseFirstChar()
        {
            string input = "a";
            string expected = "A";

            string result = StringExtensions.Capitalize(input);

            result.Should().Be(expected);
        }

        [TestMethod()]
        public void Capitalize_StringGiven_ShouldReturnUppercaseFirstChar()
        {
            string input = "helloworld";
            string expected = "Helloworld";

            string result = StringExtensions.Capitalize(input);

            result.Should().Be(expected);
        }

        [TestMethod()]
        public void RemoveFirstAndLastChar_AtLeastTwoCharLengthStringGiven_ShouldRemoveFirstAndLastChar()
        {
            string input = "abc";

            var result = StringExtensions.RemoveFirstAndLastChar(input);

            result.Should().Be("b");
        }
    }
}