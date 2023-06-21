using ClassicProblemsOfSynchronization.ProducerConsumerProblem;
using System;
using System.Threading;

namespace ProducerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Producer Consumer

            var consumer = new Consumer();
            var producer = new Producer();
            var producerThread = new Thread(producer.Run);
            //var producerThread2 = new Thread(producer.Run);
            var consumerThread = new Thread(consumer.Run);

            producerThread.Start();
            //producerThread2.Start();
            consumerThread.Start();
        }
    }
}
