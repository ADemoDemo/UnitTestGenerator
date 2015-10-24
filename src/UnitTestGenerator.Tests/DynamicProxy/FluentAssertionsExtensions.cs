using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestGenerator.DynamicProxy;
using FluentAssertions;

namespace UnitTestGenerator.DynamicProxy.Tests
{
    public static class FluentAssertionsExtensions
    {
        public static void InvokingCreateClassProxyShouldReturnSameType<T>(this CastleProxyGenerator generator)
            where T : class
        {
            //generator.Invoking(x => x.CreateClassProxy<T>())
            //    .Should().BeOfType<T>();
            var instance = generator.CreateClassProxy<T>();
            instance.Should().BeAssignableTo<T>();
        }

        public static void InvokingCreateClassProxyShouldThrowTypeAccessiblityException<T>(this CastleProxyGenerator generator)
            where T : class
        {
            generator.Invoking(x => x.CreateClassProxy<T>())
                .ShouldThrow<TypeAccessiblityException>();
        }
    }
}
