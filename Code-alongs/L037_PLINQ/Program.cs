
// Enumerable.Range(start, length) ger oss en IEnumarable<int> som vi kan iterera över och får ut en siffra i taget exempel: 1 - 10
//var range = Enumerable.Range(1, 10);

//foreach (int number in range)
//{
//    Console.WriteLine(number);
//}

// Demo: Visar skillnaden mellan Materialiserad data i en collection och en ren IEnumerable som genererar sekvensen.
//Console.WriteLine("Calculating sum");

//long sum =  Enumerable
//                .Range(1, 2000_000_000)
//                //.ToList()
//                .Select(i => (long)i)
//                .Sum();

//Console.WriteLine("Calculation completed.");

//Console.WriteLine($"Sum = {sum}");


Console.WriteLine("Begin data processing.");

var data = Enumerable
            .Range(1, 200)
            .AsParallel()
            .WithDegreeOfParallelism(Environment.ProcessorCount / 2)
            .Select(i => CostlyOperation(i))
            .ToList();

Console.WriteLine("Processing complted. Here is the processed data:");

Console.WriteLine(string.Join(", ", data));


static int CostlyOperation(int n)
{
    Console.WriteLine($"Processing item {n}");
    for (int i = 0; i < 1_000_000_000; i++) ;
    //Thread.Sleep(500);
    return n;
}