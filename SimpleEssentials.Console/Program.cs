using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleEssentials.Console.Models;
using SimpleEssentials.IO.Types;

namespace SimpleEssentials.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //var testRepo = new CustomCampaignRepository();
            //var statuses = testRepo.GetEmployeeStatuses("        9");
            //foreach (var status in statuses)
            //{
            //    System.Console.WriteLine("--------------------------------------------------");
            //    System.Console.WriteLine(status.Employee_Id);
            //    System.Console.WriteLine(status.Status.Name);
            //    System.Console.WriteLine(status.Campaign.Name);
            //    System.Console.WriteLine("--------------------------------------------------");
            //}

            //var status = testRepo.GetLatestStatus("        9", 1);
            //System.Console.WriteLine("--------------------------------------------------");
            //System.Console.WriteLine(status.Employee_Id);
            //System.Console.WriteLine(status.Status.Name + " " + status.Status_Id);
            //System.Console.WriteLine(status.Campaign.Name + " " + status.Campaign_Id);
            //System.Console.WriteLine("--------------------------------------------------");
            //testRepo.SaveInteraction("        9", 1, 1);


            var testRepo = new TestRepository();
            var clients = testRepo.GetClients(1);

            
            System.Console.WriteLine("Press Enter to Exit");
            System.Console.ReadLine();
        }
    }
}
