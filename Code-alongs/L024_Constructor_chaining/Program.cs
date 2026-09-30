
// CONSTRUCTOR CHAINING (Konstruktor-kedjning vid arv)
// Syfte: Säkerställer att ett objekts tillstånd byggs upp på ett säkert och
//        förutsägbart sätt, hela vägen från basen och ner till subklassen.

// Regler:
// 1) När en subklass instansieras bubblar anropen först UPP till toppen av
//    arvskedjan (ända till System.Object) innan något kodblock hinner exekveras.
// 2) Kodblocken körs NEDÅT, uppifrån och ner (Bas -> Subklass).
// 3) Processen sker antingen IMPLICIT (kompilatorn skjuter automatisk in
//    ett dolt ':base()' mot en parameterlös konstruktor) eller EXPLICIT
//    via nycklelorden ': base(...)' eller ': this(...)' 

using System.Runtime.CompilerServices;
using System.Xml.Linq;

Console.WriteLine("Start!");
new Dog();

class Animal
{
    public Animal()
    {
        Console.WriteLine("Running Animal constructor...");
    }
}

class Mammal : Animal
{
    public Mammal()
    {
        Console.WriteLine("Running Mammal constructor...");
    }
}

class Dog : Mammal
{
    public string Name { get; set; }
    public int Age { get; set; }
    public double Weight { get; set; }

    public Dog() : this("Karo")
    {
        Console.WriteLine("Running Dog constructor...");
    }

    public Dog(string name) : this(name, 5)
    { 
        Console.WriteLine("Assigning Dog name ...");
    }

    public Dog(string name, int age) : this(name, age, 10.0)
    {
    }

    public Dog(string name, int age, double weight) : base()
    {
        Name = name;
        Age = age;
        Weight = weight;
    }

}


