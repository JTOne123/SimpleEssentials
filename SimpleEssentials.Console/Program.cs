using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleEssentials.Console.Models;
using SimpleEssentials.Extensions;
using SimpleEssentials.IO.Types;
using SimpleEssentials.IO;
using SimpleEssentials.IO.Readers;
using SimpleEssentials.Utils;

namespace SimpleEssentials.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Doing Stuff");
            using (var progress = new ProgressBar("Doing stuff too"))
            {
                for (int i = 0; i <= 100; i++)
                {
                    progress.Report((double)i/100);
                    Thread.Sleep(100);
                }
            }




            System.Console.WriteLine("Press Enter to Exit");
            System.Console.ReadLine();
        }
    }
}
