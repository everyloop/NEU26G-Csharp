# Oktober 23

Under lektionen fortsatte vi med vanliga WPF-kontroller och gick sedan vidare till dialogrutor.

Vi arbetade i två projekt:

- **L043_Kontroller** – RadioButton, ComboBox och ListBox.
- **L044_Dialogrutor** – MessageBox, OpenFileDialog, SaveFileDialog och egna dialogfönster.



# 1. RadioButton

En `RadioButton` används när användaren ska välja **ett av flera alternativ**.

Till skillnad från en `CheckBox`, där flera alternativ kan vara markerade samtidigt, fungerar radioknappar som en grupp där **högst en knapp kan vara markerad**.

## Exempel

```xml
<StackPanel>
    <Label Content="Copy mode:" />

    <RadioButton
        x:Name="appendRadioButton"
        Content="Append"
        GroupName="CopyMode"
        IsChecked="True" />

    <RadioButton
        x:Name="overwriteRadioButton"
        Content="Overwrite"
        GroupName="CopyMode" />

    <RadioButton
        x:Name="disabledRadioButton"
        Content="Disabled"
        GroupName="CopyMode" />
</StackPanel>
```

### Viktiga properties

| Property | Beskrivning |
|---|---|
| `Content` | Texten eller innehållet som visas |
| `IsChecked` | Anger om knappen är markerad |
| `GroupName` | Anger vilken grupp knappen tillhör |
| `IsEnabled` | Anger om knappen kan användas |

`IsChecked` är av typen `bool?`, alltså en nullable bool.

Vi kontrollerar därför ofta värdet så här:

```csharp
if (appendRadioButton.IsChecked == true)
{
    // Append är valt
}
```

## GroupName

Radioknappar med samma `GroupName` tillhör samma grupp, även om de ligger i olika paneler inom samma fönster.

Vi hade två grupper:

```xml
<RadioButton Content="Append" GroupName="CopyMode" />
<RadioButton Content="Overwrite" GroupName="CopyMode" />

<RadioButton Content="Swedish" GroupName="Language" />
<RadioButton Content="English" GroupName="Language" />
```

Att välja ett språk påverkar inte valet av kopieringsläge.

Om man inte anger `GroupName` grupperas radioknappar normalt utifrån sin container.

### Kan alla vara avmarkerade?

Ja!

Om vi markerar en radioknapp avmarkeras automatiskt de andra i gruppen.

Men om vi programmatiskt sätter:

```csharp
appendRadioButton.IsChecked = false;
```

så väljs inte någon annan automatiskt.

En grupp kan alltså ha **noll eller en markerad radioknapp**.

## Använda RadioButton i programmet

Vi använde radioknapparna för att bestämma hur text skulle kopieras:

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    if (appendRadionButton.IsChecked == true)
    {
        destinationTextBox.Text += sourceTextBox.Text;
    }
    else if (overwriteRadioButton.IsChecked == true)
    {
        destinationTextBox.Text = sourceTextBox.Text;
    }
}
```

Skillnaden mellan alternativen:

- **Append** – lägger till text efter den befintliga texten med `+=`.
- **Overwrite** – ersätter den befintliga texten med `=`.
- **Disabled** – ingen av dessa kodgrenar körs.

Det sista alternativet gör alltså att knappen inte utför någon kopiering, även om själva knappen fortfarande är aktiverad. Om ni vill får ni gärna göra klart så att knappen även blir aktiv/inaktiv på ett korrekt sätt.

---

# 2. ComboBox

En `ComboBox` visar en lista med alternativ som användaren kan välja mellan.

Normalt visas bara det valda alternativet. Användaren kan öppna listan för att se resten.

Vi använde en `ComboBox` för att välja land.

```xml
<ComboBox
    x:Name="countriesComboBox"
    Margin="10"
    SelectionChanged="countriesComboBox_SelectionChanged" />
```

## ItemsSource

Med `ItemsSource` kan vi ange vilken samling objekt som kontrollen ska visa.

Först skapade vi en klass:

```csharp
class Country
{
    public Country(string name, string capital)
    {
        Name = name;
        Capital = capital;
    }

