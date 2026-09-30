using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ExtensionMethods
{
   /* public static class StringExtensions
    {
        //this - показывает к какому типу применяется метод расширения
        public static void DisplayAssembly(this object obj)
        {
            Console.WriteLine("Type: {0} \n Assembly={1}", obj.GetType(), Assembly.GetAssembly(obj.GetType()));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int i = 1;
            i.DisplayAssembly();
            Console.ReadKey();
        }
    }*/

    public static class StringExtension
    {
        public static int CharCount(this string str, char c)
        {
            int counter = 0;
            for (int i = 0; i < str.Length; i++ )
            {
                if (str[i] == c)
                {
                    counter++;
                }
            }
            return counter;
        }

        public static void DisplayAssembly(this object obj)
        {
            Console.WriteLine("Type: {0} \n Assembly={1}", obj.GetType(), Assembly.GetAssembly(obj.GetType()));
        }
    }

    class Program
    {
        public static void Main()
        {
            string s = "Hello World";
            char c = 'o';
            var res = s.CharCount(c);
            Console.WriteLine(res);

            s.DisplayAssembly();
        }
    }

    
}