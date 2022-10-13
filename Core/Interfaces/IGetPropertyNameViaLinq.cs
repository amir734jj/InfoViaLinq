using System;
using System.Linq.Expressions;

namespace InfoViaLinq.Interfaces
{
    /// <summary>
    /// Initialized instance of Utility
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public interface IInfoViaLinq<TSource>
    {
        #region Property
        
        IGetPropInfo<TSource> PropLambda(Expression<Func<TSource, object>> expr);

        #endregion
        
        #region Method

        IGetFuncInfo<TSource> FuncLambda(Expression<Func<TSource, Action>> expression);

        IGetFuncInfo<TSource> FuncLambda<TParam>(Expression<Func<TSource, Action<TParam>>> expression);

        IGetFuncInfo<TSource> FuncLambda<TResult>(Expression<Func<TSource, Func<TResult>>> expression);
        
        IGetFuncInfo<TSource> FuncLambda<TParam, TResult>(Expression<Func<TSource, Func<TParam, TResult>>> expression);

        IGetFuncInfo<TSource> FuncLambda<TParam1, TParam2, TResult>(Expression<Func<TSource, Func<TParam1, TParam2, TResult>>> expression);

        IGetFuncInfo<TSource> FuncLambda<TParam1, TParam2, TParam3, TResult>(Expression<Func<TSource, Func<TParam1, TParam2, TParam3, TResult>>> expression);
        
        #endregion
    }
}