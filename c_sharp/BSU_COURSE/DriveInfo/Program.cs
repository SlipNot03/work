using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DriveInfoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with DriveInfo *****\n");

            // Получаем информацию по дискам
            DriveInfo[] myDrives = DriveInfo.GetDrives();

            // Печатаем информацю по дискам
            foreach (DriveInfo d in myDrives)
            {

                Console.WriteLine("Name: {0}", d.Name);
                Console.WriteLine("Type: {0}", d.DriveType);

                // Подключен ли диск 
                if (d.IsReady)
                {
                    //Свободное пространство
                    Console.WriteLine("Free space: {0}", d.TotalFreeSpace);
                    //Тип файловой системы
                    Console.WriteLine("Format: {0}", d.DriveFormat);
                    //Метка
                    Console.WriteLine("Label: {0}", d.VolumeLabel);
                    Console.WriteLine();
                }
            }
            Console.ReadLine();
        }
    }
}