using System;
using System.Collections.Generic;

namespace AnonymousType
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var friends = new[]
            {
                new { Name = "Tom", Age = 20 },
                new { Name = "Ed", Age = 21 },
                new { Name = "Jake", Age = 19 }
            };
            
            var totalAge = 0;
            foreach (var f in friends)
            {
                totalAge += f.Age;
            }

            var friendList = new List<Person>
            {
                new Person { Name = "Tom", Age = 20 },
                new Person { Name = "Ed", Age = 17 },
                new Person { Name = "Jake", Age = 19 }
            };


            
            var converted = friendList.ConvertAll(delegate(Person person) {
                return new { person.Name, isAdult = (person.Age >= 18) };
            });

            foreach(var c in converted)
            {
                Console.WriteLine($"{c.Name} {c.isAdult}");
            }

        }



    }
}
