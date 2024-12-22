using System.Linq;
using Core.Tests.Models;
using Core.Tests.Utilities;
using InfoViaLinq;
using InfoViaLinq.Interfaces;
using Xunit;

namespace Core.Tests
{
    public class GetPropertyInfoViaLinqTests
    {
        private readonly IInfoViaLinq<Person> _utility;

        public GetPropertyInfoViaLinqTests()
        {
            _utility = new InfoViaLinq<Person>();
        }
        
        [Fact]
        public void Test__Basic()
        {
            // Arrange
            var lambda = PersonUtility.LambdaToExp(x => x.FirstName);
            var expected = typeof(Person).GetProperties().First(x => x.Name == "FirstName");

            // Act
            var result = _utility.PropLambda(lambda).Members().First();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test__Complex()
        {
            // Arrange
            var lambda = PersonUtility.LambdaToExp(x => x.Parents.MotherName!);
            var expected = typeof(NestedPersonInfo).GetProperties().First(x => x.Name == "MotherName");

            // Act
            var result = _utility.PropLambda(lambda).Members().Last();

            // Assert
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Test__Members()
        {
            // Arrange
            var lambda = PersonUtility.LambdaToExp(x => x.Parents.MotherName);
            var expected = new[] { "Parents", "MotherName" };

            // Act
            var result = _utility.PropLambda(lambda).Members().ToList();

            // Assert
            result.Zip(expected, (a, b) =>
            {
                Assert.Equal(a.Name, b);

                return string.Empty;
            }).ToList();
        }

        [Fact]
        public void Test__Nullable()
        {
            // Arrange
            var lambda = PersonUtility.LambdaToExp(x => x.NullableInt);
            var expected = new[] { "NullableInt" };

            // Act
            var result = _utility.PropLambda(lambda).Members().ToList();

            // Assert
            result.Zip(expected, (a, b) =>
            {
                Assert.Equal(a.Name, b);

                return string.Empty;
            }).ToList();
        }
    }
}