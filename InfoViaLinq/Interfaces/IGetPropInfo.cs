using System;
using System.Linq.Expressions;
using System.Reflection;

namespace InfoViaLinq.Interfaces
{
    /// <summary>
    /// Returns the info
    /// </summary>
    public interface IGetPropInfo<T>
    {
        MemberExpression MemberExpresion { get; }
        
        string GetPropertyName();

        PropertyInfo GetPropertyInfo();

        TAttributeType GetAttribute<TAttributeType>() where TAttributeType : Attribute;
    }
}