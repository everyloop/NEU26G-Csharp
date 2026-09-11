
Console.WriteLine("*** String methods ***\n");

string text = String.Empty;

text = "Hello world!";

int textLength = text.Length;

Console.WriteLine($"text = \"{text}\"\n");
Console.WriteLine($"text.Length => {textLength}\n");

Console.WriteLine($"text.ToLower() => {text.ToLower()}");
Console.WriteLine($"text.ToUpper() => {text.ToUpper()}");
Console.WriteLine($"text.StartsWith('H') => {text.StartsWith('H')}");
Console.WriteLine($"text.StartsWith(\"Hello\") => {text.StartsWith("Hello")}");
Console.WriteLine($"text.StartsWith(\"hello\") => {text.StartsWith("hello")}");
Console.WriteLine($"text.EndsWith('H') => {text.EndsWith('H')}");
Console.WriteLine($"text.Contains(\"wor\") => {text.Contains("wor")}");
Console.WriteLine($"text.Replace('l', '*') => {text.Replace('l', '*')}");
Console.WriteLine($"text.Replace(\"world\", \"there\") => {text.Replace("world", "there")}");
Console.WriteLine($"text.Remove(4) => {text.Remove(4)}");
Console.WriteLine($"text.Remove(4, 3) => {text.Remove(4, 3)}");
Console.WriteLine($"text.Substring(4) => {text.Substring(4)}");
Console.WriteLine($"text.Substring(4, 3) => {text.Substring(4, 3)}");
Console.WriteLine($"text.Insert(6, \"my \") => {text.Insert(6, "my ")}");
Console.WriteLine($"text.IndexOf('o') => {text.IndexOf('o')}");
Console.WriteLine($"text.IndexOf(\"world\") => {text.IndexOf("world")}");
Console.WriteLine($"text.LastIndexOf('o') => {text.LastIndexOf('o')}");
Console.WriteLine($"text.LastIndexOf(\"world\") => {text.LastIndexOf("world")}");

text = "          Hello world!            ";
Console.WriteLine($"\ntext = \"{text}\"\n");

Console.WriteLine($"text.Trim() => \"{text.Trim()}\"");
Console.WriteLine($"text.TrimStart() => \"{text.TrimStart()}\"");
Console.WriteLine($"text.TrimEnd() => \"{text.TrimEnd()}\"");

text = "This is a sample text";
Console.WriteLine($"\ntext = \"{text}\"\n");

Console.WriteLine($"text.Split(' ') => {text.Split(' ')}\n");

string[] words = text.Split(' ');

foreach (string word in words)
{
    Console.WriteLine(word);
}

Console.WriteLine($"\nString.Join(' ', words)}} => \"{String.Join(' ', words)}\"");
Console.WriteLine($"String.Join('-', words)}} => \"{String.Join('-', words)}\"");
Console.WriteLine($"String.Join(\", \", words)}} => \"{String.Join(", ", words)}\"");


Console.WriteLine("\n\n *** Char methods ***\n");

char myChar = 'A';

Console.WriteLine($"\nmyChar = '{myChar}'\n");

Console.WriteLine($"Char.IsLower(myChar)}} => {Char.IsLower(myChar)}");
Console.WriteLine($"Char.IsUpper(myChar)}} => {Char.IsUpper(myChar)}\n");

Console.WriteLine($"Char.ToLower(myChar)}} => {Char.ToLower(myChar)}");
Console.WriteLine($"Char.ToUpper(myChar)}} => {Char.ToUpper(myChar)}\n");

Console.WriteLine($"Char.IsDigit('R')}} => {Char.IsDigit('R')}");
Console.WriteLine($"Char.IsDigit('7')}} => {Char.IsDigit('7')}");
Console.WriteLine($"Char.IsDigit('%')}} => {Char.IsDigit('%')}\n");

Console.WriteLine($"Char.IsLetter('R')}} => {Char.IsLetter('R')}");
Console.WriteLine($"Char.IsLetter('7')}} => {Char.IsLetter('7')}");
Console.WriteLine($"Char.IsLetter('%')}} => {Char.IsLetter('%')}\n");

Console.WriteLine($"Char.IsLetterOrDigit('R')}} => {Char.IsLetterOrDigit('R')}");
Console.WriteLine($"Char.IsLetterOrDigit('7')}} => {Char.IsLetterOrDigit('7')}");
Console.WriteLine($"Char.IsLetterOrDigit('%')}} => {Char.IsLetterOrDigit('%')}\n");

Console.WriteLine($"Char.IsWhiteSpace(' ')}} => {Char.IsWhiteSpace(' ')}");
Console.WriteLine($"Char.IsWhiteSpace('\\t')}} => {Char.IsWhiteSpace('\t')}");
Console.WriteLine($"Char.IsWhiteSpace('\\n')}} => {Char.IsWhiteSpace('\n')}");










