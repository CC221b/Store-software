/// <summary>
///מכיוון שאנו מספר אי זוגי של בנות ואין אפשרות לעשות שלישיה,
///מרצה הקורס הורה לי לעשות לבד
///ומכיוון שרציתי לדעת איך עובד מיזוג ולתרגל עליו פתחתי 2 תוכניות ומזגתי עם עצמי
///8785-ארבעה ספרות אחרונות של הת"ז
///3252-ארבעה ספרות ראשונות של הת"ז
/// </summary>

namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome8785();
            welcome3252();
            Console.ReadKey();
        }
       
        private static void welcome8785()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0},welcome my first console application", name);
        }
        static partial void welcome3252();
    }
}