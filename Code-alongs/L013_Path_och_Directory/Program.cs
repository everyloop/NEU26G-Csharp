
Console.WriteLine("*** Path ***\n");

Console.WriteLine($"Path.VolumeSeparatorChar => {Path.VolumeSeparatorChar}");
Console.WriteLine($"Path.DirectorySeparatorChar => {Path.DirectorySeparatorChar}\n");

string path = Path.Join("c:", "Users", "Fredrik", "downloads", "music.mp3");

// Path.Combine fungerar nästan samma som Path.Join, förutom om någon av parametrarna innehåller en absolut sökväg. Jämför:
//string path   = Path.Join("c:", "Users", "Fredrik", "d:\\download", "music.mp3");
//string path2  = Path.Combine("c:", "Users", "Fredrik", "d:\\download", "music.mp3");

Console.WriteLine($"path = \"{path}\"\n");

Console.WriteLine($"Path.GetFileName(path) => {Path.GetFileName(path)}");
Console.WriteLine($"Path.GetFileNameWithoutExtension(path) => {Path.GetFileNameWithoutExtension(path)}");
Console.WriteLine($"Path.GetExtension(path) => {Path.GetExtension(path)}");
Console.WriteLine($"Path.GetDirectoryName(path) => {Path.GetDirectoryName(path)}");
Console.WriteLine($"Path.GetPathRoot(path) => {Path.GetPathRoot(path)}\n");

// Konverterar en relativ sökväg till en absolut sökväg (genom att kolla i filsystemet)
Console.WriteLine($"Path.GetFullPath(\".\\\") => {Path.GetFullPath(".\\")}\n");

Console.WriteLine($"Path.Exists(path) => {Path.Exists(path)}");
Console.WriteLine($"Path.Exists(Path.GetDirectoryName(path)) => {Path.Exists(Path.GetDirectoryName(path))}\n");

Console.WriteLine($"Path.ChangeExtension(path, \".doc\") => {Path.ChangeExtension(path, ".doc")}");


Console.WriteLine("\n\n*** Directory ***\n");

path = @"C:\Users\Fredrik\source\repos\NEU26G-Csharp\Code-alongs\L013_Path_och_Directory";

Console.WriteLine($"path = \"{path}\"\n");

Console.WriteLine($"Directory.Exists(path) => {Directory.Exists(path)}");
Console.WriteLine($"Directory.GetDirectoryRoot(path) => {Directory.GetDirectoryRoot(path)}");
Console.WriteLine($"Directory.GetParent(path) => {Directory.GetParent(path)}\n");

Console.WriteLine("Subdirectories:");

foreach (var subdir in Directory.GetDirectories(path))
{
    Console.WriteLine(Path.GetFileName(subdir));
}

Console.WriteLine("\nFiles:");

foreach (var filepath in Directory.GetFiles(path))
{
    Console.WriteLine(Path.GetFileName(filepath));
}

string newPath = Path.Join(path, "newFolder", "test", "csharp");
Console.WriteLine($"Create folder: \"{newPath}\"\n");

Directory.CreateDirectory(newPath);

string removePath = Path.Join(path, "newFolder");
Directory.Delete(removePath); // Tar bort en mapp om den är tom, annars kasta exception

// VARNING: Tar bort mappen samt alla dess undermappar och filer (utan att fråga).
// Directory.Delete(removePath, true);

