using System;
using System.Linq.Expressions;
using System.Reflection;
using InfoViaLinq.Extensions;
using InfoViaLinq.Interfaces;
using InfoViaLinq.Utilities;

namespace InfoViaLinq.Logic
{
    public class GetPropInfo<TSource> : IGetPropInfo<TSource>
    {
        private const string Deliminter = ".";

        public MemberExpression MemberExpresion { get; }

        /// <summary>
        /// Constructor that takes the member expression
        /// </summary>
        /// <param name="memberExpression"></param>
        public GetPropInfo(MemberExpression memberExpression)
        {
            MemberExpresion = memberExpression;
        }
        
        /// <summary>
        /// Returns property name using lambda
        /// </summary>
        /// <returns></returns>
        public string GetPropertyName()
        {
            // create a list of property names
            var nameTokens = new LinkedListWithInit<string>() { MemberExpresion.GetMemberExpressionName() };
            
            // get nested expression
            var parentExp = MemberExpresion.Expression;

            // while nested expression is member expression
            while (parentExp is MemberExpression parentMemberExpression)
            {
                // add string property name to the list
                nameTokens.AddFirst(parentMemberExpression.GetMemberExpressionName());

                // reset the parentExp to go one more level deep 
                parentExp = parentMemberExpression.Expression;
            }

            // join the tokens together
            return string.Join(Deliminter, nameTokens);
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
    }
}