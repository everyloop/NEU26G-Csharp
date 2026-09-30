# September 30

## 🛠️ Konstruktorkedjning (Constructor Chaining)

**Code-along:**  
[L024_Constructor_chaining](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L024_Constructor_chaining/Program.cs)

### Syfte
Säkerställer att ett objekts interna tillstånd byggs upp på ett säkert och förutsägbart sätt hela vägen från botten av arvskedjan upp till subklassen.

### Hur objektet föds i minnet
När du skriver `new Cat("Caty");` sker processen i två tydliga faser under huven:
1.  **Initieringsfasen (Nedifrån och upp):** Minne allokeras för hela objektet. .NET går från den mest specifika klassen (`Cat`) upp till roten (`System.Object`) och förbereder alla variabler och egenskapsinitierare.
2.  **Konstruktorsfasen (Uppifrån och ner):** Själva kodblocken (måsvingarna) exekveras i motsatt ordning. `System.Object` körs först, sedan rör sig koden nedåt i arvskedjan tills den når klassen du faktiskt instansierade.

### Reglerna för kedjningen
*   **Implicit (Dolt):** Om du inte anger något själv skjuter kompilatorn automatiskt in ett dolt `: base()`-anrop som letar efter en parameterlös konstruktor i basklassen.
*   **Explicit (Aktivt):** Om basklassen saknar en parameterlös konstruktor *måste* utvecklaren ta över ansvaret och manuellt kedja anropet vidare med hjälp av nyckelorden `: base(...)` eller `: this(...)`.

### Kodexempel: Exekveringsordning
```csharp
new Cat("Caty");

class Animal
{
    public Animal()
    {
        Console.WriteLine("1. Running Animal constructor...");
    }
}

class Mammal : Animal
{
    public Mammal()
    {
        Console.WriteLine("2. Running Mammal constructor...");
    }
}

class Cat : Mammal
{
    public Cat()
    {
        Console.WriteLine("3. Running Cat constructor...");
    }

    // Explicit kedjning internt till den egna klassens tomma konstruktor
    public Cat(string name) : this()
    {
        Console.WriteLine("4. Assigning Cat name...");
    }
}
```

---

## 🎯 Mönstermatchning (Pattern Matching)

**Code-along:**  
[L025_Pattern_matching](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L025_Pattern_matching/Program.cs)

### Syfte
Mönstermatchning används för att testa om en bit data har en viss "form" eller struktur. Om formen matchar kan vi plocka ut datan och köra logik på den i ett och samma steg.
*   **Tvättar bort pyramidkod:** Ersätter nästlade `if`-satser och tar bort fula, osäkra typkastningar (`cast/as`).
*   **Dataorienterat:** Perfekt når logiken ska ligga utanför klasserna själva (t.ex. vid hantering av råa API-svar eller datastrukturer).

---

### 1. Nyckelordet `is`
Används för att matcha ett värde eller ett objekt mot ett mönster.
*   **Inspekterar formen:** Kontrollerar om värdet till vänster uppfyller kriterierna till höger.
*   **Hanterar `null`:** Returnerar *alltid* `false` om objektet till vänster är `null` (om du inte uttryckligen matchar mot mönstret `null`). Detta gör `is not null` till det säkraste sättet att göra null-kontroller i C#.

```csharp
int x = 5;

Console.WriteLine($"x is 6 => {x is 6}");                        // Matchar konstant
Console.WriteLine($"x is not 6 => {x is not 6}");                // Negativt mönster
Console.WriteLine($"x is < 6 => {x is < 6}");                    // Relationsmönster
Console.WriteLine($"x is > 0 and < 6 => {x is > 0 and < 6}");    // Kombinerat mönster
```

När vi mönstermatchar på objekt inuti en arvskedja kan vi kontrollera den faktiska, underliggande typen på heapen:
```csharp
Animal animal = new Dog();

Console.WriteLine($"animal is Dog => {animal is Dog}");            // True
Console.WriteLine($"animal is Cat or Mouse => {animal is Cat or Mouse}"); // False
```

