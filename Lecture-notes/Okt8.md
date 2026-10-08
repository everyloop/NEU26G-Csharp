# Oktober 8

# Extension methods, anonyma typer och LINQ

Under lektionen gick vi igenom tre koncept:

- **Extension methods** – att lägga till metoder som kan anropas på befintliga typer.
- **Anonyma typer** – att skapa objekt utan att först deklarera en egen klass.
- **LINQ** – att söka, filtrera, sortera och transformera data i samlingar.

Dessa koncept hänger ihop. LINQ använder bland annat extension methods och lambda-uttryck, och anonyma typer är användbara när vi vill bestämma hur resultatet ska se ut.

# Extension methods

En **extension method** gör det möjligt att anropa en statisk metod som om den vore en instansmetod på en befintlig typ.

Vi skapade exempelvis en metod `Title()` som ändrar en sträng så att första bokstaven blir stor och resten små:

```csharp
static class StringMethods
{
    public static string Title(this string text)
    {
        return text[0].ToString().ToUpper() + text.Substring(1).ToLower();
    }
}
```

Det speciella är nyckelordet **`this` framför den första parametern**:

```csharp
this string text
```

Det anger att metoden ska kunna anropas som en extension method på typen `string`.

Istället för att skriva:

```csharp
StringMethods.Title(myString);
```

kan vi skriva:

```csharp
myString.Title();
```

Båda anropar samma metod.

## Skapa en extension method

För att skapa en extension method behöver vi:

1. En **statisk klass**.
2. En **statisk metod**.
3. Nyckelordet `this` framför metodens första parameter.

Vi skapade även metoden `Duplicate()`:

```csharp
public static string Duplicate(this string text, int times)
{
    string newText = string.Empty;

    for (int i = 0; i < times; i++)
    {
        newText += text;
    }

    return newText;
}
```

Den kan användas så här:

```csharp
Console.WriteLine("X".Duplicate(4)); // XXXX
```

Vi kan också **kedja metodanrop**:

```csharp
string myString = "hello WORLD!";

Console.WriteLine(myString.Title().Duplicate(3));
```

Först anropas `Title()`, och sedan anropas `Duplicate(3)` på strängen som `Title()` returnerade.

**Viktigt:** Extension methods ändrar inte den ursprungliga klassen. De är fortfarande statiska metoder, men C# låter oss anropa dem med instansmetodsyntax.

Det här är särskilt viktigt eftersom många LINQ-metoder är extension methods.

---

# Anonyma typer (Anonymous types)

En **anonym typ** är en typ som kompilatorn skapar åt oss utan att vi behöver deklarera en namngiven klass.

Vi skapar en anonym typ med:

```csharp
var person = new
{
    name = "Anders Andersson",
    age = 45
};
```

Kompilatorn skapar automatiskt en typ med properties för `name` och `age`.

Vi kan komma åt dessa properties som vanligt:

```csharp
Console.WriteLine(person.name);
Console.WriteLine(person.age);
```

Eftersom vi inte har något namn på typen använder vi normalt `var`.

## Anonyma typer kan innehålla olika datatyper

Exempel från lektionen:

```csharp
var data = new
{
    x = 3.0f,
    firstName = "Fredrik",
    z = true,
    myCat = new Cat()
};
```

En anonym typ kan alltså innehålla både värdetyper och referenstyper.

## Nästlade anonyma typer

En anonym typ kan även innehålla andra anonyma typer:

```csharp
var person = new
{
    name = "Anders Andersson",
    age = 45,
    contactInfo = new
    {
        email = "anders@gmail.com",
        phone = "0702348645"
    }
};
```

Vi kan då komma åt informationen så här:

```csharp
Console.WriteLine(person.contactInfo.email);
```

## Arrayer med anonyma typer

Vi kan skapa en array med objekt av samma anonyma typ:

```csharp
var people = new[]
{
    new { Name = "Fredrik", Age = 35 },
    new { Name = "Anna", Age = 28 },
    new { Name = "Maria", Age = 42 }
};
```

