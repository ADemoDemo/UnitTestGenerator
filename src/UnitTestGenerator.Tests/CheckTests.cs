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
    public class CheckTests
    {
        [TestMethod()]
        public void NotNull_NonNullStringGiven_Succeeds()
        {
            var value = "123";
            var parameterName = "abc";

            Check.NotNull(value, parameterName);
        }

        [TestMethod()]
        public void NotNull_NullStringGiven_ShouldThrowArgumentException()
        {
            string parameterName = "abc";

            ((Action)(() => Check.NotNull((string)null, parameterName)))
                .ShouldThrow<ArgumentNullException>()
                .Where(x => x.ParamName == parameterName);
        }

        [TestMethod()]
        public void NotNull_NullParameterNameGiven_ShouldThrowArgumentException()
        {
            ((Action)(() => Check.NotNull((string)null, null)))
                .ShouldThrow<ArgumentNullException>()
                .Where(x => x.ParamName == "parameterName");
        }

        [TestMethod()]
        public void NotNullGenericNullable_NonNullStringGiven_Succeeds()
        {
            int? value = 123;
            var parameterName = "abc";

            Check.NotNull(value, parameterName);
        }

        [TestMethod()]
        public void NotNullGenericNullable_NullStringGiven_ShouldThrowArgumentException()
        {
            string parameterName = "abc";

            ((Action)(() => Check.NotNull((int?)null, parameterName)))
                .ShouldThrow<ArgumentNullException>()
                .Where(x => x.ParamName == parameterName);
        }

        [TestMethod()]
        public void NotNullGenericNullable_NullParameterNameGiven_ShouldThrowArgumentException()
        {
            ((Action)(() => Check.NotNull((int?)null, null)))
                .ShouldThrow<ArgumentNullException>()
                .Where(x => x.ParamName == "parameterName");
        }

        [TestMethod()]
        public void NotEmpty_NonNullStringGiven_Succeeds()
        {
            var value = "123";
            var parameterName = "abc";

            Check.NotEmpty(value, parameterName);
        }

        [TestMethod()]
        public void NotEmpty_NullStringGiven_ShouldThrowArgumentException()
        {
            string parameterName = "abc";

            ((Action)(() => Check.NotEmpty(null, parameterName)))
                .ShouldThrow<ArgumentNullException>()
                .Where(x => x.ParamName == parameterName);
        }

        [TestMethod()]
        public void NotEmpty_EmptyStringGiven_ShouldThrowArgumentException()
        {
            string parameterName = "abc";
            var value = string.Empty;

            ((Action)(() => Check.NotEmpty(value, parameterName)))
                .ShouldThrow<ArgumentException>()
                .Where(x => x.ParamName == parameterName);
        }

        [TestMethod()]
        public void NotEmpty_NullParameterNameGiven_ShouldThrowArgumentException()
        {
            ((Action)(() => Check.NotEmpty(null, null)))
                .ShouldThrow<ArgumentNullException>()
                .Where(x => x.ParamName == "parameterName");
        }

        [TestMethod()]
        public void NotEmptyEnumerable_NonEmptyArrayGiven_Succeeds()
        {
            var value = new[] { "123" };
            var parameterName = "abc";

            Check.NotEmpty(value, parameterName);
        }

        [TestMethod()]
        public void NotEmptyEnumerable_NullArrayGiven_ShouldThrowArgumentException()
        {
            string[] value = null;
            string parameterName = "abc";

            ((Action)(() => Check.NotEmpty(value, parameterName)))
                .ShouldThrow<ArgumentNullException>()
                .Where(x => x.ParamName == parameterName);
        }

        [TestMethod()]
        public void NotEmptyEnumerable_EmptyArrayGiven_ShouldThrowArgumentException()
        {
            string[] value = new string[0];
            string parameterName = "abc";

            ((Action)(() => Check.NotEmpty(value, parameterName)))
                .ShouldThrow<ArgumentException>()
                .Where(x => x.ParamName == parameterName);
        }
    }
}