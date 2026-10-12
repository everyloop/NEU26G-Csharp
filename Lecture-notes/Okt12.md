# Oktober 12

Under den här lektionen började vi titta på hur datorer och program kan hantera flera saker samtidigt.

Vi gick igenom begreppen **synchronous**, **asynchronous**, **concurrency**, **parallelism**, **multitasking**, **multithreading** och **multiprocessing**.

Vi tittade också på hur trådar fungerar, hur .NET använder en ThreadPool och hur vi kan använda **PLINQ** och **Task Parallel Library (TPL)** för att utföra arbete parallellt.

Under lektionen gjorde vi två större demonstrationer:

1. LINQ, `IEnumerable<T>`, minnesförbrukning och parallella beräkningar med PLINQ.
2. `Task`, `Task<TResult>`, `Task.Run()`, `Status` och `Result`.

---

# 1. Synkron och asynkron programmering

## Synkron programmering

Vid synkron programmering väntar den anropande koden på att en operation ska bli färdig innan den fortsätter.

Ett exempel från presentationen var att göra en kopp te:

```text
GetKettle()
FillKettle()
BoilWater()
GetCup()
AddTea()
PourWater()
```

Vi gör en sak i taget, och nästa steg påbörjas först när föregående metod har returnerat.

Om `BoilWater()` tar lång tid kan vi inte fortsätta med nästa instruktion förrän metoden är klar.

## Asynkron programmering

Vid asynkron programmering kan vi påbörja en operation och låta annan kod fortsätta medan vi väntar på att operationen ska bli färdig.

I presentationen såg vi ett exempel som liknade detta:

```csharp
var boilingWater = BoilWaterAsync();

GetCup();
AddTea();

await boilingWater;

PourWater();
```

Här startar vi uppvärmningen av vattnet och gör andra saker medan vi väntar.

När vi behöver det varma vattnet väntar vi på att operationen ska bli klar.

**Viktigt:** Asynkron programmering betyder inte automatiskt att vi använder flera trådar eller flera processorkärnor.

Vi kommer att gå djupare in på `async` och `await` under nästa lektion.

---

# 2. Concurrency och Parallelism

Begreppen *concurrency* och *parallelism* är relaterade, men betyder inte samma sak.

## Concurrency – samtidighet

**Concurrency** innebär att flera uppgifter gör framsteg under samma tidsperiod.

Det behöver inte betyda att de faktiskt exekverar vid exakt samma ögonblick.

En processor kan till exempel växla snabbt mellan olika trådar:

```text
Tid ──────────────────────────────>

Task A: ███       ███       ███
Task B:    ███       ███
Task C:       ███       ███
```

De olika uppgifterna delar på processorns exekveringstid.

För användaren kan det upplevas som att allt händer samtidigt.

## Parallelism – parallellism

**Parallelism** innebär att flera uppgifter faktiskt exekverar samtidigt.

Det kräver att det finns möjlighet att exekvera på flera processorer eller processorkärnor.

```text
Tid ──────────────────────────────>

CPU 1: Task A █████████████
CPU 2: Task B █████████████
CPU 3: Task C █████████████
```

Här arbetar flera processorer samtidigt.

### Skillnaden

- **Concurrency:** Flera uppgifter hanteras under samma tidsperiod.
- **Parallelism:** Flera uppgifter exekverar samtidigt.

Ett program kan alltså vara concurrent utan att vara parallellt.

Parallell exekvering är samtidigt ett sätt att uppnå concurrency.

---

# 3. Multitasking, Multithreading och Multiprocessing

Vi gick igenom tre begrepp som beskriver olika sätt att organisera arbete.

## Multitasking

**Multitasking** innebär att ett operativsystem kan hantera flera processer under samma tidsperiod.

Exempelvis kan vi samtidigt ha:

- En webbläsare
- Visual Studio
- Spotify
- Windows Aktivitetshanterare

Varje program kan ha en eller flera processer.

Operativsystemet schemalägger vilka trådar som får använda processorn.

Om det finns fler körbara trådar än tillgängliga logiska processorer måste de dela på processortiden.

## Multithreading

**Multithreading** innebär att en process har flera trådar.

En tråd är en exekveringsenhet som operativsystemet kan schemalägga.

Trådarna i samma process delar bland annat:

- Processens minnesutrymme
- Objekt på heapen
- Statiska fält
- Andra resurser som tillhör processen

Varje tråd har däremot sin egen anropsstack och sitt eget exekveringstillstånd.

