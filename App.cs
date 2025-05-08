using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using QuoteFinder.Logic;
using QuoteFinder.UI;

namespace QuoteFinder
{
    public static class App
    {
        public static void Run()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var userInteraction = new UserInteraction();
            userInteraction.AskForData();
            stopWatch.Stop();

            Console.WriteLine(stopWatch.ElapsedMilliseconds);
        }
    }
}
