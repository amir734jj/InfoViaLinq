﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using InfoViaLinq.Interfaces;

namespace InfoViaLinq.Logic
{
    internal class GetPropInfo<TSource> : IGetPropInfo<TSource>
    {
        private readonly List<MemberInfo> _memberInfos;
        
        public MemberExpression MemberExpression { get; }

        /// <summary>
        /// Constructor that takes the member expression
        /// </summary>
        /// <param name="memberExpression"></param>
        public GetPropInfo(MemberExpression memberExpression)
        {
            MemberExpression = memberExpression;

            _memberInfos = ResolveMembers(memberExpression);
        }

        /// <summary>
        /// Returns all property infos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> Members()
        {
            return _memberInfos.Cast<PropertyInfo>();
        }

        /// <inheritdoc />
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
        private static List<MemberInfo> ResolveMembers(Expression memberExpression)
        {
            var memberExpressionVisitor = new MemberExpressionVisitor(memberExpression);
            
            // Begin node visits
            memberExpressionVisitor.Accept(out var members);

            // Return the list
            return members;
        }
    }
}