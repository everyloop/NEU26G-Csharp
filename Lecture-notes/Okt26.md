# Oktober 26

## Vad är Data Binding?

**Data Binding** innebär att vi kopplar en property på ett UI-element till ett värde från en annan källa.

WPF kan då automatiskt läsa värdet från källan och visa det i gränssnittet. Beroende på hur bindningen är konfigurerad kan ändringar även skickas tillbaka till källan.

Det gör att vi ofta slipper skriva event handlers för att manuellt hålla olika kontroller synkroniserade.

Exempelvis kan vi:

- Visa texten från en `TextBox` i en annan kontroll.
- Styra textstorleken med en `Slider`.
- Aktivera eller inaktivera en kontroll med en `CheckBox`.
- Visa och redigera properties från ett vanligt C#-objekt.
- Uppdatera gränssnittet automatiskt när objektets properties ändras.

### Utan Data Binding

Om vi vill kopiera text från en textruta till en annan skulle vi kunna använda `TextChanged`:

```csharp
private void sourceTextBox_TextChanged(object sender, TextChangedEventArgs e)
{
    destinationTextBox.Text = sourceTextBox.Text;
}
```

Om ändringar även ska fungera åt andra hållet behöver vi hantera det också.

Med Data Binding kan vi i många fall lösa detta direkt i XAML.

---

## Source, Target och Path

En binding har några viktiga delar:

| Begrepp | Förklaring |
|---|---|
| **Source** | Objektet vi hämtar värdet från |
| **Target** | Den property på UI-elementet som vi binder |
| **Path** | Vilken property på källobjektet vi vill använda |
| **Mode** | I vilken riktning värdet ska kunna uppdateras |
| **UpdateSourceTrigger** | När ändringar i target ska skickas tillbaka till source |

Exempel:

```xml
<TextBox x:Name="sourceTextBox"
         Text="Hello" />

<TextBox Text="{Binding ElementName=sourceTextBox, Path=Text}" />
```

Här är:

- **Source:** `sourceTextBox`
- **Path:** `Text`
- **Target:** `Text` på den andra textrutan

Den andra textrutan visar alltså texten från `sourceTextBox`.

### Binding-syntax

En binding skrivs vanligtvis med en *markup extension*:

```xml
{Binding ...}
```

Vi kan ange flera inställningar, separerade med kommatecken:

```xml
{Binding ElementName=sourceTextBox, Path=Text, Mode=TwoWay}
```

`Path=` kan ofta utelämnas:

```xml
{Binding ElementName=sourceTextBox, Path=Text}
```

är samma sak som:

```xml
{Binding Text, ElementName=sourceTextBox}
```

---

# ElementName

Med `ElementName` kan vi använda ett annat namngivet XAML-element som datakälla.

Det är särskilt användbart när vi vill koppla ihop kontroller utan att skriva C#-kod.

## Exempel: Två textrutor

Från dagens code-along:

```xml
<TextBox x:Name="sourceTextBox"
         Margin="5 0"
         Text="Hello" />

<TextBox x:Name="destinationTextBox"
         Margin="5 0"
         Text="{Binding ElementName=sourceTextBox,
                        Path=Text,
                        UpdateSourceTrigger=PropertyChanged,
                        Mode=TwoWay}" />
```

Här binder vi `destinationTextBox.Text` till `sourceTextBox.Text`.

Eftersom vi använder `Mode=TwoWay` kan ändringar skickas i båda riktningarna.

När vi skriver i den ena textrutan kan den andra uppdateras automatiskt.

Vi behöver alltså inte längre våra tidigare `TextChanged`-event handlers.

## ElementName fungerar med andra properties

Vi är inte begränsade till text.

Exempel med en slider:

```xml
<Slider x:Name="fontSizeSlider"
        Minimum="10"
        Maximum="50"
        Value="20" />

<TextBlock Text="Sample Text"
           FontSize="{Binding Value, ElementName=fontSizeSlider}" />
```

Här är `Slider.Value` källan och `TextBlock.FontSize` target.

Ett annat exempel:

```xml
<CheckBox x:Name="enableCheckBox"
          Content="Enable slider"
          IsChecked="True" />

<Slider IsEnabled="{Binding IsChecked, ElementName=enableCheckBox}" />
```

