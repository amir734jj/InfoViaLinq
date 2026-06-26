using System;
using System.Linq;
using Core.Tests.Models;
using Core.Tests.Utilities;
using InfoViaLinq;
using Xunit;

namespace Core.Tests
{
    public class InfoViaLinqTests
    {
        private readonly InfoViaLinq<Person> _utility;

        public InfoViaLinqTests()
        {
            _utility = new InfoViaLinq<Person>();
        }

        [Fact]
        public void Test__PropLambda_Generic()
        {
            // Arrange
            const string expected = "Age";

            // Act
            var result = _utility.PropLambda<int>(x => x.Age).Members().First().Name;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test__PropLambda_ExposesMemberExpression()
        {
            // Act
            var memberExpression = _utility.PropLambda(x => x.Age).MemberExpression;

            // Assert
            Assert.NotNull(memberExpression);
            Assert.Equal("Age", memberExpression.Member.Name);
        }

        [Fact]
        public void Test__FuncLambda_ExposesLambdaExpression()
        {
            // Act
            var lambdaExpression = _utility.FuncLambda(x => x.DoNothing).LambdaExpression;

            // Assert
            Assert.NotNull(lambdaExpression);
        }

        [Fact]
        public void Test__FieldAccess_ResolvesViaFieldBranch()
        {
            // Arrange
            var lambda = PersonUtility.LambdaToExp(x => x.PublicField);

            // Act
            var members = _utility.PropLambda(lambda).Members().ToList();

            // Assert: a field has no matching property, so the field branch yields a null member
            Assert.Single(members);
            Assert.Null(members.First());
        }

        [Fact]
        public void Test__LambdaBody_Throws()
        {
            // Act, Assert
            Assert.Throws<Exception>(() => _utility.PropLambda<Func<int, int>>(x => y => y));
        }

        [Fact]
        public void Test__UnsupportedBody_Throws()
        {
            // Act, Assert
            Assert.Throws<Exception>(() => _utility.PropLambda<int>(x => x.Age + 1));
        }
    }
}
