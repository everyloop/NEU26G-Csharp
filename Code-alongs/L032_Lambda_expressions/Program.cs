
using System.Diagnostics.CodeAnalysis;

Func<int, int> square = x => x * x;

Func<int, string> myFunc = x => x.ToString();

Action<int> printInt = x => Console.WriteLine(x);


printInt(square(3));

static int Square(int x)
{
    return x * x;
}

static void PrintInt(int i)
{
    Console.WriteLine(i);
}

static string IntToString(int x)
{
    return x.ToString();
}

// Delegate + Lambda
Func<Person, int, bool> isLegal = (person, legalAge) => person.Age >= legalAge;

// Samma funktionalitet uttryckt med en vanlig funktion.
static bool IsLegal(Person person, int legalAge)
{
    return person.Age >= legalAge;
}

// Skapa en instans av en person
var myPerson = new Person() { Age = 20 };

// ... och anropa funktionen ovan
Console.WriteLine(isLegal(myPerson, 18));
Console.WriteLine(IsLegal(myPerson, 18));


Func<int, int, int, string> add = (a, b, c) => $"{a} + {b} + {c} = {a + b + c}";
Console.WriteLine(add(1,2,3));

Func<double, double, double> volumeOfCylinder = (r, h) => Math.PI * r * r * h;
Console.WriteLine(volumeOfCylinder(3, 4));

static string stringAdd(int a, int b, int c)
{
    return $"{a} + {b} + {c} = {a+b+c}";
}


// Lambda är framförallt användbart när vi vill skicka in beteende/funktionalitet till en funktion.
// Exempel: Funktionen nedan skriver ut talen 1 till 10 och beräknar ett värde för varje tal.
//          Istället för att hårdkoda VILKEN beräkning vi ska göra i funktionen, så gör vi så att funktionen
//          tar in en parameter 'func' som låter anroparen av metoden skicka in en funktion som gör varje beräkning. 
static void PrintResults(Func<int, int> func)
{
    for (int i = 1; i <= 10; i++)
    {
        Console.WriteLine($"{i}: {func(i)}");
    }
}

// Nu kan vi använda lambda-uttryck när vi anropar PrintResults för att bestämma vilket beräkning som ska göras.
Console.WriteLine();

PrintResults(x => x * 5 + 100);

Console.WriteLine();

PrintResults(x => x % 2);

Console.WriteLine();

PrintResults(n =>
{
    int sum = 0;

    for (int i = 1; i <= n; i++)
    {
        sum += i;
    }
    return sum;
});

Console.WriteLine();

// Vi skulle också kunna skicka in en vanlig funktion så länge den matchar parameterns delagat (Func<int, int>)
// ... men då tappar vi oftast smidigheten som kommer av att använda lambda för att ange funktionalitet on-the-fly.
PrintResults(MultiplyBy3);

static int MultiplyBy3(int i)
{
    return i * 3;
}

class Person
{
    public int Age { get; set; }
}