Eftersom trådarna kan komma åt samma data behöver vi ibland skydda delad information. Vi kommer tillbaka till detta när vi pratar om **thread safety** och **race conditions**.

## Multiprocessing

I presentationen använde vi **multiprocessing** i betydelsen att en dator har flera fysiska processorer som kan utföra arbete samtidigt.

Detta är en hårdvarubaserad användning av begreppet.

I modern programvaruterminologi används *multiprocessing* också ofta för att beskriva att ett program fördelar arbete mellan flera separata processer.

Det är alltså bra att känna till att termen kan användas i olika sammanhang.

---

# 4. Threads och operativsystemets scheduler

En **thread** är en tråd av instruktioner som kan exekveras av processorn.

När ett program startar finns det normalt minst en tråd som börjar köra programmets kod.

Ett program kan också skapa eller använda ytterligare trådar.

## Hur kan många trådar dela på processorn?

Operativsystemet har en **scheduler** som bestämmer vilka trådar som ska få exekvera.

En tråd kan exempelvis vara:

- **Running:** Exekverar just nu på en processor.
- **Runnable/Ready:** Är redo att exekvera, men väntar på processortid.
- **Waiting/Blocked:** Väntar på exempelvis en timer, I/O eller synkronisering.

När operativsystemet byter från en tråd till en annan kallas det en **context switch**.

Då sparas den ena trådens exekveringstillstånd så att den senare kan fortsätta där den slutade.

### Logiska processorer

Vi tittade på datorns logiska processorer i Windows Aktivitetshanterare.

Datorn vi använde hade **12 logiska processorer**.

Det innebär att upp till 12 trådar kan exekvera samtidigt på de logiska processorerna, även om det kan finnas tusentals trådar i systemet.

Alla trådar är inte aktiva samtidigt. Många väntar på något och använder då ingen CPU-tid.

En viktig sak vi såg senare i demonstrationen är att en tråd inte behöver vara knuten till en viss logisk processor.

Windows kan flytta tråden mellan olika processorer.

---

# 5. Thread, ThreadPool och Task Parallel Library

Vi jämförde tre nivåer av abstraktion för parallell programmering i .NET.

## Thread

Klassen `Thread` låter oss skapa och hantera en tråd mer direkt.

Exempel:

```csharp
Thread thread = new Thread(() =>
{
    Console.WriteLine("Hello from another thread!");
});

thread.Start();
```

Vi ansvarar då själva för att skapa och starta tråden.

Att skapa separata trådar för varje liten arbetsuppgift kan bli onödigt kostsamt.

## ThreadPool

.NET har därför en **ThreadPool**.

Den innehåller arbetstrådar som .NET kan återanvända för olika uppgifter.

I stället för att skapa en helt ny tråd för varje arbetsuppgift kan arbete placeras i en kö och utföras av en tillgänglig tråd i poolen.

Förenklat:

```text
Arbetsuppgifter
      |
      v
   Arbetskö
      |
      v
  ThreadPool
   /   |   \
  T1   T2   T3
```

Detta är en förenklad bild av hur exempelvis arbete som skickas in med `Task.Run()` kan hanteras.

## Task Parallel Library – TPL

**Task Parallel Library (TPL)** är ett bibliotek i .NET som gör det enklare att arbeta med parallella operationer.

I stället för att själva hantera trådar kan vi beskriva vilka arbetsuppgifter vi vill utföra.

TPL och .NET hanterar sedan mycket av arbetet med att schemalägga och koordinera operationerna.

Några viktiga delar är:

- `Task`
- `Task<TResult>`
- `Task.Run()`
- `Parallel.For()`
- `Parallel.ForEach()`
- PLINQ

### En Task är inte samma sak som en Thread

Detta är en viktig skillnad.

En **Thread** är en exekveringsenhet.

En **Task** representerar en operation som kan slutföras i framtiden.

När vi använder `Task.Run()` skickas arbete normalt till ThreadPool.

Men alla Tasks behöver inte ha en egen tråd, och en Task är inte en tråd.

Det blir särskilt viktigt när vi senare arbetar med asynkrona I/O-operationer.

---

# 6. LINQ, IEnumerable och materialisering

Vi repeterade LINQ och tittade närmare på hur sekvenser fungerar.

## IEnumerable<T>

`IEnumerable<T>` är ett interface som representerar en sekvens av värden som vi kan iterera över.

Det innebär inte att alla värden måste finnas lagrade i minnet samtidigt.

