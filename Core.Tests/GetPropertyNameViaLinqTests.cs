using System.Linq;
using Core.Tests.Models;
using Core.Tests.Utilities;
using InfoViaLinq;
using InfoViaLinq.Interfaces;
using Xunit;

namespace Core.Tests
{
    public class GetPropertyNameViaLinqTests
    {
        private readonly IInfoViaLinq<Person> _utility;

        public GetPropertyNameViaLinqTests()
        {
            _utility = new InfoViaLinq<Person>();
        }

        [Fact]
        public void Test__Basic()
        {
            // Arrange
            var lambda = PersonUtility.LambdaToExp(x => x.Age);
            const string expected = "Age";

            // Act
            var result = _utility.PropLambda(lambda).Members().First().Name;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test__Complex()
        {
            // Arrange
            var lambda = PersonUtility.LambdaToExp(x => x.Parents);
            const string expected = "Parents";

            // Act
            var result = _utility.PropLambda(lambda).Members().First().Name;

            // Assert
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Test__Nested()
        {
            // Arrange
            var lambda = PersonUtility.LambdaToExp(x => x.Parents.FatherName);
            const string expected = "Parents.FatherName";

            // Act
            var result = string.Join('.', _utility.PropLambda(lambda).Members().Select(x => x.Name));

            // Assert
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Test__ComplexNested()
        {
            // Arrange
            var lambda = PersonUtility.LambdaToExp(x => x.Parents.GreatParents.Parents.GreatParents.Parents.MotherName);
            const string expected = "Parents.GreatParents.Parents.GreatParents.Parents.MotherName";

            // Act
            var result = string.Join('.', _utility.PropLambda(lambda).Members().Select(x => x.Name));

            // Assert
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Test__ComplexNestedLeaf()
        {
            // Arrange
            var lambda = PersonUtility.LambdaToExp(x => x.Parents.GreatParents.Parents.GreatParents.Parents.GreatParents.Age);
            const string expected = "Parents.GreatParents.Parents.GreatParents.Parents.GreatParents.Age";

            // Act
            var result = string.Join('.', _utility.PropLambda(lambda).Members().Select(x => x.Name));

            // Assert
            Assert.Equal(expected, result);
        }
    }
}