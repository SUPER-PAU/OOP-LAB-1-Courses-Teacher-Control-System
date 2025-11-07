using System;

namespace LAB_1_OOP.lib.Person
{
    public abstract class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();

        public string GetFullName()
        {
            return $"{FirstName.ToLower()} {LastName.ToLower()}";
        }
        
    }
}