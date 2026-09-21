
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


Cat myCat = new Cat() { name = "Pelle" };
Cat myOtherCat = new Cat() { name = "Måns" };


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

class Cat
{
    public string name = "default name";
    private int age = 1;

    public static int DefaultNumberOfLives = 9;

    public int ActualNumberOfLives;
    public static void PrintNumberOfLives()
    {
        Console.WriteLine($"Cats have {Cat.DefaultNumberOfLives} number of lives.");
    }

    public void IncreaseAge()
    {
        age++;
    }

    public void Greet()
    {
        Console.WriteLine($"Hej, jag heter {name}, och jag är {age} år gammal!");
    }

    private void Greet(string name)
    {
        Console.WriteLine($"Hej {name}, jag heter {this.name}!");
    }

    public void Greet(Cat cat)
    {
        Greet(cat.name);
        //Console.WriteLine($"Hej {cat.name}, jag heter {this.name}!");
    }

}

