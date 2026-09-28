
//// Implicit konvertering

//int myInt = 100;

//long myLong = myInt;

//Console.WriteLine(myLong);

//Cat cat = new Cat();
//Animal animal2 = new Dog();



//// Explicit konvertering

//myLong = 5_000_000_000;

//myInt = (int)myLong;

//Console.WriteLine(myInt);

//cat = (Cat)animal2;

//cat.Mew();

//return;


//Animal myAnimal = new Animal() { Name = "Orvar" };

Cat myCat = new Cat() { Name = "Pelle" };

Dog myDog = new Dog() { Name = "Fido" };

//myCat.Name = "Pelle";

//Console.WriteLine(myCat.Name);

myCat.Run();
myCat.Mew();

Console.WriteLine();

myDog.Run();
myDog.Bark();

// myCat = myDog;  // Detta går inte eftersom en hund inte är en katt.

Animal myAnimal = myDog; // Detta går eftersom en hund är även ett djur.

myDog.Bark();

Console.WriteLine();

myDog.ScareAway(myCat);

Console.WriteLine();

myCat.ScareAway(myDog);

Animal[] animals = new Animal[]
{
    myCat,
    myDog,
    new Cat() { Name = "Måns" },
    new Cat() { Name = "Bill" },
    new Dog() { Name = "Billy" },
    new Cat() { Name = "Bull" },
};

Console.WriteLine("*** Foreach ***\n");

foreach (Animal animal in animals)
{
    animal.Run();

    //if (animal is Cat tempCat)
    //{
    //    //Cat tempCat = (Cat)animal;
    //    tempCat.Mew();
    //}

    Cat tempCat = animal as Cat;

    if (tempCat is not null)
    {
        tempCat.Mew();
    }

    //else if (animal is Dog)
    //{
    //    Dog tempDog = (Dog)animal;
    //    tempDog.Bark();
    //}

    Console.WriteLine();
}

Console.WriteLine();

abstract class Animal
{
    public string Name { get; set; }

    protected bool isTired = false;

    public virtual void Run()
    {
        Console.WriteLine($"{Name} is running.");
        isTired = true;
    }

    public abstract void Walk();

    public void ScareAway(Animal animal)
    {
        Console.WriteLine($"{Name}: I'm going to eat you {animal.Name}");
        animal.Run();
    }
}

class Cat : Animal
{
    public void Mew()
    {
        Console.WriteLine("Meow!");
    }

    public override void Walk()
    {
        Console.WriteLine($"{Name} is walking like a cat.");
    }

    public override void Run()
    {
        base.Run();
        Console.WriteLine($"{Name} always get tired when running.");
        this.isTired = true;
    }
}

class Dog : Animal
{
    public void Bark()
    {
        Console.WriteLine("Woof!");
    }

    public override void Walk()
    {
        Console.WriteLine($"{Name} is walking like a dog.");
    }
}