Vi använde:

```csharp
var range = Enumerable.Range(1, 10);

foreach (int number in range)
{
    Console.WriteLine(number);
}
```

`Enumerable.Range(1, 10)` ger oss en `IEnumerable<int>` som genererar heltalen 1 till 10 när vi itererar över den.

## Klassen Enumerable

`Enumerable` är en statisk klass i `System.Linq`.

Den innehåller många av de extension methods vi känner igen från LINQ:

```csharp
.Where()
.Select()
.OrderBy()
.Take()
.ToList()
.Sum()
```

De flesta av dessa metoder arbetar med `IEnumerable<T>`.

Extension methods gör att vi kan skriva exempelvis:

```csharp
var numbers = Enumerable.Range(1, 10);

var result = numbers.Where(n => n > 5);
```

I stället för att uttryckligen anropa den statiska metoden:

```csharp
var result = Enumerable.Where(numbers, n => n > 5);
```

---

# 7. Demo 1 – IEnumerable och minnesförbrukning

Vi ville undersöka skillnaden mellan att generera värden efter hand och att lagra dem i en lista.

Vi använde följande kod:

```csharp
Console.WriteLine("Calculating sum");

long sum = Enumerable
    .Range(1, 2000_000_000)
    //.ToList()
    .Select(i => (long)i)
    .Sum();

Console.WriteLine("Calculation completed.");

Console.WriteLine($"Sum = {sum}");
```

Vi körde programmet både med och utan `.ToList()` och följde minnesförbrukningen i Aktivitetshanteraren.

## Utan ToList()

```csharp
long sum = Enumerable
    .Range(1, 2000_000_000)
    .Select(i => (long)i)
    .Sum();
```

Här behöver vi inte lagra två miljarder heltal i en lista.

`Range()` kan generera ett tal i taget, och `Sum()` kan summera talen efter hand.

## Med ToList()

```csharp
long sum = Enumerable
    .Range(1, 2000_000_000)
    .ToList()
    .Select(i => (long)i)
    .Sum();
```

Nu skapar vi först en lista som ska innehålla alla två miljarder heltal.

Det kräver väldigt mycket minne.

Vi såg hur minnesförbrukningen steg kraftigt i Aktivitetshanteraren.

**Obs:** Att materialisera två miljarder heltal kan också misslyckas på grund av minnes- eller storleksbegränsningar, beroende på miljö och tillgängliga resurser.

## Deferred execution

Många LINQ-operationer använder **deferred execution**.

Det innebär att bearbetningen skjuts upp tills vi faktiskt börjar iterera över sekvensen.

Exempel:

```csharp
var numbers = Enumerable.Range(1, 100);

var result = numbers
    .Where(n => n % 2 == 0)
    .Select(n => n * 10);
```

Här har vi beskrivit hur vi vill bearbeta sekvensen.

När vi senare exempelvis använder `foreach`, `ToList()` eller `Sum()` utförs bearbetningen.

**Viktigt:** `Sum()` går igenom sekvensen och räknar ut ett resultat, men behöver inte skapa en lista med alla värden.

---

# 8. PLINQ – Parallel LINQ

PLINQ står för **Parallel Language Integrated Query**.

Det gör att vi kan använda ett LINQ-liknande arbetssätt för att bearbeta data parallellt.

## Från LINQ till PLINQ

Det centrala metodanropet är:

```csharp
.AsParallel()
```

Exempel:

```csharp
var data = Enumerable
    .Range(1, 200)
    .AsParallel()
    .Select(i => CostlyOperation(i))
    .ToList();
```

När vi anropar `AsParallel()` får vi en `ParallelQuery<T>`.

PLINQ använder extension methods från den statiska klassen `ParallelEnumerable`, i stället för de vanliga LINQ-metoderna från `Enumerable`.

Många metoder har samma namn och liknande användning:

| LINQ | PLINQ |
|---|---|
| `IEnumerable<T>` | `ParallelQuery<T>` |
| `Enumerable` | `ParallelEnumerable` |
| `Where()` | `Where()` |
| `Select()` | `Select()` |
| `ToList()` | `ToList()` |

Det betyder att vi kan återanvända mycket av det vi redan lärt oss om LINQ.

Skillnaden är att PLINQ kan dela upp arbetet så att flera element bearbetas parallellt.

---

# 9. Demo 2 – CPU-belastning med PLINQ

Vi använde följande kod:

```csharp
Console.WriteLine("Begin data processing.");

var data = Enumerable
    .Range(1, 200)
    .AsParallel()
    .WithDegreeOfParallelism(Environment.ProcessorCount / 2)
    .Select(i => CostlyOperation(i))
    .ToList();

Console.WriteLine("Processing completed. Here is the processed data:");

Console.WriteLine(string.Join(", ", data));

static int CostlyOperation(int n)
{
    Console.WriteLine($"Processing item {n}");

    // En lång loop för att skapa CPU-belastning.
    for (int i = 0; i < 1_000_000_000; i++) ;

    // Vi testade också att jämföra med:
    // Thread.Sleep(500);

    return n;
}
```

Metoden `CostlyOperation()` representerar en tidskrävande beräkning.

Den tomma loopen användes för att skapa CPU-belastning under demonstrationen.

En sådan tom loop kan optimeras bort av kompilatorn eller JIT i vissa situationer, så den är inte lämplig som ett tillförlitligt prestandatest. Den fungerade däremot för att illustrera CPU-belastning under vår körning.

## Utan AsParallel()

När vi tog bort:

```csharp
.AsParallel()
```

bearbetades elementen sekventiellt.

En tråd utförde beräkningarna.

I Aktivitetshanteraren kunde vi ändå se aktivitet på flera olika logiska processorer.

**Varför?**

Windows scheduler kan flytta en tråd mellan olika logiska processorer.

Det innebär inte att tråden körs på flera processorer samtidigt.

## Med AsParallel()

När vi lade tillbaka:

```csharp
.AsParallel()
```

kunde PLINQ fördela arbetet mellan flera trådar.

Nu kunde flera beräkningar utföras samtidigt på olika logiska processorer.

Vi såg hur CPU-belastningen ökade kraftigt och fördelades över datorns logiska processorer.

Detta illustrerade **parallelism** i praktiken.

## WithDegreeOfParallelism()

Vi använde också:

```csharp
.WithDegreeOfParallelism(Environment.ProcessorCount / 2)
```

På datorn vi använde fanns 12 logiska processorer.

Därför blev uttrycket:

```csharp
Environment.ProcessorCount / 2
```

lika med `6`.

`WithDegreeOfParallelism(6)` begränsar hur många arbetare PLINQ använder samtidigt för frågan.

Det innebär **inte** att vi väljer sex specifika processorer.

Windows bestämmer fortfarande vilka logiska processorer som exekverar trådarna.

Vi kunde därför se aktivitet på alla tolv graferna, trots att vi begränsade parallellismen till sex.

### Viktig skillnad

**PLINQ:** Bestämmer hur arbetet kan delas upp.

**ThreadPool:** Tillhandahåller arbetstrådar.

**Operativsystemets scheduler:** Bestämmer när och på vilka processorer trådarna exekverar.

## Resultatens ordning

Vi skrev ut:

```csharp
Console.WriteLine($"Processing item {n}");
```

När flera trådar arbetar parallellt behöver dessa utskrifter inte komma i nummerordning.

PLINQ garanterar inte heller att resultatens ursprungliga ordning bevaras som standard.

Om vi behöver bevara ordningen kan vi använda `AsOrdered()`, men det kan innebära extra arbete och påverka prestandan.

## Är PLINQ alltid snabbare?

Nej.

PLINQ är framför allt användbart när:

- Vi har många element.
- Varje element kräver relativt mycket CPU-arbete.
- Elementen kan bearbetas oberoende av varandra.

Att fördela och samordna arbetet kostar också tid.

För små eller enkla operationer kan vanlig LINQ därför vara snabbare.

---

# 10. Task och Task<TResult>

Mot slutet av lektionen började vi arbeta med `Task`.

En **Task** representerar en operation.

Vi behöver inte själva skapa och hantera en separat `Thread` för att köra arbetet.

Vi tittade på två typer:

```csharp
Task myTask;

Task<string> myGenericTask;
```

## Task

En vanlig `Task` representerar en operation som inte producerar något resultatvärde.

Exempel:

```csharp
Task task = Task.Run(() =>
{
    Console.WriteLine("Running task.");
});
```

Här skickar vi en arbetsuppgift till ThreadPool.

`Task.Run()` returnerar en `Task` som vi kan använda för att följa operationen.

## Task<TResult>

`Task<TResult>` representerar en operation som producerar ett resultat av typen `TResult`.

Exempel:

```csharp
Task<int> task = Task.Run(() =>
{
    return 42;
});
```