#### Typkastning med `is` (Declaration Pattern)
Om kompilatorn med 100 % säkerhet kan spåra att ett `is`-uttryck utvärderades till `true`, skapas en färdigpackad och typsäker variabel som är redo att användas direkt inuti det blocket:
```csharp
if (animal is Dog dog)
{
    // Kompilatorn vet att detta är sant. Variabeln 'dog' är säker att använda här!
    dog.Bark(); 
}
```

---

### 2. Switch Expressions (Switch-uttryck)
Ersätter den traditionella, klumpiga `switch`-satsens `case` och `break` med en kompakt och lättläst tabell. 

**Viktig skillnad:** Vi använder ett *switch-uttryck* när vi vill **välja och returnera ett värde** — inte när vi vill köra ett helt kodblock med instruktioner.

### Regler för Switch Expressions
1.  Returnerar **alltid** ett värde direkt.
2.  Använder pil-syntax (`=>`) i stället för kolon (`:`).
3.  Mönstren utvärderas strikt **uppifrån och ner**. Det första mönstret som är sant vinner, resten ignoreras.
4.  `_` (Discard/Understreck) matchar allt och används absolut sist som en fallback (motsvarar `default:`).
5.  Kompilatorn **varnar** om inte alla tänkbara utfall täcks in i tabellen. Om programmet körs och landar på ett värde som saknar mönster kraschar applikationen med en `SwitchExpressionException`.

```csharp
int temperatur = -5;

string väderBeskrivning = temperatur switch
{
    < 0 => "Det är minusgrader och fryser.",
    0 => "Det är precis på fryspunkten.",
    > 0 and <= 10 => "Det är ganska svalt ute.",
    _ => "Det är behagligt eller varmt." // Fångar upp allt annat
};
```

---

## 🧠 Skillnaden mellan mönster- och kodoperatorer

Blanda inte ihop de traditionella logiska operatorerna med de nya mönsteroperatorerna! De används på helt olika platser i språket:

*   **`&&`, `||`, `!` (Logiska operatorer):** Arbetar på kodnivå i vanliga `if`-satser. Kräver kompletta, fristående `true`/`false`-påståenden på båda sidor (`if (ärVuxen && harKörkort)`).
*   **`and`, `or`, `not` (Mönsteroperatorer):** Arbetar enbart *inuti* mönstermatchningar (tillsammans med `is` eller i en `switch`). De bygger upp en kravspecifikation för ett och samma värde (`if (x is > 0 and < 10)`).

## Clean code

Vi gick igenom några av "Clean Code"-rekommendationerna från detta repo:  

https://github.com/Geeksltd/Programming.Tips

### Direktlänkar till olika avsnitt vi kollade på:
- [Intro](https://github.com/Geeksltd/Programming.Tips/blob/master/docs/Intro.md)
- [Code Comments](https://github.com/Geeksltd/Programming.Tips/blob/master/docs/CodeComments.md)
- [Meaningful Names](https://github.com/Geeksltd/Programming.Tips/blob/master/docs/MeaningfulNames.md)
- [Naming classes](https://github.com/Geeksltd/Programming.Tips/blob/master/docs/naming/class.md)

### Kolla gärna även på:
- [One thing per method](https://github.com/Geeksltd/Programming.Tips/blob/master/docs/methods/do-one-thing.md)
- [Avoid side effects](https://github.com/Geeksltd/Programming.Tips/blob/master/docs/methods/side-effects.md)
- [Stepdown rule](https://github.com/Geeksltd/Programming.Tips/blob/master/docs/methods/stepdown-rule.md)

## [Robert C. Martin](https://en.wikipedia.org/wiki/Robert_C._Martin)

Amerikansk mjukvaruingenjör och författare som skrivit boken "Clean Code". Mycket läsvärd om man vill djupdyka i ämnet. Ni hittar boken på t.ex. [Adlibris.se](https://www.adlibris.com/sv/bok/clean-code-9780132350884)