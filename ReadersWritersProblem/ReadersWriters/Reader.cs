using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassicProblemsOfSynchronization.ReadersWriterProblem
{
    public class Reader
    {
        public Reader()
        {
        }

        private void Read()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Reader {0} ==> Read Shared Data = {1}", Thread.CurrentThread.ManagedThreadId, ReadersWritersHelper.sharedData);
        }

        public void Run()
        {
            while (true)
            {
                // wait mutex
                ReadersWritersHelper.WaitMutex($"Reader {Thread.CurrentThread.ManagedThreadId} waiting other readers .. wanting to enter critical section 1 , thread Id: {Thread.CurrentThread.ManagedThreadId} , process Id: {Process.GetCurrentProcess().Id} , processor Id: {Thread.GetCurrentProcessorId()}");
                ReadersWritersHelper.ReadCounter++;
                if (ReadersWritersHelper.ReadCounter == 1)
                {
                    //wait wrt
                    ReadersWritersHelper.WaitWrt($"Reader { Thread.CurrentThread.ManagedThreadId } waiting other writers , thread Id: {Thread.CurrentThread.ManagedThreadId} , process Id: {Process.GetCurrentProcess().Id} , processor Id: {Thread.GetCurrentProcessorId()}");
                }
                //signal mutex
                ReadersWritersHelper.SignalMutex();
                Console.WriteLine("Reader {0} leaving critical section 1", Thread.CurrentThread.ManagedThreadId);

                //read 
                Read();

                // wait mutex
                ReadersWritersHelper.WaitMutex("Reader " + Thread.CurrentThread.ManagedThreadId + " waiting other readers .. wanting to enter critical section 2");
                ReadersWritersHelper.ReadCounter--;
                if (ReadersWritersHelper.ReadCounter == 0)
                {
                    //signal wrt
                    ReadersWritersHelper.SignalWrt();
                }

                //signal mutex
                ReadersWritersHelper.SignalMutex();
                Console.WriteLine("Reader {0} leaving critical section 2", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            }
        }

        //public void Run()
        //{
        //    while (true)
        //    {
        //        // wait mutex
        //        Console.WriteLine("Reader {0} wanting to enter critical section 1", _ReaderNumber);
        //        _helper.Mutex.WaitOne();

        //        _helper.ReadCounter++;
        //        if(_helper.ReadCounter == 1)
        //        {
        //            //wait wrt
        //            Console.WriteLine("Reader {0} waiting other writers", _ReaderNumber);
        //            _helper.Wrt.WaitOne();
        //        }
        //        //signal mutex
        //        _helper.Mutex.Release();
        //        Console.WriteLine("Reader {0} leaving critical section 1", _ReaderNumber);

        //        //read 
        //        Read();

        //        // wait mutex
        //        Console.WriteLine("Reader {0} wanting to enter critical section 2", _ReaderNumber);
        //        _helper.Mutex.WaitOne();

        //        _helper.ReadCounter--;
        //        if (_helper.ReadCounter == 0)
        //        {
        //            //signal wrt
        //            _helper.Wrt.Release();
        //        }

        //        //signal mutex
        //        _helper.Mutex.Release();
        //        Console.WriteLine("Reader {0} leaving critical section 2", _ReaderNumber);
        //        Thread.Sleep(500);
        //    }
        //}

        //private void waitMutex(ReadersWritersHelper helper)
        //{
        //    // wait mutex
        //    while (_helper.Mutex == 0)
        //    {
        //        Console.WriteLine("Reader {0} waiting other readers", _ReaderNumber);
        //        Thread.Sleep(500);
        //    }
        //    _helper.Mutex = 0;
        //}

        //private void waitWrt(ReadersWritersHelper helper)
        //{
        //    //wait wrt
        //    while (_helper.Wrt == 0)
        //    {
        //        Console.WriteLine("Reader {0} waiting other writers", _ReaderNumber);
        //        Thread.Sleep(500);
        //    }
        //    _helper.Wrt = 0;
        //}
    }
}
