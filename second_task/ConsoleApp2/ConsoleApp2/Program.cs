using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] persons = new Person[4]
            {
                new Person { Name = "person 1", Age = 17 },
                new Person { Name = "person 2", Age =9 },
                new Person { Name = "person 4", Age =10 },
                new Person { Name = "person 3", Age =12 },
            };
            SchoolYears[] years = new SchoolYears[4]
            {
                new SchoolYears {Age=12, Year=6},
                new SchoolYears {Age=9, Year=3},
                new SchoolYears {Age=17, Year=11},
                new SchoolYears {Age=10, Year=4}
            };
            var result = (from p in persons
                         join y in years on p.Age equals y.Age
                         select new WholeData{ Name = p.Name, Age = p.Age, Year = y.Year }).ToArray();
            foreach (var res in result)
                Console.WriteLine(res.Name + " " + res.Age + " " + res.Year);

            Sorter sort = new Sorter();
            result = sort.Merge_Sort(result);

            foreach (var res in result)
                Console.WriteLine(res.Name + " " + res.Age + " " + res.Year);

            persons = (from res in result
                      select  new Person { Name = res.Name, Age = res.Age }).ToArray();
            years = (from res in result
                       select new SchoolYears { Year = res.Year, Age = res.Age }).ToArray();

            foreach (var person in persons)
                Console.WriteLine(person.Name + " " + person.Age);
            foreach (var year in years)
                Console.WriteLine(year.Age + " " + year.Year);

            Console.ReadLine();
        }
    }
}
