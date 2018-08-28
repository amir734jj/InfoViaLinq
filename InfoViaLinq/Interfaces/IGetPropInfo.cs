using System;
using System.Linq.Expressions;
using System.Reflection;

namespace InfoViaLinq.Interfaces
{
    /// <summary>
    /// Returns the info
    /// </summary>
    public interface IGetPropInfo<in T>
    {
        MemberExpression MemberExpresion { get; }
        
        string GetPropertyName();

        PropertyInfo GetPropertyInfo();

        TAttributeType GetAttribute<TAttributeType>() where TAttributeType : Attribute;

        bool GetValue<TProp>(T source, out TProp value);
    }
}