using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnitTestGenerator.DynamicProxy;
using System.Linq.Expressions;
using UnitTestGenerator.UnitTestGeneration;
using UnitTestGenerator.CodeGeneration;
using UnitTestGenerator.CodeGeneration.Generators;
using UnitTestGenerator.ExpressionProviders;

        namespace UnitTestGenerator.Tests
        {
            [TestClass]
            public partial class TestMethodTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void TestMethod_Constructor_TestedMemberNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new TestMethod(null, Value.Create<string>(), Value.Create<string>(), typeof(string));		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void TestMethod_Constructor_NameNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new TestMethod(typeof(string).GetMethods()[0], null, Value.Create<string>(), typeof(string));		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void TestMethod_Constructor_SourceCodeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new TestMethod(typeof(string).GetMethods()[0], Value.Create<string>(), null, typeof(string));		 
                }

            }
        }

        namespace UnitTestGenerator.Tests
        {
            [TestClass]
            public partial class AssemblyTraverserTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Create_TargetAssemblyNullValueGiven_ShouldThrowArgumentNullException()
                {
                    AssemblyTraverser.Create(null, Value.Create<string>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void Create_CallingAssemblyNameNullValueGiven_ShouldThrowArgumentNullException()
                {
                    AssemblyTraverser.Create(typeof(string).Assembly, null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void InternalsVisible_TargetAssemblyNullValueGiven_ShouldThrowArgumentNullException()
                {
                    AssemblyTraverser.InternalsVisible(null, Value.Create<string>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void InternalsVisible_CallingAssemblyNameNullValueGiven_ShouldThrowArgumentNullException()
                {
                    AssemblyTraverser.InternalsVisible(typeof(string).Assembly, null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void AssemblyTraverser_Constructor_TargetAssemblyNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new AssemblyTraverser(null, Param_0 => Value.Create<bool>(), Value.Create<bool>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void AssemblyTraverser_Constructor_TypeFilterNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new AssemblyTraverser(typeof(string).Assembly, null, Value.Create<bool>());		 
                }

            }
        }

        namespace UnitTestGenerator.Tests
        {
            [TestClass]
            public partial class CSharpIdentifierValidatorTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void IsValidIdentifier_VarNameNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var cSharpIdentifierValidator = new CSharpIdentifierValidator();		 
                    cSharpIdentifierValidator.IsValidIdentifier(null);		 
                }

            }
        }

        namespace UnitTestGenerator.Tests
        {
            [TestClass]
            public partial class ExpressionStringBuilderTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ExpressionToString_NodeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    ExpressionStringBuilder.ExpressionToString(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void CatchBlockToString_NodeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    ExpressionStringBuilder.CatchBlockToString(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void SwitchCaseToString_NodeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    ExpressionStringBuilder.SwitchCaseToString(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void MemberBindingToString_NodeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    ExpressionStringBuilder.MemberBindingToString(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ElementInitBindingToString_NodeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    ExpressionStringBuilder.ElementInitBindingToString(null);		 
                }

            }
        }

        namespace UnitTestGenerator.Tests
        {
            [TestClass]
            public partial class TestClassBuilderTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void TestClassBuilder_Constructor_TraverserNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new TestClassBuilder(null, new [] {ProxyGenerator.CreateProxy<ITestMethodGenerator>(), ProxyGenerator.CreateProxy<ITestMethodGenerator>()});		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void TestClassBuilder_Constructor_TestGeneratorsNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new TestClassBuilder(ProxyGenerator.CreateProxy<IAssemblyTraverser>(), null);		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.UnitTestGeneration
        {
            [TestClass]
            public partial class TypeContextTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void TypeContext_Constructor_TargetTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new TypeContext(null, Value.Create<bool>());		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.UnitTestGeneration
        {
            [TestClass]
            public partial class ConstructorMetadataTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ConstructorMetadata_Constructor_ConstructorNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new ConstructorMetadata(null, Value.Create<bool>(), Value.Create<bool>());		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.UnitTestGeneration
        {
            [TestClass]
            public partial class MethodMetadataTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void MethodMetadata_Constructor_MethodNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new MethodMetadata(null, Value.Create<bool>());		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.UnitTestGeneration
        {
            [TestClass]
            public partial class NullArgumentConstructorTestMethodGeneratorTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ParameterSatisfied_ParameterNullValueGiven_ShouldThrowArgumentNullException()
                {
                    NullArgumentConstructorTestMethodGenerator.ParameterSatisfied(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void NullArgumentConstructorTestMethodGenerator_Constructor_NullArgumentConstructorTestMethodSourceCodeGeneratorNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new NullArgumentConstructorTestMethodGenerator(null);		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.UnitTestGeneration
        {
            [TestClass]
            public partial class NullArgumentMethodTestMethodGeneratorTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ParameterSatisfied_ParameterNullValueGiven_ShouldThrowArgumentNullException()
                {
                    NullArgumentMethodTestMethodGenerator.ParameterSatisfied(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void NullArgumentMethodTestMethodGenerator_Constructor_NullArgumentMethodTestMethodSourceCodeGeneratorNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new NullArgumentMethodTestMethodGenerator(null);		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.ExpressionProviders
        {
            [TestClass]
            public partial class AutoFixtureValueExpressionProviderTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void CreateValueExpression_ForTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var autoFixtureValueExpressionProvider = new ValueExpressionProvider();		 
                    autoFixtureValueExpressionProvider.CreateValueExpression(null);		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.ExpressionProviders
        {
            [TestClass]
            public partial class CastleMockProviderTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void CreateMockExpression_ForTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var castleMockProvider = new CastleMockProvider();		 
                    castleMockProvider.CreateMockExpression(null);		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.DynamicProxy
        {
            [TestClass]
            public partial class CastleProxyGeneratorTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void CreateClassProxy_TargetTypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var castleProxyGenerator = new CastleProxyGenerator();		 
                    castleProxyGenerator.CreateClassProxy(null);		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.CodeGeneration
        {
            [TestClass]
            public partial class DefaultValueForTypeMapperTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void DefaultValueForTypeMapper_Constructor_ParameterMappingNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new DefaultValueForTypeMapper(null, new [] {(Expression<Func<string>>)(() => string.Empty), (Expression<Func<string>>)(() => string.Empty)});		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void DefaultValueForTypeMapper_Constructor_DefaultValuesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new DefaultValueForTypeMapper(new Dictionary<Type, string>(), null);		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.CodeGeneration
        {
            [TestClass]
            public partial class ExpressionBuilderTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void CreateArgumentExpression_TypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var expressionBuilder = ProxyGenerator.CreateProxy<ExpressionBuilder>();		 
                    expressionBuilder.CreateArgumentExpression(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void CreateInstanceCreationExpression_TypeNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var expressionBuilder = ProxyGenerator.CreateProxy<ExpressionBuilder>();		 
                    expressionBuilder.CreateInstanceCreationExpression(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ExpressionToString_ExpressionNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var expressionBuilder = ProxyGenerator.CreateProxy<ExpressionBuilder>();		 
                    expressionBuilder.ExpressionToString(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void CreateArgumentExpressions_ParametersNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var expressionBuilder = ProxyGenerator.CreateProxy<ExpressionBuilder>();		 
                    expressionBuilder.CreateArgumentExpressions(null, Value.Create<bool>(), ProxyGenerator.CreateProxy<ParameterInfo>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ExpressionBuilder_Constructor_TestMethodValueProviderNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new ExpressionBuilder(null, ProxyGenerator.CreateProxy<IMockExpressionProvider>(), ProxyGenerator.CreateProxy<IValueExpressionProvider>(), ProxyGenerator.CreateProxy<IIdentifierValidator>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ExpressionBuilder_Constructor_MockProviderNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new ExpressionBuilder(ProxyGenerator.CreateProxy<ITestMethodValueProvider>(), null, ProxyGenerator.CreateProxy<IValueExpressionProvider>(), ProxyGenerator.CreateProxy<IIdentifierValidator>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ExpressionBuilder_Constructor_ValueExpressionProviderNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new ExpressionBuilder(ProxyGenerator.CreateProxy<ITestMethodValueProvider>(), ProxyGenerator.CreateProxy<IMockExpressionProvider>(), null, ProxyGenerator.CreateProxy<IIdentifierValidator>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ExpressionBuilder_Constructor_IdentifierValidatorNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new ExpressionBuilder(ProxyGenerator.CreateProxy<ITestMethodValueProvider>(), ProxyGenerator.CreateProxy<IMockExpressionProvider>(), ProxyGenerator.CreateProxy<IValueExpressionProvider>(), null);		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.CodeGeneration.Generators
        {
            [TestClass]
            public partial class ConstructorSourceCodeGenerationRequestTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void ConstructorSourceCodeGenerationRequest_Constructor_ConstructorNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new ConstructorSourceCodeGenerationRequest(null, Value.Create<bool>(), Value.Create<bool>(), ProxyGenerator.CreateProxy<ParameterInfo>());		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.CodeGeneration.Generators
        {
            [TestClass]
            public partial class MethodSourceCodeGenerationRequestTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void MethodSourceCodeGenerationRequest_Constructor_MethodNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new MethodSourceCodeGenerationRequest(null, Value.Create<bool>(), ProxyGenerator.CreateProxy<ParameterInfo>());		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.CodeGeneration.Generators
        {
            [TestClass]
            public partial class NullArgumentConstructorTestMethodSourceCodeGeneratorTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void BuildMethodName_RequestNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var nullArgumentConstructorTestMethodSourceCodeGenerator = ProxyGenerator.CreateProxy<NullArgumentConstructorTestMethodSourceCodeGenerator>();		 
                    nullArgumentConstructorTestMethodSourceCodeGenerator.BuildMethodName(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void NullArgumentConstructorTestMethodSourceCodeGenerator_Constructor_ExpressionBuilderNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new NullArgumentConstructorTestMethodSourceCodeGenerator(null);		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.CodeGeneration.Generators
        {
            [TestClass]
            public partial class NullArgumentMethodTestMethodSourceCodeGeneratorTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void BuildMethodName_RequestNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var nullArgumentMethodTestMethodSourceCodeGenerator = ProxyGenerator.CreateProxy<NullArgumentMethodTestMethodSourceCodeGenerator>();		 
                    nullArgumentMethodTestMethodSourceCodeGenerator.BuildMethodName(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void NullArgumentMethodTestMethodSourceCodeGenerator_Constructor_ExpressionBuilderNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new NullArgumentMethodTestMethodSourceCodeGenerator(null, ProxyGenerator.CreateProxy<ITestMethodValueProvider>());		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void NullArgumentMethodTestMethodSourceCodeGenerator_Constructor_TestMethodValueProviderNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new NullArgumentMethodTestMethodSourceCodeGenerator(ProxyGenerator.CreateProxy<IExpressionBuilder>(), null);		 
                }

            }
        }
