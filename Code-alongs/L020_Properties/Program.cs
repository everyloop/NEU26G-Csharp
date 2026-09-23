
Person myPerson = new Person("Adam") { ShoeSize = 43.0 }; // ShoeSize är get; init; och kan sättas här när objektet först skapas ...
// myPerson.ShoeSize = 44.0; // ... men inte här, efter att objektet skapats.

//try
//{
//    myPerson.SetName("Anders Johansson");
//}
//catch (ArgumentException ex)
//{
//    Console.WriteLine(ex.Message);
//}

//Console.WriteLine(myPerson.GetName());


Console.WriteLine(myPerson.Name);

myPerson.Name = "Anders";

string name = myPerson.Name;
Console.WriteLine(name);

myPerson.Age = 18;
Console.WriteLine(myPerson.Age);

//myPerson.Length = 1.80;
Console.WriteLine(myPerson.Length);

//myPerson.Name = "Fredrik";
//Console.WriteLine(myPerson.Name);

class Person
{
    private string _name = string.Empty;

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (value.Length <= 10)
            {
                _name = value;
            }
            else
            {
                throw new ArgumentException("Name contains too many characters!");
            }
        }
    }

    // Backing field till propertyn Age, private och börjar med _
    private int _age;

    // Property (code snippet: propfull)
    public int Age
    {
        get 
        { 
            return _age; 
        }
        set 
        { 
            _age = value; 
        }
    }

    // Auto-property (code snippet: prop)
    public double Length { get; set; } = 1.85; // Auto-property har en dold backing-field, men den kan ta ett initialt värde; i detta fall 1.85

    public double Weight { get; private set; } = 85.0; // Publikt/Externt är den read-only, men internt i klassen är den read/write.

    public double ShoeSize { get; init; } // Kan bara sättas i konstruktorn eller i samband med new() - därefter är den read-only.


    public Person(string name)
    {
        _name = name;
    }

    public void SetName(string name)
    {
        if (name.Length <= 10)
        {
            _name = name;
        }
        else
        {
            throw new ArgumentException("Name contains too many characters!");
        }
    }

    public string GetName()
    {
        return _name;
    }
}