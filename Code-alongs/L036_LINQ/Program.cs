List<Person> data = new List<Person>
{
    new Person() { FirstName = "Maria", LastName = "Johansson", Age = 24, City = "Göteborg", Salary = 32000 },
    new Person() { FirstName = "Erik", LastName = "Andersson", Age = 31, City = "Stockholm", Salary = 45000 },
    new Person() { FirstName = "Anna", LastName = "Nilsson", Age = 45, City = "Malmö", Salary = 38500 },
    new Person() { FirstName = "Karl", LastName = "Karlsson", Age = 28, City = "Uppsala", Salary = 29000 },
    new Person() { FirstName = "Linnéa", LastName = "Bergqvist", Age = 35, City = "Göteborg", Salary = 42000 },
    new Person() { FirstName = "Johan", LastName = "Lindqvist", Age = 52, City = "Västerås", Salary = 51000 },
    new Person() { FirstName = "Emma", LastName = "Svensson", Age = 19, City = "Örebro", Salary = 24500 },
    new Person() { FirstName = "Fredrik", LastName = "Larsson", Age = 42, City = "Stockholm", Salary = 63000 },
    new Person() { FirstName = "Sara", LastName = "Olsson", Age = 38, City = "Helsingborg", Salary = 37000 },
    new Person() { FirstName = "David", LastName = "Persson", Age = 27, City = "Linköping", Salary = 33500 },
    new Person() { FirstName = "Elin", LastName = "Gustafsson", Age = 33, City = "Jönköping", Salary = 41000 },
    new Person() { FirstName = "Mikael", LastName = "Eriksson", Age = 61, City = "Umeå", Salary = 48000 },
    new Person() { FirstName = "Sofia", LastName = "Mattsson", Age = 29, City = "Norrköping", Salary = 36000 },
    new Person() { FirstName = "Andreas", LastName = "Wallin", Age = 48, City = "Gävle", Salary = 44500 },
    new Person() { FirstName = "Amanda", LastName = "Holm", Age = 26, City = "Göteborg", Salary = 31000 }
};


//var newData = data.Where(p => p.Age < 30).ToList();
//var newData = data.Where(p => p.City == "Göteborg" && p.Age < 30 ).ToList();
//var newData = data.Where(p => p.FirstName.Length == 4).ToList();
//var newData = data.Where(p => p.Age <= 20 || p.Age >= 50).ToList();
var newData = data.Where(p => p.FirstName.StartsWith("e", StringComparison.CurrentCultureIgnoreCase)).ToList();

Console.WriteLine("newData:");

foreach (var person in newData)
{
    Console.WriteLine(person);
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