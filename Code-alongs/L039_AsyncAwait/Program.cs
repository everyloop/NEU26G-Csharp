using System.Diagnostics;
using System.Runtime.CompilerServices;

//Console.WriteLine("Start of program!");

//Task<int> calculationTask = Task.Run(() =>
//{
//    Thread.Sleep(2000);
//    Console.WriteLine("Task complete!");
//    return 42;
//});

//Console.WriteLine("Continue running.");

//// Thread.Sleep(5000);

//Console.WriteLine("Running more... ");

//Console.WriteLine($"Result: {await calculationTask}");

//Console.WriteLine("The end.");

internal class Program
{
    private static Stopwatch stopwatch = new Stopwatch();

    private static void PrintThreadInfo(string message, [CallerMemberName] string caller = null)
    {
        int threadId = Thread.CurrentThread.ManagedThreadId;
        bool isThreadPool = Thread.CurrentThread.IsThreadPoolThread;
        string elapsedTime = stopwatch.Elapsed.ToString("mm\\:ss\\.ffff");

        Console.WriteLine($"[{elapsedTime}] {caller} - {message}: Thread ID {threadId}{(isThreadPool ? " (threadpoool)" : "")}.");
    }

    private static async Task Main(string[] args)
    {
        stopwatch.Start();

        PrintThreadInfo("method start");

        Task delayTask = Delay();

        PrintThreadInfo("continue running");

        //Thread.Sleep(8000);

        await delayTask;

        Task<string> downloadTask = WebDownload();

        PrintThreadInfo("Waiting for download to complete!");

        string data = await downloadTask;

        Console.WriteLine($"Downloaded {data.Length * 2} bytes of data.");

        PrintThreadInfo("method end");


    }

    static async Task Delay()
    {
        PrintThreadInfo("method start");

        // Thread.Sleep(3000);          // Låser tråden i 3 sekunder = Väntar synkront
        // Task.Delay(3000).Wait();     // Skapar en task som är klar efter tre sekunder, och väntar på den synkront
        Task task = Task.Delay(3000);

        PrintThreadInfo("continue running");

        await task;

        PrintThreadInfo("method end");
    }

    static async Task<string> WebDownload()
    {
        HttpClient client = new HttpClient();

        PrintThreadInfo("starting download");

        string data = await client.GetStringAsync("https://www.google.com");

        PrintThreadInfo("download complete!");

        return data;
    }
}