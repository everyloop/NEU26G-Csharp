# Oktober 2

## Generiska klasser och metoder

**Code-along:**  
[L026_Generics](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L026_Generics/Program.cs)

En **generisk klass** eller **generisk metod** är kod som kan arbeta med olika datatyper utan att vi behöver skriva en separat version av koden för varje datatyp.

Datatypen anges med en **typparameter**, som skrivs mellan `< >`. Vanligtvis används bokstaven `T` (från *type*) som namn på typparametern.

Ett vanligt exempel är `List<T>`. När vi skapar listan bestämmer vi vilken datatyp `T` ska representera:

```csharp
List<int> numbers = new List<int>();
List<string> names = new List<string>();
```

I det första fallet ersätts `T` med `int`, och i det andra med `string`.

Vi kan använda samma princip när vi skriver egna generiska metoder. En metod som byter plats på två värden behöver exempelvis inte veta i förväg om värdena är `int`, `string` eller någon annan datatyp:

```csharp
static void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}
```

När metoden anropas kan kompilatorn vanligtvis själv räkna ut vilken datatyp `T` ska vara utifrån argumenten:

```csharp
Swap(ref x, ref y);
Swap(ref textA, ref textB);
```

Generics gör alltså att vi kan skriva kod som är **återanvändbar för flera datatyper**, samtidigt som vi behåller C#s typkontroll.

En generisk klass kan också ha flera typparametrar:

```csharp
class Cage<T1, T2>
{
    public T1 InhabitantA { get; set; }
    public T2 InhabitantB { get; set; }
}
```

Här kan `T1` och `T2` vara olika datatyper.

[Läs mer om generics](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics)

## Generic collections

C# innehåller ett antal färdiga generiska klasser som används för att lagra samlingar av objekt eller värden.

Till skillnad från en vanlig array erbjuder dessa collections olika sätt att lägga till, ta bort och organisera data.

Vilken collection vi väljer beror därför på **hur vi vill lagra och komma åt informationen**.

Några vanliga exempel är:

- `List<T>` – en ordnad samling där vi kan komma åt element med index.
- `Dictionary<TKey, TValue>` – lagrar värden tillsammans med unika nycklar.
- `Stack<T>` – lagrar värden enligt principen *last in, first out*.
- `Queue<T>` – lagrar värden enligt principen *first in, first out*.

![GenericCollections](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Images/GenericCollections.png)

## [List\<T\>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-10.0)

**Code-along:**  
[L027_List](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L027_List/Program.cs)

En `List<T>` liknar på många sätt en array. Den innehåller en ordnad samling värden av en bestämd datatyp, och elementen kan kommas åt med hjälp av deras index.

Den stora skillnaden är att storleken på en `List<T>` kan förändras medan programmet körs. Vi kan exempelvis använda `Add()` för att lägga till element och `Remove()` eller `RemoveAt()` för att ta bort dem.

```csharp
var countries = new List<string>
{
    "Sweden",
    "Denmark",
    "Norway"
};

countries.Add("Finland");
countries.Remove("Norway");
```

Precis som med en array kan vi använda både `for` och `foreach` för att iterera över innehållet.

### Count och Capacity

En lista har en property som heter `Count`, vilken anger **hur många element som faktiskt finns i listan**.

```csharp
Console.WriteLine(countries.Count);
```

Internt använder `List<T>` en array för att lagra sina element. Eftersom en vanlig array inte kan ändra storlek måste listan ibland skapa en ny, större array när fler element läggs till.

`Capacity` anger hur många element listan för tillfället har plats för utan att behöva göra en sådan omallokering.

```csharp
Console.WriteLine(numbers.Count);
Console.WriteLine(numbers.Capacity);
```

När listans befintliga kapacitet inte räcker utökas den automatiskt. Det innebär i praktiken att en större intern array skapas och de befintliga elementen kopieras över.

Om vi redan vet att vi kommer att lägga till många element kan vi därför ange en lämplig capacity i förväg. Det kan undvika onödiga omallokeringar:

