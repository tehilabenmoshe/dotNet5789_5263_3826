using System;
namespace targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome3826();
            Welcome5263();
            Console.ReadKey();
        }
        static partial void Welcome5263();  
        private static void Welcome3826()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0},welcome to my first console application", name);
        }
    }
}
