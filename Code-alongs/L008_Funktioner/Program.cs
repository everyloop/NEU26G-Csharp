
static void FunctionA()
{
    Console.WriteLine("Start of FunctionA.");
    FunctionB();
    Console.WriteLine("End of FunctionA.");
}

static void FunctionB()
{
    Console.WriteLine("Start of FunctionB.");
    FunctionC();
    Console.WriteLine("End of FunctionB.");
}

static void FunctionC()
{
    Console.WriteLine("This is FunctionC.");
    FunctionA();
}

//Console.WriteLine("This is the first statement.");
//Console.WriteLine("This is the second statement.");

//FunctionA();

//Console.WriteLine("The End!");

// En funktion kan ta 0, 1 eller flera (in-)parametrar.
// Parametrar skrivs kommaseparerade i parantesen efter funktionsnamnet.
// Varje parameter har en datatyp (Datatypen anges alltså per parameter).

// En funktion-signatur består av namnet på funktioen följt av (eventuella) parametrar i parantes.
// Man kan ange att en parameter är "optional" genom att ge den ett default-värde i signaturen.

static void MyFunction(string text = "Default", int count = 1, int numberOfExlamationMarks = 1)
{
    string exclamationMark = new string('!', numberOfExlamationMarks);

    //for (int i = 0; i < numberOfExlamationMarks; i++)
    //{
    //    exclamationMark += "!";
    //}

    for (int i = 0; i < count; i++)
    {
        Console.WriteLine($"Hello, {text}{exclamationMark}");
    }

}

// Indata vid funktionsanrop kalla för argument.
// Argumenten måste matcha funktionens parametrar i antal och datatyper.
// Argument skickas normal i samma ordning som parametrarna är angivna i funktionens signatur.

MyFunction();
MyFunction("Fredrik"); 
MyFunction("Johansson", 5);
MyFunction("Kalle", 2, 5);

// Man kan också använda "named arguments" för att ange argument i en annan ordning.

MyFunction(numberOfExlamationMarks: 3, text: "Hej");


// En funktion kan returnera 0 eller 1 objekt.
// Man anger datatypen på objektet som returneras före funktionsnamnet.
// Alternativ använd "void" om funktionen inte returnerar ett värde.


Console.WriteLine();

static int AddNumbers(int a, int b)
{
    if (a <= 10)
    {
        int c = a / b;
        return c;
    }
    else
    {
        return -1;
    }
}

int returnValue = AddNumbers(15, 3);
Console.WriteLine(returnValue);

Console.WriteLine(AddNumbers(10, 5));



