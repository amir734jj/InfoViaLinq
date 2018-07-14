using System.Linq.Expressions;
using System.Reflection;

namespace InfoViaLinq.Interfaces
{
    public interface IGetFuncInfo<T>
    {
        LambdaExpression LambdaExpression { get; }

        string GetMethodName();

        MethodInfo GetMethodInfo();
    }
}