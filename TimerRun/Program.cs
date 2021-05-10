using System;
using System.Threading;

namespace TimerRun
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I thinking");
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Console.WriteLine("I was...");
        }
    }
}