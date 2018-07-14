using System.Linq;
using InfoViaLinq.Interfaces;
using InfoViaLinq.Tests.Models;
using InfoViaLinq.Tests.Utilities;
using Xunit;

namespace InfoViaLinq.Tests
{
    public class GetPropertyInfoViaLinqTests
    {
        private readonly IGetPropertyInfoViaLinq<Person> _utility;

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
            var result = _utility.PropLambda(lambda).GetPropertyInfo();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test__Complex()
        {
            // Arrange
            var lambda = PersonUtility.LambdaToExp(x => x.Parents.MotherName);
            var expected = typeof(NestedPersonInfo).GetProperties().First(x => x.Name == "MotherName");

            // Act
            var result = _utility.PropLambda(lambda).GetPropertyInfo();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}