using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassicProblemsOfSynchronization.ReadersWriterProblem
{
    public static class ReadersWritersHelper
    {
        public static uint Mutex = 1;  //binary semaphore blocks readers
        public static uint Wrt = 1;  //binary semaphore blocks writers

        public static Semaphore MutexSemaphore = new Semaphore(1,1);  //binary semaphore blocks readers
        public static Semaphore WrtSemaphore = new Semaphore(1,1);  //binary semaphore blocks writers
        public static int ReadCounter { get; set; } = 0;

        public static int sharedData { get; set; } = 5;
        public static readonly object Locker = new object();

        public static void WaitMutex(string waitingMessage)
        {
            //Console.Write("");
            while (Mutex == 0)
            {
                Console.WriteLine(waitingMessage);
                Thread.Sleep(500);
            }
            MutexSemaphore.WaitOne();
            Mutex--;
        }

        public static void SignalWrt()
        {
            lock (Locker)
            {
                Wrt++;
            }
            WrtSemaphore.Release();
        }

        public static void WaitWrt(string waitingMessage)
        {
            //Console.Write("");
            while (Wrt == 0)
            {
                Console.WriteLine(waitingMessage);
                Thread.Sleep(500);
            }
            WrtSemaphore.WaitOne();
            Wrt--;
        }

        public static void SignalMutex()
        {
            lock (Locker)
            {
                Mutex++;
            }
            MutexSemaphore.Release();
        }
    }
}
