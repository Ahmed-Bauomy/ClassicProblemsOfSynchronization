using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassicProblemsOfSynchronization.ProducerConsumerProblem
{
    public static class ProducerConsumerHelper
    {

        public static uint FullSemaphore  = 0; // number of full spaces in the queue
        public static uint EmptySemaphore  = 4; // number of empty spaces in the queue
        public static Semaphore FullSem = new Semaphore(0,4); // number of full spaces in the queue
        public static Semaphore EmptySem = new Semaphore(4,4); // number of empty spaces in the queue
        //public static Queue<Item> Queue  = new Queue<Item>(4);
        public static Item[] Buffer = new Item[4];
        public static int Count = 0;
        public static Semaphore Mutex = new Semaphore(1, 1);
        public static Semaphore produceItemMutex = new Semaphore(1, 1);
        public static readonly object Locker  = new object();

        public static void WaitEmpty(string waitingMessage)
        {
            //Console.Write("");
            while (EmptySemaphore == 0)
            {
                Console.WriteLine(waitingMessage);
                Thread.Sleep(500);
            }
            EmptySem.WaitOne();
            lock (Locker)
            {
                EmptySemaphore--;
            }
        }

        public static void SignalFull()
        {
            lock (Locker)
            {
                FullSemaphore++;
            }
            FullSem.Release();
        }

        public static void WaitFull(string waitingMessage)
        {
            //Console.Write("");
            while (FullSemaphore == 0)
            {
                Console.WriteLine(waitingMessage);
                Thread.Sleep(500);
            }
            FullSem.WaitOne();
            lock (Locker)
            {
                FullSemaphore--;
            }
        }

        public static void SignalEmpty()
        {
            lock (Locker)
            {
                EmptySemaphore++;
            }
            EmptySem.Release();
        }
    }
}
