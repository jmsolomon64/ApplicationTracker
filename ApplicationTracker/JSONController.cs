using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApplicationTracker
{
    public static class JSONController
    {
        //---Important strings---//
        public static string ProgramFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        public static string OpenApps = $"{ProgramFolder}\\Open.json";
        public static string ClosedApps = $"{ProgramFolder}\\Closed.json";

        //---Specialized tasks---//
        //--Create--
        internal static void CreateApplication(ApplicationItem app)
        {
            AllApplications apps = ReadJson(OpenApps);
            apps.Applications.Add(app);
            OverwriteFile(OpenApps, apps);
        }

        //--Read--
        internal static AllApplications ReadAllApplications(string filePath)
        {
            return ReadJson(filePath);
        }

        internal static ApplicationItem ReadApplication(int id, string filePath)
        {
            AllApplications apps = ReadJson(filePath);
            return FindApplication(id, apps);
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
            AllApplications openApps = ReadJson(OpenApps);
            AllApplications closedApps = ReadJson(ClosedApps);

            //loop through openApps and move closed ones to close 
            //overwrite
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

        private static ApplicationItem FindApplication(int id, AllApplications apps)
        {
            return apps.Applications.FirstOrDefault(x => x.ID == id);
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
