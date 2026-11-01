# Oktober 30

Under den här lektionen fortsatte vi med vår Quiz Configurator och introducerade **Commands**, som är en viktig del av MVVM.

Vi skapade en generell `RelayCommand`-klass och använde den för att koppla knappar och menyalternativ till funktionalitet i en ViewModel. Vi tittade också på hur ett kommando kan avgöra om det får köras eller inte.

Därefter skapade vi ett separat WPF-projekt där vi undersökte hur UI-tråden fungerar, vad som händer när den blockeras, hur vi kan använda [`Dispatcher`](L047_Dispatcher_%26_DispatcherTimer) för att köra kod på UI-tråden och hur [`DispatcherTimer`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt30.md#del-3--dispatchertimer-och-tidm%C3%A4tning) kan användas för återkommande uppdateringar.


## 1. Commands och MVVM

**Code-along:**  
[L046_Labb3_Code-along](https://github.com/everyloop/NEU26G-Csharp/tree/master/Code-alongs/L046_Labb3_Code-along)

Tidigare har vi använt vanliga Click-events för att hantera knapptryckningar:

```xml
<Button Content="Click here!" Click="Button_Click"/>
```

Det innebär att vi skriver en eventhanterare i fönstrets eller kontrollens code-behind.

I MVVM vill vi däremot att **applikationens logik i huvudsak ska finnas i våra ViewModels**, medan Views ansvarar för hur gränssnittet presenteras.

Precis som vi kan använda databinding för att koppla en `TextBox` till en property i vår ViewModel kan vi använda **Commands** för att koppla exempelvis en knapp till en handling i vår ViewModel.

```xml
<Button Content="Change name"
        Command="{Binding ChangePackNameCommand}"/>
```

Här behöver vi ingen `Click`-eventhanterare i code-behind.

I stället letar WPF efter en property som heter `ChangePackNameCommand` i objektets `DataContext`.

### ICommand

WPF använder interfacet `ICommand` för kommandon.

Det innehåller tre viktiga medlemmar:

- `Execute(object? parameter)` – utför kommandot.
- `CanExecute(object? parameter)` – avgör om kommandot får köras.
- `CanExecuteChanged` – ett event som meddelar WPF att resultatet från `CanExecute` kan ha ändrats.

När ett kommando är kopplat till en knapp använder WPF `CanExecute` för att avgöra om knappen ska vara aktiverad.

Om `CanExecute` returnerar `false` blir knappen automatiskt inaktiverad.

## 2. Vår RelayCommand-klass

För att slippa skapa en ny klass som implementerar `ICommand` för varje handling i applikationen skrev vi en generell klass som vi kallade `RelayCommand`.

```csharp
class RelayCommand : ICommand
{
    private readonly Action<object?> execute;
    private readonly Func<object?, bool>? canExecute;

    public event EventHandler? CanExecuteChanged;

    public RelayCommand(
        Action<object?> execute,
        Func<object?, bool>? canExecute = null)
    {
        ArgumentNullException.ThrowIfNull(execute);

        this.execute = execute;
        this.canExecute = canExecute;
    }

    public void RaiseCanExecuteChanged()
        => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    public bool CanExecute(object? parameter)
        => canExecute is null ? true : canExecute(parameter);

    public void Execute(object? parameter)
        => execute(parameter);
}
```

Klassen implementerar `ICommand`, men innehåller ingen specifik logik för vår quizapplikation.

I stället skickar vi in den funktionalitet som kommandot ska använda när vi skapar ett `RelayCommand`-objekt.

### Action och Func

Vi använder två delegattyper:

**`Action<object?>`**

Representerar en metod som tar emot en parameter av typen `object?` och inte returnerar något värde.

Den används för själva handlingen som ska utföras.

**`Func<object?, bool>`**

Representerar en metod som tar emot en parameter av typen `object?` och returnerar en `bool`.

Den används för att avgöra om kommandot får köras.

`canExecute` är nullable och har standardvärdet `null`. Det innebär att vi inte måste ange något villkor när vi skapar ett kommando.

```csharp
public bool CanExecute(object? parameter)
    => canExecute is null ? true : canExecute(parameter);
```

Om inget villkor har angetts returneras alltid `true`.

### Varför tar metoderna emot object?

`ICommand` definierar att både `Execute` och `CanExecute` ska kunna ta emot en parameter av typen `object`.

Det gör att vi kan skicka information från vårt gränssnitt till kommandot via `CommandParameter`.

Exempel:

```xml
<Button Content="Click"
        Command="{Binding SomeCommand}"
        CommandParameter="My sample parameter"/>
```

Parametern skickas till kommandots `Execute` och `CanExecute`.

Om vi inte behöver parametern kan vi ignorera den med `_` i en lambda:

```csharp
new RelayCommand(_ => ChangePackName());
```

Här betyder `_` att vi tar emot parametern men inte använder den.

## 3. Använda RelayCommand i ConfigurationViewModel

Vi lade till följande property i `ConfigurationViewModel`:

```csharp
public RelayCommand ChangePackNameCommand { get; }
```

Den representerar ett kommando som ska ändra namnet på det aktiva frågepaketet.

Vi skapade kommandot i konstruktorn:

```csharp
ChangePackNameCommand = new RelayCommand(
    _ => ChangePackName(),
    _ => DemoText != string.Empty);
```

Här skickar vi in två lambdauttryck:

1. Det första anropar `ChangePackName()`.
2. Det andra returnerar `true` om `DemoText` inte är en tom sträng.

Metoden som utför handlingen är:

```csharp
private void ChangePackName()
{
    ActivePack?.Name = DemoText;
}
```

Vi använder `?.` eftersom `ActivePack` kan vara `null`.

### CanExecute och knappens tillstånd

Villkoret:

```csharp
_ => DemoText != string.Empty
```

innebär att kommandot bara får köras om `DemoText` innehåller någon text.

När kommandot binds till en knapp kommer WPF automatiskt att inaktivera knappen om `CanExecute` returnerar `false`.

Vi hade även en separat metod i vår ViewModel:

```csharp
private bool CanChangePackName(object? arg)
{
    return DemoText != string.Empty;
}
```

Den gör samma sak som vårt lambdauttryck och skulle kunna användas i stället:

```csharp
ChangePackNameCommand = new RelayCommand(
    _ => ChangePackName(),
    CanChangePackName);
```

Det är alltså möjligt att använda både lambdauttryck och vanliga metoder.

## 4. CanExecuteChanged – när behöver WPF kontrollera igen?

Det räcker inte att skriva ett villkor i `CanExecute`.

Vi behöver också kunna meddela WPF när något har ändrats som kan påverka resultatet.

I vår ViewModel hade vi:

```csharp
private string _demoText = string.Empty;

public string DemoText
{
    get => _demoText;
    set
    {
        if (SetProperty(ref _demoText, value))
        {
            ChangePackNameCommand.RaiseCanExecuteChanged();
        }
    }
}
```

`SetProperty` kommer från vår `ViewModelBase` och ansvarar för att uppdatera värdet och skicka `PropertyChanged` när det faktiskt ändras.

Om värdet ändras anropar vi dessutom:

```csharp
ChangePackNameCommand.RaiseCanExecuteChanged();
```

I `RelayCommand` ser metoden ut så här:

```csharp
public void RaiseCanExecuteChanged()
    => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
```

Detta meddelar WPF att `CanExecute` behöver kontrolleras igen.

**Skillnaden mellan våra två notifieringar:**

- `PropertyChanged` meddelar att en property har ändrats, så att databindings kan uppdateras.
- `CanExecuteChanged` meddelar att ett kommandos möjlighet att köras kan ha ändrats.

Vi behöver alltså båda i det här exemplet.

Observera att `CanExecuteChanged` inte skickar ett nytt `true`- eller `false`-värde. Det meddelar bara att kommandots villkor bör utvärderas igen.

## 5. Binding till Commands i XAML

I `ConfigurationView` skapade vi en `TextBox` och en `Button`.

```xml
<StackPanel>
    <TextBox
        Text="{Binding DemoText,
               UpdateSourceTrigger=PropertyChanged}"/>

    <Button
        Content="{Binding ActivePack.Name}"
        Command="{Binding ChangePackNameCommand}"
        CommandParameter="My sample parameter"/>
</StackPanel>
```

`TextBox` uppdaterar `DemoText` varje gång användaren ändrar texten, eftersom vi använder:

```xml
UpdateSourceTrigger=PropertyChanged
```

När `DemoText` ändras anropas `RaiseCanExecuteChanged`, vilket gör att knappens aktiverade/inaktiverade tillstånd kan uppdateras direkt.

Knappens `Content` är samtidigt bundet till det aktiva frågepaketets namn.

När kommandot körs ändras `ActivePack.Name`, och databindingen kan uppdatera knappens text.

### Samma Command på flera ställen

Vi kopplade även samma kommando till ett menyalternativ:

```xml
<MenuItem Header="Set Pack Name"
          Command="{Binding ConfigurationViewModel.ChangePackNameCommand}"/>
```

Observera skillnaden i våra binding paths.

I `ConfigurationView` hade vi:

```xml
DataContext="{Binding ConfigurationViewModel}"
```

Därför kunde vi binda direkt till:

```xml
Command="{Binding ChangePackNameCommand}"
```

Men `MenuView` ärver i vårt exempel `MainWindowViewModel` som `DataContext`.

Därför behöver vi gå via dess property `ConfigurationViewModel`:

```xml
Command="{Binding ConfigurationViewModel.ChangePackNameCommand}"
```

Det är **samma RelayCommand-objekt** som används av både knappen och menyalternativet.

När kommandot blir inaktiverat påverkas båda.

Detta är en av fördelarna med Commands: vi kan återanvända samma funktionalitet från flera olika delar av gränssnittet.

## 6. Kort om menyer och tangentbord

I vår `MenuView` använde vi bland annat:

```xml
<MenuItem Header="_File">
    <MenuItem Header="_New Question pack"/>
    <MenuItem Header="E_xit" InputGestureText="Alt+F4"/>
</MenuItem>
```

Understrecket i exempelvis `_File` anger en åtkomsttangent (access key).

Det gör att användaren kan navigera i menyer med `Alt` och bokstäver.

`InputGestureText` visar däremot bara texten för ett kortkommando i menyn. Det skapar inte i sig någon tangentbordsbindning.

För egna kortkommandon, exempelvis `Ctrl+N`, kan vi använda `KeyBinding` i fönstrets `InputBindings`.

Detta kommer vi att återkomma till när vi arbetar vidare med quizapplikationens menyer.

---

# Del 2 – Dispatcher och UI-tråden

Vi skapade sedan ett separat WPF-projekt för att undersöka hur trådar och asynkron kod fungerar tillsammans med ett grafiskt gränssnitt.

**Code-along:**  
[L047_Dispatcher_&_DispatcherTimer](https://github.com/everyloop/NEU26G-Csharp/tree/master/Code-alongs/L047_Dispatcher_%26_DispatcherTimer)

## 7. Vad händer när vi blockerar UI-tråden?

Vi började med en enkel knapp:

```xml
<Button
    x:Name="button"
    Content="Click here!"
    Click="Button_Click"/>
```

Vi ville att knappens text skulle ändras fem sekunder efter att användaren klickat.

Ett första alternativ var:

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    Thread.Sleep(5000);
    button.Content = "New value";
}
```

Det fungerar i betydelsen att texten ändras efter fem sekunder.

Men vi upptäckte ett problem: **hela gränssnittet blir låst under väntetiden.**

Vi kunde exempelvis inte interagera normalt med fönstret medan `Thread.Sleep` kördes.

### Varför?

I WPF ägs UI-elementen av en särskild tråd, vanligtvis kallad **UI-tråden**.

Den ansvarar bland annat för att:

- Hantera användarens interaktioner.
- Köra eventhanterare.
- Uppdatera UI-element.
- Bearbeta arbete som schemaläggs via Dispatcher.

När vi kör `Thread.Sleep(5000)` direkt i knappens eventhanterare blockerar vi UI-tråden i fem sekunder.

Under tiden kan den inte fortsätta bearbeta sina vanliga arbetsuppgifter.

## 8. Task.Delay och async/await

Vi jämförde sedan med:

```csharp
private async void Button_Click(object sender, RoutedEventArgs e)
{
    await Task.Delay(5000);
    button.Content = "New value";
}
```

Nu fungerar gränssnittet som vanligt medan vi väntar.

Skillnaden är att `Task.Delay` inte blockerar någon tråd under själva väntetiden.

När fördröjningen är klar fortsätter metoden efter `await`.

### Varför får vi ändra knappen efter await?

I en vanlig WPF-eventhanterare fångar `await` normalt den aktuella `SynchronizationContext`.

Det gör att fortsättningen efter `await Task.Delay(...)` schemaläggs tillbaka till UI-tråden.

Därför kan vi ändra:

```csharp
button.Content = "New value";
```

utan problem.

**Viktigt:** `async/await` innebär inte automatiskt att vi skapar eller använder en ny tråd.

## 9. Task.Run – arbete på en bakgrundstråd

För att demonstrera vad som händer om vi faktiskt försöker uppdatera UI från en annan tråd använde vi `Task.Run`.

```csharp
private async void Button_Click(object sender, RoutedEventArgs e)
{
    await Task.Run(async () =>
    {
        Thread.Sleep(5000);

        button.Content = "New value";
    });
}
```

`Task.Run` schemalägger arbetet på en trådpoolstråd.

Det innebär att koden inuti delegaten inte körs på WPF:s UI-tråd.

Om vi försöker ändra knappens `Content` direkt därifrån får vi normalt ett undantag.

Det beror på att WPF-kontroller har **thread affinity**: de tillhör den tråd som skapade dem och får normalt bara användas från den tråden.

**OBS!** Vi använde `Task.Run` här för att demonstrera ett trådproblem, inte för att det behövs för en femsekundersfördröjning.

För att enbart vänta är `await Task.Delay(5000)` den bättre lösningen.

## 10. Dispatcher – schemalägg kod på UI-tråden

WPF har en `Dispatcher` som används för att schemalägga arbete på en viss tråd.

UI-element har en koppling till sin Dispatcher.

Om vi befinner oss på en bakgrundstråd kan vi använda Dispatcher för att låta UI-tråden utföra en uppdatering.

I vårt exempel använde vi:

```csharp
await Task.Run(async () =>
{
    Thread.Sleep(5000);

    Dispatcher.Invoke(() =>
    {
        button.Content = "New value";
    });
});
```

`Dispatcher.Invoke` kör delegaten på Dispatcher-tråden.

Om vi anropar den från en bakgrundstråd väntar den anropande tråden tills delegaten har körts.

Det finns också asynkrona alternativ, exempelvis:

```csharp
await Dispatcher.InvokeAsync(() =>
{
    button.Content = "New value";
});
```

Med `InvokeAsync` schemaläggs arbetet utan att vi behöver blockera bakgrundstråden medan vi väntar på att UI-tråden ska utföra det.

### När behöver vi Dispatcher?

Vi behöver normalt inte använda Dispatcher efter en vanlig `await Task.Delay(...)` i en WPF-eventhanterare.

Men om vi arbetar på en bakgrundstråd och behöver komma åt UI-element måste vi se till att uppdateringen sker på rätt tråd.

Det är det problemet Dispatcher hjälper oss att lösa.

---

# Del 3 – DispatcherTimer och tidmätning

**Code-along:**  
[L047_Dispatcher_&_DispatcherTimer](https://github.com/everyloop/NEU26G-Csharp/tree/master/Code-alongs/L047_Dispatcher_%26_DispatcherTimer)

## 11. DispatcherTimer

Vi introducerade sedan `DispatcherTimer`.

Till skillnad från `Task.Delay`, som vi använde för en enstaka fördröjning, kan en timer användas för att utföra något återkommande.

Vi skapade en timer i konstruktorn:

```csharp
DispatcherTimer timer = new();

timer.Interval = TimeSpan.FromSeconds(0.1);
timer.Tick += Timer_Tick;

timer.Start();
```

Här anger vi att timern ska försöka köra ett `Tick` ungefär var 100:e millisekund.

Vi kopplar också en eventhanterare till `Tick`.

En viktig egenskap hos `DispatcherTimer` är att dess `Tick`-event körs via den Dispatcher som timern är kopplad till.

Eftersom vi skapade timern på UI-tråden kan vi därför uppdatera UI-element direkt i eventhanteraren.

Vi behöver alltså inte använda `Dispatcher.Invoke` inne i `Tick`.

## 12. Räkna Tick

Vi skapade en räknare:

```csharp
private int _counter = 0;
```

Och uppdaterade knappen vid varje Tick:

```csharp
private void Timer_Tick(object? sender, EventArgs e)
{
    button.Content = $"{++_counter / 10.0}";
}
```

Eftersom timern har ett intervall på 0,1 sekunder kan vi dela antalet Tick med 10 för att få ett värde som motsvarar sekunder.

Men här finns ett problem:

**Är 100 Tick verkligen alltid samma sak som 10 sekunder?**

Nej!

`DispatcherTimer` garanterar inte att varje Tick inträffar exakt vid det angivna intervallet.

Timerns event måste få möjlighet att köras på UI-tråden. Om UI-tråden är upptagen eller Dispatcher skjuter upp arbetet kan ett Tick komma senare än planerat.

Timern garanterar inte heller att den kör alla missade Tick i efterhand för att komma ikapp.

Det betyder att en räknare baserad på antalet Tick kan visa fel tid.

## 13. Stopwatch – mäta faktiskt förfluten tid

För att undersöka detta använde vi `Stopwatch`:

```csharp
private Stopwatch _stopwatch = new();
```

Vi startade den i konstruktorn:

```csharp
_stopwatch.Start();
```

Och visade både vår Tick-baserade räknare och den faktiskt förflutna tiden:

```csharp
private void Timer_Tick(object? sender, EventArgs e)
{
    button.Content =
        $"{++_counter / 10.0}\n" +
        $"{_stopwatch.Elapsed.TotalSeconds}";
}
```

`Stopwatch` mäter förfluten tid med en högupplöst, monoton tidskälla.

Det betyder att den inte är beroende av hur många gånger vår `DispatcherTimer` har tickat.

När vi jämförde värdena upptäckte vi att de kunde skilja sig åt.

### Varför skiljer de sig åt?

`DispatcherTimer` är ett verktyg för att schemalägga återkommande arbete, inte en exakt klocka.

Om ett Tick försenas kan det innebära att vår räknare kommer efter den verkliga tiden.

Även små förseningar kan ackumuleras om vi beräknar förfluten tid genom att bara räkna antalet Tick.

Under genomgången såg vi också att beteendet kunde påverkas av exempelvis körning med debugger och arbete som upptar UI-tråden.

## 14. Vad betyder detta för vårt quiz?

I Quiz Configurator ska varje fråga ha en tidsgräns, exempelvis 30 sekunder.

En enkel implementation skulle kunna minska en räknare med ett vid varje Tick:

```csharp
private void Timer_Tick(object? sender, EventArgs e)
{
    SecondsRemaining--;
}
```

Men om ett Tick försenas blir nedräkningen också försenad.

En bättre lösning är att kombinera `DispatcherTimer` med `Stopwatch`:

- `DispatcherTimer` avgör **när vi ska uppdatera gränssnittet**.
- `Stopwatch` avgör **hur mycket tid som faktiskt har gått**.

Vi kan exempelvis beräkna återstående tid:

```csharp
TimeSpan remaining =
    TimeSpan.FromSeconds(30) - _stopwatch.Elapsed;
```

Sedan kan vi visa det beräknade värdet i vårt UI.

På så sätt är den återstående tiden baserad på verkligt förfluten tid, inte på hur många Tick som har inträffat.

Om UI-tråden blir upptagen kan själva uppdateringen fortfarande bli försenad, men nästa gång timern körs kan vi visa rätt återstående tid.

För ett quiz bör vi också kontrollera om tidsgränsen passerats när användaren försöker svara, eftersom ett försenat Tick annars kan innebära att vi inte hunnit avsluta frågan.

## 15. Sammanfattning

Under lektionen har vi arbetat med två viktiga områden.

### Commands

Vi har lärt oss att:

- `ICommand` används för att koppla UI-interaktioner till funktionalitet.
- `RelayCommand` låter oss återanvända en generell implementation av `ICommand`.
- `Execute` utför en handling.
- `CanExecute` avgör om handlingen får utföras.
- `CanExecuteChanged` meddelar WPF att villkoret kan ha ändrats.
- Samma Command kan användas av flera olika UI-element.
- `CommandParameter` kan användas för att skicka information till ett kommando.

### Dispatcher och tidmätning

Vi har också sett att:

- WPF-kontroller normalt måste uppdateras från sin UI-tråd.
- `Thread.Sleep` blockerar tråden som kör metoden.
- `await Task.Delay` låter oss vänta utan att blockera tråden.
- `Task.Run` kan användas för att schemalägga arbete på en bakgrundstråd.
- `Dispatcher` kan schemalägga arbete på UI-tråden.
- `DispatcherTimer` låter oss utföra återkommande arbete på Dispatcher-tråden.
- `DispatcherTimer` är inte en exakt tidmätare.
- `Stopwatch` kan användas för att mäta faktiskt förfluten tid.

**Den viktigaste principen för quizets nedräkning är att skilja på när vi uppdaterar gränssnittet och hur mycket tid som faktiskt har passerat.**

## Att arbeta vidare med i labben

Nu har ni byggstenarna för att själva börja implementera mer av funktionaliteten i Quiz Configurator.

Fundera särskilt på hur ni kan använda Commands för att hantera användarens handlingar utan onödig logik i code-behind.

När ni bygger spelläget behöver ni också fundera på hur ni använder `DispatcherTimer` och `Stopwatch` för att implementera tidsgränsen för varje fråga.

Ni behöver inte följa exakt samma implementation som i demonstrationerna. Målet är att förstå hur teknikerna fungerar och kunna använda dem i er egen lösning.