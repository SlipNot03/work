using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace FirstAsync
{
    class Program
    {


        public async void GetWebPageAsync(string url)
        {
            WebClient wc = new WebClient();
            string page = await wc.DownloadStringTaskAsync(url);
            Console.WriteLine(page);
        }

        //метод для получения размера страницы
        public async Task<int> GetPageSizeAsync(string url)
        {
            WebClient wc = new WebClient();
            string page = await wc.DownloadStringTaskAsync(url);
            return page.Length;
        }

        //для того чтобы воспльзоваться асинхронным методом следует написать еще аснхронный метод
        //async является заразным 
        public async Task<string> FindLargestPage(string[] urls)
        {
            string largest = null;
            int largestSize = 0;
            foreach (var url in urls)
            {
                int size = await GetPageSizeAsync(url);
                if (size > largestSize)
                {
                    size = largestSize;
                    largest = url;
                }
            }
            Console.WriteLine("largest: " + largest);
            return largest;
        }

        static void Main(string[] args)
        {

            var p = new Program();
            //p.GetWebPageAsync("http://yandex.ru");

            p.FindLargestPage(new[] { "http://yandex.ru", "http://google.ru" });
            while (true)
            {

            }

        }
    }
}