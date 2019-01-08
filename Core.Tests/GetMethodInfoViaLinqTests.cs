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
            var name = InfoViaLinq<string>.New().FuncLambda<string>(x => x.ToString).GetMethodName();
            
            // Assert
            Assert.Equal(expected, name);
        }

        [Fact]
        public void Test__GetMethodInfoViaLinq_MultiParameters()
        {
            // Arrange
            const string expected = "DoSomething";
            
            // Act
            var name = InfoViaLinq<Person>.New().FuncLambda<int, double, string, string>(x => x.DoSomething).GetMethodName();
            
            // Assert
            Assert.Equal(expected, name);
        }
    }
}