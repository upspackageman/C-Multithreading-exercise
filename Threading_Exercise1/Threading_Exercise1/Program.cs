using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threading_Exercise1
{
    class Program
    {
        public static void DoWork()
        {
            for(int i=0; i < MainClass.REP; i++)
            {

                Console.Write("Y");
            }
        }
        public static void Main(string[] args)
        {
            /*
            Thread t1 = new Thread(new ThreadStart(MainClass.DoWork));
            t1.Start();*/
            /*
            Thread t2 = new Thread(MainClass.DoWork);
            t2.Start();*/

            /*Thread t3 = new Thread(() => { MainClass.DoWork(); });
            t3.Start();*/

            //start 10 new threads 
            /*
            for (int i = 0; i < 9; i++)
            {
                Thread t = new Thread(MainClass.DoWork);
                t.Name = "Thread" + i.ToString();
                t.Start();
             }*/
            //start background thread 
            Thread t = new Thread(() => {
                Console.WriteLine("Thread is starting, press Enter to continue..");
                                          Console.ReadLine();
                  });
            t.IsBackground = true;
            t.Start(); 
            //DoWork();
            Console.ReadKey();
            //main thread ends here
        }
    }
}
