The GameKeeper host service scans an assembly for classes marked by the
AddIn attribute, and loads them into a process. Debugging an AddIn in 
this manner is difficult as it requires it to be loaded into a GameKeeper
host. As such, it is recommended to set your project type to a Console 
application and use the sample Program.cs file below as the Startup Object.

using System;
using System.Threading;

namespace $rootnamespace$
{
    public class Program
    {
        private static void Main(string[] args)
        {
			/* If your [AddIn] class is named other than
			 * the default Service, you'll need to replace
			 * the below line with instantiation of your
			 * AddIn class */
            var service = new Service();  

            var quitSemaphore = new Semaphore(0, 1);

            Console.CancelKeyPress += delegate
            {
                service.Stop();
                quitSemaphore.Release();
            };

            service.Start();
            quitSemaphore.WaitOne();

        }
    }
}