
Console.WriteLine("*** Math ***\n");

Console.WriteLine($"Math.PI => {Math.PI}");
Console.WriteLine($"Math.E => {Math.E}\n");

// Absolutbelopp: Gör talet positivt.
Console.WriteLine($"Math.Abs(3) => {Math.Abs(3)}");
Console.WriteLine($"Math.Abs(-3) => {Math.Abs(-3)}\n");


// Avrundingsfunktioner
Console.WriteLine($"Math.Floor(2.731) => {Math.Floor(2.731)}");
Console.WriteLine($"Math.Ceiling(2.731) => {Math.Ceiling(2.731)}");
Console.WriteLine($"Math.Round(2.731) => {Math.Round(2.731)}");
Console.WriteLine($"Math.Truncate(2.731) => {Math.Truncate(2.731)}\n");

Console.WriteLine($"Math.Floor(-2.731) => {Math.Floor(-2.731)}");
Console.WriteLine($"Math.Truncate(-2.731) => {Math.Truncate(-2.731)}\n");

Console.WriteLine($"Math.Round(2.731, 1) => {Math.Round(2.731, 1)}");
Console.WriteLine($"Math.Round(2.731, 2) => {Math.Round(2.731, 2)}\n");


// Min, Max, Clamp
Console.WriteLine($"Math.Min(3, 7) => {Math.Min(3, 7)}");
Console.WriteLine($"Math.Max(3, 7) => {Math.Max(3, 7)}\n");
Console.WriteLine($"Math.Clamp(2, 3, 7) => {Math.Clamp(2, 3, 7)}");
Console.WriteLine($"Math.Clamp(5, 3, 7) => {Math.Clamp(5, 3, 7)}");
Console.WriteLine($"Math.Clamp(11, 3, 7) => {Math.Clamp(11, 3, 7)}\n");


int userInput = 103;
int clampedValue = Math.Clamp(userInput, 0, 100);


// Upphöjt & Kvadratrot
Console.WriteLine($"Math.Pow(10, 3) => {Math.Pow(10, 3)}");
Console.WriteLine($"Math.Sqrt(9) => {Math.Sqrt(9)}");

// Math innehåller också funktioner för bland annat trigonometri (sin, cos, tan etc..) och logaritmer (men inget ni behöver för just denna kurs).



Console.WriteLine("*** Random ***\n");

Random myRandom = new Random(); // Här har vi möjlighet att skicka in en så kallad "seed" som gör att vi alltid får samma sekvens av tal (med samma seed).

Console.WriteLine($"myRandom.Next() => {myRandom.Next()}");
Console.WriteLine($"myRandom.Next(4) => {myRandom.Next(4)}\n");

for (int i = 1; i <= 20; i++)
{
    Console.WriteLine($"myRandom.Next(4) => {myRandom.Next(1, 7)}");
}

Console.WriteLine($"\nmyRandom.Next() => {myRandom.NextDouble()}");




