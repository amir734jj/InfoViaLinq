using System;
using System.Linq.Expressions;
using System.Reflection;

namespace InfoViaLinq.Extensions
{
    public static class MemberExpressionExtension
    {        
        /// <summary>
        /// GetValue of MemberInfo
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static object GetValue(this MemberInfo memberInfo, object instance)
        {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)memberInfo).GetValue(instance);
                case MemberTypes.Property:
                    return ((PropertyInfo)memberInfo).GetValue(instance);
                default:
                    throw new NotImplementedException();
            }
        }
        
        /// <summary>
        /// SetValye of MemberInfo
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static void SetValue(this MemberInfo memberInfo, object instance)
        {
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    ((FieldInfo)memberInfo).SetValue(instance, null);
                    break;
                case MemberTypes.Property:
                    ((PropertyInfo)memberInfo).SetValue(instance, null);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}