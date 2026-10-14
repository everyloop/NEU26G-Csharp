
using System.Diagnostics.Metrics;

for (int i = 0; i < 20; i++)
{
    await Count();
}

static async Task Count()
{
    object lockA = new object();

    int counter = 0;

    Task taskA = Task.Run(DoCount);
    Task taskB = Task.Run(() => DoCount());
    Task taskC = Task.Run(DoCount);
    Task taskD = Task.Run(DoCount);

    await taskA;
    await taskB;
    await taskC;
    await taskD;

    Console.WriteLine(counter);

    void DoCount()
    {
        for (int i = 0; i < 1000; i++)
        {
            lock (lockA)
            {   
                int CpuData = counter;
                CpuData += 1;
                counter = CpuData;
            }

            // Interlocked.Increment(ref counter);
        }
    }
}
