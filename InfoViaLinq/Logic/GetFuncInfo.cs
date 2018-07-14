using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using InfoViaLinq.Interfaces;

namespace InfoViaLinq.Logic
{
    public class GetFuncInfo<T> : IGetFuncInfo<T>
    {        
        // ReSharper disable once StaticMemberInGenericType
        private static readonly bool IsNet45 = Type.GetType("System.Reflection.ReflectionContext", false) != null;

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
        public string MethodName() => GetMethodInfo()?.Name;

        /// <summary>
        /// Returns the method info
        /// </summary>
        /// <returns></returns>
        public MethodInfo GetMethodInfo()
        {
            var unaryExpression = (UnaryExpression) LambdaExpression.Body;
            var methodCallExpression = (MethodCallExpression) unaryExpression.Operand;

            if (IsNet45)
            {
                var methodCallObject = (ConstantExpression) methodCallExpression.Object;
                var methodInfo = (MethodInfo) methodCallObject?.Value;
                return methodInfo;
            }
            else
            {
                var methodInfoExpression = (ConstantExpression) methodCallExpression.Arguments.Last();
                var methodInfo = (MemberInfo) methodInfoExpression.Value;
                return (MethodInfo) methodInfo;
            }
        }
    }
}