Här binder vi två properties som båda använder booleska värden.

---

# Binding Mode

`Mode` bestämmer i vilken riktning värden kan uppdateras.

| Mode | Beskrivning |
|---|---|
| `OneWay` | Source uppdaterar target |
| `TwoWay` | Source och target kan uppdatera varandra |
| `OneTime` | Värdet läses in, men efterföljande ändringar på samma datakälla bevakas inte |
| `OneWayToSource` | Target uppdaterar source |
| `Default` | Använder standardläget för target-propertyn |

## OneWay

```xml
<TextBlock Text="{Binding Text,
                          ElementName=sourceTextBox,
                          Mode=OneWay}" />
```

När texten i `sourceTextBox` ändras uppdateras `TextBlock`.

Ändringar kan däremot inte skickas tillbaka från `TextBlock` till källan.

## TwoWay

```xml
<TextBox Text="{Binding Text,
                        ElementName=sourceTextBox,
                        Mode=TwoWay}" />
```

Nu kan ändringar skickas i båda riktningarna.

Det är vanligt för kontroller där användaren ska kunna redigera data.

## OneTime

```xml
<TextBlock Text="{Binding Text,
                          ElementName=sourceTextBox,
                          Mode=OneTime}" />
```

Bindingen hämtar värdet när den utvärderas, men fortsätter inte bevaka vanliga ändringar i källans property.

En `OneTime`-binding kan dock utvärderas igen om dess `DataContext` ändras.

## OneWayToSource

```xml
<TextBox Text="{Binding Text,
                        ElementName=sourceTextBox,
                        Mode=OneWayToSource}" />
```

Här går uppdateringarna från target till source.

Det är mindre vanligt, men kan vara användbart när vi vill skriva tillbaka ett värde utan att läsa in det från källan.

## Default

Om vi inte anger `Mode` används target-propertyns standardinställning.

Exempelvis:

- `TextBlock.Text` använder normalt `OneWay`.
- `TextBox.Text` använder normalt `TwoWay`.
- `Slider.Value` använder normalt `TwoWay`.

**Viktigt:** Standardläget beror på vilken property som är *target*, inte vilken kontroll som är source.

---

# UpdateSourceTrigger

`UpdateSourceTrigger` bestämmer **när en ändring i target ska skickas tillbaka till source**.

Det är framför allt intressant vid `TwoWay` och `OneWayToSource`.

| Värde | Beskrivning |
|---|---|
| `PropertyChanged` | Source uppdateras när target-propertyn ändras |
| `LostFocus` | Source uppdateras när kontrollen tappar fokus |
| `Explicit` | Source uppdateras först när vi begär det från C# |
| `Default` | Använder target-propertyns standardinställning |

## PropertyChanged

```xml
<TextBox Text="{Binding Text,
                        ElementName=sourceTextBox,
                        Mode=TwoWay,
                        UpdateSourceTrigger=PropertyChanged}" />
```

När användaren skriver uppdateras källan direkt.

## LostFocus

```xml
<TextBox Text="{Binding Text,
                        ElementName=sourceTextBox,
                        Mode=TwoWay,
                        UpdateSourceTrigger=LostFocus}" />
```

Nu skickas ändringarna normalt tillbaka till källan först när textrutan tappar fokus.

Detta är standardbeteendet för `TextBox.Text`.

### Viktig skillnad

`UpdateSourceTrigger` styr **target → source**.

Det styr inte när source får uppdatera target.

Därför är `UpdateSourceTrigger=PropertyChanged` inte samma sak som `INotifyPropertyChanged`, trots att namnen liknar varandra.

## Explicit

Det finns också:

```xml
UpdateSourceTrigger=Explicit
```

Då kan vi manuellt uppdatera källan från C#:

```csharp
destinationTextBox
    .GetBindingExpression(TextBox.TextProperty)?
    .UpdateSource();
```

Detta kan vara användbart om vi vill bestämma exakt när ett värde ska skickas tillbaka till datakällan.

---

# StringFormat

Ibland vill vi formatera ett värde innan det visas.

Det kan vi göra direkt i en binding med `StringFormat`.

