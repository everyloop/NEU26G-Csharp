# Oktober 7

# Lambda-uttryck

**Code-along:**  
[L032_Lambda_expressions](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L032_Lambda_expressions/Program.cs)

Ett **lambda-uttryck** är ett kort sätt att skriva en **anonym funktion**, alltså en funktion som inte behöver ett eget namn.

Vi har tidigare arbetat med delegates och sett att vi kan lagra referenser till metoder:

```csharp
Func<int, int> square = Square;

static int Square(int x)
{
    return x * x;
}
```

Med ett lambda-uttryck kan vi istället skriva funktionaliteten direkt:

```csharp
Func<int, int> square = x => x * x;
```

Båda exemplen representerar en funktion som:

- tar emot en `int`
- returnerar en `int`

## Syntax för lambda

Lambda-uttryck använder operatorn:

```text
=>
```

Den kan ungefär läsas som **"går till"**.

```csharp
x => x * x
```

kan läsas:

> `x` går till `x * x`

Vänster sida innehåller funktionens parametrar och höger sida beskriver vad funktionen gör.

```text
parameter => uttryck
```

Exempel:

```csharp
x => x * x
x => x.ToString()
x => Console.WriteLine(x)
```

## Lambda och Func

`Func` används för delegates som **returnerar ett värde**.

```csharp
Func<int, int> square = x => x * x;
```

I:

```csharp
Func<int, int>
```

är:

- första `int` typen på parametern
- sista `int` typen på returvärdet

Lambda-uttrycket:

```csharp
x => x * x
```

matchar därför `Func<int, int>`.

Ett annat exempel:

```csharp
Func<int, string> myFunc = x => x.ToString();
```

Här tar funktionen emot en `int` och returnerar en `string`.

## Lambda och Action

`Action` används när funktionen **inte returnerar något värde** (`void`).

```csharp
Action<int> printInt = x => Console.WriteLine(x);
```

Det motsvarar ungefär den namngivna metoden:

```csharp
static void PrintInt(int i)
{
    Console.WriteLine(i);
}
```

## Flera parametrar

Om lambdan har flera parametrar skriver vi dem inom parentes:

```csharp
Func<Person, int, bool> isLegal =
    (person, legalAge) => person.Age >= legalAge;
```

Här tar funktionen emot:

1. en `Person`
2. en `int`

och returnerar en `bool`.

Samma funktionalitet kan skrivas med en vanlig namngiven metod:

```csharp
static bool IsLegal(Person person, int legalAge)
{
    return person.Age >= legalAge;
}
```

Vi kan därför anropa båda på liknande sätt:

```csharp
var myPerson = new Person() { Age = 20 };

Console.WriteLine(isLegal(myPerson, 18));
Console.WriteLine(IsLegal(myPerson, 18));
```

## Func med flera parametrar

`Func` kan ha flera parametrar.

Den **sista typen är alltid returtypen**.

```csharp
Func<int, int, int, string> add =
    (a, b, c) => $"{a} + {b} + {c} = {a + b + c}";
```

Här betyder typerna:

```text
int     parameter
int     parameter
int     parameter
string  returtyp
```

Ett annat exempel:

```csharp
Func<double, double, double> volumeOfCylinder =
    (r, h) => Math.PI * r * r * h;
```

Den tar emot två `double` och returnerar en `double`.

## Lambda med flera statements

En lambda behöver inte bestå av ett enda uttryck.

Om vi behöver flera statements använder vi `{ }`:

```csharp
n =>
{
    int sum = 0;

    for (int i = 1; i <= n; i++)
    {
        sum += i;
    }

    return sum;
}
```

När vi använder `{ }` behöver vi själva skriva `return` om funktionen ska returnera ett värde.

För en enkel expression:

```csharp
x => x * x
```

returneras resultatet automatiskt.

## Skicka beteende som argument

En av de viktigaste användningarna av delegates och lambda-uttryck är att vi kan **skicka beteende till en metod**.

Istället för att en metod bestämmer exakt vad som ska göras kan anroparen skicka in funktionaliteten.

Vi skapade:

```csharp
static void PrintResults(Func<int, int> func)
{
    for (int i = 1; i <= 10; i++)
    {
        Console.WriteLine($"{i}: {func(i)}");
    }
}
```

`PrintResults` vet inte vilken beräkning som ska utföras.

Den vet bara att `func`:

- tar emot en `int`
- returnerar en `int`

Anroparen bestämmer sedan beteendet.

```csharp
PrintResults(x => x * 5 + 100);
```

eller:

