using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.Text;
using System;
using System.Collections.Generic;

namespace UnitTestGenerator.DynamicProxy.Tests
{
    [TestClass]
    public class ProxyGeneratorTests
    {
        [TestMethod]
        public void CreateProxy_IFooTypeGiven_ShouldCreateInstanceOfIFoo()
        {
            var result = ProxyGenerator.CreateProxy<TestAssembly.IFoo>();

            result.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateProxy_ListTypeGiven_ShouldCreateInstanceOfList()
        {
            var result = ProxyGenerator.CreateProxy<List<string>>();

            result.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateProxy_StringTypeGiven_ShouldThrowProxyGeneratorException()
        {
            ((Action)(() => ProxyGenerator.CreateProxy<string>()))
                .ShouldThrow<ProxyGeneratorException>();
        }
    }
}