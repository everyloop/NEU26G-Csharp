# September 4

## Debugging (felsökning) i C# och Visual Studio

Debugging innebär att vi **pausar ett program medan det körs** och undersöker vad som händer. Det är användbart både för att hitta fel och för att förstå hur koden fungerar.

## Breakpoints

En **breakpoint (brytpunkt)** gör att programmet pausas när det kommer till en viss kodrad.

Du skapar en breakpoint genom att klicka i marginalen till vänster om kodraden. Den visas som en **röd punkt**.

```csharp
int x = 10;
int y = 20;

int result = x + y;
Console.WriteLine(result);
```

Vi kan exempelvis sätta en breakpoint på raden:

```csharp
int result = x + y;
```

När programmet når raden pausas det och vi kan börja undersöka vad som händer.

## Step Over – F10

När programmet är pausat kan vi använda **Step Over** för att köra koden **en rad i taget**.

Kortkommando:

**F10**

För varje tryck på F10 körs den aktuella raden och programmet går vidare till nästa.

Det gör att vi kan följa programmet steg för steg och se hur variablernas värden förändras.

### Var befinner sig programmet?

När programmet är pausat visar Visual Studio med en **gul markering/pil** vilken kodrad som står på tur att köras.

Det gör det enkelt att följa programmets väg genom koden.

## Undersöka variabler

När programmet är pausat kan du hålla muspekaren över en variabel direkt i koden för att se dess aktuella värde.

Exempel:

```csharp
string name = "Lisa";
int age = 27;

Console.WriteLine($"{name} är {age} år.");
```

Om programmet är pausat kan vi hålla muspekaren över `name` eller `age` och se vilka värden de innehåller.

## Locals

Fönstret **Locals** visar de lokala variabler som finns tillgängliga där programmet är pausat.

Exempelvis:

| Variabel | Värde |
| --- | --- |
| `name` | `"Lisa"` |
| `age` | `27` |

När vi använder **F10** kan vi följa hur variablernas värden förändras medan programmet körs.

## Ett enkelt sätt att debugga

När programmet inte fungerar som du förväntar dig:

1. Sätt en **breakpoint** i närheten av där du tror att problemet finns.
2. Starta programmet i debug-läge.
3. Undersök variablernas värden.
4. Använd **F10 (Step Over)** för att köra en rad i taget.
5. Håll koll på värdena direkt i koden eller i **Locals**.

Fråga dig:

**"Har variablerna de värden jag förväntar mig här?"**

Om svaret är nej har du ofta kommit nära orsaken till felet.

## Kort sammanfattning

**Breakpoint** – pausar programmet på en viss kodrad.  
**F10 / Step Over** – kör en kodrad i taget.  
**Gul markering** – visar var programmet befinner sig.  
**Locals** – visar lokala variabler och deras värden.  
**Håll över en variabel** – visar variabelns aktuella värde.

## Olika typer av fel

Det finns 3 olika kategorier av fel som vi kan ha i våran kod.

### Syntax error (Kompileringsfel)
Koden är felskriven så att kompilatorn inte förstår och inte kan bygga ditt program. I detta fall så får man en lista över alla fel i koden, med beskrivning av felet, samt i vilken fil och rad felet är. 

Denna typ av fel är de enklaste att upptäcka och fixa. Googla felmedelandet, eller kolla upp i dokumentation.

### Runtime error (Exekveringsfel)
Koden är "korrekt" så tillvida att programmet kompilerar, men programmet "krashar" under körning, med ett felmeddelande.

### Logical errors (Logiskt fel)
Programmet kompilerar och startar, och det krashar inte heller. Men, resultatet av programmet är inte det förväntade.

## Arrayer

**Code-along:**  
[L006_Array](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L006_Array/Program.cs)

**Tutorials:**  
[Array](https://www.tutorialsteacher.com/csharp/array-csharp)  
[Multidimensional array](https://www.tutorialsteacher.com/csharp/csharp-multi-dimensional-array)  
[Jagged array](https://www.tutorialsteacher.com/csharp/csharp-jagged-array)  

## Foreach
Foreach är en typ av loop som används för att iterera igenom alla element i en array, eller andra dataset (t.ex alla tecken i en sträng). 

Loopens kodblock körs då en gång ***för varje*** element i arrayen, Foreach har en lokal variabel som för varje iteration tilldelas nästa elements värde, ända tills man gått igenom alla värden i arrayen.

Till skillnad från när man använder en for-loop behöver man alltså inte använda indexering för att få ut värdena från de enskilda elementen.

**Exempel:** Utskrift av alla bilar i arrayen *cars*.
``` cs
string[] cars = {"Volvo", "BMW", "Ford", "Mazda"};
foreach (string car in cars) 
{
  Console.WriteLine(car);
}
```

Den lokala variabeln (car) är alltid read-only. Det betyder att med en foreach kan man aldrig uppdatera data i datasetet man loopar igenom. Vill man uppdatera behöver man istället använda en for-loop och indexering.

[Läs mer här!](https://www.programiz.com/csharp-programming/foreach-loop)

## Strängar

**Code-along:**  
[L007_Strängar](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L007_Strängar/Program.cs)

### Indexerbara
Strängar i C# är indexerbara, d.v.s man kan komma åt enskilda tecken (datatyp Char) med hjälp av index, på samma sätt som man kan komma åt enskilda element i en array. Till skillnad från en char[] så är kan man inte skriva till (ändra) den enskilda tecknen i en array genom indexering; de är read-only.

### Escape sequences

Teckenkombinationer som består av ett backslash följt av ett annat tecken kallas för "escape sequences". De används för att i kod representera t.ex, radbyte, tab, citattecken och andra tecken som man annars inte kan skriva i en vanlig string literal.

[Läs mer här!](https://learn.microsoft.com/en-us/cpp/c-language/escape-sequences?view=msvc-170)

### Interpolation strings

String interpolation är en metod för att konkatenera, formatera och manipulera strängar.

``` cs
int operand1 = 5;
int operand2 = 7;

// Interpolation strings starts with a $
string myString = $"{operand1} + {operand2} = {operand1 + operand2}";
```
[Läs mer här!](https://www.c-sharpcorner.com/article/understanding-string-interpolation-in-c-sharp/)