using System;
using System.Collections.Generic;
using System.Linq;

namespace SetsLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var l1 = new List<int> { 1, 2, 3, 4 };
            var l2 = new List<int> { 1, 2, 3 };

            var r1 = (from l in l1 select l).Except(from l in l2 select l);
            
            /*foreach (var r in r1)
            {
                Console.WriteLine(r);
            }*/
            var r2 = (from l in l1 select l).Intersect(from l in l2 select l);
/*            foreach (var r in r2)
            {
                Console.WriteLine(r);
            }*/

            var r3 = (from l in l1 select l).Union(from l in l2 select l);
            /*            foreach (var r in r3)
                        {
                            Console.WriteLine(r);
                        }*/
            var r4 = (from l in l1 select l).Concat(from l in l2 select l);
            /*            foreach (var r in r4)
                        {
                            Console.WriteLine(r);
                        }*/

            var r5 = (from l in l1 select l).Concat(from l in l2 select l).Distinct();
            /*            foreach (var r in r5)
                        {
                            Console.WriteLine(r);
                        }*/

            //var r6 = (from l in l1 select l).Concat(from l in l2 select l).Average();
            var r6 = (from l in l1 select l).Concat(from l in l2 select l).Max();
            Console.WriteLine(r6);



        }
    }
}
