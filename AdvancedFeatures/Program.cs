using System;
using System.Collections.Generic;

namespace AdvancedFeatures
{
    public static class MyExtensions
    {
        public static string Substring(this string s, string begin, string end)
        {
            var beginIndex = s.ToUpper().IndexOf(begin.ToUpper());
            var endIndex = s.ToUpper().LastIndexOf(end.ToUpper());
            return beginIndex >= 0 && endIndex >= 0 ? s.Substring(beginIndex, endIndex - beginIndex + 1) : "";
        }
    }

    class Person
    {
        public string FirstName;
        public string LastName;

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }

    class Program
    {
        static Action CreateMethod()
        {
            int value = 10;
            return () => Console.WriteLine(value++);
        }

        static void Main(string[] args)
        {
            // Extension method
            Console.WriteLine("Amazon".Substring("a", "b"));

            // Closure
            var a = CreateMethod();
            var b = CreateMethod();
            a();
            a();
            b();
            b();

            // List.FindAll()
            var people = new List<Person>
            {
                new Person{ FirstName = "John", LastName = "Doe"},
                new Person{ FirstName = "Jane", LastName = "Doe"},
                new Person{ FirstName = "Tom", LastName = "Smith"},
            };

            var results = people.FindAll(person => person.LastName == "Doe");
            foreach (var person in results)
                Console.WriteLine(person);
        }
    }
}
