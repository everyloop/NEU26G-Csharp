List<Person> data = new List<Person>
{
    new Person() { FirstName = "Maria", LastName = "Johansson", Age = 24, City = "Göteborg", Salary = 32000 },
    new Person() { FirstName = "Erik", LastName = "Andersson", Age = 31, City = "Stockholm", Salary = 45000 },
    new Person() { FirstName = "Anna", LastName = "Nilsson", Age = 45, City = "Malmö", Salary = 38500 },
    new Person() { FirstName = "Karl", LastName = "Karlsson", Age = 28, City = "Uppsala", Salary = 29000 },
    new Person() { FirstName = "Linnéa", LastName = "Bergqvist", Age = 24, City = "Göteborg", Salary = 42000 },
    new Person() { FirstName = "Johan", LastName = "Lindqvist", Age = 52, City = "Västerås", Salary = 51000 },
    new Person() { FirstName = "Emma", LastName = "Svensson", Age = 19, City = "Örebro", Salary = 24500 },
    new Person() { FirstName = "Fredrik", LastName = "Larsson", Age = 42, City = "Stockholm", Salary = 63000 },
    new Person() { FirstName = "Sara", LastName = "Olsson", Age = 38, City = "Helsingborg", Salary = 37000 },
    new Person() { FirstName = "David", LastName = "Persson", Age = 27, City = "Linköping", Salary = 33500 },
    new Person() { FirstName = "Elin", LastName = "Gustafsson", Age = 33, City = "Jönköping", Salary = 41000 },
    new Person() { FirstName = "Mikael", LastName = "Eriksson", Age = 61, City = "Umeå", Salary = 48000 },
    new Person() { FirstName = "Sofia", LastName = "Mattsson", Age = 29, City = "Norrköping", Salary = 36000 },
    new Person() { FirstName = "Andreas", LastName = "Wallin", Age = 48, City = "Gävle", Salary = 44500 },
    new Person() { FirstName = "Amanda", LastName = "Holm", Age = 24, City = "Göteborg", Salary = 31000 }
};

Console.WriteLine("data:");

foreach (var person in data)
{
    Console.WriteLine(person);
}

Console.WriteLine();

// Hur många är från Göteborg?
Console.WriteLine($"data.Count(p => p.City == \"Göteborg\") => {data.Count(p => p.City == "Göteborg")}");

Console.WriteLine();

// Är någon person från Alingsås?
Console.WriteLine($"data.Any(p => p.City == \"Alingsås\") => {data.Any(p => p.City == "Alingsås")}");

Console.WriteLine();

// Är alla personer 10 år eller äldre?
Console.WriteLine($"data.All(p => p.Age >= 10) => {data.All(p => p.Age >= 10)}");


// Letar upp den första personen (i 'data') som matchar kriteriet (äldre än 90 år); kastar exception om ingen sådan person existerar.
//Person firstPerson = data.First(p => p.Age > 90);

// Letar upp den första personen (i 'data') som matchar kriteriet (äldre än 90 år);
// Väljer default-värdet om den inte hittar en sådan person.
// Default är normalt null för reference types, men vi kan välja en egen default om vi anger andra parametern, som nedan:
Person firstOrDefaultPerson = data.FirstOrDefault(p => p.Age > 50, new Person() { FirstName = "Fredrik", LastName = "Johansson" });

// Motsvarande metoder finns som letar bakifrån i listan istället:
Person lastPerson = data.Last(p => p.Age > 50);
Person? lastOrDefaultPerson = data.LastOrDefault(p => p.Age > 90);

// Använd .Single() när vi förväntar oss att det bara finns en match. Exempel: personnummer eller productId.
// .Single() kastar en exception om vi inte får EXAKT 1 träff.
//Person singlePerson = data.Single(p => p.Age == 62);

// .SingleOrDefault kastar också exception om det finns mer än 1 objekt som matchar; men ger oss default om inget objekt matchar.
Person? singleOrDefaultPerson = data.SingleOrDefault(p => p.Age == 61);

Console.WriteLine();


Console.WriteLine("\n******************\n");


//var newData = data.Where(p => p.Age < 30).ToList();
//var newData = data.Where(p => p.City == "Göteborg" && p.Age < 30 ).ToList();
//var newData = data.Where(p => p.FirstName.Length == 4).ToList();
//var newData = data.Where(p => p.Age <= 20 || p.Age >= 50).ToList();
var newData = data
    .Where(p => p.FirstName.StartsWith("e", StringComparison.CurrentCultureIgnoreCase)) // Filtrering - Vilka rader ska med?
    .Select(p => new { FullName = $"{p.FirstName} {p.LastName}", Age = p.Age })         // Tranformation - Hur ska data se ut?
    .OrderBy(p => p.FullName)                                                           // Sortering - Vilken ordning visas data
    .ThenByDescending(p => p.Age)
    .ToList();

Console.WriteLine("newData:");

foreach (var person in newData)
{
    Console.WriteLine(person);
}

Console.WriteLine("\n******************\n");

List<object> objects = new List<object> { 5, "Hello", true, "Hej", "Bye!", 'A', 5.0 };

var strings = objects.OfType<string>().ToList();

Console.WriteLine();

// elements.OfType<Enemy>().Any(e => e.Hp > 5)


// Deferred execution means that the evaluation of an expression is delayed until its realized value is actually required.
Console.WriteLine("\n*** Demonstration of Deferred Execution ****\n");

List<int> numbers = new List<int> { 4, 6, 9, 10, 13 };

var filteredNumbers = numbers.Where(n => n < 10).OrderBy(i => i);

foreach (var number in filteredNumbers)
{
    Console.WriteLine(number);
}

Console.WriteLine();

numbers.Add(7);

foreach (var number in filteredNumbers)
{
    Console.WriteLine(number);
}


class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
    public int Salary { get; set; }

    public override string ToString()
    {
        return $"Firstname = {FirstName}, LastName = {LastName}, Age = {Age}, City = {City}, Salary = {Salary}";
    }
}


