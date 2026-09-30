using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LinqToXML
{
    class Car
    {
        public int ID { get; set; }
        public string PetName { get; set; }
        public string Color { get; set; }
        public string Make { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            XElement doc = new XElement(
                "Inventory",
                    new XElement("Car", new XAttribute("ID", 1000),
                        new XElement("PetName", "Jimbo"),
                        new XElement("Color", "Red"),
                        new XElement("Make", "Ford")
                        )
                );
            doc.Save("inventory.xml");

            //Descendants - потомки узла
            //Elements - непосредственные потомки 
            var dest = from el in doc.Descendants()
                       select el;

            var elements = from el in doc.Elements("Car").Elements("Make")
                           select el;

            //Console.WriteLine(dest);

            //Attribute - атрибут
            var idList = from el in doc.Elements("Car")
                         select el.Attribute("ID");
          /*  foreach (var el in idList )
            {
                Console.WriteLine(el);
            }*/

            //добавление элементов дерева
            doc.Add(new XElement("Car", new XAttribute("ID", 1001),
                        new XElement("PetName", "Yaris"),
                        new XElement("Color", "Black"),
                        new XElement("Make", "Toyota")
                        )
             );
            //Console.WriteLine(doc);

            //Удаление элементов из дерева
            doc.Descendants("PetName").Remove();
            //Console.WriteLine(doc);

            //Создание нового документа
            XDocument doc2 = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("Comment"),
                    new XElement(
                        "Inventory",
                            new XElement("Car",
                                new XAttribute("ID", 1),
                                new XElement("PetName", "Jimbo"),
                                new XElement("Color", "Red"),
                                new XElement("Make", "Ford")
                                ),
                            new XElement("Car", 
                                new XAttribute("ID", 2),
                                new XElement("PetName", "Melvin"),
                                new XElement("Color", "Pink"),
                                new XElement("Make", "Yugo")
                                )
                    )
                );



            Console.WriteLine(doc2);
            doc2.Save("doc2.xml");

            //Инициализация классов из файла 
            InitCar("doc2.xml");


            //XDocument doc3 = XDocument.Load("doc2");

            Console.ReadKey();
        }


        //создание дерева из классов
        static void CreateXmlDoc()
        {
            var data = new[] {
                new {Firstname="Mandy", Age=21},
                new {Firstname="Andy", Age=32},
                new {Firstname="Dave", Age=25},
                new {Firstname="Sara", Age=27},
            };

            var query = from c in data
                        select
                        new XElement("Person",
                            new XAttribute("Age", c.Age),
                            new XElement("Firstname", c.Firstname)
                            );
            var doc = new XElement("People", query);
            Console.WriteLine(doc);
        }

        //Функция инициализации коллекции из файла XML
        static List<Car> InitCar(string pathToDoc)
        {
            XDocument doc = XDocument.Load(pathToDoc);
            var cars = doc.Descendants("Car");
            var result = new List<Car>();
            foreach (var item in cars)
            {
                var car = new Car();
                //Convert - статический класс для преобразования значений к определенному классу
                //Convert.ToInt32 - приведение значения к типу int
                car.ID = Convert.ToInt32(item.Attribute("ID").Value);
                car.PetName = item.Element("PetName").Value;
                car.Color = item.Element("Color").Value;
                car.Make = item.Element("Make").Value;
                result.Add(car);
            }

            return result;

        }
    }
}