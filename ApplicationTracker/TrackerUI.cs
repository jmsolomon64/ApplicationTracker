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
                else if(choice == "2") ViewMenu();
                else if(choice == "3") break;
                else if(choice == "4") break;
                else Console.WriteLine("Please enter valid number.\n\n");
            }
        }

        private static void CreateApplication()
        {
            string choice = VerificationMessage("create an Application?");

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
                Console.WriteLine("What Languages does the job use?");
                app.Languages = Console.ReadLine();
                Console.WriteLine("Where is the job located");
                app.Location = Console.ReadLine();
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
            Console.WriteLine("\n1) View All Open \n2) View All Closed");
            string choice = Console.ReadLine();
            if(choice == "1") ViewAll(Status.Open);
            else if(choice == "2") ViewAll(Status.Closed);
        }

        private static void ViewAll(Status stat)
        {
            AllApplications applications = JSONController.ReadAllApplications(stat);
            string format = "{0,-2}|{1,-35}|{2,-12}|{3,-30}|{4,-15}|{5,-10}|{6}";
            // Print the header
            Console.WriteLine("\n");
            Console.WriteLine(format, "ID", "Job Title", "Salary", "Location", "Languages", "Date", "Messages");

            // Print the application items
            foreach (ApplicationItem application in applications.Applications)
            {
                Console.WriteLine(format, application.ID, application.Title, application.Salary, application.Location, application.Languages, application.Email, application.Applied, "", "", "0");
            }

            Console.WriteLine("\nPress enter to continue, or select application ID to view details");
            string choice = Console.ReadLine(); //Pause operation until user presses enter
            if(choice != null) DetailView(choice, applications);
        }

        private static void DetailView(string choice, AllApplications apps)
        {
            int id = 0;
            if(int.TryParse(choice, out id))
            {
                ApplicationItem app = apps.Applications.FirstOrDefault(x => x.ID == id);
                if(app != null)
                {
                    Console.WriteLine($"{app.Title}\t{app.Applied}\n--------------------\n");
                    Console.WriteLine($"${app.Salary} >> Location: {app.Location} >> Languages: {app.Languages}");
                    Console.WriteLine("----------------------");
                    Console.WriteLine($"Recruiter: {app.Recruiter} can be reached with: {app.Recruiter}.");
                    Console.WriteLine("----------------------\nFollow ups:");
                    foreach(FollowUp followUp in app.FollowUps)
                    {
                        Console.WriteLine("\n----------------------");
                        Console.WriteLine(followUp.Arrived);    
                        Console.WriteLine($"\t{followUp.Description}");
                    }
                    Console.WriteLine("----------------------");
                }
            }
        }

        private static void DetailsMenu(ApplicationItem app)
        {
            Console.WriteLine("\n1) Add Follow Up \n2) Change Status \n3) Edit Information \n4) Delete Application");
            Console.WriteLine("----------------------\n");
            string choice = Console.ReadLine();

            if(choice == "1") FollowUpMenu(app);
            else if(choice == "2") StatusMenu(app);
            else if(choice == "3") Console.WriteLine("----------------------");
            else if(choice == "4") DeleteMenu(app);
        }

        //---The following three all call to UpdateApplication is JSONController to make changes to single Applications---
        private static void FollowUpMenu(ApplicationItem app)
        {
            string choice = VerificationMessage("add a follow up to Application");

            if(choice == "1")
            {
                FollowUp newFollowUp = new FollowUp();

                Console.WriteLine("Please enter descreption of the correspondence.\n");
                newFollowUp.Description = Console.ReadLine();
                newFollowUp.Arrived = DateTime.Now;

                //Saves app by overwriting file matching app's status
                if(app.Open) JSONController.UpdateApplication(app, (int)app.ID, JSONController.OpenFile);
                else JSONController.UpdateApplication(app, (int)app.ID, JSONController.ClosedFile);
            }
        }

        private static void StatusMenu(ApplicationItem app)
        {
            string choice = VerificationMessage("change Application status");
            if(choice == "1")
            {
                app.Open = !app.Open; //Swaps status

                //check will reflect new status and save to that
                if(app.Open)JSONController.UpdateApplication(app, (int)app.ID, JSONController.OpenFile);
                else JSONController.UpdateApplication(app, (int)app.ID, JSONController.ClosedFile);
            }
        }

        private static void DeleteMenu(ApplicationItem app)
        {
            string choice = VerificationMessage("delete Application");
            if(choice == "1")
            {
                Console.WriteLine($"To continue please type the name of this Application: {app.Title}, or 'x' to go back\n");
                string title;
                while(true)
                {
                    title = Console.ReadLine();
                    if(title == app.Title)
                    {
                        if(app.Open) JSONController.DeleteApplication((int)app.ID, JSONController.OpenFile);
                        else JSONController.DeleteApplication((int)app.ID, JSONController.ClosedFile);
                        break;
                    }
                    else if(title == "x") break;
                    else Console.WriteLine($"Please enter {app.Title} or simply 'x'\n");
                }
            }
        }

        //---Standardize user input---
        private static string VerificationMessage(string option)
        {
            Console.WriteLine($"Are you sure you want to {option}?\n1) Yes 2) No");
            Console.WriteLine("----------------------\n");
            return Console.ReadLine();
        }
    }
}
