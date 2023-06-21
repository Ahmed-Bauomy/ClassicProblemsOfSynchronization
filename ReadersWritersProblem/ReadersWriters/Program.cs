using ClassicProblemsOfSynchronization.ReadersWriterProblem;
using System;
using System.Threading;

namespace ReadersWriters
{
    class Program
    {
        static void Main(string[] args)
        {
            // Readers Writers

            var reader = new Reader();
            var writer = new Writer();
            var readerThreadone = new Thread(reader.Run);
            var readerThreadTwo = new Thread(reader.Run);
            var writerThreadone = new Thread(writer.Run);
            var writerThreadTwo = new Thread(writer.Run);

            readerThreadone.Start();
            writerThreadone.Start();
            readerThreadTwo.Start();
            writerThreadTwo.Start();
        }
    }
}