    public string Name { get; set; }
    public string Capital { get; set; }
}
```

Sedan skapade vi några objekt:

```csharp
var countries = new Country[]
{
    new Country("Sweden", "Stockholm"),
    new Country("Norway", "Oslo"),
    new Country("Denmark", "Copenhagen")
};
```

Och skickade samlingen till ComboBox:

```csharp
countriesComboBox.ItemsSource = countries;
```

Observera att `ItemsSource` inte bara kan innehålla strängar. Den kan även innehålla objekt av våra egna klasser.

## DisplayMemberPath

Hur vet ComboBox vad den ska visa när varje objekt är ett `Country`?

Vi anger vilken property som ska användas:

```csharp
countriesComboBox.DisplayMemberPath = "Name";
```

Då visas:

- Sweden
- Norway
- Denmark

Utan `DisplayMemberPath` skulle kontrollen normalt använda objektens `ToString()` för att skapa den text som visas.

## SelectedItem och SelectedIndex

Två viktiga properties:

| Property | Beskrivning |
|---|---|
| `SelectedItem` | Det valda objektet |
| `SelectedIndex` | Index för det valda objektet |
| `ItemsSource` | Samlingen med objekt |
| `DisplayMemberPath` | Vilken property som ska visas |

Vi valde det första landet direkt när programmet startade:

```csharp
countriesComboBox.SelectedIndex = 0;
```

Index börjar på 0, precis som i arrayer och listor.

Om inget objekt är valt är `SelectedIndex` normalt `-1`.

## SelectionChanged

Eventet `SelectionChanged` inträffar när valet ändras.

```csharp
private void countriesComboBox_SelectionChanged(
    object sender,
    SelectionChangedEventArgs e)
{
    capitalTextBox.Text =
        (countriesComboBox.SelectedItem as Country)?.Capital;
}
```

Här händer flera saker:

1. `SelectedItem` hämtar det valda objektet.
2. `as Country` försöker konvertera objektet till `Country`.
3. `?.Capital` hämtar huvudstaden om konverteringen lyckades.

Det är viktigt att förstå att `SelectedItem` är **själva Country-objektet**, inte bara texten som visas.

---

# 3. ListBox

En `ListBox` liknar en `ComboBox`, men alternativen visas direkt i en lista.

Vi använde samma samling länder i både ComboBox och ListBox.

```xml
<ListBox
    x:Name="listBox"
    SelectionMode="Single"
    Height="150"
    Margin="10" />
```

I konstruktorn:

```csharp
listBox.ItemsSource = countries;
listBox.DisplayMemberPath = "Capital";
```

ComboBox visade alltså ländernas namn medan ListBox visade huvudstäderna.

**Båda kontrollerna innehöll ändå samma Country-objekt.**

## SelectionMode

En `ListBox` har propertyn `SelectionMode`.

| Värde | Beskrivning |
|---|---|
| `Single` | Ett objekt kan vara valt |
| `Multiple` | Flera objekt kan väljas utan Ctrl |
| `Extended` | Flera objekt kan väljas med exempelvis Ctrl och Shift |

När flera objekt kan vara valda använder vi `SelectedItems` för att komma åt samtliga val.

## Synkronisera ComboBox och ListBox

I vårt exempel markerade vi automatiskt samma land i ListBox som i ComboBox:

```csharp
private void countriesComboBox_SelectionChanged(
    object sender,
    SelectionChangedEventArgs e)
{
    capitalTextBox.Text =
        (countriesComboBox.SelectedItem as Country)?.Capital;

    listBox.SelectedItem = countriesComboBox.SelectedItem;
}
```

Det fungerar eftersom båda kontrollerna använder **samma objektinstanser** från arrayen.

`DisplayMemberPath` påverkar bara vad som visas, inte vilket objekt som är valt.

### Skillnaden mellan ComboBox och ListBox

| ComboBox | ListBox |
|---|---|
| Tar lite plats | Visar flera alternativ samtidigt |
| Listan öppnas vid behov | Listan är synlig direkt |
| Används ofta för ett val | Kan även användas för flera val |

---

# 4. Dialogrutor

I projektet **L044_Dialogrutor** arbetade vi med dialogrutor.

En dialogruta är ett fönster som används för att exempelvis:

- Visa information.
- Ställa en fråga.
- Låta användaren välja en fil.
- Samla in uppgifter.

Vi började med färdiga dialogrutor och skapade sedan en egen.

---

# 5. MessageBox

`MessageBox` är en färdig dialogruta för att visa ett meddelande.

Den enklaste varianten:

```csharp
MessageBox.Show("This is a message!");
```

Vi kan också ange en titel:

```csharp
MessageBox.Show(
    "This is a message!",
    "Message to user:");
```

## Knappar och ikoner

Vi kan ange vilka knappar och vilken ikon som ska visas:

```csharp
var result = MessageBox.Show(
    "Add another exclamation mark?",
    "Are you sure?",
    MessageBoxButton.YesNo,
    MessageBoxImage.Warning);
