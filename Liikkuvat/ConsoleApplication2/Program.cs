using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liikkuvat;
using System.Timers;
using System.Threading;


namespace ConsoleApplication2
{

    class Program
    {




         [STAThread]
        static void Main(string[] args)
        {





/*

            System.Timers.Timer kello = new System.Timers.Timer();
                kello.Interval = 100;

                // System.Timers.Timer aTimer = new System.Timers.Timer();
                kello.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                // Set the Interval to 5 seconds.

                kello.Enabled = true;
 * */
   
              Ikkuna i = new Ikkuna();

                i.Show();
               
             Peliloop(i);
             
             
            
        }

         private static void Peliloop(Ikkuna i)
         {
            
             while (true) {
                 Thread.Sleep(1000);
                 Console.Beep();
             }
         }
        // Specify what you want to happen when the Elapsed event is raised.
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
   
        }

    }
}
