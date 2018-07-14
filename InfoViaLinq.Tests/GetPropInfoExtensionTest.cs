using AutoFixture;
using InfoViaLinq.Interfaces;
using InfoViaLinq.Tests.Models;
using Xunit;

namespace InfoViaLinq.Tests
{
    public static class GetPropInfoExtension
    {
        public static object GetValue<T>(this IGetPropInfo<T> getPropInfo, T instance) => getPropInfo.GetPropertyInfo().GetValue(instance);
    }
    
    public class GetPropInfoExtensionTest
    {
        private readonly InfoViaLinq<Person> _utility;
        
        private readonly Fixture _fixture;

        public GetPropInfoExtensionTest()
        {
            _utility = InfoViaLinq<Person>.New();
            _fixture = new Fixture();
        }

        [Fact]
        public void Test__GetValue()
        {
            // Arrange
            var person = _fixture.Build<Person>().Without(x => x.Parents).Create();    
            
            // Act, Assert
            Assert.Equal(person.Age, _utility.PropLambda(x => x.Age).GetValue(person));
        }
    }
}