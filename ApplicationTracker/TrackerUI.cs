using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTracker
{
    internal class TrackerUI
    {
        //---Notes for tomorrow 5/19/2023---
        //Need to add to the views to see closed applications/follow up messages
        //Need to do the UD of CRUD

        public static void MainMenu()
        {
            Console.WriteLine("What would you like to do?");
            while(true)
            {
                Console.WriteLine("1) Add application \n2) View applications \n3) Update existing application \n4) Delete exisiting application.\n\n");
                string choice = Console.ReadLine();

                if(choice == "1") CreateApplication();
                else if(choice == "2") break;
                else if(choice == "3") break;
                else if(choice == "4") break;
                else Console.WriteLine("Please enter valid number.\n\n");
            }
        }

        private static void CreateApplication()
        {
            Console.WriteLine("\nAre you sure you want to create a new application? \n1) Yes \n2) No");
            string choice = Console.ReadLine();

            if(choice == "1")
            {
                ApplicationItem app = new ApplicationItem();
                bool validSalary = false;

                Console.WriteLine("\nWhat is the job title?");
                app.Title = Console.ReadLine();
                
                while(!validSalary)
                {
                    Console.WriteLine("\nWhat is the salary?");
                    validSalary = app.ConvertSalary(Console.ReadLine());
                }

                app.Applied = DateTime.Now;
                Console.WriteLine("\nWhat was the recruiter's name?");
                app.Recruiter = Console.ReadLine();
                Console.WriteLine("\nWhat is the recruiter's email?");
                app.Email = Console.ReadLine();
                app.Open = true;
                app.FollowUps = new List<FollowUp>();
                JSONController.CreateApplication(app);
            }
        }

        private static void ViewMenu()
        {
            Console.WriteLine("\n1) View All ");
            string choice = Console.ReadLine();
            if(choice == "1") ViewAll(Status.Open);
        }

        private static void ViewAll(Status stat)
        {
            AllApplications apps = JSONController.ReadAllApplications(stat);
            foreach(ApplicationItem app in apps.Applications)
            {
                string applied = app.Applied.ToString("MM/dd");
                Console.WriteLine($"{app.ID}\t|{app.Title}\t|{app.Salary}\t|{applied}\t|{app.Recruiter}\t|{app.Email}\t|Messages:{app.FollowUps.Count}");
            }
            Console.ReadLine();
        }
    }
}
