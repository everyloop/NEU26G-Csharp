
// string city = "Stockholm";

// Keyword new används för att skapa ett nytt objekt - i detta fallet en ny string[]  (string-array)
// När en string[] skapas (med new) så måste man ange längden på arrayen. (ex: new string[3], eller new string[] {"A", "B", "C"})
// Denna längd kan inte ändras senare. Man kan alltså inte lägga till eller ta bort element efter en array skapats.
// Däremot kan man ändra värde på varje enskilt element med hjälp av indexering.

string[] cities = new string[] { "Stockholm", "Göteborg", "Malmö", "Västerås", "Uppsala", "Jönköping", "Alingsås" };

// Vi kan läsa och skriva värden till enskilda element/index
cities[2] = cities[0];

// Skriver ut värdet på element 2
Console.WriteLine(cities[2]);

// Updaterar värdet på element 1
cities[1] = "Alingsås";

// Detta skriver bara ut namnet på datatypen, inte värdena på varje element.
Console.WriteLine(cities);

// String.Join() är en metod för att slå ihop alla element i en array till en enda sträng. Smidigt vid t.ex utskrift av alla array-värden.
Console.WriteLine(String.Join(", ", cities));

// Om vi vill veta längden på en array (alltså hur många element den har) kan vi använda .Length
Console.WriteLine("\nArray length: " + cities.Length);



// Om man använder en for loop och indexering, så är elementen i vår array både läs- och skrivbara.
Console.WriteLine("\nUse a for-loop to print all elements of an array:");

for (int i = 0; i < cities.Length; i++)
{
    if (cities[i] != "Alingsås")
    {
        Console.WriteLine(cities[i]);
    }
    else
    {
        Console.WriteLine("****");
        cities[i] = "xxxx";
    }
}



// Med en foreachloop kan vi bara läsa elementen, inte skriva.
Console.WriteLine("\nUse a foreach-loop to print all elements of an array:");

foreach (string city in cities)
{
    //city = "Alingsås";
    Console.WriteLine(city);
}