Exempel:

```xml
<Slider x:Name="fontSizeSlider"
        Minimum="10"
        Maximum="50" />

<TextBlock Text="{Binding Value,
                          ElementName=fontSizeSlider,
                          StringFormat={}{0:F2}}" />
```

`F2` betyder att talet visas med två decimaler.

Exempel:

- `25,1234` visas som `25,12`.
- `30` visas som `30,00`.

Vi kan även kombinera värdet med vanlig text:

```xml
<TextBlock Text="{Binding Value,
                          ElementName=fontSizeSlider,
                          StringFormat='Font size: {0:F0}'}" />
```

Det kan exempelvis visa:

**Font size: 27**

`StringFormat` påverkar bara hur värdet presenteras. Det ändrar inte det faktiska värdet på källobjektet.

---

# DataContext

Hittills har vi använt `ElementName` för att binda till andra UI-element.

Men ofta vill vi i stället binda till ett vanligt C#-objekt.

Det är här `DataContext` kommer in.

`DataContext` är en property på WPF-element som anger vilket objekt deras bindings normalt ska använda som datakälla.

Om vi inte anger `ElementName`, `Source` eller `RelativeSource` i en binding används elementets `DataContext`.

Exempel:

```xml
<TextBox Text="{Binding FirstName}" />
```

WPF försöker då hitta propertyn `FirstName` på objektet som finns i textrutans `DataContext`.

## Andra sätt att ange datakälla

| Egenskap | Beskrivning |
|---|---|
| `ElementName` | Använd ett namngivet XAML-element |
| `Source` | Ange ett specifikt objekt som källa |
| `RelativeSource` | Hitta ett källobjekt relativt target |
| Ingen av dessa | Använd `DataContext` |

Vi kan bara använda en av `ElementName`, `Source` och `RelativeSource` i samma binding.

---

# Exempel: Binda till en Person

I dagens code-along skapade vi en klass som heter `Person`.

Till att börja med kan den se ut så här:

```csharp
class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
}
```

Sedan skapar vi ett objekt i fönstrets konstruktor:

```csharp
public partial class MainWindow : Window
{
    private Person _person;

    public MainWindow()
    {
        InitializeComponent();

        _person = new Person
        {
            FirstName = "Anders",
            LastName = "Svensson"
        };

        DataContext = _person;
    }
}
```

Nu är fönstrets `DataContext` en referens till vårt `Person`-objekt.

I XAML kan vi skriva:

```xml
<Label Content="{Binding Path=FirstName}" />

<TextBox Text="{Binding Path=FirstName,
                        Mode=TwoWay,
                        UpdateSourceTrigger=PropertyChanged}" />

<Label Content="Last name" />

<TextBox Text="{Binding Path=LastName,
                        Mode=TwoWay}" />
```

Alla dessa bindings använder samma `Person`-objekt som källa.

Det är alltså inte nödvändigt att ange objektet separat i varje binding.

Vi kan också skriva:

```xml
{Binding FirstName}
```

i stället för:

```xml
{Binding Path=FirstName}
```

Båda betyder samma sak.

---

# DataContext ärvs

En viktig egenskap hos `DataContext` är att den normalt **ärvs från föräldraelementet**.

Exempel:

```xml
<Window ...>
    <StackPanel>
        <TextBlock Text="{Binding FirstName}" />
        <TextBox Text="{Binding LastName}" />
    </StackPanel>
</Window>
```

Om vi sätter `DataContext` på fönstret kommer kontrollerna i `StackPanel` normalt att ärva samma värde.

Det beror på att `DataContext` är en *dependency property* som stöder **property value inheritance**.

Vi behöver alltså inte sätta `DataContext` på varje enskild kontroll.

## Ett element kan ha ett eget DataContext

Vi kan också sätta ett annat `DataContext` på exempelvis en `StackPanel`.

Då använder dess barn normalt det objektet i stället för fönstrets objekt.

Exempel:

```csharp
stackPanel.DataContext = new Person
{
    FirstName = "Anders",
    LastName = "Andersson"
};

DataContext = new Person
{
    FirstName = "Bertil",
    LastName = "Bertilsson"
};
```

Kontroller inuti `stackPanel` använder normalt Anders.

