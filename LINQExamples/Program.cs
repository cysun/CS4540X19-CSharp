using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQExamples
{
    class Program
    {
        static void Print<T>(string label, IEnumerable<T> results)
        {
            Console.WriteLine(label);
            foreach (var result in results)
                Console.WriteLine("\t{0}", result);
        }

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
            Print("Q1 (Query)", r1.Distinct());

            var r2 = c.Employees.Where(e => e.LastName == "Doe")
                .OrderBy(e => e.FirstName)
                .Select(e => (e.FirstName, e.LastName))
                .Distinct();
            Print("Q1 (Method)", r2);

            // 2. Find the leader of the project Blue
            var r3 = from p in c.Projects where p.Name == "Blue" select p.Leader;
            Print("Q2 (Query)", r3);

            var r4 = c.Projects.Where(p => p.Name == "Blue").Select(p => p.Leader);
            Print("Q2 (Method)", r3);

            // 3. Find Jane Doe's supervisor
            var r5 = from e1 in c.Employees
                     join e2 in c.Employees on e1.Id equals e2.SupervisorId
                     where e2.FirstName == "Jane" && e2.LastName == "Doe"
                     select e1;
            Print("Q3 (Query)", r5);


            // 4. Find the employees who are on both project Firestone and project Blue
            var r7 = (from p1 in c.Projects where p1.Name == "Firestone" select p1.Members).Single().Intersect
                ((from p1 in c.Projects where p1.Name == "Blue" select p1.Members).Single());
            Print("Q4 (Query)", r7);

            // 5. Find the number of employees hired in 2015
            var r8 = (from e in c.Employees where e.DateHired.Year == 2015 select e).Count();
            Console.WriteLine("Q5 (Query)");
            Console.WriteLine("\t{0}", r8);

            var r9 = c.Employees.Where(e => e.DateHired.Year == 2015).Count();
            Console.WriteLine("Q5 (Method)");
            Console.WriteLine("\t{0}", r9);

            // 6. 
        }
    }
}
