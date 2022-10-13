using System.Linq.Expressions;
using System.Reflection;
using InfoViaLinq.Interfaces;

namespace InfoViaLinq.Logic
{
    internal class GetFuncInfo<T> : IGetFuncInfo<T>
    {
        public LambdaExpression LambdaExpression { get; }

        /// <summary>
        /// Constructor that takes Lambda expression
        /// </summary>
        /// <param name="lambdaExpression"></param>
        public GetFuncInfo(LambdaExpression lambdaExpression)
        {
            LambdaExpression = lambdaExpression;
        }

        /// <summary>
        /// Returns the method name itself
        /// </summary>
        /// <returns></returns>
        public string GetMethodName() => GetMethodInfo()?.Name;


        private sealed class MemberExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _expression;

            private MethodInfo _methodInfo;

            public MemberExpressionVisitor(Expression expression)
            {
                _expression = expression;
            }

            protected override Expression VisitConstant(ConstantExpression node)
            {
                if (_methodInfo != null) return base.VisitConstant(node);
                
                switch (node.Value)
                {
                    case MethodInfo methodInfo:
                        _methodInfo = methodInfo;
                        break;
                    default:
                        _methodInfo = null;
                        break;
                }

                return base.VisitConstant(node);
            }

            public void Accept(out MethodInfo methodInfo)
            {
                _methodInfo = null;
                
                Visit(_expression);

                methodInfo = _methodInfo;
            }
        }

        /// <summary>
        /// Returns the method info
        /// </summary>
        /// <returns></returns>
        public MethodInfo GetMethodInfo()
        {
            var memberExpressionVisitor = new MemberExpressionVisitor(LambdaExpression);

            memberExpressionVisitor.Accept(out var result);

            return result;
        }
    }
}