Här är resultattypen `int`.

Därför får vi en:

```csharp
Task<int>
```

Vi kan också ha exempelvis:

```csharp
Task<string>
Task<double>
Task<bool>
```

`Task<TResult>` ärver från `Task`, vilket betyder att båda kan hanteras som Tasks.

---

# 11. Skapa en Task och starta den

Vi tittade på att det går att skapa en Task med konstruktorn:

```csharp
Task task = new Task(() => MyTask(1));
```

En sådan Task har ännu inte startats.

Vi kan kontrollera dess status:

```csharp
Console.WriteLine(task.Status);
```

Innan den startats har den normalt statusen:

```text
Created
```

Sedan kan vi starta den:

```csharp
task.Start();
```

Ett sammanhängande exempel:

```csharp
Task task = new Task(() => MyTask(1));

Console.WriteLine(task.Status); // Created

task.Start();

Console.WriteLine(task.Status);

static void MyTask(int n)
{
    for (int i = 0; i < 1000; i++)
    {
        Console.WriteLine($"Task {n}: {i}");
    }
}
```

Efter att vi har anropat `Start()` kan statusen variera beroende på hur långt operationen har kommit.

## Task.Run()

Vanligtvis är det enklare att använda:

```csharp
Task task = Task.Run(() => MyTask(1));
```

`Task.Run()` både skapar och schemalägger arbetsuppgiften för exekvering.

Vi behöver alltså inte anropa `Start()` separat.

För CPU-krävande arbete är detta ett vanligt sätt att låta ThreadPool utföra en operation.

---

# 12. Task.Status

En Task har en egenskap som heter:

```csharp
task.Status
```

Den visar vilket tillstånd Tasken befinner sig i.

Några exempel:

| Status | Betydelse |
|---|---|
| `Created` | Tasken har skapats men inte startats |
| `WaitingToRun` | Väntar på att få börja exekvera |
| `Running` | Operationen exekverar |
| `RanToCompletion` | Operationen har slutförts utan fel |
| `Faulted` | Operationen har misslyckats med ett undantag |
| `Canceled` | Operationen har avbrutits |

Det finns även andra tillstånd.

Vi kan skriva:

```csharp
Console.WriteLine(task.Status);
```

Men statusen kan ändras mycket snabbt.

Om vi skriver ut status direkt efter att vi har startat en Task kan vi därför få olika resultat vid olika körningar.

Det beror på hur operativsystemet och .NET schemalägger arbetet.

---

# 13. Demo 3 – Flera operationer samtidigt

Vi tittade på ett exempel där en Task skrev ut tal samtidigt som programmets huvudflöde fortsatte att göra annat.

En förenklad version av koden vi arbetade med:

```csharp
Task task = Task.Run(() => MyTask(1));

Console.WriteLine($"Task status: {task.Status}");

// Huvudflödet fortsätter medan Tasken arbetar.
for (int i = 0; i < 1000; i++)
{
    Console.WriteLine($"Main Process: {i}");
}

Console.WriteLine($"Task status: {task.Status}");

Console.WriteLine("The end!");

static void MyTask(int n)
{
    for (int i = 0; i < 1000; i++)
    {
        Console.WriteLine($"Task {n}: {i}");
    }
}
```

Här har vi två exekveringsflöden:

1. Huvudflödet skriver ut `Main Process: ...`.
2. En Task kör `MyTask()` och skriver ut `Task 1: ...`.

Utskrifterna kan blandas eftersom arbetet kan utföras samtidigt.

Vi kan inte förutsäga exakt i vilken ordning utskrifterna kommer.

### Vad händer vid "The end!"?

Huvudflödet väntar inte automatiskt på att Tasken ska bli klar.

Det innebär att utskriften:

```csharp
Console.WriteLine("The end!");
```

kan ske innan Tasken har avslutat sitt arbete.

I en konsolapplikation kan programmet också avslutas medan arbete som körs på ThreadPool fortfarande pågår.

Detta är en anledning till att vi behöver kunna vänta på att Tasks blir färdiga.

Hur vi gör det går vi vidare med nästa lektion.

---

# 14. Demo 4 – Task<TResult> och Result

Vi avslutade med ett exempel där en Task utförde en beräkning och returnerade ett värde.

Här är samma kod, med några kommentarer och lite tydligare formatering:

