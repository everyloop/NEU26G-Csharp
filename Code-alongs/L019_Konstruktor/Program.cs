// Keyword 'new' används för att skapa ett nytt objekt (en instans av en klass)
// 1 - Allokera minne för att lagra objektet.
// 2 - Skapar objektet på den platsen i minnet, med default värden.
// 3 - Kör den konstruktorn.
// 4 - Returnerar en referens till det nya objektet.


Person myPerson = new Person("Adam"); //{ name = "Adam" };
Console.WriteLine();

Person myOtherPerson = new Person();
Person myThirdPerson = new Person("Kalle", 23);

// Konstruktorer används för att intialisera fields, properties etc i objekten som skapas; ofta med parametrar som skickas med new()
class Person
{
    private string name = String.Empty;

    private int age = 0;


    //En konstruktor är en speciell metod som alltid heter samma som klassen, och inte har void eller datatyp framför namnet.
    public Person(string name)
    {
        this.name = name;
    }

    // Man kan ha flera version av konstruktorn med olika parameterar (overloading)
    public Person(string name, int age)
    {
        this.name = name;
        this.age = age;
    }

    // Om man inte själv angett en konstruktor i sin klass så finns det alltid en default konstruktor som inte tar några parametrar.

    // Denna ersätts dock med den första konstruktorn man explicit implementerar... så om man fortfarande vill ha en version som
    // inte tar några parametrar så behöver man själv lägga till en sådan i dessa fall:
    public Person()
    {
        name = "Fredrik";
    }
}