
using System.Text.Json;
using System.Text.Json.Serialization;

var people = new Person[] 
{
    new Person("Maria", "Andersson", 23) { FavoriteColor = ConsoleColor.Red},
    new Person("Peter", "Svensson", 28),
    new Person("Sven", "Petersson", 31),
};


people[0].BestFriend = people[1];
people[1].BestFriend = people[0];

//var colors = Enum.GetValues<ConsoleColor>();

var options = new JsonSerializerOptions()
{
    WriteIndented = true,
    Converters = { new JsonStringEnumConverter() },
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    IgnoreReadOnlyProperties = false,
    IncludeFields = true,
    ReferenceHandler = ReferenceHandler.Preserve
};

string json = JsonSerializer.Serialize(people, options);

Console.WriteLine(json);

File.WriteAllText("people.json", json);


class Person
{
    public Person(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }

    public string myField = "test";

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; }

    public ConsoleColor? FavoriteColor { get; set; }

    public Person? BestFriend { get; set; } = null;
}