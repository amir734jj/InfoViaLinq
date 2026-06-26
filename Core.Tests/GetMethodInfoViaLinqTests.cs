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

        [Fact]
        public void Test__Action_NoParameter()
        {
            // Arrange
            const string expected = "DoNothing";

            // Act
            var name = new InfoViaLinq<Person>().FuncLambda(x => x.DoNothing).GetMethodInfo().Name;

            // Assert
            Assert.Equal(expected, name);
        }

        [Fact]
        public void Test__Action_OneParameter()
        {
            // Arrange
            const string expected = "DoNothing";

            // Act
            var name = new InfoViaLinq<Person>().FuncLambda<int>(x => x.DoNothing).GetMethodInfo().Name;

            // Assert
            Assert.Equal(expected, name);
        }

        [Fact]
        public void Test__Action_TwoParameters()
        {
            // Arrange
            const string expected = "DoNothing";

            // Act
            var name = new InfoViaLinq<Person>().FuncLambda<int, double>(x => x.DoNothing).GetMethodInfo().Name;

            // Assert
            Assert.Equal(expected, name);
        }

        [Fact]
        public void Test__Action_ThreeParameters()
        {
            // Arrange
            const string expected = "DoNothing";

            // Act
            var name = new InfoViaLinq<Person>().FuncLambda<int, double, string>(x => x.DoNothing).GetMethodInfo().Name;

            // Assert
            Assert.Equal(expected, name);
        }

        [Fact]
        public void Test__Func_OneParameter()
        {
            // Arrange
            const string expected = "Square";

            // Act
            var name = new InfoViaLinq<Person>().FuncLambda<int, int>(x => x.Square).GetMethodInfo().Name;

            // Assert
            Assert.Equal(expected, name);
        }

        [Fact]
        public void Test__Func_TwoParameters()
        {
            // Arrange
            const string expected = "Combine";

            // Act
            var name = new InfoViaLinq<Person>().FuncLambda<int, double, string>(x => x.Combine).GetMethodInfo().Name;

            // Assert
            Assert.Equal(expected, name);
        }
    }
}