```csharp
numbers.Capacity = 40;
```

`TrimExcess()` kan användas för att minska överflödig kapacitet när den inte längre behövs.

I de flesta program behöver vi dock inte själva hantera `Capacity` – `List<T>` sköter detta automatiskt. Det viktiga är att förstå **varför en `List<T>` kan växa trots att den internt använder en array**.

## [Dictionary\<TKey, TValue\>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-10.0)

**Code-along:**  
[L028_Dictionary](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L028_Dictionary/Program.cs)

En `Dictionary<TKey, TValue>` lagrar data som **nyckel-värde-par** (*key-value pairs*).

I stället för att använda ett numeriskt index för att hitta ett värde använder vi en **nyckel**.

Exempelvis kan vi skapa en enkel svensk-engelsk ordlista:

```csharp
var dictionary = new Dictionary<string, string>();

dictionary.Add("boy", "pojke");
dictionary.Add("girl", "flicka");
```

Här är både nyckeln (`TKey`) och värdet (`TValue`) av typen `string`. De behöver dock inte ha samma datatyp. Vi skulle exempelvis kunna skapa:

```csharp
Dictionary<string, int>
```

där nyckeln är en `string` och värdet är en `int`.

### Nycklar måste vara unika

Varje nyckel i en dictionary måste vara unik. Om vi försöker lägga till samma nyckel två gånger med `Add()` kastas ett exception.

Ett värde kan hämtas genom att använda nyckeln:

```csharp
string translation = dictionary["boy"];
```

Men om nyckeln inte finns kastas en `KeyNotFoundException`.

Vi kan därför kontrollera om en nyckel finns innan vi försöker läsa värdet:

```csharp
if (dictionary.ContainsKey("boy"))
{
    Console.WriteLine(dictionary["boy"]);
}
```

En dictionary har också `Keys` och `Values`, vilka gör det möjligt att iterera separat över alla nycklar eller alla värden.

När vi itererar direkt över en dictionary får vi däremot nyckel-värde-par:

```csharp
foreach (KeyValuePair<string, string> pair in dictionary)
{
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}
```

En `Dictionary<TKey, TValue>` är därför särskilt användbar när ett värde har någon **unik identifierare** som vi vill använda för att hitta det, snarare än dess position i en lista.

## [Stack\<T\>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1?view=net-10.0)

En `Stack<T>` är en collection där elementen hanteras enligt principen **LIFO – Last In, First Out**.

Det betyder att det element som läggs in **sist** också är det som tas ut **först**.

Man kan tänka sig en hög med tallrikar: vi lägger en ny tallrik överst på högen och tar också den översta tallriken när vi behöver en.

De viktigaste operationerna är:

- `Push()` – lägger ett element överst på stacken.
- `Pop()` – tar bort och returnerar det översta elementet.
- `Peek()` – returnerar det översta elementet utan att ta bort det.

En stack passar alltså bra när **ordningen som saker läggs till i avgör i vilken ordning de senare ska tas ut**, exempelvis historik eller operationer som ska kunna behandlas i omvänd ordning.

## [Queue\<T\>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1?view=net-10.0)

En `Queue<T>` fungerar istället enligt principen **FIFO – First In, First Out**.

Det element som läggs in **först** är också det som tas ut **först**.

Man kan tänka sig en vanlig kö: den person som ställer sig först i kön är också den som får lämna kön först.

De viktigaste operationerna är:

- `Enqueue()` – lägger ett element sist i kön.
- `Dequeue()` – tar bort och returnerar elementet längst fram i kön.
- `Peek()` – returnerar elementet längst fram utan att ta bort det.

En queue passar därför bra när saker ska **behandlas i samma ordning som de kommer in**, exempelvis jobb, meddelanden eller andra uppgifter som väntar på att behandlas.

## Stack vs Queue

Den viktigaste skillnaden är alltså ordningen:

**Stack:** sist in → först ut (**LIFO**)  
**Queue:** först in → först ut (**FIFO**)