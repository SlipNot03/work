using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace LateBinding
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Type type = typeof(StringBuilder);
            object obj = Activator.CreateInstance(type);
            var sb = (StringBuilder)obj;
            sb.Append("test");
            Console.WriteLine(sb);

            //поиск и вызов метода
            MethodInfo mi = type.GetMethod("Append", new[] { typeof(string) });
            mi.Invoke(obj, new[] { "test2" });
            Console.WriteLine(obj);


            Console.ReadKey();*/

            var assembly = Assembly.LoadFrom("CarLib.dll");
            var types = assembly.GetTypes();
            foreach (var t in types)
            {
                Console.WriteLine(t);
            }
            Type type = assembly.GetType("CarLib.Car");
            var obj = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod("Accelerate");

            methodInfo.Invoke(obj, new object[] { 20 });

        }
    }
}