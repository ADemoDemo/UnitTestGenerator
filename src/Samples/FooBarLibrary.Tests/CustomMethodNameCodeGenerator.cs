using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestGenerator;
using UnitTestGenerator.CodeGeneration.Generators;

namespace FooBarLibrary.Tests
{
    public class CustomMethodNameCodeGenerator : NullArgumentMethodTestMethodSourceCodeGenerator
    {
        public CustomMethodNameCodeGenerator(IExpressionBuilder expressionBuilder, ITestMethodValueProvider testMethodValueProvider) : base(expressionBuilder, testMethodValueProvider)
        {

        }

        public override string BuildMethodName(MethodSourceCodeGenerationRequest request)
        {
            return request.Method.Name + Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}
