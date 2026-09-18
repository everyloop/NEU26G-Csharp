
// Console.WriteLine($"Current directory: {Directory.GetCurrentDirectory()}");
// Directory.SetCurrentDirectory(@"C:\");

//try
//{
//    using (StreamReader reader = new StreamReader(@"C:\Users\Fredrik\source\repos\NEU26G-Csharp\Code-alongs\L013_Path_och_Directory\program.cs"))

//    // Console.WriteLine(reader.ReadToEnd());

//    while (!reader.EndOfStream)
//    {
//        // Console.WriteLine(reader.ReadLine());
//        Console.Write((char)reader.Read());
//        Thread.Sleep(50);
//    }
//}
//catch (IOException ex)
//{
//    Console.WriteLine($"Fel vid filläsning: {ex.Message}");
//}


using (StreamWriter writer = new StreamWriter("myFile.txt", append: true))
{
    writer.WriteLine("Hello");
    writer.WriteLine("World");

    for (int i = 65; i <= 90; i++)
    {
        writer.Write((char)i);
    }

    writer.WriteLine();
}
