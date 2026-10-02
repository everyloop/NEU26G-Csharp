
int x = 5;
int y = 2;

Console.WriteLine($"x = {x}, y = {y}");

Swap(ref x, ref y);

Console.WriteLine($"x = {x}, y = {y}");


string textA = "hello";
string textB = "world";

Console.WriteLine($"\ntextA = {textA}, textB = {textB}");

Swap(ref textA, ref textB);

Console.WriteLine($"textA = {textA}, textB = {textB}");

static void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}

Console.WriteLine(AreAllElementsEqual(new int[] { 2, 2, 4, 2, 2 } ));
Console.WriteLine(AreAllElementsEqual(new char[] { '2', '2', '2', '2', '2' }));
Console.WriteLine(AreAllElementsEqual(new string[] { "hello", "hello", "hello" }));

static bool AreAllElementsEqual<T>(T[] array)
{
    for (int i = 0; i < array.Length; i++)
    {
        if (!array[i].Equals(array[0])) return false;
    }

    return true;
}






//var cage = new Cage<Bird, Cat>();

//cage.InhabitantA = new Bird();
//cage.InhabitantB = new Cat();

//class Cat { }
//class Bird { }

//class Cage<T1, T2>
//{
//    public T1 InhabitantA { get; set; }
//    public T2 InhabitantB { get; set; }
//}