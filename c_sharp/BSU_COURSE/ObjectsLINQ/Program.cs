using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectsLINQ
{
    class ProductInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }

        public int CategoryId { get; set; }
        public override string ToString()
        {
            return string.Format("Name={0} Description={1} Number={2} CategoryId={3}", Name, Description, Number, CategoryId);
        }
    }

    class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var categories = new Category[]
            {
                new Category{ Id = 1, Name = "Cat1"},
                new Category{ Id = 2, Name = "Cat2"},
                new Category{ Id = 3, Name = "Cat3"},
                new Category{ Id = 4, Name = "Cat4"},
            };

            var products = new List<ProductInfo>()
            {
                new ProductInfo { Name="P1", Description="Product1", Number = 1, CategoryId = 1},
                new ProductInfo { Name="P2", Description="Product2", Number = 2, CategoryId = 1},
                new ProductInfo { Name="P3", Description="Product3", Number = 3, CategoryId = 1},
                new ProductInfo { Name="P4", Description="Product4", Number = 4, CategoryId = 2},
                new ProductInfo { Name="P5", Description="Product5", Number = 5, CategoryId = 1},
                new ProductInfo { Name="P6", Description="Product6", Number = 6, CategoryId = 3},
                new ProductInfo { Name="P7", Description="Product7", Number = 7, CategoryId = 1},
                new ProductInfo { Name="P8", Description="Product8", Number = 8, CategoryId = 2},
                new ProductInfo { Name="P9", Description="Product9", Number = 9, CategoryId = 3},
                new ProductInfo { Name="P10", Description="Product10", Number = 10, CategoryId = 1}
            };

            //var r1 = from p in products select p;
            var r1 = products.Select(p=>p);

/*            foreach (var r in r1)
            {
                Console.WriteLine($"{r} {r.GetType()}");
            }*/

            var r2 = from p in products
                     let length = p.Description.Length
                     orderby length descending
                     select new { Name = p.Name, Length = length };

            /*            foreach (var r in r2)
                        {
                            Console.WriteLine($"{r.Name} {r.Length} {r.GetType()}");
                        }*/

            var r3 = from c in categories
                     join p in products on c.Id equals p.CategoryId
                     select new { c.Name, Product = p };
            /*            foreach (var r in r3)
                        {
                            Console.WriteLine(r);
                        }*/

            var r4 = from c in categories
                     join p in products on c.Id equals p.CategoryId into ProductGroup
                     select new { c.Name, Products = ProductGroup };
            /*            foreach (var r in r4)
                        {
                            Console.WriteLine(r.Name);
                            foreach (var p in r.Products)
                            {
                                Console.WriteLine(p);
                            }
                        }*/

            var r5 = from c in categories
                     join p in products on c.Id equals p.CategoryId into ProductGroup
                     from pp in ProductGroup
                     where pp.CategoryId == 1
                     select new { c.Name, Products = ProductGroup };
            /*            foreach (var r in r5)
                        {
                            Console.WriteLine(r.Name);
                            foreach (var p in r.Products)
                            {
                                Console.WriteLine(p);
                            }
                        }*/

            var r6 = from c in categories
                     join p in products on c.Id equals p.CategoryId into pGroup
                     from item in pGroup.DefaultIfEmpty(new ProductInfo { CategoryId = 0 })
                     where !pGroup.Any()
                     select new { CatName = c.Name, ProductName = item.Name, item.CategoryId };

            foreach (var r in r6)
            {
                Console.WriteLine(r);
            }


        }
    }
}
