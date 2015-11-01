using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestGenerator;
using UnitTestGenerator.CodeGeneration.Generators;
using UnitTestGenerator.UnitTestGeneration;

namespace FooBarLibrary.Tests
{
    public class CustomTestMethodGenerator : ITestMethodGenerator
    {
        readonly MyCustomCodeGenerator sourceCodeGenerator;

        public CustomTestMethodGenerator(MyCustomCodeGenerator generator)
        {
            this.sourceCodeGenerator = generator;
        }

        public IEnumerable<TestMethod> GenerateTestMethods(TypeContext typeContext)
        {
            foreach (var method in GetMethods(typeContext))
            {
                var request = new MethodSourceCodeGenerationRequest(method, true, method.GetParameters().First());
                var methodName = sourceCodeGenerator.BuildMethodName(request);
                var sourceCode = sourceCodeGenerator.BuildSourceCode(request);
                yield return new TestMethod(method, methodName, sourceCode);
            }
        }

        private static System.Reflection.MethodInfo[] GetMethods(TypeContext typeContext)
        {
            return typeContext.TargetType.GetMethods()
                .Where(x => x.GetParameters().Count() > 1)
                .ToArray();
        }
    }
}
