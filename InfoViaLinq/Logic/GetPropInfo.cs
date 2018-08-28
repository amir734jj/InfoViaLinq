using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using InfoViaLinq.Extensions;
using InfoViaLinq.Interfaces;

namespace InfoViaLinq.Logic
{
    public class GetPropInfo<TSource> : IGetPropInfo<TSource>
    {
        private readonly List<MemberInfo> _memberInfos;
        
        private const string Deliminter = "";

        public MemberExpression MemberExpresion { get; }

        /// <summary>
        /// Constructor that takes the member expression
        /// </summary>
        /// <param name="memberExpression"></param>
        public GetPropInfo(MemberExpression memberExpression)
        {
            MemberExpresion = memberExpression;

            _memberInfos = ProcessPropLambdaMemberExpression(memberExpression);
        }
        
        /// <summary>
        /// Returns property name using lambda
        /// </summary>
        /// <returns></returns>
        public string GetPropertyName()
        {
            // Join the tokens together
            return string.Join(Deliminter, _memberInfos.Select(x => x.Name));
        }

        /// <summary>
        /// Returns the nested value of root object
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool GetValue<T>(TSource source, out T value)
        {
            // If Source is null just return null as value and false as flag
            if (source == null)
            {
                value = default(T);
                return false;
            }
            
            // Get node and implicitly cast it to object
            object nodeSource = source;
            
            _memberInfos.ForEach(x =>
            {
                nodeSource = nodeSource?.GetType().GetProperty(x.Name)?.GetValue(nodeSource);
            });

            // Return the value
            value = (T) nodeSource;

            return true;
        }

        /// <summary>
        /// Returns the property info via linq
        /// </summary>
        /// <returns></returns>
        public PropertyInfo GetPropertyInfo()
        {
            return MemberExpresion?.Member as PropertyInfo;
        }

        /// <summary>
        /// Returns a attribute via linq
        /// </summary>
        /// <typeparam name="TAttributeType"></typeparam>
        /// <returns></returns>
        public TAttributeType GetAttribute<TAttributeType>() where TAttributeType : Attribute
        {
            return MemberExpresion.Member.GetCustomAttribute<TAttributeType>();
        }

        /// <summary>
        /// Processes the PropLambda
        /// </summary>
        /// <param name="memberExpression"></param>
        /// <returns></returns>
        private static List<MemberInfo> ProcessPropLambdaMemberExpression(MemberExpression memberExpression)
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var memberExpressionPropertyInfos = new LinkedList<MemberInfo>();

            // Initialize the root
            memberExpressionPropertyInfos.AddFirst(memberExpression.Member);

            // get nested expression
            var parentExp = memberExpression.Expression;

            // while nested expression is member expression
            while (parentExp is MemberExpression parentMemberExpression)
            {
                // add string property name to the list
                memberExpressionPropertyInfos.AddFirst(parentMemberExpression.Member);

                // reset the parentExp to go one more level deep 
                parentExp = parentMemberExpression.Expression;
            }

            // Return the list
            return memberExpressionPropertyInfos.ToList();
        }
    }
}