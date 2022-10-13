using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace InfoViaLinq.Interfaces
{
    /// <summary>
    /// Returns the info
    /// </summary>
    public interface IGetPropInfo<in T>
    {
        MemberExpression MemberExpression { get; }
        
        /// <summary>
        /// Returns all member PropertyInfos
        /// </summary>
        /// <returns></returns>
        IEnumerable<PropertyInfo> Members();
    }
}