Kontroller som ärver direkt från fönstret, utan att ligga under en annan källa, använder Bertil.

## Ta bort ett lokalt DataContext

Vi kan ta bort ett lokalt satt värde:

```csharp
stackPanel.ClearValue(FrameworkElement.DataContextProperty);
```

Då kan `stackPanel` återgå till att ärva fönstrets `DataContext`.

Detta är inte samma sak som:

```csharp
stackPanel.DataContext = null;
```

Den senare raden sätter uttryckligen ett lokalt värde till `null`, vilket normalt bryter arvet.

### Viktigt att skilja på

- **Inget lokalt DataContext:** Elementet kan ärva från sin förälder.
- **DataContext satt till null:** Elementet har ett eget värde som är `null`.
- **ClearValue():** Tar bort det lokala värdet så att exempelvis arv kan börja gälla igen.

---

# DataContext kan också sättas i XAML

Vi behöver inte alltid sätta `DataContext` från C#.

Exempel:

```xml
<StackPanel>
    <StackPanel.DataContext>
        <local:Person FirstName="Fredrik"
                      LastName="Johansson" />
    </StackPanel.DataContext>

    <TextBlock Text="{Binding FirstName}" />
    <TextBlock Text="{Binding LastName}" />
</StackPanel>
```

Här skapar XAML ett `Person`-objekt och använder det som panelens `DataContext`.

Det fungerar för en klass som XAML kan skapa, exempelvis en klass med en tillgänglig parameterlös konstruktor.

En fördel är att objektet även kan användas av XAML-designern.

I större applikationer är det dock vanligt att skapa datakällan från C#.

---

# Vad händer när objektets properties ändras?

Vi har nu lärt oss hur WPF kan läsa värden från ett vanligt C#-objekt.

Men det finns ett problem.

Anta att vi har följande klass:

```csharp
class Person
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
```

Och en binding:

```xml
<Label Content="{Binding FirstName}" />
```

Om vi ändrar objektet direkt från C#:

```csharp
_person.FirstName = "Maria";
```

så får WPF normalt ingen information om att propertyn har ändrats.

Objektets värde har uppdaterats, men gränssnittet visar fortfarande det gamla värdet.

## Men varför fungerade det när vi skrev i en TextBox?

Om en `TextBox` har en `TwoWay`-binding kan WPF:s bindningsmotor själv skriva det nya värdet till källobjektet.

När flera bindings använder samma property kan WPF också hålla dessa synkroniserade vid sådana uppdateringar.

Det innebär att våra textrutor kan verka fungera som förväntat även utan `INotifyPropertyChanged`.

Problemet blir tydligare när vi ändrar objektet direkt från C#, exempelvis i en knapptryckning.

För att objektet själv ska kunna meddela WPF att en property ändrats använder vi `INotifyPropertyChanged`.

---

# INotifyPropertyChanged

`INotifyPropertyChanged` är ett interface från:

```csharp
using System.ComponentModel;
```

Det används för att objekt ska kunna meddela andra delar av programmet när deras properties ändras.

Interfacet innehåller ett event:

```csharp
public event PropertyChangedEventHandler? PropertyChanged;
```

WPF:s databindningssystem kan lyssna på detta event.

När vi ändrar en property skickar vi ett `PropertyChanged`-event med namnet på propertyn som har ändrats.

WPF kan då läsa det nya värdet och uppdatera berörda bindings.

## Exempel från dagens code-along

```csharp
using System.ComponentModel;
using System.Runtime.CompilerServices;

class Person : INotifyPropertyChanged
{
    public string FirstName
    {
        get => field;

        set
        {
            field = value;
            NotifyPropertyChanged(nameof(FirstName));
        }
    } = string.Empty;

    private string? _lastName;

    public string? LastName
    {
        get
        {
            return _lastName;
        }

        set
        {
            _lastName = value;

            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs("LastName"));
        }
    }

    private void NotifyPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
```

Här visar vi två olika sätt att implementera properties med ändringsnotifieringar.

---

# Backing fields och C# 14:s field

En vanlig automatisk property:

```csharp
public string FirstName { get; set; } = string.Empty;
```

har ett backing field som kompilatorn skapar åt oss.

