string workingFolder = ApplicationTracker.JSONController.ProgramFolder;
string openFile = ApplicationTracker.JSONController.OpenFile;
string closedFile = ApplicationTracker.JSONController.ClosedFile;

if (!Directory.Exists(workingFolder)) Directory.CreateDirectory(workingFolder);
if (!File.Exists(openFile)) File.Create(openFile);
if (!File.Exists(closedFile)) File.Create(closedFile);

ApplicationTracker.TrackerUI.MainMenu();