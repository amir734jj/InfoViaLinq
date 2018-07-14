using System;
using System.Collections.Generic;
using AutoFixture;
using InfoViaLinq.Interfaces;
using InfoViaLinq.Tests.Models;
using Xunit;

namespace InfoViaLinq.Tests
{
    public static class GetFuncInfoExtension
    {
        public static IEnumerable<Type> GetGenericArgs<T>(this IGetFuncInfo<T> getFuncInfo) => getFuncInfo.GetMethodInfo().GetGenericArguments();
    }
    
    public class GetFuncInfoExtensionTest
    {
        private readonly InfoViaLinq<Person> _utility;
        
        private readonly Fixture _fixture;

        public GetFuncInfoExtensionTest()
        {
            _utility = InfoViaLinq<Person>.New();
            _fixture = new Fixture();
        }

        [Fact]
        public void Test__GetGenericArgs()
        {
            // Arrange
            var args = InfoViaLinq<Person>.New().FuncLambda<int, double, string, string>(x => x.DoSomething).GetGenericArgs();    
            
            // Act, Assert
            Assert.Empty(args);
        }
    }
}