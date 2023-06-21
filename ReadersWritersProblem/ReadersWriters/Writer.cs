using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassicProblemsOfSynchronization.ReadersWriterProblem
{
    public class Writer
    {
        public Writer()
        {
        }

        private void Write()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Writer {0} update Shared Data To = {1}", Thread.CurrentThread.ManagedThreadId, ++ReadersWritersHelper.sharedData);
        }

        public void Run()
        {
            while (true)
            {
                // wait wrt
                ReadersWritersHelper.WaitWrt($"writer { Thread.CurrentThread.ManagedThreadId } waiting other readers or writers , thread Id: {Thread.CurrentThread.ManagedThreadId} , process Id: {Process.GetCurrentProcess().Id} , processor Id: {Thread.GetCurrentProcessorId()}");

                //write 
                Write();

                //signal wrt
                ReadersWritersHelper.SignalWrt();
                Console.WriteLine("writer {0} leaving critical section", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(500);
            }
        }

        //private void waitWrt(ReadersWritersHelper helper)
        //{
        //    //wait wrt
        //    while (_helper.Wrt == 0)
        //    {
        //        Console.WriteLine("writer waiting ......");
        //        Thread.Sleep(500);
        //    }
        //    _helper.Wrt = 0;
        //}
    }
}
