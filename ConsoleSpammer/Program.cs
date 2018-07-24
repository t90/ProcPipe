using System;
using System.Threading;

namespace ConsoleSpammer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            while (!Console.KeyAvailable)
            {
                Console.WriteLine("stdout {0}",DateTime.Now);
                Console.Error.WriteLine("stderr {0}",DateTime.Now);
                
                Thread.Sleep(1000);
            }
        }
    }
}