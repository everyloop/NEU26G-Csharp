
//Task myTask;
//Task<string> myGenericTask;

// Generic Delegates
// Action - Referens till metod som inte tar inparametrar och inte returnerar något
// Exmepel: Action => void MyFunction();

// Action<T1, T2 .... Tn> - Referens till metod som tar n inparametrar och inte returnerar något
// Exempel: Action<int, string>  void MyFunction(int i, string s);

// Func<T1, T2, ..., Tn, TResult> - Referens till funktion som tar n inparametrar och returnerar TResult
// Exempel: Func<int, string>   string MyFunction(int i)

//Task task = new Task(() => MyTask(1));  //Task.Run(Task1);

//Console.WriteLine(task.Status);

//task.Start();

//Task.Run(() => MyTask(2));

//Console.WriteLine(task.Status);


//for (int i = 0; i < 1000; i++)
//{
//    Console.WriteLine($"Main Process: {i}");
//}

//Console.WriteLine(task.Status);

//Console.WriteLine("The end!");

//void MyTask(int n)
//{
//    for (int i = 0; i < 1000; i++)
//    {
//        Console.WriteLine($"Task {n}: {i}");
//    }
//}



//Task task1 = Task.Run(() => Console.WriteLine("Running task."));

Console.WriteLine("Starting calculation Task.");

Task<long> task2 = Task.Run(() => {

	long sum = 0;

	for (int i = 0; i < 1000_000_000; i++)
	{
		sum += i;
	}
    return sum;
});

Console.WriteLine("Doing something else...");

Console.WriteLine("Waiting for task2 to complete, so we can show the result ...");

Console.WriteLine($"Result: {task2.Result}");

Console.WriteLine("The end!");