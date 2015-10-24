using UnitTestGenerator.DynamicProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAssembly;
using FluentAssertions;
using System.Reflection;

namespace UnitTestGenerator.DynamicProxy.Tests
{
    [TestClass]
    public class CastleProxyGeneratorTests
    {
        public CastleProxyGenerator generator { get; private set; }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_AbstractClassGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<AbstractClass>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_InterfaceConstructorGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<InterfaceConstructorParameter>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_AbstractClassContructorParameterGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<AbstractClassContructorParameter>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_ClassContructorGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<ClassContructor>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_IFooGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<IFoo>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_InterfaceConstructorParameterGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<InterfaceConstructorParameter>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_PublicClassGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<PublicClass>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_InternalClassGiven_ShouldThrowTypeAccessiblityException()
        {
            generator.InvokingCreateClassProxyShouldThrowTypeAccessiblityException<InternalClass>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_InternalContructorParameterGiven_ShouldThrowTypeAccessiblityException()
        {
            generator.InvokingCreateClassProxyShouldThrowTypeAccessiblityException<InternalContructorParameter>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_InternalConstructorGiven_ShouldThrowTypeAccessiblityException()
        {
            generator.InvokingCreateClassProxyShouldThrowTypeAccessiblityException<InternalConstructor>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_InternalInterfaceGiven_ShouldThrowTypeAccessiblityException()
        {
            generator.InvokingCreateClassProxyShouldThrowTypeAccessiblityException<InternalInterface>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_PrivateConstructorGiven_ShouldThrowTypeAccessiblityException()
        {
            generator.InvokingCreateClassProxyShouldThrowTypeAccessiblityException<PrivateConstructor>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_ProtectedConstructorGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<ProtectedConstructor>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_ProtectedInternalConstructorGiven_ShouldThrowTypeAccessiblityException()
        {
            generator.InvokingCreateClassProxyShouldThrowTypeAccessiblityException<ProtectedInternalConstructor>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_NestedPublicClassGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<NestedClasses.NestedPublicClass>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_NestedInternalClassGiven_ShouldThrowTypeAccessiblityException()
        {
            generator.InvokingCreateClassProxyShouldThrowTypeAccessiblityException<NestedClasses.NestedInternalClass>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_FriendlyInternalClassGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<FriendlyTestAssembly.InternalClass>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_FriendlyInternalContructorParameterGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<FriendlyTestAssembly.InternalContructorParameter>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_FriendlyInternalInterfaceGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<FriendlyTestAssembly.InternalInterface>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_FriendlyPublicClassInternalCtorGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<FriendlyTestAssembly.PublicClassInternalCtor>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_FriendlyNestedPublicClassGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<FriendlyTestAssembly.NestedClasses.NestedPublicClass>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_FriendlyNestedInternalClassGiven_ShouldReturnInstanceOfSameType()
        {
            generator.InvokingCreateClassProxyShouldReturnSameType<FriendlyTestAssembly.NestedClasses.NestedInternalClass>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_ValueTypeGiven_ShouldThrowProxyGeneratorException()
        {
            generator.Invoking(x => x.CreateClassProxy(typeof(int)))
                .ShouldThrow<ProxyGeneratorException>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_StringGiven_ShouldThrowProxyGeneratorException()
        {
            generator.Invoking(x => x.CreateClassProxy(typeof(string)))
                .ShouldThrow<ProxyGeneratorException>();
        }

        [TestMethod]
        [TestCategory("DynamicProxy")]
        public void CreateClassProxy_MemberInfogGiven_ShouldReturnInstanceOfSameType()
        {
            generator.Invoking(x => x.CreateClassProxy(typeof(MemberInfo)))
                .ShouldThrow<TypeAccessiblityException>();
        }

        [TestInitialize]
        public void Initialize()
        {
            generator = new CastleProxyGenerator();
        }
    }
}