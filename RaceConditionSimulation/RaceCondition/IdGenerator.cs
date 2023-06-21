using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassicProblemsOfSynchronization.RaceCondition
{
    public class IdGenerator
    {
        public int LastIdValue = 42;
        public void IncrementValue()
        {
            var result = ++LastIdValue;
            Console.WriteLine($"lastIdValue = {result} , thread Id: {Thread.CurrentThread.ManagedThreadId} , process Id: {Process.GetCurrentProcess().Id} , processor Id: {Thread.GetCurrentProcessorId()}");
            //return result;
        }
    }
    
}