```csharp
PrintResults(x => x % 2);
```

Samma metod kan alltså utföra olika beräkningar beroende på vilken funktion vi skickar in.

## Lambda kan definieras direkt vid anropet

En stor fördel med lambda-uttryck är att vi kan definiera en liten funktion **direkt där den behövs**:

```csharp
PrintResults(x => x * 5 + 100);
```

Vi behöver inte först skapa:

```csharp
static int Calculate(int x)
{
    return x * 5 + 100;
}
```

och sedan:

```csharp
PrintResults(Calculate);
```

Det gör lambdas särskilt användbara för kort funktionalitet som bara behövs på ett specifikt ställe.

## Vanliga metoder fungerar också

En parameter av typen:

```csharp
Func<int, int>
```

kräver inte att vi använder en lambda.

Vi kan även skicka en vanlig namngiven metod med rätt signatur:

```csharp
PrintResults(MultiplyBy3);

static int MultiplyBy3(int i)
{
    return i * 3;
}
```

Både:

```csharp
PrintResults(x => x * 3);
```

och:

```csharp
PrintResults(MultiplyBy3);
```

fungerar eftersom båda matchar `Func<int, int>`.

Lambda är alltså inte ett krav för delegates. Det är ett **smidigt sätt att skapa funktionalitet där den behövs**.

## Lambda är en anonym funktion

En lambda är en **anonym funktion** eftersom den inte behöver något eget namn.

Namngiven metod:

```csharp
static int Square(int x)
{
    return x * x;
}
```

Lambda:

```csharp
x => x * x
```

Vilken typ av delegate lambdan kan användas som beror på dess parametrar och returvärde.

Exempelvis passar:

```csharp
x => x * x
```

som:

```csharp
Func<int, int>
```

om `x` är en `int`.

## Sammanfattning

Ett **lambda-uttryck** är ett kort sätt att skriva en anonym funktion:

```csharp
x => x * x
```

Parametrarna står till vänster om `=>` och funktionaliteten till höger.

Lambdas används ofta tillsammans med delegates som:

```csharp
Action<T>
Func<T, TResult>
```

Exempel:

```csharp
Action<int> print = x => Console.WriteLine(x);

Func<int, int> square = x => x * x;

Func<int, int, bool> greaterThan =
    (x, y) => x > y;
```

En särskilt viktig användning är att **skicka beteende som argument till andra metoder**:

```csharp
PrintResults(x => x * 5);
```

Det gör att metoden som tar emot delegaten inte behöver veta exakt **vilket beteende** som ska utföras. Anroparen kan bestämma det genom att skicka in en lambda.

Det här kommer vi bland annat att ha stor användning av när vi arbetar med **LINQ**.



# Arv, komposition och interfaces

**Code-along:**  
[L033_Interface](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L033_Interface/Program.css)

När vi bygger program med flera klasser behöver vi kunna beskriva hur olika typer relaterar till varandra.

Tre användbara sätt att tänka är:

- **Arv:** *is-a* — ett objekt **är en** viss typ av objekt.
- **Komposition:** *has-a* — ett objekt **har ett** annat objekt.
- **Interface:** *can-do* — ett objekt **kan göra/erbjuder** något.

## Arv – is-a relationship

Arv används när en klass är en mer specifik variant av en annan klass.

```csharp
class Character
{
}

class Player : Character
{
}
```

En `Player` **är en** `Character`.

På samma sätt hade vi:

```csharp
abstract class Weapon
{
}

class Sword : Weapon
{
}

class Knife : Weapon
{
}
```

Både `Sword` och `Knife` **är** olika typer av `Weapon`.

En klass kan bara ärva från **en** basklass.

## Komposition – has-a relationship

Komposition innebär att ett objekt innehåller eller använder andra objekt.

Vår `Player` har exempelvis ett `Inventory`:

```csharp
class Player : Character
{
    public Inventory Inventory { get; set; }

    public Player()
    {
        Inventory = new Inventory();
    }
}
```

En `Player` **är en** `Character`, men en `Player` **har ett** `Inventory`.

Vi använde också komposition för spelarens utrustning:

```csharp
public IEquiplable LeftHandEquipment { get; set; }
public IEquiplable RightHandEquipment { get; set; }
```

Ett objekt kan alltså både ärva från en annan klass och vara uppbyggt av andra objekt.

## Interfaces – ett kontrakt

Ett interface beskriver ett **kontrakt** som en klass kan välja att implementera.

Vi skapade exempelvis:

```csharp
interface ICollectable
{
    void Collect();
    void Drop();
}
```

