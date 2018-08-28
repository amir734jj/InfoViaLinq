using System;
using System.Linq.Expressions;
using InfoViaLinq.Interfaces;
using InfoViaLinq.Logic;
using InfoViaLinq.Utilities;

namespace InfoViaLinq
{
    /// <summary>
    /// Utility to get property info via linq expression
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public class InfoViaLinq<TSource> : BaseBuilder<InfoViaLinq<TSource>>, IInfoViaLinq<TSource>
    {
        /// <summary>
        /// Converts lambda expression to member expression
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        protected virtual MemberExpression ToMemeberExpression<TResult>(Expression<Func<TSource, TResult>> exp)
        {
            MemberExpression resultExp;
            var body = exp.Body;
            
            switch (body)
            {
                case MemberExpression memberExpression:
                    resultExp = memberExpression;
                    break;
                case UnaryExpression unaryExpression:
                    resultExp = unaryExpression.Operand as MemberExpression;
                    break;
                case LambdaExpression _:
                    throw new Exception("Lambda expressions cannot be decomposed!");
                default:
                    throw new Exception("Something is wrong with the type!");
            }
            
            return resultExp;
        }

        /// <summary>
        /// GetInfo given member expression
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public IGetPropInfo<TSource> PropLambda(Expression<Func<TSource, object>> expr)
        {
            return new GetPropInfo<TSource>(ToMemeberExpression(expr));
        }

        /// <summary>
        /// Get info given member expression
        /// </summary>
        /// <param name="expr"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public IGetPropInfo<TSource> PropLambda<TResult>(Expression<Func<TSource, TResult>> expr)
        {
            return new GetPropInfo<TSource>(ToMemeberExpression(expr));
        }

        /// <summary>
        /// Void method
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IGetFuncInfo<TSource> FuncLambda(Expression<Func<TSource, Action>> expression)
        {
            return new GetFuncInfo<TSource>(expression);
        }

        /// <summary>
        /// Void with one praramter
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="TParam"></typeparam>
        /// <returns></returns>
        public IGetFuncInfo<TSource> FuncLambda<TParam>(Expression<Func<TSource, Action<TParam>>> expression)
        {
            return new GetFuncInfo<TSource>(expression);
        }

        /// <summary>
        /// Void with two praramters
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="TParam1"></typeparam>
        /// <typeparam name="TParam2"></typeparam>
        /// <returns></returns>
        public IGetFuncInfo<TSource> FuncLambda<TParam1, TParam2>(Expression<Func<TSource, Action<TParam1, TParam2>>> expression)
        {
            return new GetFuncInfo<TSource>(expression);
        }

        /// <summary>
        /// Void with three praramters
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="TParam1"></typeparam>
        /// <typeparam name="TParam2"></typeparam>
        /// <typeparam name="TParam3"></typeparam>
        /// <returns></returns>
        public IGetFuncInfo<TSource> FuncLambda<TParam1, TParam2, TParam3>(Expression<Func<TSource, Action<TParam1, TParam2, TParam3>>> expression)
        {
            return new GetFuncInfo<TSource>(expression);
        }

        /// <summary>
        /// Method with no prarameter
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public IGetFuncInfo<TSource> FuncLambda<TResult>(Expression<Func<TSource, Func<TResult>>> expression)
        {
            return new GetFuncInfo<TSource>(expression);
        }

        /// <summary>
        /// Method with one prarameter
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="TParam"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public IGetFuncInfo<TSource> FuncLambda<TParam, TResult>(Expression<Func<TSource, Func<TParam, TResult>>> expression)
        {
            return new GetFuncInfo<TSource>(expression);
        }

        /// <summary>
        /// Method with two parameters
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="TParam1"></typeparam>
        /// <typeparam name="TParam2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public IGetFuncInfo<TSource> FuncLambda<TParam1, TParam2, TResult>(Expression<Func<TSource, Func<TParam1, TParam2, TResult>>> expression)
        {
            return new GetFuncInfo<TSource>(expression);
        }

        /// <summary>
        /// Method with three prameters
        /// </summary>
        /// <param name="expression"></param>
        /// <typeparam name="TParam1"></typeparam>
        /// <typeparam name="TParam2"></typeparam>
        /// <typeparam name="TParam3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public IGetFuncInfo<TSource> FuncLambda<TParam1, TParam2, TParam3, TResult>(Expression<Func<TSource, Func<TParam1, TParam2, TParam3, TResult>>> expression)
        {
            return new GetFuncInfo<TSource>(expression);
        }
    }
}