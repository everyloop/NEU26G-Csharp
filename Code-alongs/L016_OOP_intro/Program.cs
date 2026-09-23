
//Cat myCat = new Cat() { name = "Pelle", age = 5 };
////myCat.name = "Pelle";
////myCat.age = 5;

//Cat myOtherCat = new Cat();
//myOtherCat.name = "Måns";
//myOtherCat.age = 6;

//Cat myThirdCat = new Cat() { name = "Bill", age = 3 };
//Cat myForthCat = new Cat() { name = "Bull", age = 3 };

//Cat[] cats = new Cat[] {
//    new Cat() { name = "Pelle", age = 5 },
//    new Cat() { name = "Måns", age = 6 },
//    new Cat() { name = "Bill", age = 3 },
//    new Cat() { name = "Bull", age = 3 },
//};


//cats[2].name = cats[3].name;

//cats[1] = null; //cats[0];

//foreach (Cat cat in cats)
//{
//    if (cat is null) continue;

//    Console.WriteLine($"{cat.name} är {cat.age} år gammal.");
//}

//Console.WriteLine(cats[0].name);


// Keyword 'new' används för skapa ett nytt objekt (en instans av en klass)
// myCat i raden nedan är inte en instans av Cat, utan en referens till ett objekt av typen Cat.
Cat myCat = new Cat() { name = "Pelle" };

// Exmepel: I detta fallet skapas inget nytt (new) objekt, utan myOtherCat pekar på samma katt som myCat
// Det spelar alltså ingen roll vilken av referenserna vi använder för att ändra eller läsa kattens namn.
Cat myOtherCat = myCat; //new Cat() { name = "Måns" };
myOtherCat.name = "hello";
Console.WriteLine(myCat.name);

myCat.Greet();
myOtherCat.Greet();
//myCat.Greet("Fredrik");
//myOtherCat.Greet("Fredrik");

myCat.Greet(myOtherCat); // Skriv en Greet(Cat cat) som gör att detta anrop fungerar. ("Hej Måns, Jag heter Pelle!")
myOtherCat.Greet(myCat);

Console.WriteLine();
myCat.Greet();
myCat.IncreaseAge();
myCat.Greet();

Console.WriteLine(Cat.DefaultNumberOfLives);
Cat.PrintNumberOfLives();

// En klass används för att definera en ny typ av objekt. Den beskriver data (fields & properties) och funktionalitet (metoder) som ett objekt har.
class Cat
{
    // Access modfiers bestämmer vilken del av koden som har åtkomst till en specifik member (t.ex metod eller field)
    // Private - Endast kod som finns internt i klass-definitionen kan komma åt denna. 
    // Public  - Denna kan kommas åt även utanför klassen.

    // Fields - en plats i minnet att lagra data. Jämförbart med en lokal variabel; men kopplad till ett objekt
    public string name = "default name";
    private int age = 1;
    public int ActualNumberOfLives;

    // Om en field (eller annan member) är markerad som "static" så hör den inte till de enskilda objektinstansterna, utan till datatypen
    public static int DefaultNumberOfLives = 9;

    // Exempel: eftersom denna metod är static så anropas den på datatypen, alltså Cat.PrintNumberOfLives; inte myCat.PrintNumberOfLives()
    public static void PrintNumberOfLives()
    {
        Console.WriteLine($"Cats have {Cat.DefaultNumberOfLives} number of lives.");
    }

    public void IncreaseAge()
    {
        age++;
    }

    // Method overloading: C# låter oss ha flera definitioner av samma metod (samma namn) med olika inparametrar.
    // ... vid anrop så väljs den definition där parametrarna matchar argumenten i anropet.
    public void Greet()
    {
        Console.WriteLine($"Hej, jag heter {name}, och jag är {age} år gammal!");
    }

    private void Greet(string name)
    {
        // Keyword 'this' referensen till det egna objektet - alltså det objekt som metoden körs på.
        Console.WriteLine($"Hej {name}, jag heter {this.name}!");
    }

    public void Greet(Cat cat)
    {
        Greet(cat.name);
        //Console.WriteLine($"Hej {cat.name}, jag heter {this.name}!");
    }
}