```

### MessageBoxButton

Några vanliga alternativ:

- `OK`
- `OKCancel`
- `YesNo`
- `YesNoCancel`

### MessageBoxImage

Några vanliga ikoner:

- `Information`
- `Warning`
- `Error`
- `Question`

## Läsa användarens svar

`MessageBox.Show()` returnerar ett värde av typen `MessageBoxResult`.

```csharp
var result = MessageBox.Show(
    "Add another exclamation mark?",
    "Are you sure?",
    MessageBoxButton.YesNo,
    MessageBoxImage.Warning);

if (result == MessageBoxResult.Yes)
{
    YesNoButton.Content += "!";
}
```

Om användaren klickar på **Yes** lägger vi till ett utropstecken på knappens text.

---

# 6. OpenFileDialog

`OpenFileDialog` används när användaren ska välja en befintlig fil.

Klassen finns i namespace:

```csharp
using Microsoft.Win32;
```

Vi skapade dialogen:

```csharp
var dialog = new OpenFileDialog();
```

Sedan ställde vi in olika properties:

```csharp
dialog.FileName = "Test.txt";

dialog.InitialDirectory =
    Environment.GetFolderPath(
        Environment.SpecialFolder.Desktop);

dialog.ShowHiddenItems = true;

dialog.Filter = "Office Files|*.doc;*.xls;*.ppt|All Files|*.*";

dialog.Title = "Select file to open:";
```

## Viktiga properties

| Property | Beskrivning |
|---|---|
| `FileName` | Föreslaget filnamn eller den valda filens sökväg |
| `InitialDirectory` | Föreslagen startmapp |
| `Title` | Dialogrutans titel |
| `Filter` | Vilka filtyper som visas som val |
| `ShowHiddenItems` | Om dolda filer ska visas |
| `Multiselect` | Om flera filer får väljas |

## Filter

Ett filter består av en beskrivning och ett filmönster, separerade med `|`.

```csharp
dialog.Filter = "Text Files|*.txt|All Files|*.*";
```

Här får användaren två alternativ:

- Text Files – visar `.txt`-filer.
- All Files – visar alla filtyper.

Flera filändelser kan anges med semikolon:

```csharp
"Office Files|*.doc;*.xls;*.ppt"
```

Filtret styr vilka filer som visas som val. Det är inte någon säkerhetskontroll av filens innehåll.

## ShowDialog()

Vi visar dialogrutan med:

```csharp
if (dialog.ShowDialog() == true)
{
    openFileButton.Content = dialog.FileName;
}
```

`ShowDialog()` returnerar `bool?`:

- `true` – användaren bekräftade sitt val.
- `false` – användaren avbröt.
- `null` – möjligt i det generella nullable-resultatet.

För dessa fildialoger använder vi normalt:

```csharp
if (dialog.ShowDialog() == true)
```

Efter ett godkänt val kan vi läsa sökvägen via `FileName`.

**Viktigt:** `OpenFileDialog` öppnar inte filens innehåll åt oss. Den låter bara användaren välja fil.

Om vi vill läsa innehållet kan vi exempelvis använda:

```csharp
string text = File.ReadAllText(dialog.FileName);
```

---

# 7. SaveFileDialog

`SaveFileDialog` används när användaren ska välja var en fil ska sparas.

Vi använde:

```csharp
var dialog = new SaveFileDialog();

dialog.FileName = "Test.txt";

dialog.InitialDirectory =
    Environment.GetFolderPath(
        Environment.SpecialFolder.Desktop);

