using Core.Tests.Models;
using InfoViaLinq;
using Xunit;

namespace Core.Tests
{
    public class GetMethdoInfoViaLinqTests
    {
        [Fact]
        public void Test__GetMethodInfoViaLinq()
        {
            // Arrange
            const string expected = "ToString";
            
            // Act
            var name = new InfoViaLinq<string>().FuncLambda<string>(x => x.ToString).GetMethodInfo().Name;
            
            // Assert
            Assert.Equal(expected, name);
        }

        [Fact]
        public void Test__GetMethodInfoViaLinq_MultiParameters()
        {
            // Arrange
            const string expected = "DoSomething";
            
            // Act
            var name = new InfoViaLinq<Person>().FuncLambda<int, double, string, string>(x => x.DoSomething).GetMethodInfo().Name;
            
            // Assert
            Assert.Equal(expected, name);
        }
    }
}