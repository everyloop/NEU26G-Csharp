# September 18

I dag kollar vi .NET klasser som används för filhantering.

Klicka på rubrikerna för att komma till den officiella dokumentationen.

## [Path](https://learn.microsoft.com/en-us/dotnet/api/system.io.path?view=net-10.0)

Utför operationer på strängar som innehåller sökvägar.

<!--
**Code-along:**  
[L013_Path_and_Directory](https://github.com/everyloop/NEU25G-Csharp/blob/master/Code-alongs/L013_Path_and_Directory/Program.cs)
-->

### Sökvägar

Kolla gärna avsnittet om [absoluta och relativa sökvägar](https://github.com/everyloop/NEU25G-Csharp/blob/master/Lecture-notes/Sep1.md) från tidigare lektion.

## [Directory](https://learn.microsoft.com/en-us/dotnet/api/system.io.directory?view=net-10.0)

Tillhandahåller metoder för att bland annat skapa, flytta, och ta bort, och visa innehållet i mappar.

<!--
**Code-along:**  
[L013_Path_and_Directory](https://github.com/everyloop/NEU25G-Csharp/blob/master/Code-alongs/L013_Path_and_Directory/Program.cs)
-->

## [File](https://learn.microsoft.com/en-us/dotnet/api/system.io.file?view=net-10.0)

Tillhandahåller metoder för att bland annat skapa, flytta, kopiera och ta bort filer.

## [StreamReader](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamreader?view=net-10.0)

Används för att läsa text-data från en stream.

<!--
**Code-along:**  
[L014_StreamReader_&_StreamWriter](https://github.com/everyloop/NEU25G-Csharp/blob/master/Code-alongs/L014_StreamReader_%26_StreamWriter/Program.cs)
-->

## [StreamWriter](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamwriter?view=net-10.0)

Används för att skriva text-data till en stream.

<!--
**Code-along:**  
[L014_StreamReader_&_StreamWriter](https://github.com/everyloop/NEU25G-Csharp/blob/master/Code-alongs/L014_StreamReader_%26_StreamWriter/Program.cs)
-->

## [FileStream](https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream?view=net-10.0)

Används för att läsa/skriva binär-data från/till en fil.

## [StreamReader](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamreader?view=net-10.0)

Används för att läsa text-data från en stream.

<!--
**Code-along:**  
[L015_FileStream](https://github.com/everyloop/NEU25G-Csharp/blob/master/Code-alongs/L015_FileStream/Program.cs)
-->

## 📖 Lektionsanteckningar: Strömmar (Streams) i C#

### 💡 Vad är en Stream?
En **Stream** (ström) är en .NET-arkitektur för att flytta data från en plats till en annan. 
* **Metafor:** Tänk på en Stream som ett **vattenrör**. 
* **Data i bitar (Bytes):** Istället för att ladda in en hel gigantisk fil i datorns RAM-minne på en gång, öppnar du en ström. Datan flyttas då i mindre datablock (**bytes**) genom röret, i en jämn takt.
* **Enkelriktat:** Strömmar är oftast enkelriktade. Antingen läser du *in* data eller skriver *ut* data.
* **Måste stängas:** En ström är en operativsystemresurs. Du måste alltid stänga kranen (anropa `.Dispose()` eller använda `using`) när du är klar, annars låses filen i Windows.

### 🚰 De konkreta rören (Filsystem, Nätverk, Minne)
Klassen `Stream` är en abstrakt basklass (en ritning). I praktiken använder vi de konkreta klasserna som ärver från den:
* **`FileStream`** – Ett rör kopplat till en fysisk fil på hårddisken.
* **`NetworkStream`** – Ett rör kopplat till internet eller ett nätverk.
* **`MemoryStream`** – Ett rör kopplat direkt till en tillfällig yta i internminnet (RAM).

> ⚠️ **Viktigt:** Alla dessa rör transporterar bara **råa bytes** (anonyma ettor och nollor i grupper om åtta). En `FileStream` har ingen aning om ifall den flyttar en bild, en låt eller ett textdokument.

---

### 🚰 Munstyckena (Readers & Writers)
För att vi ska kunna förstå datan som sprutar ur röret behöver vi ett verktyg som översätter råa bytes till mänskligt språk. Det är här **`StreamReader`** och **`StreamWriter`** kommer in. De är *inte* strömmar själva, utan verktyg som vi skruvar fast på änden av en ström.

#### 1. `StreamReader` (Vattenfiltret)
* **Syfte:** Läser råa bytes från en ström och omvandlar dem till färdig text (`strings` och `chars`).
* **Koppling:** Du skruvar fast den på valfri ström (t.ex. en `FileStream` eller `NetworkStream`).
* **Vanliga metoder:** `ReadLine()` (läser en rad) och `ReadToEnd()` (läser resten av filen).

#### 2. `StreamWriter` (Injektorn)
* **Syfte:** Tar din mänskliga text (`strings`) och omvandlar den till råa bytes som kan skickas in i strömmen och sparas på disken eller skickas över nätverket.
* **Vanliga metoder:** `WriteLine()` (skriver en rad text) och `Flush()` (tvingar ut den sista datan ur bufferten till disken).

---

### 🛠️ Arkitekturen i praktiken (ASCII-diagram)
Här ser du hur klasserna samarbetar genom **komposition** (att stoppa in en klass i en annan):

```text
       [ Text i C# ]  (string text = "Hej!")
             │
             ▼
   ┌──────────────────┐
   │   StreamWriter   │  <-- Munstycket (Översätter text till bytes)
   └─────────┬────────┘
             │ (Bytes)
             ▼
   ┌──────────────────┐
   │    FileStream    │  <-- Röret (Transporterar rådata till disken)
   └─────────┬────────┘
             │
             ▼
      [ Hårddisken ]  (anteckningar.txt)
```

### 💻 Kodexempel: Det moderna mönstret
Här är hur vi kopplar samman ett rör (`FileStream`) med ett munstycke (`StreamReader`) på ett säkert sätt med modern `using`-syntax och felhantering:

```csharp
try
{
    // 1. Öppna röret till filen (Görs med en FileStream)
    using FileStream ror = new FileStream("anteckningar.txt", FileMode.Open);

    // 2. Skruva på munstycket på röret (Skicka med strömmen till konstruktorn)
    using StreamReader filter = new StreamReader(ror);

    // 3. Läs texten rad för rad
    while (!filter.EndOfStream)
    {
        string rad = filter.ReadLine();
        Console.WriteLine(rad);
    }
} // <-- HÄR stängs både filtret och röret automatiskt (Out of Scope)!
catch (FileNotFoundException)
{
    Console.WriteLine("Fel: Filen hittades inte.");
}
catch (IOException ex)
{
    Console.WriteLine($"Ett filfel uppstod: {ex.Message}");
}
```

---

### 🧠 Sammanfattning för minnet
* **`Stream`** = Transportören av råa bytes (Röret).
* **`StreamReader / Writer`** = Tolken av text (Munstycket).
* **`using`** = Garanterar att kranen stängs (Dispose) automatiskt när blocket/scopet tar slut.
* **`try-catch`** = Tar hand om olyckor (Undantag), som att filen saknas.
