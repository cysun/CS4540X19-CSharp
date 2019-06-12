using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] names = { "John", "Jane", "Tom", "Bob" };

            // List the names that start with "J" in alphabetic order

            var results1 = from name in names where name.StartsWith("J") orderby name select name;

            foreach (var result in results1)
                Console.WriteLine("{0}", result);

            names[2] = "Jack";

            foreach (var result in results1)
                Console.WriteLine("{0}", result);

            var results2 = names.Where(n => n.StartsWith("J")).OrderBy(n => n).Select(n => n);
            foreach (var result in results2)
                Console.WriteLine("{0}", result);

            Company c = new Company();

            // 1. Find the employees whose last names are Doe

            var r1 = from e in c.Employees
                     where e.LastName == "Doe"
                     orderby e.FirstName ascending
                     select (e.FirstName, e.LastName);
            foreach (var result in r1.Distinct())
                Console.WriteLine(result.FirstName + " " + result.LastName);

            var r2 = c.Employees.Where(e => e.LastName == "Doe")
                .OrderBy(e => e.FirstName)
                .Select(e => (e.FirstName, e.LastName))
                .Distinct();
            foreach (var result in r1)
                Console.WriteLine(result.FirstName + " " + result.LastName);

            // 2. Find the names of the leader of the project Blue

            var r3 = from p in c.Projects
                     where p.Name == "Blue"
                     select (p.Leader.FirstName, p.Leader.LastName);
            foreach (var result in r3)
                Console.WriteLine(result.FirstName + " " + result.LastName);

            var r4 = c.Projects.Where(p => p.Name == "Blue")
                .Select(p => (p.Leader.FirstName, p.Leader.LastName));
            foreach (var result in r4)
                Console.WriteLine(result.FirstName + " " + result.LastName);
        }
    }
}
