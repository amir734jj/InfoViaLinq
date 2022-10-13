using System;
using System.Collections.Generic;
using AutoFixture;
using Core.Tests.Models;
using InfoViaLinq;
using InfoViaLinq.Interfaces;
using Xunit;

namespace Core.Tests
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
            _utility = new InfoViaLinq<Person>();
            _fixture = new Fixture();
        }

        [Fact]
        public void Test__GetGenericArgs()
        {
            // Arrange
            var args = new InfoViaLinq<Person>().FuncLambda<int, double, string, string>(x => x.DoSomething).GetGenericArgs();    
            
            // Act, Assert
            Assert.Empty(args);
        }
    }
}