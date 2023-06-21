using ClassicProblemsOfSynchronization.RaceCondition;
using System;
using System.Threading;

namespace RaceCondition
{
    class Program
    {
        static void Main(string[] args)
        {
            //Race Condition

            IdGenerator generator = new IdGenerator();
            var threadOne = new Thread(generator.IncrementValue);
            var threadTwo = new Thread(generator.IncrementValue);

            threadOne.Start();
            threadTwo.Start();
            threadOne.Join();
            threadTwo.Join();
            Console.WriteLine($"lastIdUsed: {generator.LastIdValue}");


            SomeWork a = new SomeWork();
            Thread worker1 = new Thread(a.Work1);
            Thread worker2 = new Thread(a.Work2);
            worker1.Start();
            worker2.Start();
            //worker1.Join();
            //worker2.Join();
            Console.WriteLine(a.result);
            Console.Read();
        }
    }
}
