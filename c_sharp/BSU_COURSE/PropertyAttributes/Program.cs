using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Attribites
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class StringMinMaxLengthAttribute : Attribute
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        //конструктор с позиционным параметром
        //остальные параметры - именованные
        public StringMinMaxLengthAttribute(int min)
        {
            MinLength = min;
            MaxLength = int.MaxValue;
        }

    }

    public class Person
    {
        [StringMinMaxLength(3, MaxLength = 20)]
        public string Name { get; set; }
    }


    class Program
    {


        static void Main(string[] args)
        {

            Person person = new Person { Name = "John" };
            var type = person.GetType();


            var attributes = type.GetProperties().Where(p => p.Name == "Name").FirstOrDefault().GetCustomAttributes(false);

            foreach (var attr in attributes)
            {
                if (attr is StringMinMaxLengthAttribute)
                {
                    var minmaxatrr = attr as StringMinMaxLengthAttribute;
                    //var minmaxatrr = (StringMinMaxLengthAttribute)attr;
                    Console.WriteLine(string.Format("min={0} max={1} length={2}", minmaxatrr.MinLength, minmaxatrr.MaxLength, person.Name.Length));
                }
            }

            Console.ReadKey();
        }
    }
}

