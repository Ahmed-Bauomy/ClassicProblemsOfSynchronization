using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassicProblemsOfSynchronization.ProducerConsumerProblem
{
    public class Producer
    {
        private int LastGeneratedId { get; set; }
        public Producer()
        {
        }
        private Item ProduceItem()
        {
            var item = new Item() { Id = ++LastGeneratedId, Name = "Name_" + Guid.NewGuid().ToString() };
            Console.WriteLine("start produce item {0}", item.Id);
            Thread.Sleep(4000);
            Console.WriteLine("Item {0} Produced", item.Id);
            return item;
        }

        public void Run()
        {
            while (true)
            {
                // produce new item
                ProducerConsumerHelper.produceItemMutex.WaitOne();
                var newItem =  ProduceItem();
                ProducerConsumerHelper.produceItemMutex.Release();
                // wait for empty 
                ProducerConsumerHelper.WaitEmpty($"producer {Thread.CurrentThread.ManagedThreadId} waiting , thread Id: {Thread.CurrentThread.ManagedThreadId} , process Id: {Process.GetCurrentProcess().Id} , processor Id: {Thread.GetCurrentProcessorId()}");

                // place new item in the queue
                ProducerConsumerHelper.Mutex.WaitOne();
                //ProducerConsumerHelper.Queue.Enqueue(newItem);
                ProducerConsumerHelper.Buffer[ProducerConsumerHelper.Count] = newItem;
                ProducerConsumerHelper.Count++;
                Console.WriteLine("item {0} added to Buffer",newItem.Id);
                ProducerConsumerHelper.Mutex.Release();
                // end placing new item in the queue

                // signal full semaphore
                ProducerConsumerHelper.SignalFull();
            }
        }
    }
}