dialog.ShowHiddenItems = true;
dialog.OverwritePrompt = true;
dialog.Filter = "Office Files|*.doc;*.xls;*.ppt|All Files|*.*";
dialog.Title = "Save title example!";
```

## OverwritePrompt

```csharp
dialog.OverwritePrompt = true;
```

Det innebär att användaren normalt får en varning om en befintlig fil håller på att skrivas över.

## Visa dialogen

```csharp
if (dialog.ShowDialog() == true)
{
    saveFileButton.Content = dialog.FileName;
}
```

I vårt exempel visade vi bara den valda sökvägen på knappen.

**SaveFileDialog sparar inte filen automatiskt.**

För att faktiskt skriva en textfil kan vi använda:

```csharp
if (dialog.ShowDialog() == true)
{
    File.WriteAllText(dialog.FileName, "Hello world!");
}
```

Skillnaden är alltså:

- `OpenFileDialog` – välj vilken fil som ska öppnas.
- `SaveFileDialog` – välj var och under vilket namn filen ska sparas.

Själva läsningen och skrivningen gör vi separat.

---

# 8. Skapa en egen dialogruta

Vi skapade ett nytt WPF-fönster med namnet **AddCountry**.

Dialogen innehöll:

- En `TextBox` för landets namn.
- En `TextBox` för huvudstaden.
- En Cancel-knapp.
- En OK-knapp.

Vi ville kunna öppna dialogen från huvudfönstret och sedan lägga till ett nytt land i vår lista.

## AddCountry.xaml

En förenklad version av innehållet:

```xml
<StackPanel>
    <Label Content="Country:" />
    <TextBox x:Name="countryTextBox" Margin="10 0" />

    <Label Content="Capital:" />
    <TextBox x:Name="capitalTextBox" Margin="10 0" />

    <StackPanel Orientation="Horizontal"
                HorizontalAlignment="Center">

        <Button Content="Cancel"
                Click="CancelButton_Click" />

        <Button Content="OK"
                Click="OkButton_Click" />
    </StackPanel>
</StackPanel>
```

Vi angav också:

```xml
ResizeMode="NoResize"
```

Det hindrar användaren från att ändra dialogfönstrets storlek.

---

# 9. Skicka data från dialogen

Vi ville att huvudfönstret skulle kunna läsa vad användaren skrivit i dialogen.

Därför skapade vi två publika properties i `AddCountry`:

```csharp
public string Country
{
    get { return countryTextBox.Text; }
    set { countryTextBox.Text = value; }
}

public string Capital
{
    get { return capitalTextBox.Text; }
    set { capitalTextBox.Text = value; }
}
```

Dessa properties fungerar som ett gränssnitt mot dialogens TextBox-kontroller.

Huvudfönstret behöver alltså inte komma åt kontrollerna direkt.

Vi kan läsa värden:

```csharp
string name = dialog.Country;
string capital = dialog.Capital;
```

Men vi kan också ange startvärden innan dialogen öppnas:

```csharp
dialog.Country = "Denmark";
dialog.Capital = "Copenhagen";
```

Då fylls textrutorna med dessa värden.

Det är ett enkelt exempel på **inkapsling**: dialogen ansvarar själv för sina kontroller, medan andra klasser använder dess publika properties.

---

# 10. DialogResult

När användaren klickar på OK vill vi tala om för huvudfönstret att uppgifterna ska användas.

I `AddCountry.xaml.cs`:

```csharp
private void OkButton_Click(
    object sender,
    RoutedEventArgs e)
{
    DialogResult = true;
    Close();
}
```

För Cancel:

```csharp
private void CancelButton_Click(
    object sender,
    RoutedEventArgs e)
{
    DialogResult = false;
    Close();
}
```

`DialogResult` är en property på `Window` av typen `bool?`.

När ett fönster visas med `ShowDialog()` stängs det automatiskt när `DialogResult` sätts till `true` eller `false`.

Därför är de explicita anropen till `Close()` egentligen överflödiga i just dessa handlers.

Vi hade kunnat skriva:

```csharp
private void OkButton_Click(
    object sender,
    RoutedEventArgs e)
{
    DialogResult = true;
}
```

---

# 11. Öppna den egna dialogen

I huvudfönstret skapade vi en instans av `AddCountry`:

```csharp
var dialog = new AddCountry();
```

Sedan angav vi ägarfönster och placering:

```csharp
dialog.Owner = this;

dialog.WindowStartupLocation =
    WindowStartupLocation.CenterOwner;
```

Och visade dialogen:

```csharp
if (dialog.ShowDialog() == true)
{
    _countries.Add(
        new Country(dialog.Country, dialog.Capital));
}
```

Här händer följande:

1. Vi skapar ett nytt dialogfönster.
2. Vi anger att huvudfönstret är ägare.
3. Vi centrerar dialogen över huvudfönstret.
4. Vi visar dialogen och väntar på att den stängs.
5. Om användaren klickade OK läser vi dialogens properties.
6. Vi skapar ett nytt `Country`-objekt och lägger till det i samlingen.

Det är viktigt att skilja på **att visa ett fönster** och **att läsa resultatet från det**.

---

# 12. Owner och WindowStartupLocation

## Owner

```csharp
dialog.Owner = this;
```

`Owner` anger vilket fönster som äger dialogen.

Det innebär bland annat att:

- Dialogen hålls framför sitt ägarfönster.
- Dialogen följer ägarfönstrets minimering.
- Dialogen stängs om ägarfönstret stängs.

Det är vanligt att ange `Owner` när man öppnar ett nytt fönster från huvudfönstret.

**Owner gör inte automatiskt ett fönster modalt.**

## WindowStartupLocation

Vi kan välja hur fönstret ska placeras:

| Värde | Beskrivning |
|---|---|
| `Manual` | Placeringen styrs manuellt eller av systemet |
| `CenterScreen` | Centreras på skärmen |
| `CenterOwner` | Centreras över ägarfönstret |

Exempel:

```csharp
dialog.WindowStartupLocation =
    WindowStartupLocation.CenterOwner;
