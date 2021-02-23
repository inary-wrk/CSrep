using System;


namespace Greeting
{
    class Program
    {
        static void Main(string[] args)
        {
            var conf = Properties.Settings.Default;
            Console.WriteLine(conf.greeting);
            if (conf.name != String.Empty || conf.age != 0 || conf.occupation != String.Empty)
            {
                Console.WriteLine($"Name: {conf.name}\n" +
                                  $"Age: {conf.age}\n" +
                                  $"Occupation: {conf.occupation}\n");
            }


            Console.Write("Enter\nName: ");
            conf.name = Console.ReadLine();
            Console.Write("Age: ");
            try
            {
                conf.age = Byte.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                conf.age = 0;
            }
            Console.Write("Occupation: ");
            conf.occupation = Console.ReadLine();
            conf.Save();

        }
    }
}
