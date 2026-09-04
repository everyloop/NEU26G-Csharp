
// Strängar går att indexera. De fungerar i princip som en read-only array av Char.

var text = "Hello world!"; // .ToCharArray();

// Få ut längden på text som ett heltal.
Console.WriteLine(text.Length);

// text[2] = 'A';  // Strängar är read-only och man kan alltså inte uppdatera enskilda tecken.

Console.WriteLine(text[2]);

Console.WriteLine("\nLoopa igenom bokstav för bokstav med hjälp av index:");

for (int i = 0; i < text.Length; i++)
{
    Console.Write(text[i]);

    if (i < 4)
    {
        Console.Write("-");
    }
}


Console.WriteLine("\n\nLoopa igenom bokstav för bokstav med foreach:");

foreach (char character in text)
{
    Console.WriteLine(character);
}


char myChar = '\'';

string myString = string.Empty;


// Standard string literal, startar och slutar med "

Console.WriteLine("\nDetta är \"en\" te\txt! \\n");

// Escape sequences - backslash (\) följt av vissa andra tecken har specifika betydelser
// '\n' - new line
// '\t' - tab
// '\'' - single quote (')
// '\"' - double quote (")
// '\\' - backslash (\)


Console.WriteLine();

// Interpolation string literal, startar med $ före ""
Console.WriteLine($"Detta är en {3+5} interpolation-string. Här är en single quote: {myChar}");

Console.WriteLine();

// Verbatim string literal, startar med @ före ""
Console.WriteLine(@"C:\Users\""Fredrik Johansson""\Download");

Console.WriteLine();

// Raw string literal, börjar och slutar med """
Console.WriteLine("""
for (int i = 0; i < text.Length; i++)
{
    Console.Write(text[i]);

    if (i < 4)
    {
        Console.Write("-");
    }
}
""");