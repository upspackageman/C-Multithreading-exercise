using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Passing_in_data
{
    class MainClass
    {
        private const int REP = 1000; 

        public static void DoWork(object obj)
        {
            char c = (char)obj;
            for(int i=0; i < REP; i++)
            {
                Console.Write(c);
            }
        }
        public static void DoWork2(char c)
        {
            for(int i=0; i<REP; i++)
            {
                Console.Write(c);
            }

        }
        /* 
        * -Use ParamaterizedThreadStart and provide Start argument
        *      +single parameter only
        *      +parameter has to be of type object
        *  -Use LAmbda expression()=>{...}
        *      +pass in constants 
        *      +or: provide unique variables 
        */
        static void Main(string[] args)
        {
            /*
            #region start new thread with ParameterizedThreadStart delegate
            Thread t1 = new Thread(DoWork);
            t1.Start("X");

            //Continue simultaneous work;
            DoWork("Y");
            #endregion 
            */

            #region start new thread with lamda expression
            Thread t2 = new Thread(() => { DoWork2('Y'); });
            t2.Start();
            DoWork2('X');
            Console.ReadKey();
            #endregion
        }
    }
}
