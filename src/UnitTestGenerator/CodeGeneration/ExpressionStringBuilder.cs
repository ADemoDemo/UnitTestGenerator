/* ****************************************************************************
 * Copyright 2015 Peter Csikós
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.

 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Apache License, Version 2.0. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the  Apache License, Version 2.0, please send an email to 
 * dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Apache License, Version 2.0.
 *
 * You must not remove this notice, or any other, from this software.
 *
 *
 * ***************************************************************************/

using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace UnitTestGenerator
{
    [ExcludeFromCodeCoverage]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
    public sealed class ExpressionStringBuilder : ExpressionVisitor, IDisposable
    {
        private StringBuilder _out;

        // Associate every unique label or anonymous parameter in the tree with an integer.
        // The label is displayed as Label_#.
        private Dictionary<object, int> _ids;
        private readonly CSharpCodeProvider codeProvider = new CSharpCodeProvider();


        private ExpressionStringBuilder()
        {
            _out = new StringBuilder();
        }

        public override string ToString()
        {
            return _out.ToString();
        }

        private void AddLabel(LabelTarget label)
        {
            if (_ids == null)
            {
                _ids = new Dictionary<object, int>();
                _ids.Add(label, 0);
            }
            else
            {
                if (!_ids.ContainsKey(label))
                {
                    _ids.Add(label, _ids.Count);
                }
            }
        }

        private int GetLabelId(LabelTarget label)
        {
            if (_ids == null)
            {
                _ids = new Dictionary<object, int>();
                AddLabel(label);
                return 0;
            }
            else
            {
                int id;
                if (!_ids.TryGetValue(label, out id))
                {
                    //label is met the first time
                    id = _ids.Count;
                    AddLabel(label);
                }
                return id;
            }
        }

        private void AddParam(ParameterExpression p)
        {
            if (_ids == null)
            {
                _ids = new Dictionary<object, int>();
                _ids.Add(_ids, 0);
            }
            else
            {
                if (!_ids.ContainsKey(p))
                {
                    _ids.Add(p, _ids.Count);
                }
            }
        }

        private int GetParamId(ParameterExpression p)
        {
            if (_ids == null)
            {
                _ids = new Dictionary<object, int>();
                AddParam(p);
                return 0;
            }
            else
            {
                int id;
                if (!_ids.TryGetValue(p, out id))
                {
                    // p is met the first time
                    id = _ids.Count;
                    AddParam(p);
                }
                return id;
            }
        }

        #region The printing code

        private void Out(string s)
        {
            _out.Append(s);
        }

        private void Out(char c)
        {
            _out.Append(c);
        }

        #endregion

        #region Output an expresstion tree to a string

        /// <summary>
        /// Output a given expression tree to a string.
        /// </summary>
        public static string ExpressionToString(Expression node)
        {
            Check.NotNull(node, "node");
            using (ExpressionStringBuilder esb = new ExpressionStringBuilder())
            {
                esb.Visit(node);
                return esb.ToString();
            }
        }

        public static string CatchBlockToString(CatchBlock node)
        {
            Check.NotNull(node, "node");
            using (ExpressionStringBuilder esb = new ExpressionStringBuilder())
            {
                esb.VisitCatchBlock(node);
                return esb.ToString();
            }
        }

        public static string SwitchCaseToString(SwitchCase node)
        {
            Check.NotNull(node, "node");
            using (ExpressionStringBuilder esb = new ExpressionStringBuilder())
            {
                esb.VisitSwitchCase(node);
                return esb.ToString();
            }
        }

        /// <summary>
        /// Output a given member binding to a string.
        /// </summary>
        internal static string MemberBindingToString(MemberBinding node)
        {
            Check.NotNull(node, "node");
            using (ExpressionStringBuilder esb = new ExpressionStringBuilder())
            {
                esb.VisitMemberBinding(node);
                return esb.ToString();
            }
        }

        /// <summary>
        /// Output a given ElementInit to a string.
        /// </summary>
        internal static string ElementInitBindingToString(ElementInit node)
        {
            Check.NotNull(node, "node");
            using (ExpressionStringBuilder esb = new ExpressionStringBuilder())
            {
                esb.VisitElementInit(node);
                return esb.ToString();
            }
        }

        // More proper would be to make this a virtual method on Action
        private static string FormatBinder(CallSiteBinder binder)
        {
            ConvertBinder convert;
            GetMemberBinder getMember;
            SetMemberBinder setMember;
            DeleteMemberBinder deleteMember;
            InvokeMemberBinder call;
            UnaryOperationBinder unary;
            BinaryOperationBinder binary;

            if ((convert = binder as ConvertBinder) != null)
            {
                return "Convert " + convert.Type;
            }
            else if ((getMember = binder as GetMemberBinder) != null)
            {
                return "GetMember " + getMember.Name;
            }
            else if ((setMember = binder as SetMemberBinder) != null)
            {
                return "SetMember " + setMember.Name;
            }
            else if ((deleteMember = binder as DeleteMemberBinder) != null)
            {
                return "DeleteMember " + deleteMember.Name;
            }
            else if (binder is GetIndexBinder)
            {
                return "GetIndex";
            }
            else if (binder is SetIndexBinder)
            {
                return "SetIndex";
            }
            else if (binder is DeleteIndexBinder)
            {
                return "DeleteIndex";
            }
            else if ((call = binder as InvokeMemberBinder) != null)
            {
                return "Call " + call.Name;
            }
            else if (binder is InvokeBinder)
            {
                return "Invoke";
            }
            else if (binder is CreateInstanceBinder)
            {
                return "Create";
            }
            else if ((unary = binder as UnaryOperationBinder) != null)
            {
                return unary.Operation.ToString();
            }
            else if ((binary = binder as BinaryOperationBinder) != null)
            {
                return binary.Operation.ToString();
            }
            else
            {
                return "CallSiteBinder";
            }
        }

        private void VisitExpressions<T>(char open, IList<T> expressions, char close) where T : Expression
        {
            VisitExpressions(open, expressions, close, ", ");
        }

        private void VisitExpressions<T>(char open, IList<T> expressions, char close, string seperator) where T : Expression
        {
            Out(open);
            if (expressions != null)
            {
                bool isFirst = true;
                foreach (T e in expressions)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                    else
                    {
                        Out(seperator);
                    }
                    Visit(e);
                }
            }
            Out(close);
        }

        protected override Expression VisitDynamic(DynamicExpression node)
        {
            Out(FormatBinder(node.Binder));
            VisitExpressions('(', node.Arguments, ')');
            return node;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.ArrayIndex)
            {
                Visit(node.Left);
                Out("[");
                Visit(node.Right);
                Out("]");
            }
            else
            {
                string op;
                bool requiresParentheses = false;
                switch (node.NodeType)
                {
                    // AndAlso and OrElse were unintentionally changed in
                    // CLR 4. We changed them to "AndAlso" and "OrElse" to
                    // be 3.5 compatible, but it turns out 3.5 shipped with
                    // "&&" and "||". Oops.
                    case ExpressionType.AndAlso:
                        op = "AndAlso";
#if SILVERLIGHT
                        if (Expression.SilverlightQuirks) op = "&&";
#endif
                        break;
                    case ExpressionType.OrElse:
                        op = "OrElse";
#if SILVERLIGHT
                        if (Expression.SilverlightQuirks) op = "||";
#endif
                        break;
                    case ExpressionType.Assign: op = "=";
                        break;
                    case ExpressionType.Equal:
                        op = "==";
#if SILVERLIGHT
                        if (Expression.SilverlightQuirks) op = "=";
#endif
                        break;
                    case ExpressionType.NotEqual: op = "!="; break;
                    case ExpressionType.GreaterThan: op = ">"; break;
                    case ExpressionType.LessThan: op = "<"; break;
                    case ExpressionType.GreaterThanOrEqual: op = ">="; break;
                    case ExpressionType.LessThanOrEqual: op = "<="; break;
                    case ExpressionType.Add: op = "+"; break;
                    case ExpressionType.AddAssign: op = "+="; break;
                    case ExpressionType.AddAssignChecked: op = "+="; break;
                    case ExpressionType.AddChecked: op = "+"; break;
                    case ExpressionType.Subtract: op = "-"; break;
                    case ExpressionType.SubtractAssign: op = "-="; break;
                    case ExpressionType.SubtractAssignChecked: op = "-="; break;
                    case ExpressionType.SubtractChecked: op = "-"; break;
                    case ExpressionType.Divide: op = "/"; break;
                    case ExpressionType.DivideAssign: op = "/="; break;
                    case ExpressionType.Modulo: op = "%"; break;
                    case ExpressionType.ModuloAssign: op = "%="; break;
                    case ExpressionType.Multiply: op = "*"; break;
                    case ExpressionType.MultiplyAssign: op = "*="; break;
                    case ExpressionType.MultiplyAssignChecked: op = "*="; break;
                    case ExpressionType.MultiplyChecked: op = "*"; break;
                    case ExpressionType.LeftShift: op = "<<"; break;
                    case ExpressionType.LeftShiftAssign: op = "<<="; break;
                    case ExpressionType.RightShift: op = ">>"; break;
                    case ExpressionType.RightShiftAssign: op = ">>="; break;
                    case ExpressionType.And:
                        if (node.Type == typeof(bool) || node.Type == typeof(bool?))
                        {
                            op = "And";
                        }
                        else
                        {
                            op = "&";
                        }
                        break;
                    case ExpressionType.AndAssign:
                        if (node.Type == typeof(bool) || node.Type == typeof(bool?))
                        {
                            op = "&&=";
                        }
                        else
                        {
                            op = "&=";
                        }
                        break;
                    case ExpressionType.Or:
                        if (node.Type == typeof(bool) || node.Type == typeof(bool?))
                        {
                            op = "Or";
                        }
                        else
                        {
                            op = "|";
                        }
                        break;
                    case ExpressionType.OrAssign:
                        if (node.Type == typeof(bool) || node.Type == typeof(bool?))
                        {
                            op = "||=";
                        }
                        else { op = "|="; }
                        break;
                    case ExpressionType.ExclusiveOr: op = "^"; break;
                    case ExpressionType.ExclusiveOrAssign: op = "^="; break;
                    case ExpressionType.Power: op = "^"; break;
                    case ExpressionType.PowerAssign: op = "**="; break;
                    case ExpressionType.Coalesce: op = "??"; break;

                    default:
                        throw new InvalidOperationException();
                }
                if (requiresParentheses)
                {
                    Out("(");
                }
                Visit(node.Left);
                Out(' ');
                Out(op);
                Out(' ');
                Visit(node.Right);
                if (requiresParentheses)
                {
                    Out(")");
                }
            }
            return node;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.IsByRef)
            {
                Out("ref ");
            }
            string name = node.Name;
            if (String.IsNullOrEmpty(name))
            {
#if SILVERLIGHT
                if (Expression.SilverlightQuirks) {
                    Out(name ?? "<param>");
                    return node;
                }
#endif
                Out("Param_" + GetParamId(node));
            }
            else
            {
                Out(name);
            }
            return node;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            if (node.Parameters.Count == 1)
            {
                // p => body
                Visit(node.Parameters[0]);
            }
            else
            {
                // (p1, p2, ..., pn) => body
                VisitExpressions('(', node.Parameters, ')');
            }
            Out(" => ");
            Visit(node.Body);
            return node;
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            Visit(node.NewExpression);
            Out(" {");
            for (int i = 0, n = node.Initializers.Count; i < n; i++)
            {
                if (i > 0)
                {
                    Out(", ");
                }
                Out(node.Initializers[i].ToString());
            }
            Out("}");
            return node;
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            Out("IIF(");
            Visit(node.Test);
            Out(", ");
            Visit(node.IfTrue);
            Out(", ");
            Visit(node.IfFalse);
            Out(")");
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value != null)
            {
                if (node.Value is Type)
                {
                    this.Out("typeof(");
                    var type = (Type)node.Value;
                    Out(GetTypePrimitiveName(type));
                    Out(")");
                }
                else
                {
                    if (node.Value is bool)
                    {
                        Out((bool)node.Value ? "true" : "false");
                    }
                    else
                    {
                        string text = node.Value.ToString();
                        if (node.Value is string)
                        {
                            this.Out("\"");
                            this.Out(text);
                            this.Out("\"");
                        }
                        else
                        {
                            if (text == node.Value.GetType().ToString())
                            {
                                this.Out("value(");
                                this.Out(text);
                                this.Out(")");
                            }
                            else
                            {
                                this.Out(text);
                            }
                        }
                    }
                }
            }
            else
            {
                this.Out("null");
            }
            return node;
        }

        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            string s = String.Format(
                CultureInfo.CurrentCulture,
                "<DebugInfo({0}: {1}, {2}, {3}, {4})>",
                node.Document.FileName,
                node.StartLine,
                node.StartColumn,
                node.EndLine,
                node.EndColumn
            );
            Out(s);
            return node;
        }

        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            VisitExpressions('(', node.Variables, ')');
            return node;
        }

        // Prints ".instanceField" or "declaringType.staticField"
        private void OutMember(Expression instance, MemberInfo member)
        {
            if (instance != null)
            {
                Visit(instance);
                Out("." + member.Name);
                return;
            }
            // For static members, include the type name
            OutType(member.DeclaringType);
            this.Out("." + member.Name);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            OutMember(node.Expression, node.Member);
            return node;
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            if (node.NewExpression.Arguments.Count == 0 &&
                node.NewExpression.Type.Name.Contains("<"))
            {
                // anonymous type constructor
                Out("new");
            }
            else
            {
                Visit(node.NewExpression);
            }
            Out(" {");
            for (int i = 0, n = node.Bindings.Count; i < n; i++)
            {
                MemberBinding b = node.Bindings[i];
                if (i > 0)
                {
                    Out(", ");
                }
                VisitMemberBinding(b);
            }
            Out("}");
            return node;
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment assignment)
        {
            Out(assignment.Member.Name);
            Out(" = ");
            Visit(assignment.Expression);
            return assignment;
        }

        protected override MemberListBinding VisitMemberListBinding(MemberListBinding binding)
        {
            Out(binding.Member.Name);
            Out(" = {");
            for (int i = 0, n = binding.Initializers.Count; i < n; i++)
            {
                if (i > 0)
                {
                    Out(", ");
                }
                VisitElementInit(binding.Initializers[i]);
            }
            Out("}");
            return binding;
        }

        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding binding)
        {
            Out(binding.Member.Name);
            Out(" = {");
            for (int i = 0, n = binding.Bindings.Count; i < n; i++)
            {
                if (i > 0)
                {
                    Out(", ");
                }
                VisitMemberBinding(binding.Bindings[i]);
            }
            Out("}");
            return binding;
        }

        protected override ElementInit VisitElementInit(ElementInit initializer)
        {
            Out(initializer.AddMethod.ToString());
            string sep = ", ";
#if SILVERLIGHT
            if (Expression.SilverlightQuirks) sep = ",";
#endif
            VisitExpressions('(', initializer.Arguments, ')', sep);
            return initializer;
        }

        protected override Expression VisitInvocation(InvocationExpression node)
        {
            Out("Invoke(");
            Visit(node.Expression);
            string sep = ", ";
#if SILVERLIGHT
            if (Expression.SilverlightQuirks) sep = ",";
#endif
            for (int i = 0, n = node.Arguments.Count; i < n; i++)
            {
                Out(sep);
                Visit(node.Arguments[i]);
            }
            Out(")");
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            int start = 0;
            Expression ob = node.Object;

            if (Attribute.GetCustomAttribute(node.Method, typeof(ExtensionAttribute)) != null)
            {
                start = 1;
                ob = node.Arguments[0];
            }

            if (ob == null)
            {
                Out(node.Method.DeclaringType.Name);
                Out(".");
            }
            else
            {
                Visit(ob);
                Out(".");
            }
            Out(node.Method.Name);
            if (node.Method.IsGenericMethod)
            {
                this.Out("<");
                var args = node.Method.GetGenericArguments();
                foreach (var arg in args)
                {
                    OutType(arg);
                }
                this.Out(">");
            }
            Out("(");
            for (int i = start, n = node.Arguments.Count; i < n; i++)
            {
                if (i > start)
                    Out(", ");
                Visit(node.Arguments[i]);
            }
            Out(")");
            return node;
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.NewArrayBounds:
                    // new MyType[](expr1, expr2)
                    Out("new " + node.Type.ToString());
                    VisitExpressions('(', node.Expressions, ')');
                    break;
                case ExpressionType.NewArrayInit:
                    // new [] {expr1, expr2}
                    Out("new [] ");
                    VisitExpressions('{', node.Expressions, '}');
                    break;
            }
            return node;
        }

        private void OutType(Type t)
        {
            if (t.IsGenericType)
            {
                var generic = t.GetGenericTypeDefinition();
                Out(generic.Name.Substring(0, generic.Name.IndexOf('`')));
                Out('<');

                int i = 0;
                foreach (var arg in t.GetGenericArguments())
                {
                    if (i++ > 0)
                    {
                        Out(", ");
                    }
                    OutType(arg);
                }
                //Out(string.Join(", ", t.GetGenericArguments().Select(x => OutType(x)).ToArray()));
                Out('>');
            }
            else
            {
                Out(GetTypePrimitiveName(t));
            }
        }

        protected override Expression VisitNew(NewExpression node)
        {
            Out("new ");
            OutType(node.Type);
            Out("(");
            var members = node.Members;
            for (int i = 0; i < node.Arguments.Count; i++)
            {
                if (i > 0)
                {
                    Out(", ");
                }
                if (members != null)
                {
                    string name = members[i].Name;
#if SILVERLIGHT
                    // Members can be the get/set methods rather than the fields/properties
                    PropertyInfo pi = null;
                    if (Expression.SilverlightQuirks &&
                        members[i].MemberType == MemberTypes.Method &&
                        (pi = GetPropertyNoThrow((MethodInfo)members[i])) != null) {
                        name = pi.Name;
                    }
#endif
                    Out(name);
                    Out(" = ");
                }
                Visit(node.Arguments[i]);
            }
            Out(")");
            return node;
        }

        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            Out("(");
            Visit(node.Expression);
            switch (node.NodeType)
            {
                case ExpressionType.TypeIs:
                    Out(" Is ");
                    break;
                case ExpressionType.TypeEqual:
                    Out(" TypeEqual ");
                    break;
            }
            Out(node.TypeOperand.Name);
            Out(")");
            return node;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        protected override Expression VisitUnary(UnaryExpression node)
        {
            ExpressionType nodeType = node.NodeType;
            if (nodeType == ExpressionType.Convert)
            {
                Out("(");
                OutType(node.Type);
                Out(")");
                if (node.Operand.NodeType == ExpressionType.Quote)
                {
                    Out("(");
                }
                Visit(node.Operand);
                if (node.Operand.NodeType == ExpressionType.Quote)
                {
                    Out(")");
                }
                return node;
            }

            switch (node.NodeType)
            {
                case ExpressionType.TypeAs:
                    Out("(");
                    break;
                case ExpressionType.Not:
                    Out("Not(");
                    break;
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                    Out("-");
                    break;
                case ExpressionType.UnaryPlus:
                    Out("+");
                    break;
                case ExpressionType.Quote:
                    break;
                case ExpressionType.Throw:
                    Out("throw(");
                    break;
                case ExpressionType.Increment:
                    Out("Increment(");
                    break;
                case ExpressionType.Decrement:
                    Out("Decrement(");
                    break;
                case ExpressionType.PreIncrementAssign:
                    Out("++");
                    break;
                case ExpressionType.PreDecrementAssign:
                    Out("--");
                    break;
                case ExpressionType.OnesComplement:
                    Out("~(");
                    break;
                default:
                    Out(node.NodeType.ToString());
                    Out("(");
                    break;
            }

            Visit(node.Operand);

            switch (node.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.UnaryPlus:
                case ExpressionType.PreDecrementAssign:
                case ExpressionType.PreIncrementAssign:
                case ExpressionType.Quote:
                    break;
                case ExpressionType.TypeAs:
                    Out(" As ");
                    Out(node.Type.Name);
                    Out(")");
                    break;
                case ExpressionType.PostIncrementAssign:
                    Out("++");
                    break;
                case ExpressionType.PostDecrementAssign:
                    Out("--");
                    break;
                default:
                    Out(")");
                    break;
            }
            return node;
        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            Out("{");
            foreach (var v in node.Variables)
            {
                Out("var ");
                Visit(v);
                Out(";");
            }
            Out(" ... }");
            return node;
        }

        protected override Expression VisitDefault(DefaultExpression node)
        {
            Out("default(");
            Out(node.Type.Name);
            Out(")");
            return node;
        }

        protected override Expression VisitLabel(LabelExpression node)
        {
            Out("{ ... } ");
            DumpLabel(node.Target);
            Out(":");
            return node;
        }

        protected override Expression VisitGoto(GotoExpression node)
        {
            Out(node.Kind.ToString().ToLower(CultureInfo.CurrentCulture));
            DumpLabel(node.Target);
            if (node.Value != null)
            {
                Out(" (");
                Visit(node.Value);
                Out(") ");
            }
            return node;
        }

        protected override Expression VisitLoop(LoopExpression node)
        {
            Out("loop { ... }");
            return node;
        }

        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            Out("case ");
            VisitExpressions('(', node.TestValues, ')');
            Out(": ...");
            return node;
        }

        protected override Expression VisitSwitch(SwitchExpression node)
        {
            Out("switch ");
            Out("(");
            Visit(node.SwitchValue);
            Out(") { ... }");
            return node;
        }

        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            Out("catch (" + node.Test.Name);
            if (node.Variable != null)
            {
                Out(node.Variable.Name ?? "");
            }
            Out(") { ... }");
            return node;
        }

        protected override Expression VisitTry(TryExpression node)
        {
            Out("try { ... }");
            return node;
        }

        protected override Expression VisitIndex(IndexExpression node)
        {
            if (node.Object != null)
            {
                Visit(node.Object);
            }
            else
            {
                //                Debug.Assert(node.Indexer != null);
                Out(node.Indexer.DeclaringType.Name);
            }
            if (node.Indexer != null)
            {
                Out(".");
                Out(node.Indexer.Name);
            }

            VisitExpressions('[', node.Arguments, ']');
            return node;
        }

        protected override Expression VisitExtension(Expression node)
        {
            // Prefer an overriden ToString, if available.
            var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.ExactBinding;
            var toString = node.GetType().GetMethod("ToString", flags, null, Type.EmptyTypes, null);
            if (toString.DeclaringType != typeof(Expression))
            {
                Out(node.ToString());
                return node;
            }

            Out("[");
            // For 3.5 subclasses, print the NodeType.
            // For Extension nodes, print the class name.
            if (node.NodeType == ExpressionType.Extension)
            {
                Out(node.GetType().FullName);
            }
            else
            {
                Out(node.NodeType.ToString());
            }
            Out("]");
            return node;
        }

        private void DumpLabel(LabelTarget target)
        {
            if (!String.IsNullOrEmpty(target.Name))
            {
                Out(target.Name);
            }
            else
            {
                int labelId = GetLabelId(target);
                Out("UnamedLabel_" + labelId);
            }
        }


        private string GetTypePrimitiveName(Type t)
        {
            // Just to prove a point...

            if ((t.IsValueType || t == typeof(string)) && !t.IsEnum)
            {
                var type = new CodeTypeReference(t);
                return codeProvider.GetTypeOutput(type);
            }
            return t.Name;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    codeProvider.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
        #endregion
    }
}