Alla elementen måste ha **samma anonyma typ**, vilket innebär samma propertynamn, propertytyper och ordning.

## När används anonyma typer?

Anonyma typer är framför allt användbara när vi behöver en **tillfällig datastruktur**, exempelvis när vi använder LINQ för att välja ut vissa properties ur en samling.

```csharp
var result = people.Select(p => new
{
    p.FirstName,
    p.Age
});
```

Här skapas ett resultat med endast `FirstName` och `Age`, utan att vi behöver skapa en ny klass för det.

---

# LINQ – Language Integrated Query

**LINQ** står för *Language Integrated Query*.

LINQ låter oss söka, filtrera, sortera och transformera data direkt i C#.

Vi kan exempelvis använda LINQ på:

- Listor
- Arrayer
- Andra samlingar och sekvenser
- Databaser, exempelvis via Entity Framework

LINQ använder ofta **extension methods** och **lambda-uttryck**.

Exempel:

```csharp
var result = data.Where(p => p.Age >= 18);
```

Här är:

- `Where()` en extension method.
- `p => p.Age >= 18` ett lambda-uttryck.
- `data` samlingen vi arbetar med.

Många LINQ-metoder arbetar med typen `IEnumerable<T>`, som representerar en sekvens av element som vi kan iterera över.

## Count – räkna element

`Count()` räknar antalet element i en sekvens.

Vi kan även skicka in ett villkor:

```csharp
int count = data.Count(p => p.City == "Göteborg");
```

Detta räknar hur många personer som bor i Göteborg.

Observera skillnaden mellan:

```csharp
data.Count     // Property på List<T>
data.Count()   // LINQ-metod
```

## Any – finns det minst en?

`Any()` kontrollerar om en sekvens innehåller minst ett element.

Med ett villkor kontrollerar den om **minst ett element matchar**:

```csharp
bool result = data.Any(p => p.City == "Alingsås");
```

Resultatet blir `true` om minst en person bor i Alingsås, annars `false`.

## All – uppfyller alla villkoret?

`All()` kontrollerar om **alla element** uppfyller ett villkor.

```csharp
bool result = data.All(p => p.Age >= 10);
```

Resultatet blir `true` om samtliga personer är minst 10 år gamla.

Skillnaden:

```csharp
data.Any(p => p.Age >= 18); // Minst en matchar
data.All(p => p.Age >= 18); // Alla matchar
```

---

# Hämta enskilda element

LINQ har flera metoder för att hämta ett enskilt element från en sekvens.

## First och FirstOrDefault

`First()` hämtar det första elementet som matchar villkoret:

```csharp
Person person = data.First(p => p.Age > 50);
```

Om ingen person matchar kastas en **exception**.

`FirstOrDefault()` fungerar liknande, men returnerar ett standardvärde om ingen matchning finns:

```csharp
Person? person = data.FirstOrDefault(p => p.Age > 90);
```

För en referenstyp som `Person` är standardvärdet normalt `null`.

Vi kan även ange ett eget standardvärde:

```csharp
Person person = data.FirstOrDefault(
    p => p.Age > 90,
    new Person { FirstName = "Fredrik", LastName = "Johansson" }
);
```

Om ingen person är äldre än 90 år returneras den person vi angav som standardvärde.

## Last och LastOrDefault

`Last()` och `LastOrDefault()` fungerar på liknande sätt, men hämtar den **sista** matchningen istället för den första.

```csharp
Person person = data.Last(p => p.Age > 50);

Person? otherPerson = data.LastOrDefault(p => p.Age > 90);
```

## Single och SingleOrDefault

`Single()` används när vi **förväntar oss exakt en matchning**.

```csharp
Person person = data.Single(p => p.Age == 61);
```

`Single()` kastar en exception om:

- Ingen matchar.
- Fler än en matchar.

Det är användbart när vi vet att något ska vara unikt, exempelvis ett personnummer eller ett produkt-ID.

