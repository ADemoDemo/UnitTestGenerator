
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Collections.Generic;
using System.Reflection;
using UnitTestGenerator.DynamicProxy;
using System.Linq.Expressions;
using UnitTestGenerator.Tests;
using UnitTestGenerator.UnitTestGeneration;
using UnitTestGenerator.CodeGeneration;
using UnitTestGenerator.CodeGeneration.Generators;

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

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                public void AssemblyTraverser_Constructor()
                {
                    var instance = new AssemblyTraverser(typeof(string).Assembly, Param_0 => Value.Create<bool>(), Value.Create<bool>());		 
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

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                public void TestClassBuilder_Constructor()
                {
                    var instance = new TestClassBuilder(ProxyGenerator.CreateProxy<IAssemblyTraverser>(), new [] {ProxyGenerator.CreateProxy<ITestMethodGenerator>(), ProxyGenerator.CreateProxy<ITestMethodGenerator>()});		 
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

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                public void ConstructorMetadata_Constructor()
                {
                    var instance = new ConstructorMetadata(typeof(string).GetConstructors()[0], Value.Create<bool>(), Value.Create<bool>());		 
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

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                public void MethodMetadata_Constructor()
                {
                    var instance = new MethodMetadata(typeof(string).GetMethods()[0], Value.Create<bool>());		 
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

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                public void NullArgumentConstructorTestMethodGenerator_Constructor()
                {
                    var instance = new NullArgumentConstructorTestMethodGenerator(ProxyGenerator.CreateProxy<INullArgumentConstructorTestMethodSourceCodeGenerator>());		 
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

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                public void NullArgumentMethodTestMethodGenerator_Constructor()
                {
                    var instance = new NullArgumentMethodTestMethodGenerator(ProxyGenerator.CreateProxy<INullArgumentMethodTestMethodSourceCodeGenerator>());		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.UnitTestGeneration
        {
            [TestClass]
            public partial class RandomArgumentConstructorTestMethodGeneratorTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void RandomArgumentConstructorTestMethodGenerator_Constructor_IgnoreTypesNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new RandomArgumentConstructorTestMethodGenerator(null, new RandomArgumentConstructorTestMethodSourceCodeGenerator(ProxyGenerator.CreateProxy<IExpressionBuilder>()));		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void RandomArgumentConstructorTestMethodGenerator_Constructor_RandomArgumentConstructorTestMethodSourceCodeGeneratorNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new RandomArgumentConstructorTestMethodGenerator(new [] {typeof(string), typeof(string)}, null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                public void RandomArgumentConstructorTestMethodGenerator_Constructor()
                {
                    var instance = new RandomArgumentConstructorTestMethodGenerator(new [] {typeof(string), typeof(string)}, new RandomArgumentConstructorTestMethodSourceCodeGenerator(ProxyGenerator.CreateProxy<IExpressionBuilder>()));		 
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

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                public void ExpressionBuilder_Constructor()
                {
                    var instance = new ExpressionBuilder(ProxyGenerator.CreateProxy<ITestMethodValueProvider>(), ProxyGenerator.CreateProxy<IMockExpressionProvider>(), ProxyGenerator.CreateProxy<IValueExpressionProvider>(), ProxyGenerator.CreateProxy<IIdentifierValidator>());		 
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

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                public void ConstructorSourceCodeGenerationRequest_Constructor()
                {
                    var instance = new ConstructorSourceCodeGenerationRequest(typeof(string).GetConstructors()[0], Value.Create<bool>(), Value.Create<bool>(), ProxyGenerator.CreateProxy<ParameterInfo>());		 
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

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                public void MethodSourceCodeGenerationRequest_Constructor()
                {
                    var instance = new MethodSourceCodeGenerationRequest(typeof(string).GetMethods()[0], Value.Create<bool>(), ProxyGenerator.CreateProxy<ParameterInfo>());		 
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

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                public void NullArgumentConstructorTestMethodSourceCodeGenerator_Constructor()
                {
                    var instance = new NullArgumentConstructorTestMethodSourceCodeGenerator(ProxyGenerator.CreateProxy<IExpressionBuilder>());		 
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

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                public void NullArgumentMethodTestMethodSourceCodeGenerator_Constructor()
                {
                    var instance = new NullArgumentMethodTestMethodSourceCodeGenerator(ProxyGenerator.CreateProxy<IExpressionBuilder>(), ProxyGenerator.CreateProxy<ITestMethodValueProvider>());		 
                }

            }
        }

        namespace UnitTestGenerator.Tests.CodeGeneration.Generators
        {
            [TestClass]
            public partial class RandomArgumentConstructorTestMethodSourceCodeGeneratorTests
            {
                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void BuildMethodName_RequestNullValueGiven_ShouldThrowArgumentNullException()
                {
                    var randomArgumentConstructorTestMethodSourceCodeGenerator = ProxyGenerator.CreateProxy<RandomArgumentConstructorTestMethodSourceCodeGenerator>();		 
                    randomArgumentConstructorTestMethodSourceCodeGenerator.BuildMethodName(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                [ExpectedException(typeof(System.ArgumentNullException))]
                public void RandomArgumentConstructorTestMethodSourceCodeGenerator_Constructor_ExpressionBuilderNullValueGiven_ShouldThrowArgumentNullException()
                {
                    new RandomArgumentConstructorTestMethodSourceCodeGenerator(null);		 
                }

                [TestMethod]
                [TestCategory("UnitTestGenerator.ArgumentCheck")]
                public void RandomArgumentConstructorTestMethodSourceCodeGenerator_Constructor()
                {
                    var instance = new RandomArgumentConstructorTestMethodSourceCodeGenerator(ProxyGenerator.CreateProxy<IExpressionBuilder>());		 
                }

            }
        }
