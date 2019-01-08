using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Core.Tests.Models;
using Core.Tests.Utilities;
using InfoViaLinq;
using InfoViaLinq.Interfaces;
using Xunit;

namespace Core.Tests
{
    public class GetAttributeViaLinqTests
    {
        private readonly IInfoViaLinq<Person> _utility;

        public GetAttributeViaLinqTests()
        {
            _utility = InfoViaLinq<Person>.New();
        }
        
        [Fact]
        public void Test__Basic()
        {
            // Arrange
            var lambda = PersonUtility.LambdaToExp(x => x.FirstName);
            const string expected = "Attribute.FirstName";

            // Act
            var result = _utility.PropLambda(lambda).Members().Last().GetCustomAttribute<DisplayAttribute>().Name;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test__Complex()
        {
            // Arrange
            var lambda = PersonUtility.LambdaToExp(x => x.Parents.MotherName);
            const string expected = "Attribute.MotherName";

            // Act
            var result = _utility.PropLambda(lambda).Members().Last().GetCustomAttribute<DisplayAttribute>().Name;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test__ComplexNested()
        {
            // Arrange
            var lambda = PersonUtility.LambdaToExp(x => x.Parents.GreatParents.FirstName);
            const string expected = "Attribute.FirstName";

            // Act
            var result = _utility.PropLambda(lambda).Members().Last().GetCustomAttribute<DisplayAttribute>().Name;

            // Assert
            Assert.Equal(expected, result);
        }
    }
}