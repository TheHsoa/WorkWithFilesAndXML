using System;
using System.IO;

namespace WorkWithFileHW
{
    internal class Program
    {
        private static void Main()
        {


            Console.WriteLine("Enter number of numbers:");
            try
            {
                var n = long.Parse(Console.ReadLine());

                if (n <= 0)
                    throw new FormatException($"You enter {n}, but number of numbers can't be zero or less than zero");

                var writer = new StreamWriter("output.txt");

                Console.WriteLine($"Enter {n} numbers");

                for (var i = 0; i < n; i++)
                {
                    writer.WriteLine(long.Parse(Console.ReadLine()));
                }

                writer.Close();

                var reader = new StreamReader("output.txt");

                Console.WriteLine("Numbers than was wrote in the File: ");

                for (var i = 0; i < n; i++)
                {
                    Console.WriteLine(reader.ReadLine());
                }

                reader.Close();
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}