
// Dictonary<TKey, TValue> är en generisk collection av key-value-pairs.
// I detta exempel skapar vi en dictionary där både nyckel och värde har datatypen string:
var myDictionary = new Dictionary<string, string>();

myDictionary.Add("boy", "pojke");
myDictionary.Add("girl", "flicka");
myDictionary.Add("man", "man");
myDictionary.Add("woman", "kvinna");

// Försöker vi lägga till samma nyckel som redan existerar så får vi en
// System.ArgumentException: 'An item with the same key has already been added.
// myDictionary.Add("man", "man");

// Iterera över nycklarna:
Console.WriteLine("Keys:");

foreach (string key in myDictionary.Keys)
{
    Console.WriteLine(key);
}


// Iterera över värdena:
Console.WriteLine("\nValues:");

foreach (string value in myDictionary.Values)
{
    Console.WriteLine(value);
}

// Iterera över nyckel-värde-par:
Console.WriteLine("\nKeys with values:");

foreach (KeyValuePair<string, string> keyValuePair in myDictionary)
{
    Console.WriteLine($"The key {keyValuePair.Key} holds the value {keyValuePair.Value}");
}



// Vi kan använda myDictionar[key] för att få ut ett värde; men om key inte existerar i vår dictionary
// så kastas en KeyNotFoundException: 'The given key 'fail' was not present in the dictionary.'
// string s = myDictionary["fail"];


// Loopen nedan använder .ContainsKey för att kolla om nyckeln som användaren matar in finns i myDictionary:

Console.WriteLine("\nEnter keys to look up in the dictionary:");

string input;

do
{
    input = Console.ReadLine();

    if (myDictionary.ContainsKey(input))
    {
        Console.WriteLine($"myDictionary[\"{input}\"] => {myDictionary[input]}");
    }
    else if (input != string.Empty)
    {
        Console.WriteLine($"myDictionary[\"{input}\"] => Nyckeln saknas.");
    }

} while (input != string.Empty);


