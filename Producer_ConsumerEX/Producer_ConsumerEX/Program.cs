/*
 * This producer consumer exercise; In this exercise I have ManualREsetEvent Class, AutoResetEvent, and CountdownEvent. 
    AutoResetEvent: 1 thread signals another thread to do something.
    IE: signal to consumers that new work has arrived 
    
    ManualResetEvent: 1 thread signals many threads to do something 
    IE: suspend and resume all consumers 
    
    CountdownEvent: Many threads signal 1 thread to do something.
    IE: wait until all consumers have quit
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Producer_ConsumerEX
{
    class Program
    {
        //the arrray of consumer threads 
        private static List<Thread> consumers = new List<Thread>();

        //the task queue 
        private static Queue<Action> tasks = new Queue<Action>();

        //the synchronization object for locking the task queue
        private static readonly object queueLock = new object();

        // this wait handle notifies consumers of a new task
        private static EventWaitHandle newTaskAvail = new AutoResetEvent(false);

        #region
        //this wait handle pauses consumers
        //private static EventWaitHandle pauseConsumer = new ManualResetEvent(true);
        #endregion

        //the synchronization object for flocking the console color 
        private static readonly object consoleLock = new object();

        //the wait handle to quit consumers 
        private static CountdownEvent quitConsumers = new CountdownEvent(3);

        //the flag to request that consumes quit
        private static bool quitRequest = false;

        //the synchronization object for locking the console color
        private static readonly object quitLock = new object();

       //enqueue a new task 
        private static void EnqueueTask(Action task)
        {
            lock (queueLock)
            {
                tasks.Enqueue(task);
            }
            newTaskAvail.Set();
        }

        //thread work method for consumers
        private static void ThreadWork(ConsoleColor color)
        {
            #region
            /* for Manul reset class 
            while (true)
            {
                #region
                //check if producer asked to pause
                //pauseConsumer.WaitOne();

                //get a new task
                //Action task = null;
                #endregion
                lock (queueLock)
                {
                    if (tasks.Count>0)
                    {
                        task = tasks.Dequeue();
                    }
                }
                if (task != null)
                {
                    //set console to the task's color
                    lock (consoleLock)
                    {
                        Console.ForegroundColor = color;
                    }
                    //execute task
                    task();
                }
                else
                {
                    newTaskAvail.WaitOne();
                }
            }
            */
            #endregion
            while (true)
            {
               
                lock (quitLock)
                {
                    
                    if (quitRequest)
                    {
                        Console.WriteLine("\nConsumer " + color + " is quiting");
                        quitConsumers.Signal();
                        break;
                    }
                }

                //get a new task
                Action task = null;
                lock (queueLock)
                {
                    if (tasks.Count > 0)
                    {
                        task = tasks.Dequeue();
                    }
                }
                if (task != null)
                {
                    lock (consoleLock)
                    {
                        Console.ForegroundColor = color;
                    }

                    //execute task
                    task();
                }
                else
                {
                    newTaskAvail.WaitOne(750);
                }
                
            }
        }


        public static void Main(string[] args)
        {
            consumers.Add(new Thread(() => { ThreadWork(ConsoleColor.Red); }));
            consumers.Add(new Thread(() => { ThreadWork(ConsoleColor.DarkBlue); }));
            consumers.Add(new Thread(() => { ThreadWork(ConsoleColor.Green); }));

            //start all consumers 
            consumers.ForEach((t) => { t.Start(); });

            int iterations = 0;

            #region
            //bool consumerIsPaused = false;
            #endregion
            while (true)
            {
                Random r = new Random();
                EnqueueTask(() => {

                    //the task is to write a random number to the console
                    int numb = r.Next(10);
                    Console.Write(numb);
                });

                //random sleep to simulate workload
                Thread.Sleep(r.Next(1000));
                #region
                //press any key to pause/resume the consumer
                /*if (Console.KeyAvailable)
                {
                    Console.Read();
                    if (consumerIsPaused)
                    {
                        pauseConsumer.Set();
                        Console.WriteLine("Consumers resumed");
                    }
                    else
                    {
                        pauseConsumer.Reset();
                        Console.WriteLine("Consumers paused");
                    }
                    consumerIsPaused = !consumerIsPaused;
                }*/
                #endregion

                if (iterations++ >= 10)
                {
                    //request consumer quit 
                    lock (quitLock)
                    {
                        quitRequest = true;
                    }
                    //wait until all consumers have signaled
                    quitConsumers.Wait();

                    Console.WriteLine("All consumers have quit");
                    break;
                }
            }
            Console.ReadKey();

        }
    }
}
