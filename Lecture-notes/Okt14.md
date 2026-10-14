# Oktober 14

Under dagens lektion fortsatte vi med [`Task`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt14.md#1-fr%C3%A5n-taskresult-till-await) och gick vidare till [`async` och `await`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt14.md#2-vad-betyder-async-och-await). Vi undersökte hur asynkrona metoder körs, hur de förhåller sig till trådar, hur undantag hanteras och vad som kan gå fel när flera trådar arbetar med samma data.

**Code-along:**  
[L039_AsyncAwait](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L039_AsyncAwait/Program.cs)


Vi avslutade med [race conditions](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt14.md#8-race-conditions--n%C3%A4r-flera-tr%C3%A5dar-%C3%A4ndrar-samma-data), atomära operationer, `lock`, `Interlocked` och en introduktion till deadlocks.

**Code-along:**  
[L040_Race_condition](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L040_Race_condition/Program.cs)

---

## 1. Från Task.Result till await


Under föregående lektion använde vi `Task.Run()` för att starta arbete på en tråd från ThreadPool.

Vi hämtade också resultat från en `Task<T>` med `.Result`.

Problemet med `.Result` är att den **blockerar den anropande tråden** om uppgiften inte är färdig.

Med `await` kan vi i stället invänta resultatet utan att blockera tråden medan vi väntar.

### Exempel

```csharp
Console.WriteLine("Start of program!");

Task<int> calculationTask = Task.Run(() =>
{
    Thread.Sleep(2000);
    Console.WriteLine("Task complete!");
    return 42;
});

Console.WriteLine("Continue running.");

Console.WriteLine($"Result: {await calculationTask}");

Console.WriteLine("The end.");
```

Här händer följande:

1. `Task.Run()` schemalägger arbetet på ThreadPool.
2. Programmet fortsätter direkt till `Continue running`.
3. När vi når `await calculationTask` kontrolleras om Tasken är färdig.
4. Om den inte är färdig pausas den asynkrona metoden.
5. När Tasken är färdig fortsätter metoden och vi kan använda resultatet.

**Viktigt:** `await` startar inte en ny tråd. I detta exempel är det `Task.Run()` som gör att beräkningen körs på ThreadPool.

### Task och Task<T>

- `Task` representerar en operation som kan bli färdig senare.
- `Task<T>` representerar en operation som dessutom ger ett resultat av typen `T`.
- `await task` inväntar operationen.
- `await taskOfT` inväntar operationen och ger tillbaka dess resultat.

Exempel:

```csharp
Task<int> task = Task.Run(() => 42);

int result = await task;
```

---

## 2. Vad betyder async och await?

En metod som använder `await` markeras normalt med `async`.

```csharp
static async Task Delay()
{
    await Task.Delay(3000);
}
```

- `async` gör det möjligt att använda `await` i metoden.
- `await` inväntar en operation utan att blockera den anropande tråden när operationen ännu inte är färdig.
- `Task.Delay(3000)` skapar en Task som blir färdig efter ungefär tre sekunder.

### En async-metod börjar köras direkt

Det är viktigt att förstå att en asynkron metod **inte automatiskt körs på en annan tråd**.

När vi anropar den börjar den köras direkt på den aktuella tråden.

```csharp
Task delayTask = Delay();
```

Koden inuti `Delay()` körs omedelbart, fram till dess att den når ett `await` på en Task som ännu inte är färdig.

Först då pausas metoden och kontrollen lämnas tillbaka till anroparen.

### Vår code-along

```csharp
private static async Task Main(string[] args)
{
    PrintThreadInfo("method start");

    Task delayTask = Delay();

    PrintThreadInfo("continue running");

    await delayTask;

    PrintThreadInfo("method end");
}

static async Task Delay()
{
    PrintThreadInfo("method start");

    Task task = Task.Delay(3000);

    PrintThreadInfo("continue running");

    await task;

    PrintThreadInfo("method end");
}
```

En typisk ordning på utskrifterna är:

```text
Main - method start
Delay - method start
Delay - continue running
Main - continue running
Delay - method end
Main - method end
```

Notera särskilt att `Delay()` hinner skriva ut sina två första meddelanden **innan `Main()` fortsätter efter anropet**.

Det beror på att `Delay()` körs synkront fram till sitt första `await` som faktiskt behöver vänta.

### Vad händer vid await?

När programmet når:

```csharp
await task;
```

finns två huvudsakliga möjligheter:

**Tasken är redan färdig:**

Metoden fortsätter direkt, utan att behöva pausas.

**Tasken är inte färdig:**

Metoden pausas och kan fortsätta senare när Tasken blir färdig.

Den fortsatta körningen kan ske på samma tråd eller en annan tråd, beroende på programmets miljö och schemaläggning.

---

## 3. Thread.Sleep jämfört med Task.Delay

Vi jämförde tre olika sätt att vänta.

### Thread.Sleep

```csharp
Thread.Sleep(3000);
```

Detta blockerar den aktuella tråden i ungefär tre sekunder.

Tråden gör inget användbart arbete under tiden, men är fortfarande upptagen med att vänta.

### Task.Delay med Wait()

```csharp
Task.Delay(3000).Wait();
```

Här skapas en Task som blir färdig efter ungefär tre sekunder.

Men `.Wait()` blockerar ändå den anropande tråden tills Tasken är färdig.

### Task.Delay med await

```csharp
await Task.Delay(3000);
```

Nu behöver ingen tråd vara blockerad bara för att vänta på att tiden ska gå.

Den asynkrona metoden kan pausas och fortsätta när fördröjningen är klar.

**Skillnaden är inte hur länge vi väntar, utan vad som händer med tråden under väntetiden.**

---

## 4. Vilken tråd kör koden?

För att undersöka detta använde vi en hjälpfunktion:

```csharp
private static Stopwatch stopwatch = new Stopwatch();

private static void PrintThreadInfo(
    string message,
    [CallerMemberName] string caller = null)
{
    int threadId = Thread.CurrentThread.ManagedThreadId;
    bool isThreadPool = Thread.CurrentThread.IsThreadPoolThread;

    string elapsedTime = stopwatch.Elapsed.ToString("mm\\:ss\\.ffff");

    Console.WriteLine(
        $"[{elapsedTime}] {caller} - {message}: " +
        $"Thread ID {threadId}" +
        $"{(isThreadPool ? " (threadpool)" : "")}.");
}
```

Vi använde:

- `Stopwatch` för att se hur mycket tid som gått.
- `ManagedThreadId` för att identifiera vilken tråd som kör en viss kodrad.
- `IsThreadPoolThread` för att se om tråden kommer från ThreadPool.
- `[CallerMemberName]` för att automatiskt visa namnet på metoden som anropade hjälpfunktionen.

### Vad såg vi?

I en vanlig konsolapplikation körs början av `Main()` normalt på programmets ursprungliga huvudtråd.

Efter ett `await` som faktiskt pausar metoden kan den fortsatta koden köras på en tråd från ThreadPool.

Det innebär att vi kan se olika tråd-ID före och efter `await`.

Men:

**Ett byte av tråd är inte garanterat. `await` betyder inte "byt tråd".**

### Experiment med Thread.Sleep

Vi testade också att lägga in:

```csharp
Thread.Sleep(8000);
```

i `Main()` innan vi awaitade vår `Delay()`.

Detta blockerade `Main()`s tråd, medan den asynkrona fördröjningen kunde bli färdig under tiden.

När `Main()` senare nådde:

```csharp
await delayTask;
```

kunde Tasken redan vara färdig.

Då behövde `Main()` inte pausas vid detta `await`.

Detta visar att **`await` bara pausar metoden om operationen ännu inte är färdig**.

---

## 5. Asynkron I/O – hämta data från webben

Vi använde sedan `HttpClient` för att demonstrera en verklig asynkron I/O-operation.

```csharp
static async Task<string> WebDownload()
{
    HttpClient client = new HttpClient();

    PrintThreadInfo("starting download");

    string data = await client.GetStringAsync("https://www.google.com");

    PrintThreadInfo("download complete!");

    return data;
}
```

Och anropade den från `Main()`:

```csharp
Task<string> downloadTask = WebDownload();

PrintThreadInfo("Waiting for download to complete!");

string data = await downloadTask;

Console.WriteLine($"Downloaded {data.Length * 2} bytes of data.");
```

### Vad händer?

1. `WebDownload()` börjar köras direkt.
2. `GetStringAsync()` påbörjar en HTTP-förfrågan.
3. Om svaret inte är färdigt pausas `WebDownload()` vid `await`.
4. `Main()` kan fortsätta köras.
5. När HTTP-förfrågan är färdig kan `WebDownload()` fortsätta och returnera texten.
6. `Main()` får texten genom `await downloadTask`.

### CPU-bound och I/O-bound

Det är viktigt att skilja mellan två sorters arbete.

**CPU-bound:** En beräkning som aktivt använder processorn.

Exempel:

```csharp
Task<int> task = Task.Run(() => ExpensiveCalculation());
```

`Task.Run()` kan vara lämpligt när vi vill köra CPU-krävande arbete på ThreadPool.

**I/O-bound:** En operation som till stor del väntar på något utanför processorn, exempelvis nätverk eller filsystem.

Exempel:

```csharp
string html = await client.GetStringAsync(url);
```

Här behöver vi normalt inte använda `Task.Run()`.

Den asynkrona I/O-operationen kan inväntas utan att en tråd måste stå blockerad under nätverksväntan.

### En detalj om vår utskrift

I demon skrev vi:

```csharp
Console.WriteLine($"Downloaded {data.Length * 2} bytes of data.");
```

Det är en förenklad uppskattning baserad på två byte per `char` i .NET-strängens UTF-16-representation.

Det är **inte nödvändigtvis antalet byte som faktiskt överfördes över nätverket**, eftersom HTTP-svaret kan ha en annan teckenkodning.

---

## 6. Exceptions i asynkrona Tasks

Nästa demonstration handlade om hur undantag fungerar när kod körs inuti en Task.

Vi använde:

```csharp
Task downloadTask = Task.Run(() =>
{
    PrintThreadInfo("Download started!");
    throw new Exception("Download failed!");
});
```

När undantaget kastas inne i `Task.Run()` fångas det av Task-mekanismen.

Tasken får status:

```text
Faulted
```

Undantaget lagras i Tasken.

### Varför fångar inte try/catch runt Task.Run felet?

Vi började med ungefär följande struktur:

```csharp
Task downloadTask;

try
{
    downloadTask = Task.Run(() =>
    {
        throw new Exception("Download failed!");
    });
}
catch (Exception ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
    return;
}

await downloadTask;
```

Här ligger `await` **utanför** `try/catch`.

Undantaget kastas inne i Tasken, men `Task.Run()` returnerar normalt en Task till anroparen.

När vi senare når:

```csharp
await downloadTask;
```

kastas det lagrade undantaget vidare.

Eftersom `await` ligger utanför `try/catch` fångas det inte där.

Om undantaget fortsätter hela vägen ut ur programmets `Main()` kan programmet avslutas med ett ohanterat undantag.

### Flytta await in i try/catch

```csharp
try
{
    Task downloadTask = Task.Run(() =>
    {
        throw new Exception("Download failed!");
    });

    await downloadTask;
}
catch (Exception ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}
```

Nu fångas undantaget!

**Det är vid `await` som undantaget från den misslyckade Tasken kastas vidare till vår kod.**

### Task.Run behöver inte ligga inuti try/catch

Vi flyttade därefter starten av Tasken utanför `try/catch`.

```csharp
Task downloadTask = Task.Run(() =>
{
    throw new Exception("Download failed!");
});

try
{
    await downloadTask;
}
catch (Exception ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}
```

Undantaget fångas fortfarande.

Det visar att `try/catch` behöver omge den kod där undantaget kastas vidare, alltså här vårt `await`.

---

## 7. En Task kan vara Faulted utan att programmet kraschar

Vi gjorde sedan ett experiment där vi **inte awaitade den misslyckade Tasken**.

```csharp
Task downloadTask = Task.Run(() =>
{
    throw new Exception("Download failed!");
});

await Task.Delay(1000);

PrintThreadInfo($"Download status: {downloadTask.Status}");
```

Vi kunde se:

```text
Download status: Faulted
```

Trots detta kunde programmet fortsätta normalt.

Det beror på att undantaget hade lagrats i Tasken, men ännu inte kastats vidare till vår metod genom exempelvis `await`.

### Vi väntade ytterligare tre sekunder

Till sist använde vi:

```csharp
await Task.Delay(1000);
PrintThreadInfo($"Download status: {downloadTask.Status}");

await Task.Delay(3000);
PrintThreadInfo("Awaiting downloadTask");

await downloadTask;
```

Allt detta placerades inuti ett `try`-block med en tillhörande `catch`.

Först såg vi att Tasken redan hade status `Faulted`.

Sedan fortsatte programmet i ytterligare tre sekunder.

Först när vi nådde:

```csharp
await downloadTask;
```

kastades det lagrade undantaget vidare och fångades av `catch`.

Eftersom Tasken redan var färdig behövde detta `await` inte pausa metoden.

Det kastade undantaget direkt.

### Viktig slutsats

**Det är inte `await` som får Tasken att misslyckas.**

Tasken misslyckades tidigare. `await` gör att vår metod får det lagrade undantaget vidarebefordrat.

Det här förklarar också varför vi bör undvika att starta Tasks och sedan glömma bort dem.

En Task som misslyckas utan att någon hanterar felet kan lämna ett fel oupptäckt.

Tumregel:

> När du startar en Task bör du normalt awaita den, returnera den till anroparen eller på annat sätt se till att eventuella fel hanteras.

**Tips från demon:** Kör gärna med `Ctrl+F5` i Visual Studio. Debuggern kan annars pausa när ett undantag kastas, även om det senare kommer att fångas av vår kod.

---

## 8. Race conditions – när flera trådar ändrar samma data

Efter `async/await` gick vi vidare till ett annat problem inom parallell programmering: **race conditions**.

En race condition uppstår när programmets resultat beror på i vilken ordning olika trådar råkar utföra sina operationer.

Vi skapade fyra Tasks som alla ökade samma räknare.

```csharp
static async Task Count()
{
    int counter = 0;

    Task taskA = Task.Run(DoCount);
    Task taskB = Task.Run(DoCount);
    Task taskC = Task.Run(DoCount);
    Task taskD = Task.Run(DoCount);

    await taskA;
    await taskB;
    await taskC;
    await taskD;

    Console.WriteLine(counter);

    void DoCount()
    {
        for (int i = 0; i < 1000; i++)
        {
            counter++;
        }
    }
}
```

Varje Task ökar räknaren 1000 gånger.

Vi har fyra Tasks, så vi förväntar oss:

```text
4000
```

Men resultatet kan bli lägre.

### Varför?

Därför att:

```csharp
counter++;
```

inte är en atomär operation.

Den kan förenklat beskrivas som tre steg:

1. Läs värdet från `counter`.
2. Öka värdet med ett.
3. Skriv tillbaka värdet.

Två trådar kan hinna läsa samma värde innan någon av dem skriver tillbaka det nya värdet.

Exempel:

```text
counter = 10

Tråd A läser 10
Tråd B läser 10

Tråd A räknar ut 11
Tråd B räknar ut 11

Tråd A skriver 11
Tråd B skriver 11

Resultat: 11, trots att två ökningar gjordes.
```

Det kallas ibland en **lost update**.

### Vi körde testet flera gånger

```csharp
for (int i = 0; i < 20; i++)
{
    await Count();
}
```

Detta gjorde det lättare att se att resultatet kunde variera mellan körningar.

En race condition kan vara svår att upptäcka eftersom programmet ibland ger rätt resultat och ibland fel.

---

## 9. Atomära operationer

En **atomär operation** är en operation som sker som en odelbar enhet ur andra trådars perspektiv.

Andra trådar kan inte se operationen som halvfärdig.

Det betyder inte att operativsystemet aldrig kan avbryta tråden, utan att själva operationen inte kan observeras som delvis genomförd.

Vanliga uttryck i C#, exempelvis:

```csharp
counter++;
```

är inte automatiskt atomära bara för att de skrivs på en enda rad.

---

## 10. Lösning 1 – lock

Vi använde `lock` för att se till att bara en tråd åt gången fick ändra räknaren.

```csharp
object lockA = new object();

int counter = 0;

void DoCount()
{
    for (int i = 0; i < 1000; i++)
    {
        lock (lockA)
        {
            counter++;
        }
    }
}
```

### Hur fungerar lock?

`lock` använder ett gemensamt objekt för att samordna trådarna.

När en tråd når:

```csharp
lock (lockA)
{
    counter++;
}
```

försöker den ta låset som hör till `lockA`.

- Om låset är ledigt får tråden gå in och köra blocket.
- Om en annan tråd redan håller låset måste den vänta.
- När den första tråden lämnar blocket släpps låset.
- Därefter kan en annan tråd få tillgång.

**Det är inte själva variabeln `counter` som låses.**

`lock` ser till att trådar som använder **samma låsobjekt** inte kör sina skyddade kodblock samtidigt.

### Varför använder vi ett object?

I vårt exempel skrev vi:

```csharp
object lockA = new object();
```

Objektet fungerar som en gemensam låsidentitet.

Det viktiga är att alla trådar använder samma objekt.

I modern C# finns också typen `System.Threading.Lock`, särskilt avsedd för detta.

### Best practices för lock

- Håll de låsta kodblocken så korta som möjligt.
- Använd ett gemensamt, dedikerat låsobjekt för data som ska skyddas.
- Se till att all relevant åtkomst till samma delade data följer samma låsregler.
- Undvik långsamma operationer inuti låset.
- Använd inte `await` inuti ett vanligt `lock`-block.
- Om flera lås behövs, ta dem alltid i en konsekvent ordning.

Ett `lock`-block släpper automatiskt sitt lås när blocket lämnas, även om ett undantag kastas.

---

## 11. Lösning 2 – Interlocked

För enkla operationer som att öka en räknare behöver vi inte alltid använda `lock`.

Vi kan i stället skriva:

```csharp
Interlocked.Increment(ref counter);
```

`Interlocked.Increment()` gör ökningen atomär.

Det innebär att flera trådar kan öka samma räknare utan att ökningar går förlorade.

### Jämförelse

```csharp
// Inte trådsäker vid samtidiga ökningar:
counter++;

// Trådsäker med lock:
lock (lockA)
{
    counter++;
}

// Atomär ökning:
Interlocked.Increment(ref counter);
```

`Interlocked` passar bra för enkla operationer som att öka, minska eller byta ett värde atomärt.

`lock` passar bättre när vi behöver skydda ett större kodavsnitt eller flera operationer som måste höra ihop.


## 12. Deadlocks – när trådar väntar på varandra

Vi avslutade med att lägga till två låsobjekt:

```csharp
object lockA = new object();
object lockB = new object();
```

Och nästlade lås:

```csharp
lock (lockA)
{
    lock (lockB)
    {
        counter++;
    }
}
```

Detta fungerar så länge trådarna kan få låsen i den ordning de behöver.

Men tänk om två trådar gör följande:

**Tråd 1:**

```csharp
lock (lockA)
{
    lock (lockB)
    {
        // ...
    }
}
```

**Tråd 2:**

```csharp
lock (lockB)
{
    lock (lockA)
    {
        // ...
    }
}
```

Då kan följande hända:

1. Tråd 1 tar lås A.
2. Tråd 2 tar lås B.
3. Tråd 1 försöker ta B, men måste vänta på tråd 2.
4. Tråd 2 försöker ta A, men måste vänta på tråd 1.

Båda trådarna väntar nu på varandra.

Ingen av dem kan fortsätta.

Detta kallas **deadlock**.

### Hur undviker vi deadlocks?

En viktig tumregel är:

> Om flera lås måste tas, se till att alla trådar tar dem i samma ordning.

Exempelvis alltid A före B.

**Viktigt:** Våra två nästlade lås i den slutliga code-alongen orsakade inte i sig någon deadlock, eftersom alla Tasks tog låsen i samma ordning. Vi använde dem för att förklara hur en deadlock *skulle kunna* uppstå om en annan tråd tog dem i motsatt ordning.

---

## Sammanfattning

Dagens viktigaste lärdomar:

1. **`async` och `await` gör det möjligt att invänta operationer utan att blockera tråden under väntetiden.**
2. En `async`-metod börjar köras direkt och fortsätter synkront fram till första `await` som faktiskt behöver vänta.
3. `await` betyder inte att en ny tråd skapas.
4. `Thread.Sleep()` och `.Wait()` blockerar den anropande tråden, medan `await Task.Delay()` inte behöver göra det.
5. Asynkron I/O, exempelvis `HttpClient.GetStringAsync()`, behöver normalt inte `Task.Run()`.
6. Undantag i en Task lagras i Tasken. När vi `await`ar en misslyckad Task kastas undantaget vidare.
7. En Task kan ha status `Faulted` utan att programmet kraschar, om felet aldrig observeras.
8. Race conditions kan uppstå när flera trådar ändrar delad data samtidigt.
9. `counter++` är inte atomär.
10. `lock` och `Interlocked` är två sätt att skydda delad data.
11. Deadlocks kan uppstå när trådar håller olika lås och väntar på att få varandras lås.

### Att komma ihåg

**Asynkron programmering handlar om att kunna vänta effektivt.**

**Parallell programmering handlar om att kunna utföra arbete samtidigt.**

Och när flera trådar arbetar med samma föränderliga data måste vi dessutom tänka på **trådsäkerhet**.