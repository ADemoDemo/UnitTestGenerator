using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestGenerator;
using UnitTestGenerator.CodeGeneration.Generators;

namespace FooBarLibrary.Tests
{
    public class MyCustomCodeGenerator : NullArgumentMethodTestMethodSourceCodeGenerator
    {
        public MyCustomCodeGenerator(IExpressionBuilder expressionBuilder, 
            ITestMethodValueProvider testMethodValueProvider) 
            : base(expressionBuilder, testMethodValueProvider)
        {
        }

        protected override void BuildArrangeSourceCode(MethodSourceCodeGenerationRequest request)
        {
            AppendLine("//TODO: Arrange");
        }

        protected override void BuildActSourceCode(MethodSourceCodeGenerationRequest request)
        {
            AppendLine("//TODO: Act");
            Append("//");
            Append("TODO: call ");
            AppendLine(request.Method.Name);
        }

        protected override void BuildAssertSourceCode(MethodSourceCodeGenerationRequest request)
        {
            AppendLine("//TODO: Assert");
        }

        public override string BuildMethodName(MethodSourceCodeGenerationRequest request)
        {
            return request.Method.Name;
        }
    }
}
