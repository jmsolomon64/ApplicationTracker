string workingFolder = ApplicationTracker.JSONController.ProgramFolder;
string openFile = ApplicationTracker.JSONController.OpenFile;
string closedFile = ApplicationTracker.JSONController.ClosedFile;

string setup = "{\napplications:[]\n}\n";

if (!Directory.Exists(workingFolder)) Directory.CreateDirectory(workingFolder);
if (!File.Exists(openFile)) 
{
    File.Create(openFile);
    File.WriteAllText(openFile, setup);
}
if (!File.Exists(closedFile))
{
    File.Create(closedFile);
    File.WriteAllText(closedFile, setup);
}

ApplicationTracker.TrackerUI.MainMenu();