`SingleOrDefault()` tillåter att ingen matchning finns:

```csharp
Person? person = data.SingleOrDefault(p => p.Age == 61);
```

Men den kastar fortfarande en exception om **fler än ett element matchar**.

| Metod | Ingen matchning | Flera matchningar |
|---|---|---|
| `First()` | Exception | Första matchningen |
| `FirstOrDefault()` | Default-värde | Första matchningen |
| `Single()` | Exception | Exception |
| `SingleOrDefault()` | Default-värde | Exception |

---

# Where – filtrera data

`Where()` används för att **filtrera vilka element som ska ingå i resultatet**.

Exempel:

```csharp
var result = data.Where(p => p.Age < 30);
```

Detta väljer ut alla personer yngre än 30 år.

Vi kan använda vanliga logiska operatorer:

```csharp
var result = data.Where(
    p => p.City == "Göteborg" && p.Age < 30
);
```

Vi kan också använda metoder och properties i villkoret:

```csharp
var result = data.Where(p => p.FirstName.Length == 4);
```

Eller:

```csharp
var result = data.Where(
    p => p.Age <= 20 || p.Age >= 50
);
```

`Where()` returnerar en sekvens med de element som matchar villkoret.

---

# Select – transformera data

`Select()` används för att **bestämma hur resultatet ska se ut**.

Exempelvis kan vi välja ut endast personernas förnamn:

```csharp
var names = data.Select(p => p.FirstName);
```

Här går vi från en sekvens med `Person`-objekt till en sekvens med `string`.

Vi kan även använda anonyma typer:

```csharp
var result = data.Select(p => new
{
    FullName = $"{p.FirstName} {p.LastName}",
    Age = p.Age
});
```

Nu skapas ett nytt objekt för varje person, med properties `FullName` och `Age`.

**Skillnaden mellan Where och Select:**

- `Where()` bestämmer **vilka element** som ska vara med.
- `Select()` bestämmer **hur varje element i resultatet ska se ut**.

---

# OrderBy och ThenBy – sortera data

`OrderBy()` sorterar element i stigande ordning:

```csharp
var result = data.OrderBy(p => p.FirstName);
```

`OrderByDescending()` sorterar i fallande ordning:

```csharp
var result = data.OrderByDescending(p => p.Age);
```

Om vi vill sortera på flera saker använder vi `ThenBy()` eller `ThenByDescending()`:

```csharp
var result = data
    .OrderBy(p => p.City)
    .ThenByDescending(p => p.Age);
```

Här sorteras personerna först efter stad och därefter efter ålder inom varje stad.

---

# Kedja LINQ-metoder

En av de stora fördelarna med LINQ är att vi kan **kedja flera operationer**.

Under lektionen skrev vi:

```csharp
var newData = data
    .Where(p => p.FirstName.StartsWith(
        "e",
        StringComparison.CurrentCultureIgnoreCase))
    .Select(p => new
    {
        FullName = $"{p.FirstName} {p.LastName}",
        Age = p.Age
    })
    .OrderBy(p => p.FullName)
    .ThenByDescending(p => p.Age)
    .ToList();
```

Varje steg har ett eget ansvar:

1. **Where:** Filtrerar fram personer vars förnamn börjar med `e`, utan hänsyn till stora eller små bokstäver.
2. **Select:** Skapar nya anonyma objekt med `FullName` och `Age`.
3. **OrderBy:** Sorterar efter fullständigt namn.
4. **ThenByDescending:** Sorterar efter ålder i fallande ordning om flera har samma fullständiga namn.
5. **ToList:** Skapar en lista med resultatet.

Detta gör att vi kan uttrycka ganska avancerade operationer på ett läsbart sätt.

---

# OfType – filtrera efter typ

`OfType<T>()` väljer ut de element som är kompatibla med en viss typ.

Vi hade exempelvis en lista med olika typer av objekt:

```csharp
List<object> objects = new List<object>
{
    5, "Hello", true, "Hej", "Bye!", 'A', 5.0
};
```

Vi kan välja ut endast strängarna:

```csharp
var strings = objects.OfType<string>().ToList();
```

Resultatet innehåller:

```text
Hello
Hej
Bye!
```

Detta är särskilt användbart när vi har samlingar med objekt av olika typer, exempelvis i ett spel:

```csharp
elements.OfType<Enemy>().Any(e => e.Hp > 5);
```

Detta kontrollerar om det finns minst en `Enemy` med mer än 5 HP.

---

# Deferred execution – uppskjuten exekvering

Många LINQ-metoder använder **deferred execution**, vilket innebär att frågan inte körs färdigt när vi skapar den.

Istället utförs arbetet när vi börjar läsa resultatet, exempelvis med `foreach`.

Vi demonstrerade detta med:

```csharp
List<int> numbers = new List<int> { 4, 6, 9, 10, 13 };

var filteredNumbers = numbers
    .Where(n => n < 10)
    .OrderBy(n => n);

foreach (var number in filteredNumbers)
{
    Console.WriteLine(number);
}
```

Första gången får vi:

```text
4
6
9
```

Sedan ändrade vi den ursprungliga listan:

```csharp
numbers.Add(7);
```

När vi itererar över samma LINQ-resultat igen:

```csharp
foreach (var number in filteredNumbers)
{
    Console.WriteLine(number);
}
```

får vi nu:

```text
4
6
7
9
```

**Varför?**

Eftersom frågan exekveras när vi itererar över resultatet, och inte när vi först deklarerade `filteredNumbers`.

## ToList – materialisera resultatet

Om vi istället skriver:

```csharp
var filteredNumbers = numbers
    .Where(n => n < 10)
    .OrderBy(n => n)
    .ToList();
```

körs frågan direkt och resultatet lagras i en ny lista.

Om vi sedan ändrar `numbers` påverkas inte vilka element som redan finns i `filteredNumbers`.

Detta kallas **materialisering**.

Viktigt att komma ihåg:

- `Where()`, `Select()` och `OrderBy()` använder normalt deferred execution.
- `ToList()` exekverar frågan och skapar en ny lista.
- Metoder som `Any()`, `Count()` och `FirstOrDefault()` behöver däremot beräkna ett svar direkt när de anropas.

---

# Sammanfattning

## Extension methods

Låter oss anropa statiska metoder med instansmetodsyntax:

```csharp
"hello".Title();
```

## Anonyma typer

Låter oss skapa tillfälliga typer utan att deklarera egna klasser:

```csharp
var person = new { Name = "Anna", Age = 25 };
```

## LINQ

Används för att arbeta med sekvenser och samlingar.

| Metod | Användning |
|---|---|
| `Where()` | Filtrera element |
| `Select()` | Transformera element |
| `OrderBy()` | Sortera stigande |
| `OrderByDescending()` | Sortera fallande |
| `ThenBy()` | Ytterligare sortering |
| `Count()` | Räkna element |
| `Any()` | Kontrollera om minst ett element matchar |
| `All()` | Kontrollera om alla element matchar |
| `First()` / `Last()` | Hämta första/sista matchningen |
| `FirstOrDefault()` / `LastOrDefault()` | Hämta första/sista matchningen eller default |
| `Single()` | Kräver exakt en matchning |
| `SingleOrDefault()` | Kräver högst en matchning |
| `OfType<T>()` | Välja element av en viss typ |
| `ToList()` | Skapa en lista av resultatet |

Det viktigaste är att förstå hur **extension methods, lambda-uttryck och LINQ samverkar**:

```csharp
var result = data
    .Where(p => p.Age >= 18)
    .OrderBy(p => p.FirstName)
    .Select(p => new
    {
        p.FirstName,
        p.Age
    })
    .ToList();
```

Vi kan filtrera, sortera och transformera data i flera tydliga steg, utan att behöva skriva egna loopar för varje operation.