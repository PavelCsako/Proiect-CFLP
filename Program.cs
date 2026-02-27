using System;

namespace JocDeCruce
{
    class Program
    {
        static void Main(string[] args)
        {
            MotorJoc joc = new MotorJoc();
            joc.Start();

            Console.WriteLine("\nJoc terminat. Apasa orice tasta pentru a inchide.");
            Console.ReadKey();
        }
    }
}