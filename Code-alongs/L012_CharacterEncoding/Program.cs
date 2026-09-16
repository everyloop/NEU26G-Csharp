
using System.Text;

static string GetSurrogateType(char c)
{
    if (char.IsLowSurrogate(c)) return "Low Surrogate";
    if (char.IsHighSurrogate(c)) return "High Surrogate";
    return "No Surrogate";
}

Console.OutputEncoding = Encoding.Unicode;

char ZeroWidthJoiner = '\u200D';

string text = "👨‍👩‍👧‍👦";
Console.WriteLine(text.Length);
Console.WriteLine(text.Substring(0,8));

// char myChar = 'Ֆ';

// char myChar = '\u0556';


//Console.WriteLine($"{(int)myChar} {myChar}");

//for (int i = 97; i <= 122; i++)
//{
//    Console.Write((char)i);
//}

//string text = "He\u0556llo😀";

//Console.WriteLine(text.Length);

foreach (char c in text)
{
    Console.WriteLine($"{(int)c} {c} {GetSurrogateType(c)}");
}


