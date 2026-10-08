

string myString = "hello WORLD!";


Console.WriteLine(myString);

Console.WriteLine(StringMethods.Title(myString));

Console.WriteLine(myString.Title().Duplicate(3));

Console.WriteLine("X".Duplicate(4));

static class StringMethods
{
    public static string Title(this string text)
    {
        return text[0].ToString().ToUpper() + text.Substring(1).ToLower();
    }

    public static string Duplicate(this string text, int times)
    {
        string newText = string.Empty;

        for (int i = 0; i < times; i++)
        {
            newText += text;
        }
        return newText;
    }
}