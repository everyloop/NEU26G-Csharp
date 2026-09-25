# September 25


**Lecture slide:**  
[ValueAndReferenceTypes.pdf](https://github.com/everyloop/NEU26G-Csharp/blob/master/Resources/ValueAndReferenceTypes.pdf)


## Stack & Heap

För att förstå minnes hantering i C#, och därmed bättre förstå value- och reference types, pass-by-value, pass-by-reference, och när man bör använda vad, så kan det vara en god idé att först förstå skillnaden på stack och heap.

[Läs mer här!](https://endjin.com/blog/2022/07/understanding-the-stack-and-heap-in-csharp-dotnet)

## Value types & Reference types

Alla datatyper i .NET är antingen en value type eller en reference type.

Value types lagrar det faktiska datavärdet direkt i minnet för variabeln.

Reference types lagrar i stället en referens till det faktiska värdet/objektet.

[Läs mer här!](https://medium.com/@dev.msalah/value-vs-reference-types-in-c-573e3cf6c5bf)

## Pass-by-value & Pass-by-reference

När man skickar in variabler i metoder kan dessa skickas antingen som pass-by-value eller pass-by-reference. Detta gäller oavsett om det är en value type eller en reference typ.

Pass-by-reference markeras med keyword "ref" eller "out" i C#, och innebär att metoden kan uppdatera den variabel som skickats in som argument vid anropet.

[Läs mer här!](https://www.c-sharpcorner.com/UploadFile/f1047f/story-of-pass-by-value-and-pass-by-reference-in-C-Sharp/)

[Se även: Ref vs Out](https://medium.com/@nwonahr/understanding-the-ref-and-out-keywords-in-c-f23757684575)

**Code-along:**  
[L021_Pass_by_value_vs_Pass_by_reference](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L021_Pass_by_value_vs_Pass_by_reference/Program.cs)

## Struct (Value Type) vs Class (Reference Type)

En jämförelse av `struct` och `class` i C#.

---

### 1. Den fundamentala skillnaden

| Egenskap | `struct` (Value Type) | `class` (Reference Type) |
| :--- | :--- | :--- |
| **Var lagras datan?** | På **Stacken** (eller direkt inuti det objekt den tillhör). | På **Heapen** (variabeln på stacken är bara en adress/pekar). |
| **Vid tilldelning (`=`)** | Hela värdet **kopieras**. | Endast **referensen** (adressen) kopieras. |
| **Standardjämförelse** | Jämför om alla **värden** i fälten är lika. | Jämför om de pekar på **samma objekt** i minnet. |
| **Null-värden** | Kan inte vara `null` som standard (måste vara t.ex. `int?` eller `Point?`). | Kan vara `null`. |

---

### 2. Kodexempel (Det vi körde på lektionen)

Tänk dig följande två instanser av en punkt:
```csharp
Point p1 = new Point(3.0, 5.0);
Point p2 = new Point(3.0, 5.0);
```

#### Exempel A: Jämförelse med `.Equals()`
```csharp
Console.WriteLine(p1.Equals(p2));
```
*   **Om Point är en `struct`:** Skriver ut `True`. C# tittar på innehållet och ser att båda har X=3 och Y=5.
*   **Om Point är en `class`:** Skriver ut `False`. C# tittar på minnesadressen. Eftersom vi använde `new` två gånger ligger de på olika ställen på heapen.

#### Exempel B: Kopiering och ändring
```csharp
Point p3 = p2; // Tilldelning
p3.X = 4.0;    // Vi ändrar X på p3
```

Vad händer med `p2.X`?
*   **Om Point är en `struct`:** `p2.X` är fortfarande `3.0`. När vi gjorde `p3 = p2` skapades en helt oberoende kopia. Att ändra `p3` påverkar inte `p2`.
*   **Om Point är en `class`:** `p2.X` ändras också till `4.0`! Både `p2` och `p3` pekar på exakt samma objekt på heapen.

---

### 3. Minnesbild (Skiss)

När du skapar en `class` vs en `struct` ser minneshanteringen ut så här under huven:

```text
[ STRUCT - Allt på Stacken ]
Stack:
  p1 -> [ X: 3.0, Y: 5.0 ]
  p2 -> [ X: 3.0, Y: 5.0 ]
  p3 -> [ X: 4.0, Y: 5.0 ]  <-- En helt egen låda i minnet!


[ CLASS - Referenser på Stacken, Data på Heapen ]
Stack:              Heap:
  p1 ----------->  [ Objekt 1: X: 3.0, Y: 5.0 ]
  p2 -----\
  p3 ------+---->  [ Objekt 2: X: 4.0, Y: 5.0 ]  <-- Båda pekar hit!
```

---

### 4. När ska man använda vad?

#### Använd `struct` när:
1.  Objektet representerar ett **enda litet värde** (t.ex. koordinater, färger, valuta, datum).
2.  Objektet är **litet** (en tumregel är under 16 bytes, t.ex. max 2-4 primitiva typer).
3.  Objektet ska vara **kortlivat** och prestandan på stacken är viktig.
4.  *Best Practice:* Det bör vara **immutabelt** (oföränderligt).

#### Använd `class` när:
1.  Objektet representerar en **identitet eller affärslogik** (t.ex. `User`, `Customer`, `Invoice`).
2.  Objektet är **stort** eller innehåller mycket data.
3.  Du behöver använda **arv** (en struct kan inte ärva från en annan struct).
4.  Objektet behöver kunna ändra sitt tillstånd ofta utan att hela värdet kopieras runt i minnet.


**Code-along:**  
[L022_Struct_&_Enum](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L022_Struct_vs_Class/Program.cs)
