using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassicProblemsOfSynchronization.ProducerConsumerProblem
{
    public class Consumer
    {
        public Consumer()
        {
        }
        public void ConsumeItem(Item item)
        {
            Console.WriteLine("start consume item {0}", item.Id);
            Thread.Sleep(2000);
            Console.WriteLine("Item {0} Consumed",item.Id);
        }
        public void Run()
        {
            while (true)
            {
                // wait full
                ProducerConsumerHelper.WaitFull($"consumer {Thread.CurrentThread.ManagedThreadId} waiting , thread Id: {Thread.CurrentThread.ManagedThreadId} , process Id: {Process.GetCurrentProcess().Id} , processor Id: {Thread.GetCurrentProcessorId()}");

                // take an item from queue
                ProducerConsumerHelper.Mutex.WaitOne();
                //var item = ProducerConsumerHelper.Queue.Dequeue();
                var item = ProducerConsumerHelper.Buffer[ProducerConsumerHelper.Count - 1];
                ProducerConsumerHelper.Count--;
                Console.WriteLine("item {0} taken from Buffer.", item.Id);
                ProducerConsumerHelper.Mutex.Release();

                // signal empty semaphore
                ProducerConsumerHelper.SignalEmpty();
                // consume item
                ConsumeItem(item);
            }
            // wait full
        }
    }
}
