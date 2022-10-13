using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using InfoViaLinq;
using Xunit;

namespace Core.Tests
{
    public interface IModel
    {
        [Display(Name = "Parent.Name")]
        string Name { get; set; }

        [Display(Name = "Parent.Ref")]
        IModel Ref { get; set; }
    }

    public class Model : IModel
    {
        [Display(Name = "Child.Name")]
        public string Name { get; set; }

        [Display(Name = "Child.Ref")]
        public IModel Ref { get; set; }
    }

    public static class Utility
    {
        public static IEnumerable<KeyValuePair<PropertyInfo, PropertyInfo>> GetBasic<T>() where T : IModel
        {
            return new InfoViaLinq<T>().PropLambda(x => x.Name).MappedMembers();
        }

        public static IEnumerable<KeyValuePair<PropertyInfo, PropertyInfo>> GetComplex<T>() where T : IModel
        {
            return new InfoViaLinq<T>().PropLambda(x => x.Ref.Ref.Name).MappedMembers();
        }
    }

    public class GetMappedMembersTest
    {
        [Fact]
        public void Test__Basic()
        {
            // Arrange
            var (item1, item2) = ("Parent.Name", "Child.Name");

            // Act
            var (key, value) = Utility.GetBasic<Model>().First();

            var (val1, val2) = (
                key.GetCustomAttribute<DisplayAttribute>()?.Name,
                value.GetCustomAttribute<DisplayAttribute>()?.Name
            );

            // Assert
            Assert.Equal(item1, val1);
            Assert.Equal(item2, val2);
        }

        [Fact]
        public void Test__Complex()
        {
            // Arrange, Act, Assert
            Utility.GetComplex<Model>().ToList().ForEach(x =>
            {
                var (key, value) = x;
                
                var (parentAttr, childAttr) = (
                    key.GetCustomAttribute<DisplayAttribute>(),
                    value.GetCustomAttribute<DisplayAttribute>()
                );
                
                Assert.DoesNotContain("Child", parentAttr.Name);
                Assert.DoesNotContain("Parent", childAttr.Name);
            });
        }
    }
}