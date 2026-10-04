# Oktober 5

## Delegates

En **delegate** är en datatyp som kan innehålla en referens till en metod.

Vilka metoder som kan refereras bestäms av delegatens **signatur** – alltså vilka parametrar metoden tar emot och vilken datatyp den returnerar.

Vi kan deklarera en egen delegate-typ med nyckelordet `delegate`:

```csharp
delegate int Calculation(int x, int y);
```

En variabel av typen `Calculation` kan nu referera till metoder som:

- tar emot två `int`
- returnerar en `int`

Exempel:

```csharp
static int Add(int x, int y)
{
    return x + y;
}

static int Multiply(int x, int y)
{
    return x * y;
}

Calculation calculation = Add;

int result = calculation(5, 3);
```

Variabeln `calculation` innehåller här en referens till metoden `Add`.

Vi anropar sedan metoden genom delegaten:

```csharp
calculation(5, 3);
```

Vi kan även låta samma variabel referera till en annan metod med samma signatur:

```csharp
calculation = Multiply;
```

Delegates gör det alltså möjligt att **behandla metoder som data**. Vi kan bland annat lagra en referens till en metod i en variabel eller skicka en metod som argument till en annan metod.

### Delegates som parametrar

En delegate kan exempelvis användas som parameter:

```csharp
static int Calculate(int x, int y, Calculation calculation)
{
    return calculation(x, y);
}
```

Vi kan då bestämma vilken beräkning som ska utföras när vi anropar metoden:

```csharp
Calculate(5, 3, Add);
Calculate(5, 3, Multiply);
```

`Calculate()` behöver inte veta *vad* `Add()` eller `Multiply()` gör. Den vet bara att metoden den får in följer den signatur som definieras av `Calculation`.

Delegates är **reference types**. Alla delegate-typer ärver i slutändan från `System.Delegate`.

[Läs mer om delegates](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/)

---

## Generiska delegate-typer

Vi behöver sällan deklarera en egen delegate-typ för vanliga metodsignaturer.

.NET innehåller färdiga generiska delegate-typer. De vanligaste är:

- `Action`
- `Func`
- `Predicate`

### Action

`Action` används för metoder som **inte returnerar något värde** (`void`).

En `Action` utan typparametrar representerar en metod utan parametrar:

```csharp
Action action = SayHello;
```

`Action<T>` representerar en metod som tar emot ett argument:

```csharp
Action<string> action = PrintMessage;
```

Flera typparametrar kan användas om metoden har flera parametrar:

```csharp
Action<string, int>
```

Detta representerar en metod med signaturen:

```csharp
void SomeMethod(string text, int number)
```

---

### Func

`Func` används för metoder som **returnerar ett värde**.

Den **sista typparametern anger returtypen**:

```csharp
Func<int> getNumber;
```

representerar:

```csharp
int SomeMethod()
```

Med fler typparametrar representerar de första typparametrarna metodens parametrar och den sista returtypen:

```csharp
Func<int, int, int>
```

representerar alltså en metod med signaturen:

```csharp
int SomeMethod(int x, int y)
```

Exempel:

```csharp
Func<int, int, int> calculation = Add;
```

Det är samma sorts metodsignatur som vi tidigare kunde beskriva genom vår egen delegate:

```csharp
delegate int Calculation(int x, int y);
```

`Func` gör därför att vi ofta slipper skapa egna delegate-typer.

---

### Predicate

`Predicate<T>` är en specialiserad delegate som tar emot ett värde av typen `T` och returnerar en `bool`.

```csharp
Predicate<int> predicate;
```

motsvarar en metod med signaturen:

```csharp
bool SomeMethod(int value)
```

Ett predicate representerar alltså ett **villkor eller test**:

```csharp
static bool IsPositive(int number)
{
    return number > 0;
}

Predicate<int> predicate = IsPositive;
```

Samma metodsignatur kan också representeras med:

```csharp
Func<int, bool>
```

`Predicate<T>` finns fortfarande och används av flera .NET-API:er, exempelvis vissa metoder på `List<T>`. I ny kod används ofta `Func<T, bool>`, särskilt tillsammans med LINQ.

---

## Multicast delegates

En delegate kan innehålla referenser till **flera metoder samtidigt**. Detta kallas en **multicast delegate**.

Vi kan lägga till metoder med `+=`:

```csharp
Action notify = SendEmail;

notify += SendSms;
notify += WriteLog;

notify();
```

När `notify()` anropas körs samtliga metoder i delegatens **invocation list**, i den ordning de lades till.

En metod kan tas bort med `-=`:

```csharp
notify -= SendSms;
```

Multicast delegates är särskilt relevanta för **events**, där flera metoder kan prenumerera på samma händelse.

---

## Events

Ett **event** används när ett objekt behöver kunna meddela andra delar av programmet om att **något har hänt**.

Exempel på events skulle kunna vara:

- en spelare dog
- en fil har laddats ner
- en knapp klickades
- en temperatur ändrades
- en beställning blev klar

Objektet som signalerar att något har hänt brukar kallas **publisher**.

Objekt som reagerar på eventet kallas **subscribers**.

### Events bygger på delegates

Ett event bygger på en delegate-typ.

Vi kan exempelvis deklarera ett event som använder `Action`:

