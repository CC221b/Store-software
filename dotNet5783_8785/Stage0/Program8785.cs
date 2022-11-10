//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

//מכיוון שאנו מספר אי זוגי של בנות ולא ניתנ הלי רשות לעשות שלישה, אין לי איך לעשות את רעיון המיזוג שבשלב 0.
namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome8785();
            Console.ReadKey();
        }

        private static void welcome8785()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0},welcome my first console application", name);
        }
    }
}