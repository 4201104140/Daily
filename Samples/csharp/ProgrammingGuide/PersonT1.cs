using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.ProgrammingGuide
{
    public class PersonT1
    {
        // Constructor that takes no arguments:
        public PersonT1()
        {
            Name = "unknown";
        }

        // Constructor that takes one argument:
        public PersonT1(string name)
        {
            Name = name;
        }

        // Auto-implemented readonly property:
        public string Name { get; }

        // Method that overrides the base class (System.Object) implementation.
        public override string ToString()
        {
            return Name;
        }
    }
    public class TestPersonT1
    {
        public static void Example()
        {
            // Call the constructor that has no parameters.
            var person1 = new PersonT1();
            Console.WriteLine(person1.Name);

            // Call the constructor that has one parameter.
            var person2 = new PersonT1("Phn Tn Ti");
            Console.WriteLine(person2.Name);
            // Get the string representation of person2 instance.
            Console.WriteLine(person2);
        }
    }

}
