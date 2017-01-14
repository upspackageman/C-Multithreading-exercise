using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading_Exercise1
{
    class MainClass
    {
        public const int REP = 1000;

        public static void DoWork()
        {
            for(int i = 0; i < REP; i++)
            {
                Console.Write("X"); 
            }
        }

        
    }
}
