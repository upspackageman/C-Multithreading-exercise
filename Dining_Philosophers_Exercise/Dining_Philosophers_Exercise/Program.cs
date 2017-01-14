using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dining_Philosophers_Exercise
{
    class Program
    {
        //the number of philosiphers 
        public const int NUM_PHILOSOPHER = 5;

        //maxinum amount of time spent thinking in ms
        public const int THINK_TIME = 10;

        //the max amount for time spent eating 
        public const int EAT_TIME = 10;

        //the timeout for locking forks in ms 
        public const int LOCK_TIMEOUT = 15;

        //total program recovery time  
        public const int RECOVERY_TIME = 15;

        //total program runtime  
        public const int RUN_TIME = 10000;

        //the chosticks, implemented as threads
        public static object[] chopsticks = new object[NUM_PHILOSOPHER];

        //the philosiphers, implemented as threads
        public static Thread[] philosopher = new Thread[NUM_PHILOSOPHER];

        //stopwatch o abort the program after timeout 
        public static Stopwatch stopwatch = new Stopwatch();

        //a dictionary that is shared  to measure how many philospers have eaten 
        public static Dictionary<int, int> eatingTime = new Dictionary<int, int>();

        //a sync object to protect access to the eating field
        private static object eatingSync = new object();

        //let philosophers think method
        public static void Think()
        {
            Random random = new Random();
            Thread.Sleep(random.Next(THINK_TIME));

        }

        //this method lets the philosipher eat 
        public static void Eat(int index)
        {
            Random random = new Random();
            int time_spent_eating = random.Next(EAT_TIME);
            Thread.Sleep(random.Next(THINK_TIME));

            //total eating time
            lock (eatingSync)
            {
                eatingTime[index] += time_spent_eating;
            }
        }

        public static void Run(int index, object chop1, object chop2)
        {
            bool lock_1 = false;
            bool lock_2 = false;
            do
            {
              

                if (lock_1 && lock_2)
                {
                    Think();
                }

                lock_1 = false;
                lock_2 = false;

                try
                {
                    Monitor.TryEnter(chop1, LOCK_TIMEOUT, ref lock_1);
                    if (lock_1)
                    {
                        try
                        {
                            Monitor.TryEnter(chop2, LOCK_TIMEOUT, ref lock_2);
                            if (lock_2)
                            {
                                Eat(index);
                            }
                            else
                            {
                                Random random = new Random();
                                int time_spent_eating = random.Next(RECOVERY_TIME);
                                Thread.Sleep(time_spent_eating);
                            }
                        }
                        finally
                        {
                            if (lock_2)
                            {
                                Monitor.Exit(chop2);
                            }
                        }
                    }

                }
                finally
                {
                    if (lock_1)
                    {
                        Monitor.Exit(chop1);
                    }

                    
                }
               

                   

                } while (stopwatch.ElapsedMilliseconds<RUN_TIME) ;
            
          }
         
        public static void Main(string[] args)
        {
            // set up the dictionary that measures total eating time
            for (int i = 0; i < NUM_PHILOSOPHER; i++)
            {
                eatingTime.Add(i, 0);
            }

            // set up the chopsticks
            for (int i = 0; i < NUM_PHILOSOPHER; i++)
            {
                chopsticks[i] = new object();
            }

            // set up the philosophers
            for (int i = 0; i < NUM_PHILOSOPHER; i++)
            {
                int index = i;
                object chopstick1 = chopsticks[i];
                object chopstick2 = chopsticks[(i + 1) % NUM_PHILOSOPHER];
                philosopher[i] = new Thread(
                    _ =>
                    {
                        Run(index, chopstick1, chopstick2);
                    }
                );
            }

            // start the philosophers
            stopwatch.Start();
            Console.WriteLine("Starting philosophers...");
            for (int i = 0; i < NUM_PHILOSOPHER; i++)
            {
                philosopher[i].Start();
            }

            // wait for all philosophers to complete
            for (int i = 0; i < NUM_PHILOSOPHER; i++)
            {
                philosopher[i].Join();
            }
            Console.WriteLine("All philosophers have finished");

            // report the total time spent eating
            int total_eating_time = 0;
            for (int i = 0; i < NUM_PHILOSOPHER; i++)
            {
                Console.WriteLine("Philosopher {0} has eaten for {1}ms", i, eatingTime[i]);
                total_eating_time += eatingTime[i];
            }
            Console.WriteLine("Total time spent eating: {0}ms", total_eating_time);
            Console.WriteLine("Optimal time spent eating: {0}ms", stopwatch.ElapsedMilliseconds * 2);
            Console.ReadKey();
        }
    }
}
