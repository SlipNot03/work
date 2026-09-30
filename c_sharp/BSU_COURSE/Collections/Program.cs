using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reflection.Metadata.Ecma335;

namespace Collections
{
    class Program
    {
        public static void Print(dynamic collection)
        {
            Console.WriteLine("*****************");
            foreach (var item in collection)
                Console.WriteLine(item);
        }

        static void Main(string[] args)
        {
            //List<string> people = new List<string>();
            //List<string> people = new List<string>() { "Tom", "Bob", "Sam" };
            var people = new List<string>() { "Tom", "Bob", "Sam" };
            //var employees = new List<string>(people);
            //List<string> people = new List<string>(16);
            //Console.WriteLine(people[0]);

            Console.WriteLine($"{people.Count} {people.Capacity}");
            people.Add("New Test");
            Print(people);
            Console.WriteLine(people.Find(item => item.Contains(" "))); //function(string item) { item.contains() }
            Console.WriteLine(people.IndexOf("Bob"));
            Console.WriteLine(people.Remove("Sam"));
            people.Sort();
            Print(people);

            /*var emp = new LinkedList<string>(people);
            var node = emp.First; //LinkedLisNode
            while (node != null)
            {
                Console.WriteLine($"{node.Value}  {node.GetType()}");
                node = node.Next;
            }
            emp.AddAfter(emp.First, "Test");
            Print(emp);*/

           /* var q = new Queue<string>(people);
            q.Enqueue("Test");
            Print(q);
            var item = q.Peek();
            Print(q);
            var item = q.Dequeue();
            Print(q);*/

            /*            var emp = new Stack<String>(people);
                        Print(emp);
                        emp.Push("Test");
                        Print(emp);
                        var item = emp.Pop();
                        Print(emp);*/

            /*
            Dictionary<int, string> emp = new Dictionary<int, string>()
            {
                [0] = "Tom0",
                [1] = "Tom1",
                [2] = "Tom2",
            };


                        var i = people.Count;
                        foreach (var item in people)
                        {
                            emp.Add(i, item);
                            i++;
                        }*/

            //Print(emp);
            /*            for (int i = 0; i < emp.Count; i++)
                        {
                            //KeyValuePair
                            Console.WriteLine(emp[i]);
                        }*/
            /*            foreach(var item in emp)
                        {
                            Console.WriteLine($"{item.Key} {item.Value}");
                        }*/


            ObservableCollection<string> emp = new ObservableCollection<string>(people);

            //void NotifyCollectionChangedEventHandler(object? sender, NotifyCollectionChangedEventArgs e);
            emp.CollectionChanged += (o, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        Console.WriteLine($"{e.Action} {e.NewItems[0]}");
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        Console.WriteLine($"{e.Action} {e.OldItems[0]}");
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        Console.WriteLine($"{e.Action} {e.NewItems[0]} {e.OldItems[0]}");
                        break;
                    case NotifyCollectionChangedAction.Move:
                        Console.WriteLine($"{e.Action} {e.NewStartingIndex} {e.OldStartingIndex}");
                        break;
                }
            };

            emp.Add("Test");
            emp.Remove("Test");
            emp[0] = "Test";

        }
    }
}
