

var countries = new List<string> { "Sweden", "Denmark", "Norway" };

// List<T> har en property .Count som fungerar likadant som .Length på arrayer.
Console.WriteLine($"\nNumber of countries: {countries.Count}");

// Ta bort alla objekt på listan:
// countries.Clear();

// Lägg till ett objekt på listan:
countries.Add("Finland");
countries.Add("Germany");

// Ta bort ett specifik objekt:
countries.Remove("Norway");

// Ta bort ett objekt på angivet index:
countries.RemoveAt(2);

// Tar bort 2 objekt från index 1 och framåt:
countries.RemoveRange(1, 2);

// Vi kan använda foreach eller for-loopar på samma sätt som vi gjort med arrayer.
foreach (var country in countries)
{
    Console.WriteLine(country);
}

string myCountry = "Sweden";
Console.WriteLine($"\ncountries.Contains(\"{myCountry}\") => {countries.Contains(myCountry)}");


Console.WriteLine($"\ncountries.Capacity => {countries.Capacity}");


Console.WriteLine();


// Internt så lagarar List<T> alla items i en array.

var numbers = new List<int>();

// Storleken på arrayen kan sättas, och läsas av med propertyn Capacity.

// Om man lägger till (Add) objekt när den interna arrayen inte är tillräckligt stor så dubblas capacity.
// ... det innebär att den skapar en ny, större, array och kopierar över befintliga värden.

// Om man vet med sig att man ska lägga in många objekt med en gång kan man sätta Capacity i förväg
// för att undvika onödiga om allokeringar av minne och förflyttning av data.
//numbers.Capacity = 40;

for (int i = 0; i < 40; i++)
{
    Console.WriteLine($"Count: {numbers.Count}, Capacity: {numbers.Capacity}");
    numbers.Add(42);
}


// Man kan även i efterhand trimma ner storleken på arrayen så inte överflödiga minnesplatser allokeras i onödan.
numbers.TrimExcess();
Console.WriteLine($"Count: {numbers.Count}, Capacity: {numbers.Capacity}");

// TrimExess() är i princip samma sak som:
//numbers.Capacity = numbers.Count;

// Vi kan sätta Capacity till valfritt värde:
numbers.Capacity += 5;

numbers.Add(42);
Console.WriteLine($"Count: {numbers.Count}, Capacity: {numbers.Capacity}");


// I bakgrunden används Array.Resize.

int[] myArray = numbers.ToArray();
Array.Resize(ref myArray, 10);
Array.Resize(ref myArray, 20);
Console.WriteLine();