En klass som implementerar `ICollectable` lovar att den tillhandahåller dessa medlemmar:

```csharp
class Sword : Weapon, ICollectable
{
    public void Collect()
    {
        Console.WriteLine("You collected a sword.");
    }

    public void Drop()
    {
        // ...
    }
}
```

Det spelar ingen roll för `ICollectable` om objektet är ett `Weapon`, en `Potion` eller något helt annat.

Det viktiga är att objektet uppfyller kontraktet.

## En klass kan implementera flera interfaces

Till skillnad från arv kan en klass implementera **flera interfaces**.

```csharp
class HealthPotion : Potion, ICollectable, IConsumable
{
    // ...
}
```

En `HealthPotion`:

- **är en** `Potion`
- **kan samlas in** (`ICollectable`)
- **kan konsumeras** (`IConsumable`)

Interfaces kan därför användas för att beskriva olika förmågor hos ett objekt utan att dessa behöver byggas in i samma arvshierarki.

Ett interface kan också vara tomt:

```csharp
interface IConsumable
{
}
```

Ett sådant interface kan användas för att markera att en typ tillhör en viss kategori eller har en viss egenskap.

## Interfaces som typer

Ett interface är också en typ. Därför kunde vårt inventory innehålla:

```csharp
private List<ICollectable> _items;
```

Listan kan innehålla objekt av **olika konkreta typer**, så länge de implementerar `ICollectable`.

Exempelvis kan både dessa läggas till:

```csharp
myPlayer.Inventory.Add(new HealthPotion());
myPlayer.Inventory.Add(new Knife());
```

`HealthPotion` och `Knife` har olika arvshierarkier, men båda implementerar `ICollectable`.

Detta är ett exempel på **polymorfism**.

## Begränsa vilka objekt en metod accepterar

Vår vanliga `Add` tog emot:

```csharp
public void Add(ICollectable item)
{
    _items.Add(item);
    item.Collect();
}
```

Eftersom parametern är `ICollectable` kan vi bara skicka in objekt som implementerar interfacet:

```csharp
myPlayer.Inventory.Add(new Sword());   // OK
myPlayer.Inventory.Add(new Knife());   // OK
```

Ett objekt som inte implementerar `ICollectable` kan inte skickas till metoden.

Detta gör att kompilatorn kan hjälpa oss att upprätthålla vårt kontrakt.

## Kontrollera ett interface vid runtime

Vi skapade även en metod som accepterade vilket objekt som helst:

```csharp
public bool TryAdd(object item)
{
    if (item is ICollectable collectable)
    {
        _items.Add(collectable);
        collectable.Collect();
        return true;
    }

    return false;
}
```

Här kombinerar vi **interface med pattern matching**:

```csharp
item is ICollectable collectable
```

Det kontrollerar om objektet implementerar `ICollectable`.

Om det gör det skapas samtidigt en variabel:

```csharp
collectable
```

som har typen `ICollectable`.

Därför fungerar:

```csharp
if (!myPlayer.Inventory.TryAdd(new Shield()))
{
    Console.WriteLine("Could not collect item.");
}
```

`Shield` implementerar inte `ICollectable`, så `TryAdd` returnerar `false`.

## Interface och polymorfism

Vi använde även interfaces för spelarens utrustning:

```csharp
public IEquiplable LeftHandEquipment { get; set; }
public IEquiplable RightHandEquipment { get; set; }
```

Det betyder att egenskaperna inte är bundna till exempelvis `Sword` eller `Shield`.

De kan referera till **vilket objekt som helst som implementerar `IEquiplable`**.

```csharp
LeftHandEquipment = new Sword();
RightHandEquipment = new Shield();
```

Det konkreta objektet kan alltså vara en `Sword`, medan referensens typ är `IEquiplable`.

## Sammanfattning

**Arv – is-a**

```csharp
class Player : Character
```

En `Player` är en `Character`.

**Komposition – has-a**

```csharp
public Inventory Inventory { get; set; }
```

En `Player` har ett `Inventory`.

**Interface – can-do / kontrakt**

```csharp
class Sword : Weapon, ICollectable
```

En `Sword` uppfyller kontraktet `ICollectable`.

Interfaces gör framför allt att **olika typer kan behandlas på samma sätt utan att de behöver tillhöra samma arvshierarki**.

Det gjorde exempelvis att vårt `Inventory` kunde arbeta med:

```csharp
List<ICollectable>
```

istället för att behöva veta om ett objekt är en `Sword`, `Knife`, `HealthPotion` eller någon annan konkret typ.
```