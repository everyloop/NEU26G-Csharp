# Oktober 28

**Code-along:**  
[L046_Labb3_Code-along](https://github.com/everyloop/NEU26G-Csharp/tree/master/Code-alongs/L046_Labb3_Code-along)

Under dagens code-along började vi bygga grunden till vår quizapplikation. Vi utgick från en tom WPF-applikation och skapade stegvis modeller, ViewModels och Views.

Målet var att skapa en struktur som vi kan fortsätta bygga vidare på under labben.  

(Vi blev inte helt färdiga, utan kommer fortsätta med Code-Along på Fredag.)

Vi gick igenom:

- Designmönstret **MVVM** och varför det används i WPF.
- Modellerna `Question` och `QuestionPack`.
- Hur en ViewModel kan omsluta en modell och göra den lämplig för databindning.
- `INotifyPropertyChanged`, `RaisePropertyChanged()` och `ViewModelBase`.
- `SetProperty<T>()` för properties med vanliga backing fields.
- `ObservableCollection<T>` och skillnaden mot `List<T>`.
- Hur vi delar upp gränssnittet i flera `UserControl`.
- Hur `DataContext` fungerar när vi använder flera Views och ViewModels.

Vi avslutade med att skapa ett frågepaket med några exempel­frågor, så att vi har data att arbeta med när vi fortsätter bygga gränssnittet.

---

## 1. Vad är MVVM?

**MVVM** står för:

- **Model**
- **View**
- **ViewModel**

Det är ett designmönster som används för att separera olika ansvarsområden i en applikation.

I en liten WPF-applikation kan vi lägga nästan all kod direkt i `MainWindow.xaml.cs`. Det fungerar, men när applikationen växer riskerar vi att få en stor klass som ansvarar för allt: användargränssnitt, data, logik och filhantering.

MVVM hjälper oss att organisera koden så att varje del har ett tydligare ansvar.

### Model – programmets data

Modellen beskriver informationen som applikationen arbetar med.

I vår quizapplikation har vi exempelvis:

- `Question` – en fråga med ett korrekt svar och tre felaktiga svar.
- `QuestionPack` – ett frågepaket med namn, svårighetsgrad, tidsgräns och en samling frågor.
- `Difficulty` – en enum med svårighetsgraderna `Easy`, `Medium` och `Hard`.

Modellerna är vanliga C#-klasser. De behöver inte känna till WPF eller hur informationen visas på skärmen.

### View – det användaren ser

View ansvarar för applikationens användargränssnitt.

I WPF beskriver vi vanligtvis gränssnittet med **XAML**.

Våra Views är:

- `MainWindow` – huvudfönstret.
- `MenuView` – applikationens meny.
- `ConfigurationView` – där användaren ska kunna hantera frågepaket och frågor.
- `PlayerView` – där användaren ska kunna spela quizet.

En View ska i första hand beskriva **hur något visas**, inte innehålla all logik för vad applikationen gör.

### ViewModel – kopplingen mellan View och Model

ViewModel fungerar som ett mellanlager mellan användargränssnittet och programmets data.

En ViewModel kan bland annat:

- Exponera data som en View kan binda till.
- Meddela gränssnittet när data förändras.
- Hålla reda på vilken fråga eller vilket frågepaket som är valt.
- Tillhandahålla funktioner som användaren kan utföra.

Det är framför allt genom **databindning** som View och ViewModel kommunicerar.

En förenklad bild av relationen:

```text
       View
    (XAML / UI)
         |
         | Databindning
         |
      ViewModel
         |
         | Läser och ändrar
         |
        Model
       (Data)
```

En viktig poäng är att **ViewModel inte behöver känna till de specifika kontrollerna i View**.

Vi ska exempelvis kunna ändra ett frågepakets namn utan att ViewModel behöver veta vilken `TextBox` som visar namnet.

### Är MVVM ett krav?

Nej. WPF kräver inte MVVM.

Det är ett sätt att strukturera applikationen, och det finns andra sätt att lösa samma problem.

För vår labb är MVVM användbart eftersom applikationen har flera vyer, flera typer av data och funktionalitet som kommer att byggas ut successivt.

---

## 2. Våra modeller: Question och QuestionPack

Vi började med att skapa modellerna, eftersom de beskriver den information som applikationen behöver.

### Question

En `Question` innehåller:

| Property | Beskrivning |
|---|---|
| `Query` | Själva frågetexten |
| `CorrectAnswer` | Det korrekta svaret |
| `IncorrectAnswers` | En array med tre felaktiga svar |

Vi använder en array för de felaktiga svaren eftersom varje fråga ska ha exakt tre sådana svar.

Tillsammans med `CorrectAnswer` ger det fyra svarsalternativ.

### QuestionPack

Ett `QuestionPack` innehåller:

| Property | Beskrivning |
|---|---|
| `Name` | Frågepaketets namn |
| `Difficulty` | Svårighetsgrad |
| `TimeLimitInSeconds` | Tidsgräns i sekunder |
| `Questions` | En `List<Question>` med frågor |

Ett frågepaket innehåller alltså flera frågor.

```text
QuestionPack
├── Name
├── Difficulty
├── TimeLimitInSeconds
└── Questions
    ├── Question
    ├── Question
    └── Question
```

Modellerna innehåller vanliga C#-properties och är inte beroende av WPF.

Det är avsiktligt. Senare ska vi även kunna spara och läsa in dessa data, och då är det praktiskt att modellerna är separerade från användargränssnittet.

---

## 3. Varför skapade vi QuestionPackViewModel?

Efter modellerna skapade vi `QuestionPackViewModel`.

Den tar emot ett `QuestionPack` i konstruktorn och sparar en referens till modellen:

```csharp
private readonly QuestionPack _model;

public QuestionPackViewModel(QuestionPack model)
{
    _model = model;
    Questions = new ObservableCollection<Question>(_model.Questions);
}
```

Vi kan säga att ViewModeln **omsluter modellen**.

Syftet är att göra informationen tillgänglig för WPF:s databindningar och samtidigt kunna meddela gränssnittet när något ändras.

### Properties som vidarebefordrar till modellen

I modellen har vi en vanlig property:

```csharp
public string Name { get; set; }
```

I `QuestionPackViewModel` har vi också en property som heter `Name`, men den arbetar med modellens värde:

```csharp
public string Name
{
    get => _model.Name;
    set
    {
        if (_model.Name == value)
            return;

        _model.Name = value;
        RaisePropertyChanged();
    }
}
```

Det viktiga här är att vi **inte lagrar två olika namn**.

När någon läser `QuestionPackViewModel.Name` hämtas värdet från `_model.Name`.

När någon ändrar `QuestionPackViewModel.Name` ändras värdet i modellen.

ViewModeln lägger dessutom till en notifiering så att WPF kan uppdatera gränssnittet.

Samma princip använder vi för:

- `Difficulty`
- `TimeLimitInSeconds`

### Varför kontrollerar vi om värdet redan är samma?

Vi har exempelvis:

```csharp
if (_model.Name == value)
    return;
```

Om värdet inte har ändrats behöver vi inte göra någon uppdatering eller skicka någon notifiering.

Det undviker onödigt arbete och gör det tydligt att `PropertyChanged` ska användas när en property faktiskt förändras.

---

## 4. INotifyPropertyChanged och RaisePropertyChanged()

Från föregående lektion vet vi att WPF behöver få veta när en property ändras i C#-koden.

Det är därför vi använder `INotifyPropertyChanged`.

Interfacet definierar ett event:

```csharp
public event PropertyChangedEventHandler? PropertyChanged;
```

När eventet skickas kan WPF:s databindningssystem få veta att en property har ändrats och läsa in det nya värdet.

För att slippa skriva samma eventanrop överallt skapade vi metoden:

```csharp
public void RaisePropertyChanged(
    [CallerMemberName] string? propertyName = null)
{
    PropertyChanged?.Invoke(
        this,
        new PropertyChangedEventArgs(propertyName));
}
```

### Vad gör CallerMemberName?

Attributet `[CallerMemberName]` gör att C# automatiskt kan fylla i namnet på den medlem som anropar metoden.

När vi skriver:

```csharp
RaisePropertyChanged();
```

i settern för `Name` används alltså namnet `"Name"`.

Vi slipper skriva:

```csharp
RaisePropertyChanged(nameof(Name));
```

båda varianterna är dock giltiga.

Det senare är användbart om vi vill meddela att **en annan property** också behöver uppdateras.

### PropertyChanged ändrar inte värdet

Det är viktigt att skilja på två saker:

1. Vi ändrar värdet i modellen.
2. Vi meddelar WPF att värdet har ändrats.

`RaisePropertyChanged()` gör bara det andra.

Den ändrar inte själva propertyvärdet.

---

## 5. ViewModelBase – återanvändbar funktionalitet

Vi började med notifieringskoden direkt i `QuestionPackViewModel`.

Men vi kommer att skapa flera ViewModels, och många av dem behöver samma funktionalitet.

Därför flyttade vi den gemensamma koden till `ViewModelBase`.

```csharp
internal class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void RaisePropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }

    // SetProperty<T>() finns också här.
}
```

Sedan låter vi `QuestionPackViewModel` ärva från klassen:

```csharp
internal class QuestionPackViewModel : ViewModelBase
```

Då behöver vi inte implementera `INotifyPropertyChanged` separat i varje ViewModel.

Det här är ett exempel på **arv för att återanvända gemensam funktionalitet**.

### SetProperty<T>()

Lite senare lade vi även till metoden `SetProperty<T>()` i `ViewModelBase`.

Den förenklar properties som använder ett vanligt backing field.

```csharp
protected bool SetProperty<T>(
    ref T field,
    T value,
    [CallerMemberName] string? propertyName = null)
{
    if (EqualityComparer<T>.Default.Equals(field, value))
        return false;

    field = value;
    RaisePropertyChanged(propertyName);

    return true;
}
```

Metoden:

1. Kontrollerar om det nya värdet skiljer sig från det gamla.
2. Uppdaterar fältet om värdet har ändrats.
3. Skickar `PropertyChanged`.
4. Returnerar `true` om en ändring gjordes, annars `false`.

Vi använder den exempelvis för `ActivePack` i `MainWindowViewModel`:

```csharp
private QuestionPackViewModel _activePack;

public QuestionPackViewModel ActivePack
{
    get => _activePack;
    set => SetProperty(ref _activePack, value);
}
```

Detta ersätter en längre setter med samma logik.

### Varför använder vi inte SetProperty för Name i QuestionPackViewModel?

Därför att `Name` lagras i modellen:

```csharp
_model.Name
```

Det är en **property**, inte ett fält.

`SetProperty` tar emot ett fält via `ref`, och vi kan inte skicka in en vanlig property som `ref`-argument.

Därför använder vi fortfarande en egen setter med `RaisePropertyChanged()` i `QuestionPackViewModel`.

Båda sätten är alltså användbara, men i olika situationer.

---

## 6. Vi testade databindningen innan vi byggde vidare

När vi hade skapat `QuestionPackViewModel` gjorde vi en liten tillfällig demonstration direkt i `MainWindow`.

Vi använde en `QuestionPackViewModel` som fönstrets `DataContext`.

Sedan band vi bland annat:

- En `TextBox.Text` till `Difficulty`.
- En `Button.Content` till `TimeLimitInSeconds`.

Med ett vanligt `Click`-event ändrade vi sedan båda värdena i C#-koden.

Syftet var att se att **ViewModeln meddelar WPF när properties ändras**, så att kontrollerna kan visa de nya värdena.

Det var samma princip som vi arbetade med under förra lektionens databindningsövningar, men nu med en separat ViewModel.

Den här demonstrationen tog vi sedan bort när vi började bygga applikationens riktiga struktur.

---

## 7. List<T> och ObservableCollection<T>

I modellen använder vi:

```csharp
public List<Question> Questions { get; set; }
```

Men i `QuestionPackViewModel` använder vi:

```csharp
public ObservableCollection<Question> Questions { get; }
```

Varför två olika samlingstyper?

### List<T>

`List<T>` är en vanlig lista.

Vi kan lägga till, ta bort och hämta element, men listan skickar inte automatiskt notifieringar till WPF när innehållet ändras.

### ObservableCollection<T>

`ObservableCollection<T>` är en samling som kan meddela när element läggs till, tas bort eller flyttas.

Det gör den särskilt användbar för exempelvis `ListBox`, där vi vill att gränssnittet ska reagera när samlingen ändras.

Vi skapar samlingen i konstruktorn:

```csharp
Questions = new ObservableCollection<Question>(_model.Questions);
```

Det innebär att vi skapar en **ny samling** som från början innehåller samma `Question`-objekt som modellens lista.

Det är viktigt att förstå skillnaden:

- Själva samlingen är ny.
- Frågorna i samlingen är fortfarande referenser till samma objekt.
- Om vi lägger till en fråga i `ObservableCollection` läggs den inte automatiskt till i modellens `List<Question>`.

Vi kommer senare att behöva ta hänsyn till detta när vi arbetar med att spara frågepaket.

### ObservableCollection meddelar inte automatiskt när en fråga ändras

`ObservableCollection<T>` meddelar när **samlingens innehåll** förändras.

Den meddelar däremot inte automatiskt när en property hos ett objekt i samlingen ändras.

Exempelvis:

```csharp
Questions[0].Query = "En ny frågetext";
```

Detta ändrar frågans text, men `Question` implementerar inte `INotifyPropertyChanged`.

Det kan påverka om andra kontroller uppdateras direkt när vi redigerar en fråga. Vi återkommer till det när vi bygger redigeringsgränssnittet.

---

## 8. Vi delar upp gränssnittet i flera Views

Efter den tillfälliga databindningsdemonstrationen började vi bygga applikationens riktiga gränssnitt.

I stället för att lägga all XAML i `MainWindow` skapade vi separata `UserControl`.

Vi har:

- `MenuView`
- `ConfigurationView`
- `PlayerView`

Ett `UserControl` är en återanvändbar WPF-kontroll som kan innehålla andra kontroller.

Det gör att vi kan dela upp ett större gränssnitt i mindre, mer hanterbara delar.

### MainWindow som behållare

Vårt huvudfönster innehåller en `Grid` med två rader.

Den första raden har höjden `Auto` och innehåller menyn.

Den andra raden har höjden `*` och använder det återstående utrymmet för den aktiva vyn.

Förenklat:

```xml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="*" />
    </Grid.RowDefinitions>

    <views:MenuView Grid.Row="0" />
    <views:ConfigurationView Grid.Row="1" />
</Grid>
```

För att använda ett `UserControl` från en annan namespace behöver vi deklarera ett XML-namespace, exempelvis:

```xml
xmlns:views="clr-namespace:L046_Labb3_Code_along.Views"
```

Sedan kan vi använda våra egna kontroller med prefixet `views:`.

### Hur växlar vi mellan vyerna?

Just nu växlar vi genom att kommentera ut antingen `ConfigurationView` eller `PlayerView` i `MainWindow.xaml`.

Det är en tillfällig lösning medan vi bygger grunden.

Senare ska användaren kunna växla mellan vyerna inifrån applikationen.

Vi har också skapat en enkel meny, men ännu inte kopplat menyvalen till funktionalitet.

---

## 9. MainWindowViewModel – applikationens gemensamma tillstånd

När vi började använda flera Views behövde vi också fundera på var informationen ska finnas.

Vi skapade därför `MainWindowViewModel`.

Den fungerar som en central ViewModel för huvudfönstret och innehåller bland annat:

```csharp
public ObservableCollection<QuestionPackViewModel> Packs { get; }

public QuestionPackViewModel ActivePack { get; set; }
```

I vår riktiga kod använder `ActivePack` ett backing field och `SetProperty()`.

### Packs

`Packs` är tänkt att innehålla alla frågepaket som finns i applikationen.

Vi har förberett denna property, men ännu inte börjat använda samlingen.

### ActivePack

`ActivePack` representerar det frågepaket som användaren arbetar med just nu.

Det är viktigt eftersom både konfigurationsvyn och spelvyn ska kunna arbeta med samma aktuella frågepaket.

Vi vill inte att varje View ska skapa sitt eget, oberoende frågepaket.

### Tillfälliga exempeldata

I konstruktorn för `MainWindowViewModel` skapar vi ett frågepaket med två frågor:

```csharp
var questionPack = new QuestionPack(
    "Mina frågor!",
    Difficulty.Easy);

// Här lägger vi till våra exempelfrågor.

ActivePack = new QuestionPackViewModel(questionPack);
```

Detta är tillfälliga testdata som gör att vi kan bygga och testa gränssnittet innan vi har implementerat inläsning från filer.

Det betyder att ni redan har data som går att visa i `ConfigurationView`.

---

## 10. Flera ViewModels – hur hänger de ihop?

Vi skapade också:

- `ConfigurationViewModel`
- `PlayerViewModel`

Tanken är att varje View ska ha en ViewModel som ansvarar för den information och funktionalitet som hör till just den vyn.

`MainWindowViewModel` skapar dessa ViewModels:

```csharp
PlayerViewModel = new PlayerViewModel(this);
ConfigurationViewModel = new ConfigurationViewModel(this);
```

Här skickar vi in `this`, alltså en referens till den aktuella `MainWindowViewModel`.

Det gör att de andra ViewModels kan komma åt gemensam information.

### Varför skickar vi in MainWindowViewModel?

Tänk på att både `ConfigurationViewModel` och `PlayerViewModel` behöver kunna komma åt det aktiva frågepaketet.

I stället för att skapa ett nytt frågepaket i varje ViewModel kan de använda informationen från `MainWindowViewModel`.

I `ConfigurationViewModel` har vi därför:

```csharp
private readonly MainWindowViewModel? _mainWindowViewModel;

public QuestionPackViewModel? ActivePack
    => _mainWindowViewModel?.ActivePack;
```

När någon läser `ConfigurationViewModel.ActivePack` hämtas alltså det aktiva frågepaketet från `MainWindowViewModel`.

Det skapas **inte ett nytt frågepaket**.

Vi arbetar fortfarande med samma objekt.

### Hur ser objektrelationerna ut?

```text
MainWindowViewModel
│
├── ActivePack
│   └── QuestionPackViewModel
│       └── QuestionPack
│           └── Questions
│
├── ConfigurationViewModel
│   └── Referens tillbaka till MainWindowViewModel
│
└── PlayerViewModel
    └── Referens tillbaka till MainWindowViewModel
```

Det här är ett sätt att dela information mellan flera ViewModels.

Det finns andra lösningar, men för vår labb ger det oss en ganska enkel struktur att börja med.

**Observera:** När vi senare börjar byta `ActivePack` behöver vi också se till att de andra ViewModels kan meddela sina Views om förändringen. Den delen har vi inte implementerat ännu.

---

## 11. DataContext – den viktigaste kopplingen mellan View och ViewModel

Det här är förmodligen den viktigaste delen att förstå innan ni börjar bygga vidare på gränssnittet.

Vi har flera Views och flera ViewModels, men hur vet WPF vilken ViewModel som ska användas av vilken View?

Svaret är **DataContext**.

### Steg 1: MainWindow får en ViewModel

I `MainWindow.xaml.cs` har vi:

```csharp
public MainWindow()
{
    InitializeComponent();

    DataContext = new MainWindowViewModel();
}
```

Det innebär att bindningar i `MainWindow` som standard använder `MainWindowViewModel`.

Om vi exempelvis skriver:

```xml
<TextBlock Text="{Binding ActivePack.Name}" />
```

försöker WPF hitta:

```text
MainWindowViewModel
    .ActivePack
    .Name
```

### Steg 2: DataContext ärvs av underliggande kontroller

I WPF ärvs `DataContext` normalt nedåt genom det logiska elementträdet.

När vi placerar ett `UserControl` inuti `MainWindow` får det därför tillgång till samma `DataContext`, så länge vi inte anger något annat.

Det innebär att vår `ConfigurationView` till en början ärver `MainWindowViewModel` som DataContext.

### Steg 3: ConfigurationView byter till sin egen ViewModel

I `ConfigurationView.xaml` har vi:

```xml
DataContext="{Binding ConfigurationViewModel}"
```

Den här bindningen använder det DataContext som kontrollen ärver från `MainWindow`.

WPF letar alltså efter propertyn:

```csharp
MainWindowViewModel.ConfigurationViewModel
```

När den hittas används objektet som nytt `DataContext` för `ConfigurationView`.

Därmed får kontrollerna **inuti ConfigurationView** tillgång till `ConfigurationViewModel`.

Hela kedjan ser ut så här:

```text
MainWindow
│
│ DataContext = MainWindowViewModel
│
└── ConfigurationView
    │
    │ Ärver först MainWindowViewModel
    │
    │ DataContext="{Binding ConfigurationViewModel}"
    │
    │ Byter till ConfigurationViewModel
    │
    └── Button / TextBox / ListBox / ...
        │
        │ Ärver ConfigurationViewModel
        │
        └── Binding ActivePack.Name
```

### Steg 4: Bindningar inuti ConfigurationView

Vi har redan följande knapp:

```xml
<Button Content="{Binding ActivePack.Name}" />
```

Eftersom knappens DataContext är `ConfigurationViewModel` betyder bindningen:

```text
ConfigurationViewModel.ActivePack.Name
```

Men `ConfigurationViewModel.ActivePack` hämtar i sin tur värdet från `MainWindowViewModel.ActivePack`.

Så den fullständiga kedjan blir:

```text
ConfigurationView
    ↓ DataContext
ConfigurationViewModel
    ↓ ActivePack
QuestionPackViewModel
    ↓ Name
QuestionPack.Name
```

Det är därför knappen kan visa namnet på vårt frågepaket.

### Kom ihåg

När en binding inte fungerar är en av de första frågorna du bör ställa:

**Vilket objekt är DataContext för just den här kontrollen?**

Om du vet det blir det mycket lättare att förstå vilken property WPF försöker hitta.

---

## 12. Att arbeta vidare med: ConfigurationView

Nu har vi tillräckligt mycket struktur på plats för att börja bygga det riktiga gränssnittet.

I morgon (torsdag) är rekommendationen att ni framför allt arbetar med **XAML-designen för ConfigurationView**.

Målet är att användaren ska kunna:

1. Se vilka frågor som finns i det aktiva frågepaketet.
2. Välja en fråga i en lista.
3. Se frågetext och svarsalternativ för den valda frågan.
4. Redigera frågetext och svarsalternativ.

Ni behöver inte implementera knapparna för att lägga till eller ta bort frågor ännu. Vi kommer att arbeta med kommandon längre fram.

Ni behöver inte heller kopiera designen från demonstrationsapplikationen exakt. Det viktiga är att funktionaliteten finns.

### Förslag på layout

Ett lämpligt upplägg är en `Grid` med två kolumner:

```text
+----------------------------+-----------------------------+
|                            |                             |
|  Lista med frågor          |  Frågetext                  |
|                            |                             |
|  Question 1                |  [ TextBox                ]  |
|  Question 2                |                             |
|  Question 3                |  Correct Answer             |
|                            |  [ TextBox                ]  |
|                            |                             |
|                            |  Incorrect Answer 1         |
|                            |  [ TextBox                ]  |
|                            |                             |
|                            |  Incorrect Answer 2         |
|                            |  [ TextBox                ]  |
|                            |                             |
|                            |  Incorrect Answer 3         |
|                            |  [ TextBox                ]  |
+----------------------------+-----------------------------+
```

Detta går att bygga med de WPF-kontroller vi redan har arbetat med.

### Ledtråd 1: ItemsSource

För att visa frågorna kan ni använda en `ListBox`.

En `ListBox` har en property som heter `ItemsSource`.

Den kan bindas till en samling:

```xml
<ListBox ItemsSource="{Binding ActivePack.Questions}" />
```

Fundera på varför bindningen börjar med `ActivePack` och inte direkt med `Questions`.

*Ledtråd: Vilken ViewModel är DataContext för ConfigurationView?*

### Ledtråd 2: DataTemplate

Om ni bara binder en lista med `Question`-objekt till `ItemsSource` vet WPF inte automatiskt hur ni vill presentera varje fråga.

Därför kan ni använda en `DataTemplate` via `ListBox.ItemTemplate`.

En `DataTemplate` beskriver **hur ett objekt i en samling ska visas**.

När en `DataTemplate` används för en fråga är DataContext inne i mallen själva `Question`-objektet.

Det betyder att ni där kan binda direkt till exempelvis:

```xml
<TextBlock Text="{Binding Query}" />
```

Ni kan även kombinera flera kontroller i en `DataTemplate`, exempelvis en rubrik och frågetext.

Undersök gärna hur `ListBox.ItemTemplate` och `DataTemplate` fungerar.

### Ledtråd 3: SelectedItem

En `ListBox` har en property som heter `SelectedItem`.

Den representerar det objekt som användaren har markerat i listan.

När användaren väljer en fråga vill vi att vår `ConfigurationViewModel` ska veta vilken fråga som är vald, så att vi kan visa och redigera den.

Fundera på:

- Vilken property behöver vi lägga till i `ConfigurationViewModel` för att hålla reda på den valda frågan?
- Vilken datatyp ska den ha? Vad ska värdet vara när ingen fråga är vald?
- Hur kan vi använda `SelectedItem` och databindning för att hålla propertyn uppdaterad?
- Behöver `ConfigurationViewModel` ärva från någon av våra befintliga klasser?

Tänk tillbaka på hur vi använde `SetProperty()` och `INotifyPropertyChanged` under code-alongen.

### Ledtråd 4: Redigera den valda frågan

När användaren markerar en fråga i listan ska informationen om den frågan visas i formuläret till höger.

Vi vill kunna visa och redigera:

- Frågetexten.
- Det korrekta svaret.
- De tre felaktiga svaren.

Ni behöver inte skriva någon kod i `Click`-events eller manuellt kopiera värden mellan kontrollerna.

Det ska gå att lösa med **databindning**.

Fundera på:

- Vilket objekt är `DataContext` för kontrollerna i `ConfigurationView`?
- Hur kan ni skriva en binding som når en property hos den valda frågan?
- Hur kommer ni åt de tre elementen i arrayen `IncorrectAnswers`?
- Vilket `Binding.Mode` och vilket `UpdateSourceTrigger` är lämpligt när användaren ska kunna redigera text?

**Målet är att användaren ska kunna välja olika frågor i listan och se motsvarande information i formuläret, samt ändra frågornas innehåll.**

---

## 13. Om ni hinner: börja titta på PlayerView

Om ni kommer långt med `ConfigurationView` kan ni börja bygga layouten för `PlayerView`.

Den ska så småningom kunna visa:

- Den aktuella frågan.
- Fyra svarsalternativ som knappar.
- Vilken fråga spelaren befinner sig på.
- Poäng och eventuell återstående tid.

Här behöver vi mer logik än i konfigurationsvyn.

Vi ska bland annat hålla reda på vilken fråga som visas, slumpa svarsalternativ och hantera användarens svar.

Ni behöver därför inte få spelvyn funktionell ännu.

Det räcker gott att börja experimentera med XAML-layouten.

---

## 14. Vad kommer vi att fortsätta med?

Vi har nu byggt grunden, men mycket återstår.

Under kommande lektioner kommer vi bland annat att arbeta med:

- Att lägga till och ta bort frågor och frågepaket.
- Att byta aktivt frågepaket.
- Att växla mellan `ConfigurationView` och `PlayerView`.
- `ICommand` och `RelayCommand` för knappar och menyer.
- Att spara och läsa in frågepaket.
- Spelvyn och quizets logik.

Vi har redan pratat kort om att `Command` kan användas i exempelvis `Button` och `MenuItem` i stället för vanliga `Click`-events.

Vi kommer att implementera detta längre fram.

---

## Sammanfattning

Det viktigaste från dagens lektion är inte att memorera alla klasser och metoder, utan att förstå **varför vi har delat upp applikationen på det här sättet**.

- **Model** beskriver informationen i applikationen.
- **View** beskriver hur användargränssnittet ser ut.
- **ViewModel** exponerar information och funktionalitet för View.
- **DataContext** avgör vilket objekt en binding utgår från.
- **INotifyPropertyChanged** gör att WPF kan reagera när properties ändras.
- **ViewModelBase** låter flera ViewModels återanvända samma notifieringskod.
- **ObservableCollection** meddelar WPF när element i en samling läggs till eller tas bort.
- **UserControl** gör det möjligt att dela upp ett större gränssnitt i mindre Views.

### Frågor att kunna resonera kring

1. Varför har vi både `QuestionPack` och `QuestionPackViewModel`?
2. Varför använder vi `INotifyPropertyChanged` i våra ViewModels?
3. Vad är skillnaden mellan `List<T>` och `ObservableCollection<T>`?
4. Varför ärver `QuestionPackViewModel` från `ViewModelBase`?
5. Vad gör `SetProperty<T>()`, och varför kan vi inte använda den direkt med `_model.Name`?
6. Vilket objekt är `DataContext` för `MainWindow`?
7. Hur får `ConfigurationView` sin egen `ConfigurationViewModel` som DataContext?
8. Varför fungerar bindningen `{Binding ActivePack.Name}` inuti `ConfigurationView`?
9. Vad är skillnaden mellan `ItemsSource`, `ItemTemplate` och `SelectedItem` i en `ListBox`?

**Fokus inför nästa lektion:** Försök få `ConfigurationView` att visa och redigera frågorna i vårt aktiva frågepaket. Använd databindningar och den struktur vi redan har skapat, snarare än att lägga logik direkt i code-behind.