using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Tests.Models
{
    public class NestedPersonInfo
    {
        [Display(Name = "Attribute.MotherName")]
        public string MotherName { get; set; }

        public string FatherName { get; set; }
        
        public bool Status { get; set; }
        
        public Person GreatParents { get; set; }
    }

    public class Person
    {
        [Display(Name = "Attribute.FirstName")]
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public int Age { get; set; }
        
        public double Height { get; set; }
        
        public float Worth { get; set; }
        
        public long Weight { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        
        public char Initial { get; set; }
        
        public NestedPersonInfo Parents { get; set; }

        public string DoSomething(int prop1, double prop2, string prop3)
        {
            throw new NotImplementedException();
        }
    }
}