using System.Linq.Expressions;

namespace InfoViaLinq.Extensions
{
    public static class MemberExpressionExtension
    {
        /// <summary>
        /// Returns member name of member expression
        /// </summary>
        /// <param name="memberExpression"></param>
        /// <returns></returns>
        public static string GetMemberExpressionName(this MemberExpression memberExpression)
        {
            return memberExpression.Member.Name;
        }
    }
}