Men om vi behöver köra egen kod när värdet ändras behöver vi en setter med egen logik.

## Traditionellt backing field

Tidigare skrev man vanligtvis:

```csharp
private string _firstName = string.Empty;

public string FirstName
{
    get => _firstName;

    set
    {
        _firstName = value;
        NotifyPropertyChanged(nameof(FirstName));
    }
}
```

Vi deklarerar själva ett fält där värdet lagras.

## C# 14: field

I C# 14 kan vi använda det nya kontextuella nyckelordet `field`:

```csharp
public string FirstName
{
    get => field;

    set
    {
        field = value;
        NotifyPropertyChanged(nameof(FirstName));
    }
} = string.Empty;
```

`field` ger oss tillgång till det automatiskt genererade backing field som hör till propertyn.

Vi slipper alltså deklarera ett separat `_firstName`.

**Observera:** Den här syntaxen kräver C# 14 eller senare.

---

# Skicka PropertyChanged-eventet

När en property ändras kan vi skicka ett event:

```csharp
PropertyChanged?.Invoke(
    this,
    new PropertyChangedEventArgs(nameof(FirstName)));
```

Här betyder:

- `PropertyChanged`: Eventet som WPF kan lyssna på.
- `?.`: Anropa bara eventet om någon prenumererar på det.
- `this`: Objektet som skickar eventet.
- `PropertyChangedEventArgs`: Information om vilken property som ändrats.
- `nameof(FirstName)`: Ger strängen `"FirstName"`.

## Varför nameof?

Vi kan skriva:

```csharp
new PropertyChangedEventArgs("FirstName")
```

Men det är bättre att skriva:

```csharp
new PropertyChangedEventArgs(nameof(FirstName))
```

Om vi senare byter namn på propertyn med Visual Studios refactoring-funktion uppdateras `nameof(FirstName)` automatiskt.

En hårdkodad sträng riskerar däremot att bli fel.

---

# Hjälpmetod för notifieringar

Eftersom vi behöver skicka samma typ av event från många properties kan vi skapa en hjälpfunktion:

```csharp
private void NotifyPropertyChanged(
    [CallerMemberName] string? propertyName = null)
{
    PropertyChanged?.Invoke(
        this,
        new PropertyChangedEventArgs(propertyName));
}
```

Då kan vi skriva:

```csharp
NotifyPropertyChanged(nameof(FirstName));
```

Men tack vare `[CallerMemberName]` kan vi förenkla det ytterligare:

```csharp
NotifyPropertyChanged();
```

Om anropet sker inifrån settern för `FirstName` fyller kompilatorn automatiskt i namnet `"FirstName"`.

Exempel:

```csharp
public string FirstName
{
    get => field;

    set
    {
        field = value;
        NotifyPropertyChanged();
    }
} = string.Empty;
```

`[CallerMemberName]` är ett attribut från:

```csharp
using System.Runtime.CompilerServices;
```

Det är kompilatorn som fyller i namnet, inte WPF.

---

# Undvik onödiga notifieringar

I våra första exempel skickar vi `PropertyChanged` varje gång någon tilldelar propertyn ett värde.

Men om det nya värdet är samma som det gamla behöver vi normalt inte skicka någon notifiering.

Vi kan därför kontrollera värdet först:

```csharp
public string FirstName
{
    get => field;

    set
    {
        if (field == value)
            return;

        field = value;
        NotifyPropertyChanged();
    }
} = string.Empty;
```

Nu skickas eventet bara om värdet faktiskt ändras.

Det här är ett vanligt mönster när man implementerar `INotifyPropertyChanged`.

---

# Ett viktigt krav när vi implementerar INotifyPropertyChanged

Om en klass implementerar `INotifyPropertyChanged` bör alla properties som ska meddela gränssnittet om ändringar också skicka notifieringar.

Exempel:

```csharp
class Person : INotifyPropertyChanged
{
    public string FirstName
    {
        get => field;
        set
        {
            field = value;
            NotifyPropertyChanged();
        }
    } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    // ...
}
```

Här skickar `FirstName` notifieringar, men inte `LastName`.

Det innebär att WPF inte får något `PropertyChanged`-event när `LastName` ändras.