```csharp
public event Action SomethingHappened;
```

`Action` bestämmer här vilken signatur metoder som prenumererar på eventet måste ha.

Andra objekt kan prenumerera på eventet med `+=`:

```csharp
publisher.SomethingHappened += HandleSomething;
```

Metoden som ska köras när eventet inträffar brukar kallas en **event handler**:

```csharp
static void HandleSomething()
{
    Console.WriteLine("Something happened!");
}
```

Flera metoder kan prenumerera på samma event:

```csharp
publisher.SomethingHappened += HandlerA;
publisher.SomethingHappened += HandlerB;
```

När publishern signalerar eventet anropas de registrerade event handlers.

En subscriber kan sluta lyssna på eventet med `-=`:

```csharp
publisher.SomethingHappened -= HandlerA;
```

---

### Vad gör `event`?

Nyckelordet `event` kan framför allt förstås som en **begränsning av hur delegaten får användas utanför klassen som deklarerar den**.

Om vi istället exponerar en vanlig delegate:

```csharp
public Action SomethingHappened;
```

kan kod utanför klassen göra allt detta:

```csharp
publisher.SomethingHappened += Handler;  // Lägg till
publisher.SomethingHappened -= Handler;  // Ta bort

publisher.SomethingHappened = Handler;   // Ersätt
publisher.SomethingHappened();           // Anropa
```

Det innebär att andra objekt får väldigt stor kontroll över delegaten.

Om vi istället lägger till `event`:

```csharp
public event Action SomethingHappened;
```

begränsas vad kod utanför klassen får göra.

Den får fortfarande **prenumerera**:

```csharp
publisher.SomethingHappened += Handler;
```

och **avprenumerera**:

```csharp
publisher.SomethingHappened -= Handler;
```

Men den får inte ersätta delegaten:

```csharp
// Inte tillåtet:
publisher.SomethingHappened = Handler;
```

och den får inte själv signalera eventet:

```csharp
// Inte tillåtet:
publisher.SomethingHappened();
```

Klassen som deklarerar eventet behåller alltså kontrollen över **när eventet signaleras**.

Inifrån klassen kan eventet exempelvis signaleras med:

```csharp
SomethingHappened?.Invoke();
```

En bra mental modell är därför:

> **Ett event bygger på en delegate. Nyckelordet `event` begränsar åtkomsten till delegaten så att kod utanför klassen bara kan prenumerera (`+=`) och avprenumerera (`-=`). Klassen som deklarerar eventet behåller kontrollen över när eventet signaleras.**

Det är alltså inte riktigt `event` som är en ny sorts delegate. Delegate-typen kan fortfarande vara exempelvis `Action` eller `EventHandler`. `event` styr istället **hur delegaten exponeras och får användas**.

---

## Skicka information med events

Ofta behöver en subscriber få information om **vad som har hänt**.

Eftersom ett event bygger på en delegate kan eventet ha parametrar:

```csharp
public event Action<int> ScoreChanged;
```

När eventet signaleras kan publishern skicka med det nya värdet:

```csharp
ScoreChanged?.Invoke(score);
```

Subscribers behöver då ha en event handler med motsvarande signatur:

```csharp
static void HandleScoreChanged(int newScore)
{
    Console.WriteLine($"New score: {newScore}");
}
```

Prenumerationen kan då se ut så här:

```csharp
player.ScoreChanged += HandleScoreChanged;
```

Publishern behöver inte veta **vilka** objekt som lyssnar på eventet eller vad de gör när eventet inträffar.

Det skapar en lösare koppling mellan olika delar av programmet.

---

## EventHandler och EventArgs

.NET har också en etablerad standard för events där delegate-typen `EventHandler` används:

```csharp
public event EventHandler SomethingHappened;
```

En sådan event handler tar emot två parametrar:

```csharp
void Handler(object? sender, EventArgs e)
```

`sender` är objektet som signalerade eventet.

`EventArgs` används för information som hör till eventet.

Om vi behöver skicka egen information kan vi skapa en klass som ärver från `EventArgs` och använda den generiska typen:

```csharp
public event EventHandler<ScoreChangedEventArgs> ScoreChanged;
```

Detta är en vanlig .NET-konvention för publika events.

---

## Sammanfattning

En **delegate** är en typ som beskriver vilka metoder vi kan referera till:

```csharp
delegate int Calculation(int x, int y);
```

`Action` och `Func` är generiska delegate-typer som gör att vi ofta slipper deklarera egna:

```csharp
Action<string>
Func<int, int, int>
```

En delegate kan innehålla flera metodreferenser, vilket kallas en **multicast delegate**:

```csharp
action += MethodA;
action += MethodB;
```

Ett **event** bygger på en delegate men begränsar hur den får användas utanför klassen:

```csharp
public event Action SomethingHappened;
```

Subscribers får prenumerera och avprenumerera:

```csharp
publisher.SomethingHappened += Handler;
publisher.SomethingHappened -= Handler;
```

medan publishern behåller kontrollen över när eventet signaleras:

```csharp
SomethingHappened?.Invoke();
```

Det ger oss ett sätt att skapa kommunikation mellan objekt där publishern inte behöver känna till vilka objekt som reagerar på eventet.