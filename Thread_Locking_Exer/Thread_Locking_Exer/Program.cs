using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thread_Locking_Exer
{
    class Program
    {
        private static int val_1 = 1;
        private static int val_2 = 1;

        //Synchronization Object
        //unique private method is recommended to prevent any 
        //type of race condition
        private static object synchObj = new Object();

        //Thread work method or critical section
        public static void DoDiv()
        {
            lock (synchObj)
            {
                if (val_2 > 0)
                {
                    Console.WriteLine(val_1 / val_2);
                    val_2 = 0;
                }
            }
        } 

        public static void DoWork()
        {
            /*syntactic sugar statement for Lock; Monitor.Enter. Monitor.Exit
               pair and sets up a critical section. TryEnter method supports a lock timeout value*/
            bool lockInitiate = false;
            try
            {
                Monitor.Enter(synchObj, ref lockInitiate);

                if(val_2>0)
                {
                    Console.WriteLine(val_1 / val_2);
                    val_2 = 0;
                }
            }
            finally
            {

                if (lockInitiate)
                {
                    Monitor.Exit(synchObj);
                }
            }
        }
        public static void Main(string[] args)
        {
            Thread t1 = new Thread(DoWork);
            Thread t2 = new Thread(DoWork);
            Thread t3 = new Thread(DoWork); 
            t1.Start();
            t2.Start();
            t3.Start();
            Console.ReadKey();
        }
    }
}
