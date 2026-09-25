
// Value type
int a = 5;
int b = a;  // Kopierar värdet 5 till b
a = 3;

Console.WriteLine($"a = {a}, b = {b}\n");

// Reference type

Person personA = new Person() { name = "Adam" };
Person personB = personA;  // Koperar referensen till "Adam" till personB

// Om man ändrar namnet på objektet som personA refererar till, så är det samma objekt som personB refererar till.
personA.name = "Bertil";

// Om man däremot ändrar referensen i sig, så påverkas inte referensen personB
//personA = new Person() { name = "Bertil" };

Console.WriteLine($"personA.name = {personA.name}, personB.name = {personB.name}\n");


Console.WriteLine($"PassValuetypeByValue(a);");
PassValuetypeByValue(a);
Console.WriteLine($"=> a = {a}\n");

static void PassValuetypeByValue(int x)
{
    x += 2;
    Console.WriteLine($"x = {x}");
}

Console.WriteLine($"PassValuetypeByReference(a);");
PassValuetypeByReference(ref a);
Console.WriteLine($"=> a = {a}\n");

static void PassValuetypeByReference(ref int x)
{
    x += 2;
    Console.WriteLine($"x = {x}");
}

Console.WriteLine($"PassReferencetypeByValue(personA);");
PassReferencetypeByValue(personA);
Console.WriteLine($"=> personA.name = {personA.name}\n");

static void PassReferencetypeByValue(Person p)
{
    //p.name = "Camilla";
    p = new Person() { name = "Camilla" };
    Console.WriteLine($"p.name = {p.name}");
}

Console.WriteLine($"PassReferencetypeByReference(personA);");
PassReferencetypeByReference(ref personA);
Console.WriteLine($"=> personA.name = {personA.name}\n");

static void PassReferencetypeByReference(ref Person p)
{
    //p.name = "Camilla";
    p = new Person() { name = "Camilla" };
    Console.WriteLine($"p.name = {p.name}");
}


class Person
{
    public string name;
}