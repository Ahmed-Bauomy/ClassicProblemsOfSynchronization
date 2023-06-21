using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicProblemsOfSynchronization.RaceCondition
{
    public class SomeWork
    {
        public int result = 0;
        public void Work1() { result = 1; }
        public void Work2() { result = 2; }
    }
}