Vi bör därför implementera notifieringar konsekvent för de properties som ska kunna uppdatera UI:t.

---

# Testa med en knapp

Från dagens code-along:

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    _person.FirstName = "Maria";
    _person.LastName = "Johansson";
}
```

När `Person` implementerar `INotifyPropertyChanged` och båda properties skickar notifieringar uppdateras gränssnittet automatiskt.

Vi behöver inte skriva:

```csharp
firstNameLabel.Content = _person.FirstName;
```

eller uppdatera varje kontroll manuellt.

WPF:s bindningssystem tar hand om det.

---

# Data Binding och INotifyPropertyChanged är inte samma sak

Det är viktigt att skilja mellan de olika mekanismerna.

| Mekanism | Ansvar |
|---|---|
| `Binding` | Kopplar en target-property till en datakälla |
| `ElementName` | Anger ett namngivet UI-element som källa |
| `DataContext` | Anger vilket objekt bindings normalt ska använda |
| `Mode=TwoWay` | Tillåter uppdateringar i båda riktningarna |
| `UpdateSourceTrigger` | Bestämmer när target skickar ändringar till source |
| `INotifyPropertyChanged` | Låter källobjektet meddela att en property har ändrats |

**Exempel:**

När användaren skriver i en `TextBox` kan en `TwoWay`-binding uppdatera objektets property.

När objektets property ändras från C# kan `INotifyPropertyChanged` meddela WPF att det finns ett nytt värde att visa.

---

# Binding-fel och felsökning

Om en binding inte fungerar kan vi undersöka Visual Studios **Output**-fönster.

Exempel:

```xml
<TextBox Text="{Binding FirstName}" />
```

Om `DataContext` är ett objekt som saknar `FirstName` kan WPF rapportera ett binding-fel.

Vanliga orsaker är:

- Felstavat propertynamn.
- Fel `DataContext`.
- Att kontrollen ärver ett annat `DataContext` än vi tänkt oss.
- Att källobjektet saknar den property som anges i `Path`.
- Att en property inte skickar `PropertyChanged` när den ändras.

Att `DataContext` tillfälligt är `null` behöver däremot inte innebära ett binding-fel.

Det är exempelvis normalt att vi sätter `DataContext` efter `InitializeComponent()`:

```csharp
public MainWindow()
{
    InitializeComponent();

    DataContext = new Person();
}
```

Bindingarna kan då börja använda objektet när `DataContext` har satts.

---

# Sammanfattning

Under dagens lektion har vi gått igenom:

1. **Data Binding** – koppla UI-properties till andra värden.
2. **Source, Target och Path** – förstå vad som binds till vad.
3. **ElementName** – använda andra kontroller som datakälla.
4. **Mode** – bestämma i vilken riktning ändringar kan skickas.
5. **UpdateSourceTrigger** – bestämma när ändringar skrivs tillbaka.
6. **StringFormat** – formatera värden direkt i en binding.
7. **DataContext** – använda vanliga C#-objekt som datakälla.
8. **DataContext-arv** – låta flera kontroller använda samma objekt.
9. **INotifyPropertyChanged** – meddela WPF när objektets properties ändras.
10. **CallerMemberName och field** – förenkla implementationen.

## Inför nästa lektion: MVVM

Vi har nu sett hur vi kan binda vårt gränssnitt till vanliga C#-objekt.

Men vi har fortfarande kod i vårt fönsters code-behind, exempelvis:

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    _person.FirstName = "Maria";
}
```

Nästa steg är att strukturera applikationen så att gränssnittet och programmets logik blir mer separerade.

Det är här **MVVM (Model–View–ViewModel)** kommer in.

Vi kommer bland annat att använda:

- En **ViewModel** som innehåller data och logik för gränssnittet.
- `INotifyPropertyChanged` för att hålla gränssnittet uppdaterat.
- `ICommand` för att koppla knappar till metoder utan vanliga `Click`-event handlers.
- En gemensam `ViewModelBase` för att slippa upprepa notifieringskod.
- En `RelayCommand` för att göra kommandon enklare att skapa.

De databindningar vi lärt oss idag är alltså grunden för det vi ska göra med MVVM.