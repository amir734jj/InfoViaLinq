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
        
        private const string Deliminter = ".";

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
        /// Simple Expression visitor to visit MemberExpression types
        /// </summary>
        private sealed class MemberExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _expression;
            
            private List<MemberInfo> _members;

            public MemberExpressionVisitor(Expression expression)
            {
                _expression = expression;
            }
            
            protected override Expression VisitMember(MemberExpression node)
            {
                // Add PropertyInfo to the list
                _members = new[] {node.Member}.Concat(_members).ToList();
                
                return base.VisitMember(node);
            }

            public void Accept(out List<MemberInfo> members)
            {
                _members = new List<MemberInfo>();
                
                Visit(_expression);

                members = _members;
            }
        }

        /// <summary>
        /// Processes the PropLambda
        /// </summary>
        /// <param name="memberExpression"></param>
        /// <returns></returns>
        private static List<MemberInfo> ProcessPropLambdaMemberExpression(Expression memberExpression)
        {
            var memberExpressionVisitor = new MemberExpressionVisitor(memberExpression);
            
            // Begin node visits
            memberExpressionVisitor.Accept(out var members);

            // Return the list
            return members;
        }
    }
}