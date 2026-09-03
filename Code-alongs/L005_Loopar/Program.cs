
// WHILE-loop körs så länge den utvärderas till true - noll, en eller flera gånger.

//string name = string.Empty;

//while (name != "fredrik")
//{
//    Console.WriteLine("Gissa ditt namn: ");
//    name = Console.ReadLine();
//}

//Console.WriteLine("Yay! Du gissade rätt!");



// DO-WHILE-loop kör så länge den utvärders till true, men alltid minst en gång.

//string name = string.Empty;
//do
//{
//    Console.WriteLine("Gissa ditt namn: ");
//    name = Console.ReadLine();
//} while (name != "fredrik");

//Console.WriteLine("Yay! Du gissade rätt!");


Console.WriteLine("\nExempel som använder WHILE som räknar 1 till 5");

int j = 1;

while (j <= 5)
{
    Console.WriteLine(j);
    j++;
}


Console.WriteLine("\nExempel som använder FOR som räknar 1 till 5");

for (int i = 1; i <= 5; i++)
{
    Console.WriteLine(i);
}


Console.WriteLine("\nExempel med BREAK");

for (int i = 1; i <= 10; i++)
{
    if (i == 5)
    {
        break;
    }
    
    Console.WriteLine(i);
}

Console.WriteLine("\nExempel med CONTINUE");

for (int i = 1; i <= 10; i++)
{
    Console.WriteLine(i);
    
    if (i == 5 || i == 8)
    {
        continue;
    }

}


Console.WriteLine("The End!");

Console.WriteLine("\nExempel med nested/nästlad loop");

for (int y = 1; y <= 5; y++)
{
    for (int x = 1; x <= 9; x++)
    {
        Console.Write(x);
    }
    Console.WriteLine();
}
