using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;



namespace ApplicationTracker
{
    enum Status {Open, Closed}

    public static class JSONController
    {
        //---Important strings---//
        public static string ProgramFolder = "assets";
        public static string OpenFile = Path.Combine(ProgramFolder, "Open.json");
        public static string ClosedFile = Path.Combine(ProgramFolder, "Closed.json");

        //---Specialized tasks---//
        //--Create--
        internal static void CreateApplication(ApplicationItem app)
        {
            AllApplications apps = ReadJson(OpenFile);
            if(apps.Applications == null) apps.Applications = new List<ApplicationItem>();
            apps.Applications.Add(app);
            OverwriteFile(OpenFile, apps);
        }

        //--Read--
        internal static AllApplications ReadAllApplications(Status stat)
        {
            if(stat == Status.Open) return ReadJson(OpenFile);
            else return ReadJson(ClosedFile);
        }

        internal static ApplicationItem ReadApplication(int id, string filePath)
        {
            AllApplications apps = ReadJson(filePath);
            return apps.Applications.FirstOrDefault(x => x.ID == id);
        }

        //--Update--
        internal static void UpdateApplication(ApplicationItem app, int id, string filePath)
        {
            AllApplications apps = ReadJson(filePath);
            apps.Applications[id] = app;
            OverwriteFile(filePath, apps);
        }

        //--Delete--
        internal static void DeleteApplication(int id, string filePath)
        {
            AllApplications apps = ReadJson(filePath);
            apps.Applications.Remove(apps.Applications[id]);
            OverwriteFile(filePath, apps);
        }

        //--Migrate closed apps from Open.json to Closed.json--
        internal static void MigrateApplications()
        {
            AllApplications openApps = ReadJson(OpenFile);
            AllApplications closedApps = ReadJson(ClosedFile);

            for(int i = 0; i < openApps.Applications.Count - 1;i++)
            {
                ApplicationItem app = openApps.Applications[i];
                if(!app.Open)
                {
                    openApps.Applications.Remove(app);
                    closedApps.Applications.Add(app);
                }
            } 
            
            OverwriteFile(OpenFile, openApps);
            OverwriteFile(ClosedFile, closedApps);
        }

        //---Multi purpose methods---//
        private static AllApplications ReadJson(string filePath)
        {
            string content = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<AllApplications>(content);
        }

        private static void OverwriteFile(string filePath, AllApplications apps)
        {
            UpdateIDs(apps);
            string content = JsonConvert.SerializeObject(apps);
            File.WriteAllText(filePath, content);
        }

        private static void UpdateIDs(AllApplications apps)
        {
            int id = 0;
            foreach(ApplicationItem app in apps.Applications)
            {
                app.ID = id;
                id ++;
            }
        }

        

    }
}
