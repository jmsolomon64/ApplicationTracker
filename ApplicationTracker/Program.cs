string workingFolder = ApplicationTracker.JSONController.ProgramFolder;
string openFile = ApplicationTracker.JSONController.OpenApps;
string closedFile = ApplicationTracker.JSONController.ClosedApps;

if (!Directory.Exists(workingFolder)) Directory.CreateDirectory(workingFolder);
if (!File.Exists(openFile)) File.Create(openFile);
if (!File.Exists(closedFile)) File.Create(closedFile);

ApplicationTracker.TrackerUI.MainMenu();