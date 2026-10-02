

var myDictionary = new Dictionary<string, string>();

myDictionary.Add("boy", "pojke");
myDictionary.Add("girl", "flicka");
myDictionary.Add("man", "man");
myDictionary.Add("woman", "kvinna");

// Försöker vi lägga till samma nyckel som redan existerar så får vi en exception:
// myDictionary.Add("man", "man");

Console.WriteLine("Keys:");

foreach (string key in myDictionary.Keys)
{
    Console.WriteLine(key);
}

Console.WriteLine("\nValues:");

foreach (string value in myDictionary.Values)
{
    Console.WriteLine(value);
}

Console.WriteLine("\nKeys with values:");

foreach (KeyValuePair<string, string> keyValuePair in myDictionary)
{
    Console.WriteLine($"The key {keyValuePair.Key} holds the value {keyValuePair.Value}");
}

Console.WriteLine();

// Vi kan använda myDictionar[key] för att få ut ett värde; men om key inte existerar i vår dictionary kastas en exception.

string input;

do
{
    input = Console.ReadLine();

    if (myDictionary.ContainsKey(input))
    {
        Console.WriteLine($"myDictionary[\"{input}\"] => {myDictionary[input]}");
    }
    else
    {
        Console.WriteLine("Nyckeln saknas.");
    }
} while (input != string.Empty);


