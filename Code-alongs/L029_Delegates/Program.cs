
// Exempel på hur vi skapar en ny instans myDelegate och pekar den på en funktion CountWords (som tar in en string, returnerar int)
CounterDelegate myDelegate = CountWords; // new MyDelegate(CountWords); <== Alternativ syntax.

// Här använder vi en egendefinerad generisk delegattyp; och kan då välja datatyperna - i detta fall en string in och en int ut.
MyGenericDelegate<string, int> myGenericDelegate = CountWords;

// Normalt definerar vi dock INTE egna generiska delegat, utan använder de inbyggda: Action<>, eller Func<>
Func<string, int> myBuiltInGenericDelegate = CountWords;



// Exempel på hur vi använder de inbyggda generiska Action<> och Func<> för att matcha olika funktioners signatur:
Action<string> exampleA = ExampleA;
Func<string, int> exampleB = ExampleB;
Action<string, char, bool> exampleC = ExampleC;
Action exampleD = ExampleD;
Func<string> exampleE = ExampleE;
Func<string, string, double, double, char> exampleF = ExampleF;
Action<string, string, double, double, char> exampleG = ExampleG;

static void ExampleA(string s) { };
static int ExampleB(string s) { return 0; }
static void ExampleC(string s, char c, bool b) { /*return false;*/ }
static void ExampleD() { Console.WriteLine("Hello"); };
static string ExampleE() { return string.Empty; }
static char ExampleF(string s1, string s2, double d1, double d2) { return ' '; }
static void ExampleG(string s1, string s2, double d1, double d2, char c) { }

// Vi kan nu göra ett anrop som om myDelegate var en faktisk funktion - den anropar då den funktion som myDelegate refererar till.
int result = myDelegate("Hello world!");
Console.WriteLine(result);
Console.WriteLine();




string[] strings = new string[] { "Hello world", "This is a SAMPLE text", "Yet anoter text" };

// Med hjälpa av delegat kan samma metod användas med olika beteende beroende på vilken funktion vi skickar in som (ex. andra) parameter.
ProcessAndPrintStrings(strings, CountWords);
Console.WriteLine();
ProcessAndPrintStrings(strings, CountChars);
Console.WriteLine();
ProcessAndPrintStrings(strings, CountUppercaseLetters);




// Delegat tillåter oss att skicka in beteende/funktionalitet till en metod - tidigare har vi endast skickat in data.
static void ProcessAndPrintStrings(string[] strings, Func<string, int> counterMethod)
{
    foreach (var text in strings)
    {
        // Istället för att hårdkoda vilken funktion som ska köras, så anropar vi counterMethod: funktionen som skickats in vid anrop.
        Console.WriteLine($"{text} => {counterMethod(text)}");
    }
}





// OM vi inte haft delegat hade vi istället fått upprepa hela metoden enligt nedan,
// med enda skillnaden vilken metod vi använder för att räkna. Detta vill vi unvika - DRY (Don't repeat yourself).

//CountCharsAndPrintStrings(strings);
//Console.WriteLine();
//CountWordsAndPrintStrings(strings);

//static void CountCharsAndPrintStrings(string[] strings)
//{
//    foreach (var text in strings)
//    {
//        Console.WriteLine($"{text} => {CountChars(text)}");
//    }
//}

//static void CountWordsAndPrintStrings(string[] strings)
//{
//    foreach (var text in strings)
//    {
//        Console.WriteLine($"{text} => {CountWords(text)}");
//    }
//}

// ... samt ytterligare upprepningar för varje sätt vi har att räkna - ex. CountUppercaseLetters.



static int CountChars(string text)
{
    return text.Length;
}

static int CountWords(string text)
{
    return text.Split(' ').Length;
}

static int CountUppercaseLetters(string text)
{
    int count = 0;

    foreach (var myChar in text)
    {
        if (Char.IsUpper(myChar)) count++;
    }

    return count;
}



// Delegat är en typdefinition: en referens till en metod/funktion.
// Delegatet CounterDelegate är en referens till en funktion som tar en string som parameter, och returnerar en int.
public delegate int CounterDelegate(string s);

public delegate bool AnotherDelegate(string s1, string s2);

// Här har vi gjort ett eget generiskt delegat enbart i syfte att repetera generiska metoder och för att förstå hur generiska delegat fungerar ...
public delegate TResult MyGenericDelegate<T, TResult>(T data);

// ... normalt definierar man INTE sina egna generiska delegat, utan använder de inbyggda Action<> och Func<>.