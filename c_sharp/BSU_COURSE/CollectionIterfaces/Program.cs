using System;
using System.Collections;
using System.Collections.Generic;

namespace CollectionIterfaces
{
    class Week : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return days.GetEnumerator();
        }
        string[] days = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
    }

    class Numbers
    {
        public IEnumerator<int> GetEnumerator()
        {
            for (int i=0; i< 6; i++)
            {
                yield return i * i;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var w = new Week();
            foreach (var item in w)
            {
                Console.WriteLine(item);
            }
            /*            var n = new Numbers();
                        foreach(var i in n)
                        {
                            Console.WriteLine(i);
                        }*/
        }
    }
}