```csharp
Console.WriteLine("Starting calculation Task.");

// Task.Run startar en beräkning på ThreadPool.
// Eftersom vår lambda returnerar en long får vi en Task<long>.
Task<long> task2 = Task.Run(() =>
{
    long sum = 0;

    for (int i = 0; i < 1000_000_000; i++)
    {
        sum += i;
    }

    return sum;
});

// Huvudflödet kan fortsätta medan beräkningen pågår.
Console.WriteLine("Doing something else...");

Console.WriteLine(
    "Waiting for task2 to complete, so we can show the result ..."
);

// Result hämtar resultatet från Task<long>.
// Om beräkningen inte är klar blockeras den anropande tråden.
Console.WriteLine($"Result: {task2.Result}");

Console.WriteLine("The end!");
```

## Vad händer i programmet?

### 1. Vi startar beräkningen

```csharp
Task<long> task2 = Task.Run(() =>
{
    // ...
    return sum;
});
```

`Task.Run()` skickar beräkningen till ThreadPool.

Eftersom vår lambda returnerar en `long` blir resultatet en `Task<long>`.

### 2. Huvudflödet fortsätter

```csharp
Console.WriteLine("Doing something else...");
```

Programmet behöver inte vänta direkt på att beräkningen ska bli färdig.

Huvudflödet kan fortsätta med annat arbete.

### 3. Vi hämtar resultatet

```csharp
Console.WriteLine($"Result: {task2.Result}");
```

`Result` är en egenskap hos `Task<TResult>`.

Den ger oss värdet som operationen returnerade.

Men det finns en viktig detalj:

**Om Tasken inte är färdig kommer `.Result` att blockera den anropande tråden tills operationen har avslutats.**

Om beräkningen redan är färdig kan resultatet hämtas direkt.

### 4. Programmet fortsätter

När vi har fått resultatet fortsätter huvudflödet:

```csharp
Console.WriteLine("The end!");
```

## Varför använder vi Task här?

Vi hade också kunnat göra hela beräkningen direkt i huvudflödet.

Då hade programmet inte kunnat fortsätta till nästa instruktion förrän beräkningen var färdig.

Med `Task.Run()` kan beräkningen utföras av en arbetstråd medan huvudflödet gör något annat.

Men när vi anropar `.Result` behöver vi resultatet, och då väntar vi om beräkningen fortfarande pågår.

Detta knyter tillbaka till vår diskussion om concurrency och parallelism.

---

# 15. Vad vi tar med oss från lektionen

## Begrepp

- **Synchronous:** Den anropande koden väntar på att en operation ska returnera.
- **Asynchronous:** En operation kan pågå utan att den anropande tråden behöver vara blockerad under hela väntetiden.
- **Concurrency:** Flera uppgifter gör framsteg under samma tidsperiod.
- **Parallelism:** Flera uppgifter exekverar samtidigt.
- **Multitasking:** Operativsystemet hanterar flera processer.
- **Multithreading:** En process använder flera trådar.
- **ThreadPool:** .NET återanvänder arbetstrådar för olika uppgifter.
- **TPL:** Bibliotek som förenklar parallell programmering med bland annat Tasks.

## LINQ och PLINQ

- `IEnumerable<T>` behöver inte innebära att alla element finns lagrade i minnet.
- `ToList()` materialiserar en sekvens.
- `Enumerable` innehåller vanliga LINQ-extension methods.
- `AsParallel()` gör det möjligt att använda PLINQ.
- `ParallelEnumerable` innehåller motsvarande metoder för parallella frågor.
- `WithDegreeOfParallelism()` styr graden av parallellism, inte vilka specifika processorer som används.
- PLINQ passar främst för CPU-krävande arbete som kan delas upp i oberoende operationer.

## Task

- `Task` representerar en operation.
- `Task<TResult>` representerar en operation som returnerar ett resultat.
- `Task.Run()` schemalägger arbete för exekvering på ThreadPool.
- `Task.Status` visar Taskens aktuella tillstånd.
- `Task<TResult>.Result` hämtar resultatet och blockerar om operationen inte är färdig.
- En Task är inte samma sak som en Thread.

---

# Nästa lektion

Vi fortsätter med Tasks och tittar närmare på hur vi kan vänta på att operationer ska slutföras.

Vi ska bland annat arbeta vidare med:

- `Task.Wait()`
- `Task<TResult>.Result`
- `Task.WhenAll()`
- `async` och `await`

Därefter går vi vidare till problem som kan uppstå när flera trådar arbetar med samma data, exempelvis **race conditions**, **thread safety** och **deadlocks**.