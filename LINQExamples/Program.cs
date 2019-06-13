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

        static void SearchEmployees(IEnumerable<Employee> employees)
        {
            Console.Write("Please enter first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Please enter last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Please enter year hired: ");
            string year = Console.ReadLine();

            var results = employees;
            if (!String.IsNullOrEmpty(firstName))
                results = results.Where(e => e.FirstName == firstName);
            if (!String.IsNullOrEmpty(lastName))
                results = results.Where(e => e.LastName == lastName);
            if (!String.IsNullOrEmpty(year))
                results = results.Where(e => e.DateHired.Year == Int32.Parse(year));

            Print("Employee Search (AND)", results.Select(e => e));
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

            var r6 = c.Employees.Join(c.Employees, e1 => e1.Id, e2 => e2.SupervisorId, (e1, e2) => (e1, e2))
                .Where(r => r.e2.FirstName == "Jane" && r.e2.LastName == "Doe")
                .Select(r => r.e1);
            Print("Q3 (Method)", r6);

            // 4. Find the employees who are on both project Firestone and project Blue

            var r7 = (from p in c.Projects where p.Name == "Firestone" select p.Members).Single()
                .Intersect((from p in c.Projects where p.Name == "Blue" select p.Members).Single());
            Print("Q4 (Query)", r7);

            var r8 = c.Projects.Where(p => p.Name == "Firestone").Select(p => p.Members).Single()
                .Intersect(c.Projects.Where(p => p.Name == "Blue").Select(p => p.Members).Single());
            Print("Q4 (Method)", r8);

            // 5. Find the number of employees hired in 2015

            var r9 = (from e in c.Employees where e.DateHired.Year == 2015 select e).Count();
            Console.WriteLine("Q5 (Query)");
            Console.WriteLine("\t{0}", r9);

            var r10 = c.Employees.Where(e => e.DateHired.Year == 2015).Count();
            Console.WriteLine("Q5 (Method)");
            Console.WriteLine("\t{0}", r10);

            // 6. Group the employees by the year in which they were hired.

            var r11 = from e in c.Employees group e by e.DateHired.Year;
            Console.WriteLine("Q6 (Query)");
            foreach (var group in r11)
            {
                Console.WriteLine("\t{0}", group.Key);
                foreach (var employee in group)
                    Console.WriteLine("\t\t{0}", employee);
            }

            // Employee Search
            SearchEmployees(c.Employees);
        }
    }
}