```

För `CenterOwner` bör vi också ange `Owner`.

---

# 13. Show() och ShowDialog()

Ett WPF-fönster kan visas på två vanliga sätt.

## Show()

```csharp
var window = new AddCountry();
window.Show();
```

Det öppnar ett **modellöst fönster** (*modeless window*).

Användaren kan fortsätta arbeta i huvudfönstret medan det nya fönstret är öppet.

Metoden returnerar direkt, utan att vänta på att fönstret stängs.

## ShowDialog()

```csharp
var dialog = new AddCountry();

if (dialog.ShowDialog() == true)
{
    // Användaren klickade OK
}
```

Det öppnar ett **modalt fönster** (*modal window*).

Användaren måste stänga dialogen innan hen kan fortsätta arbeta i de andra fönster som dialogen blockerar.

Den anropande koden fortsätter först när dialogen har stängts.

## Jämförelse

| `Show()` | `ShowDialog()` |
|---|---|
| Modellöst | Modalt |
| Returnerar direkt | Väntar tills dialogen stängs |
| Returnerar `void` | Returnerar `bool?` |
| Huvudfönstret kan användas | Huvudfönstret blockeras för användaren |
| Passar exempelvis verktygsfönster | Passar exempelvis formulär som kräver bekräftelse |

`DialogResult` används tillsammans med `ShowDialog()`, inte med vanliga modellösa fönster.

---

# 14. ObservableCollection

I huvudfönstret skapade vi en samling länder:

```csharp
private ObservableCollection<Country> _countries =
    new ObservableCollection<Country>();
```

För att använda klassen behövs:

```csharp
using System.Collections.ObjectModel;
```

Sedan kopplade vi samlingen till ListBox:

```csharp
countryListBox.ItemsSource = _countries;
countryListBox.DisplayMemberPath = "Name";
```

När användaren klickar OK i dialogen:

```csharp
_countries.Add(
    new Country(dialog.Country, dialog.Capital));
```

ListBox uppdateras automatiskt och visar det nya landet.

## Varför ObservableCollection?

En vanlig `List<Country>` fungerar också som `ItemsSource`, men om vi lägger till ett objekt i listan efteråt får WPF inte automatiskt information om ändringen.

`ObservableCollection<T>` skickar meddelanden när samlingen ändras, exempelvis när objekt läggs till eller tas bort.

Därför kan WPF uppdatera ListBox automatiskt.

Observera att `ObservableCollection<T>` meddelar om ändringar i **samlingen**. Om en property som `Country.Name` ändras krävs ytterligare mekanismer, exempelvis `INotifyPropertyChanged`, för att gränssnittet automatiskt ska uppdatera det visade värdet.

Det återkommer vi till när vi arbetar mer med databindning.


# Sammanfattning

Under lektionen lärde vi oss:

- **RadioButton** – välja ett alternativ i en grupp.
- **GroupName** – skapa separata grupper av radioknappar.
- **ComboBox** – visa en nedfällbar lista med alternativ.
- **ListBox** – visa en lista där användaren kan välja objekt.
- **ItemsSource** – ange vilken samling som ska visas.
- **DisplayMemberPath** – välja vilken property från objekten som ska visas.
- **SelectedItem** – hämta det valda objektet.
- **MessageBox** – visa meddelanden och frågor.
- **OpenFileDialog** – välja en fil att öppna.
- **SaveFileDialog** – välja var en fil ska sparas.
- **Egna dialogfönster** – skapa ett nytt Window med egna kontroller.
- **DialogResult** – meddela om användaren bekräftade eller avbröt.
- **Owner** – ange relationen mellan två fönster.
- **WindowStartupLocation** – styra var ett fönster visas.
- **Show()** – visa ett modellöst fönster.
- **ShowDialog()** – visa ett modalt fönster och invänta ett resultat.
- **ObservableCollection** – låta gränssnittet reagera när en samling ändras.
