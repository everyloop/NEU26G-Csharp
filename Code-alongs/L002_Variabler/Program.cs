
// Variables (variabler) - En behållare för att lagra data (av en given typ).

// I C# måste variabler deklareras innan de kan användas.
// Syntax för deklaration av variabel: [Datatyp] [Indentifer]

int x;
int y = 3;
double z = 3.45;
char letterA = 'A';         // Char literal values omges av single quotes '
string name = "Fredrik";    // String literal values omges av double quotes "
bool isEnabled = true;


// Literal values
// t.ex 3, 3.35, 'A', "Fredrik", true

// literal values kan använda sig av suffix för att avgöra typen på det 'hårdkodad' värdet:
// t.ex: 1.0f är float-värde, medan 1.0 är double-värde.


// Data types (datatyper) - Anger vilken typ av data vi lagrar i t.ex en variabel.

// byte b = 300; Exempel på ett värde som ligger utanför datatypens "range". 
short s = 300;
// byte b = -100; Byte är en UNSIGNED integer, och kan därför inte lagra negativa tal.
sbyte sb = -100;

// C# är ett statiskt typat språk:
// Innan koden kompileras så måste datatypen för varje enskild variabel vara känd.

// C# är ett starkt typat språk:
// Om man försöker sätta in data av felaktig typ i en variabel så resulterar det i ett kompileringsfel.
//name = 3;

// C# är ett typsäkert språk:
// Man kan bara utföra operationer som en given datatyp tillåter
Console.WriteLine(y + 3);
//Console.WriteLine(isEnabled + 3); // <- Otillåten operation mellan bool och int.


// Implicit (underförstått)
// Explict (uttryckligen)

// Explicit typad variabel - Vi har uttryckligen i koden angett att datatype på myInt är 'int'.
int myInt;

// Implicit typad variabel - Med keyword 'var' så avgör kompilatorn datatyp (utifrån den initialt tilldelade värdet)
var myVariable = 3.24f;


// Scope - En variabels (ett objekts) livstid (lifetime)
// Tre nivåer: Class-scope, Method-scope & Block-scope

bool myBool = true;

if (isEnabled)
{
    bool myOtherBool = true;

    if (true)
    {
        bool myThirdBool = true;
        Console.WriteLine(myBool);
        Console.WriteLine(myOtherBool);
        Console.WriteLine(myThirdBool);
    }

    if (true)
    {
        bool myThirdBool = true;
        Console.WriteLine(myBool);
        Console.WriteLine(myOtherBool);
        Console.WriteLine(myThirdBool);
    }

    Console.WriteLine(myBool);
    Console.WriteLine(myOtherBool);
    Console.WriteLine(myThirdBool);
}

Console.WriteLine(myBool);
Console.WriteLine(myOtherBool);
Console.WriteLine(myThirdBool);

