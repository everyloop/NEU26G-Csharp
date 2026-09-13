# September 14

## Exceptions i C# – try, catch, finally och throw

Vi har [tidigare](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Sep4.md#Olika-typer-av-fel) pratat om tre olika typer av fel:

- **Kompileringsfel** – koden kan inte kompileras.
- **Runtime errors / exekveringsfel** – något går fel när programmet körs.
- **Logiska fel** – programmet körs, men resultatet blir inte som vi tänkt oss.

Vi har redan stött på flera exempel på exceptions:

```csharp
int number = int.Parse("hej");  // FormatException
```

```csharp
int[] numbers = { 10, 20, 30 };

Console.WriteLine(numbers[10]); // IndexOutOfRangeException
```

Koden går att kompilera, men när raden exekveras inträffar ett (exekverings)fel och ett **exception kastas**.

Om det inte finns någon kod som fångar och hanterar exceptionet kallas det ett **unhandled exception** (ohanterat undantag). Programmet avbryts och vi får ett felmeddelande som visar vilket exception som inträffade.

## try och catch

Med `try` och `catch` kan vi hantera exceptions.

```csharp
try
{
    int number = int.Parse(Console.ReadLine());

    Console.WriteLine(number);
}
catch (FormatException)
{
    Console.WriteLine("Du måste skriva ett heltal.");
}
```

Om användaren exempelvis skriver `42` fungerar `Parse()` och koden fortsätter precis som vanligt:

```text
try
 ↓
Parse lyckas
 ↓
Console.WriteLine(number)
 ↓
catch hoppas över
 ↓
programmet fortsätter
```

`catch` körs alltså **inte alls** när allt går som det ska. Koden i `try` beter sig i normalfallet precis som den hade gjort utan `try-catch`.

Men om användaren istället skriver `hej` kan `Parse()` inte genomföras och ett `FormatException` kastas:

```text
try
 ↓
Parse misslyckas → FormatException
                  ↓
            catch körs
                  ↓
          programmet fortsätter
```

Så fort exceptionet kastas **avbryts resten av `try`-blocket**. Programmet letar istället efter en `catch` som kan hantera exceptionet.

```csharp
try
{
    int number = int.Parse("hej");

    Console.WriteLine(number); // Körs aldrig
}
catch (FormatException)
{
    Console.WriteLine("Du måste skriva ett heltal.");
}
```

## Fånga specifika exceptions

Olika saker kan gå fel och vi kan vilja hantera dem på olika sätt.

```csharp
try
{
    int number = int.Parse(Console.ReadLine());
    int result = 100 / number;

    Console.WriteLine(result);
}
catch (FormatException)
{
    Console.WriteLine("Du måste skriva ett heltal.");
}
catch (DivideByZeroException)
{
    Console.WriteLine("Du kan inte dividera med noll.");
}
```

Här kan två olika typer av exceptions uppstå:

- `FormatException` om texten inte kan omvandlas till ett heltal.
- `DivideByZeroException` om vi försöker dividera med `0`.

Genom att fånga **den specifika typen av exception** kan vi hantera olika problem på olika sätt.

Det går också att skriva:

```csharp
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
```

`Exception` fångar många olika typer av exceptions. Det kan ibland vara användbart, men då behandlar vi väldigt olika fel på samma sätt.

En bra tumregel är därför:

> **Fånga den typ av exception som du förväntar dig och vet hur du vill hantera.**

Fånga *inte* alla exceptions bara för att hindra programmet från att krascha.

## Information om exceptionet

När vi skriver:

```csharp
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.StackTrace);
}
```

får vi tillgång till information om exceptionet.

**`Message`** innehåller felmeddelandet.

**`StackTrace`** visar var felet inträffade och vilka funktionsanrop som ledde fram till det.

Detta hänger ihop med **call stacken** som vi har tittat på [tidigare](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Sep7.md#Call-stack).

## Exceptions och call stack

Ett exception behöver inte fångas i samma funktion där det uppstår.

```csharp
static void A()
{
    B();
}

static void B()
{
    int number = int.Parse("hej");
}

try
{
    A();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
```

Exceptionet uppstår i `B()`.

Eftersom `B()` inte hanterar det går exceptionet tillbaka genom anropskedjan och letar efter en passande `catch`:

```text
B()             ← exception uppstår
 ↑
A()             ← ingen catch
 ↑
Top-level kod   ← catch!
```

Det normala flödet genom funktionerna avbryts alltså.

Om ingen funktion eller annan anropande kod hanterar exceptionet fortsätter det upp genom call stacken. Om ingen passande `catch` hittas har vi ett **unhandled exception** och programmet avbryts.

## finally

Ett `finally`-block körs när vi lämnar `try`, oavsett om ett exception inträffade eller inte.

```csharp
try
{
    int number = int.Parse(Console.ReadLine());
}
catch (FormatException)
{
    Console.WriteLine("Felaktig inmatning.");
}
finally
{
    Console.WriteLine("Detta körs alltid.");
}
```

Skillnaden mot att bara skriva kod efter `try-catch` blir tydlig om exempelvis en funktion gör `return`:

```csharp
static int GetNumber()
{
    try
    {
        return 10;
    }
    finally
    {
        Console.WriteLine("Finally körs ändå!");
    }
}
```

`finally` körs innan funktionen lämnas, trots att vi har gjort `return`.

Det används framför allt när vi har något som **måste göras när vi lämnar `try`**, exempelvis stänga eller frigöra en resurs.

## throw – kasta egna exceptions

Hittills har C# eller en annan funktion kastat våra exceptions.

Vi kan också själva använda `throw`.

```csharp
static void SetAge(int age)
{
    if (age < 0)
    {
        throw new ArgumentException("Ålder kan inte vara negativ.");
    }

    Console.WriteLine($"Ålder: {age}");
}
```

Funktionen vet att den inte kan utföra sitt jobb med ett negativt värde och signalerar därför detta genom att kasta ett exception.

Den som anropar funktionen kan sedan välja hur situationen ska hanteras:

```csharp
try
{
    SetAge(-5);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
```

Ett sätt att tänka på det är:

> **`throw`** – "Jag kan inte utföra det jag blivit ombedd att göra."

> **`catch`** – "Jag vet vad vi ska göra när det händer."

Det är särskilt användbart när vi skriver funktioner eller bibliotek. Funktionen som upptäcker problemet behöver inte veta hur resten av programmet vill hantera det.

## Exceptions ska inte ersätta vanlig programlogik

Använd inte exceptions som ett alternativ till vanliga kontroller när situationen enkelt kan kontrolleras.

Till exempel har vi tidigare använt `TryParse()`:

```csharp
if (int.TryParse(Console.ReadLine(), out int number))
{
    Console.WriteLine($"Du skrev {number}");
}
else
{
    Console.WriteLine("Du måste skriva ett heltal.");
}
```

Om felaktig input är något vi **förväntar oss** är detta ofta bättre än att använda exceptions för den vanliga programlogiken.

## Kort sammanfattning

**Exception** – signalerar att något gick fel när programmet kördes.  
**`try`** – innehåller kod där ett exception kan uppstå.  
**`catch`** – fångar och hanterar ett exception.  
**`finally`** – körs när vi lämnar `try`, oavsett om ett exception inträffade.  
**`throw`** – används för att själv kasta ett exception.  
**`Message`** – felmeddelandet.  
**`StackTrace`** – visar anropskedjan som ledde fram till felet.

### Grundstruktur

```csharp
try
{
    // Kod som kan kasta ett exception
}
catch (FormatException ex)
{
    // Hantera ett specifikt exception
}
catch (Exception ex)
{
    // Hantera andra exceptions
}
finally
{
    // Körs när vi lämnar try
}
```

### Kom ihåg

När inget går fel körs koden i `try` som vanligt och `catch` hoppas över.

När ett exception kastas avbryts det normala kodflödet. Exceptionet färdas upp genom call stacken tills en passande `catch` hittas.

**Fånga den typ av exception som du förväntar dig och vet hur du vill hantera – och fånga inte exceptions bara för att förhindra en krasch.**


## Tutorials:  
[Exception handling](https://www.tutorialsteacher.com/csharp/csharp-exception-handling)  
[Built-in Exceptions](https://www.tutorialsteacher.com/csharp/csharp-exception)  
[throw](https://www.tutorialsteacher.com/csharp/throw-csharp)  