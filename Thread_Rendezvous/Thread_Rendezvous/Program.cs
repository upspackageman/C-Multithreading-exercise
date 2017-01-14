using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thread_Rendezvous
{
    class Program
    {
        private const int  SIZE= 20;
        //wait handles to threads
        public static Barrier barrier = new Barrier(SIZE, barrier => Console.WriteLine());

        //thread work method 
        public static void DoThis()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.Write(i + " ");

                //rendezvous with other threads
                barrier.SignalAndWait();
            }
        }
        public static void Main(string[] args)
        {
            for (int i = 0; i < SIZE; i++)
            {
                Thread t = new Thread(DoThis);
                t.Name = "Thread" + i.ToString();
                t.Start();
            }
            Console.ReadKey();
        }
    }
}
