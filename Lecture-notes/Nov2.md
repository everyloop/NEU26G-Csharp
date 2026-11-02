# November 2

I dag har vi kollat på:

- [**Resources**](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Nov2.md#1-resources) – återanvändbara objekt i XAML.
- [**Styles**](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Nov2.md#2-styles) – gemensamma egenskaper för flera kontroller.
- [**ResourceDictionary**](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Nov2.md#3-resourcedictionary) – ett sätt att samla resurser i separata filer.
- [**DataTemplates**](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Nov2.md#6-datatemplates) – bestämmer hur objekt ska presenteras i gränssnittet.
- [**Öppna fönster i MVVM**](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Nov2.md#11-%C3%B6ppna-nya-f%C3%B6nster-i-mvvm) -utan att viewmodel behöver känna till specifika WPF-fönster
- [**Converters**](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Nov2.md#14-converters-i-wpf) - för att kunna binda två properties som inte har samma datatyp.

**Code-alongs:**  
[L048_Styles_&_Templates](https://github.com/everyloop/NEU26G-Csharp/tree/master/Code-alongs/L048_Styles_%26_Templates)  
[L049_Windows_in_MVVM](https://github.com/everyloop/NEU26G-Csharp/tree/master/Code-alongs/L049_Windows_in_MVVM)


## 1. Resources

En **Resource** är ett objekt som vi ger ett namn (en nyckel) och sedan kan återanvända på olika ställen i XAML.

Resurser kan exempelvis vara färger, Styles, DataTemplates eller vanliga C#-objekt.

### Skapa en resurs

Vi börjar med att skapa en färgresurs i `Window.Resources`:

```xml
<Window.Resources>
    <SolidColorBrush Color="Red" x:Key="MyColor"/>
</Window.Resources>
```

`SolidColorBrush` är ett objekt som beskriver en pensel med en enda färg.

`x:Key="MyColor"` är resursens nyckel. Det är den vi använder för att hitta resursen senare.

Vi kan använda färgen på en knapp:

```xml
<Button Content="Button A"
        Background="{StaticResource MyColor}"/>
```

`StaticResource` letar upp resursen med nyckeln `MyColor` och använder den som värde för knappens `Background`.

Fördelen är att flera kontroller kan använda samma resurs. Vill vi byta färg behöver vi bara ändra den på ett ställe.

### Var kan resurser placeras?

Resurser kan definieras på olika nivåer:

- `Grid.Resources` – tillgängliga inom gridden och dess underliggande element.
- `Window.Resources` – tillgängliga inom fönstret.
- `Application.Resources` – tillgängliga i hela applikationen, exempelvis via `App.xaml`.

Exempel:

```xml
<Window.Resources>
    <SolidColorBrush Color="Red" x:Key="MyColor"/>
</Window.Resources>

<Grid>
    <Grid.Resources>
        <SolidColorBrush Color="LightBlue" x:Key="MyColor"/>
    </Grid.Resources>

    <Button Background="{StaticResource MyColor}"
            Content="Button A"/>
</Grid>
```

Här finns två resurser med samma nyckel.

WPF söker efter resurser med början nära kontrollen och fortsätter sedan uppåt i elementhierarkin.

Därför kommer knappen att använda den ljusblå färgen från `Grid.Resources`.

Om vi tar bort den resursen används i stället den röda färgen från `Window.Resources`.

Det här kallas **resource lookup**.

### StaticResource och DynamicResource

Vi använder oftast `StaticResource`:

```xml
Background="{StaticResource MyColor}"
```

Resursen slås upp när XAML laddas.

Det finns också `DynamicResource`:

```xml
Background="{DynamicResource MyColor}"
```

Den behåller en dynamisk koppling till resursnyckeln och kan uppdateras om resursen ersätts under körning.

`DynamicResource` kan exempelvis vara användbart när en applikation ska kunna byta tema.

För vanliga resurser som inte behöver bytas under körning räcker `StaticResource`.

## 2. Styles

Om vi har flera knappar som ska se likadana ut kan vi skriva samma egenskaper på varje knapp:

```xml
<Button Content="Button A"
        Background="LightBlue"
        BorderBrush="Blue"
        BorderThickness="3"
        Margin="10"/>

<Button Content="Button B"
        Background="LightBlue"
        BorderBrush="Blue"
        BorderThickness="3"
        Margin="10"/>
```

Det fungerar, men leder till mycket upprepad kod.

Om vi vill ändra utseendet på alla knappar måste vi ändra varje knapp separat.

Det kan vi lösa med en **Style**.

### Skapa en Style

```xml
<Grid.Resources>
    <Style TargetType="Button">
        <Setter Property="Background" Value="LightBlue"/>
        <Setter Property="BorderBrush" Value="Blue"/>
        <Setter Property="BorderThickness" Value="3"/>
        <Setter Property="Margin" Value="10"/>
    </Style>
</Grid.Resources>
```

En `Style` innehåller en samling `Setter`-element.

Varje `Setter` anger vilken egenskap som ska ändras och vilket värde den ska få.

`TargetType="Button"` anger vilken typ av kontroll stilen är avsedd för.

Nu kan vi skriva:

```xml
<Button Content="Button A"/>
<Button Content="Button B"/>
<Button Content="Button C"/>
```

Alla tre knapparna får automatiskt samma utseende.

### Implicit Style

Eftersom vår Style har `TargetType="Button"` men saknar `x:Key`, är den en **implicit Style**.

Det innebär att den automatiskt används av knappar inom resursens räckvidd.

En implicit Style för `Button` får i praktiken en nyckel som motsvarar:

```xml
{x:Type Button}
```

Vi behöver alltså inte skriva `Style="..."` på varje knapp.

### Explicit Style

Vi kan också ge en Style en egen nyckel:

```xml
<Style x:Key="DangerButton" TargetType="Button">
    <Setter Property="Background" Value="Tomato"/>
    <Setter Property="BorderBrush" Value="Red"/>
</Style>
```

Då används den bara av kontroller som uttryckligen refererar till den:

```xml
<Button Content="Delete"
        Style="{StaticResource DangerButton}"/>
```

Det här är en **explicit Style**.

Skillnaden är alltså:

- **Implicit Style:** används automatiskt för en viss kontrolltyp.
- **Explicit Style:** används när vi refererar till den med dess nyckel.

### Ärva från en annan Style med BasedOn

Vi kanske vill att vår `DangerButton` ska ha samma marginaler och kanttjocklek som övriga knappar, men andra färger.

Då kan vi använda `BasedOn`:

```xml
<Style TargetType="Button">
    <Setter Property="Background" Value="LightBlue"/>
    <Setter Property="BorderBrush" Value="Blue"/>
    <Setter Property="BorderThickness" Value="3"/>
    <Setter Property="Margin" Value="10"/>
</Style>

<Style x:Key="DangerButton"
       TargetType="Button"
       BasedOn="{StaticResource {x:Type Button}}">
    <Setter Property="Background" Value="Tomato"/>
    <Setter Property="BorderBrush" Value="Red"/>
</Style>
```

`BasedOn` innebär att den nya stilen utgår från en annan Style.

Här refererar vi till den implicita stilen för `Button` med:

```xml
{StaticResource {x:Type Button}}
```

`DangerButton` ärver då bland annat `Margin` och `BorderThickness`, men skriver över `Background` och `BorderBrush`.

Vi slipper upprepa alla egenskaper.

### Lokala värden kan skriva över en Style

Även om en knapp använder en Style kan vi ange egna egenskaper direkt på knappen:

```xml
<Button Content="Button D"
        Background="LightGreen"/>
```

Knappen får fortfarande exempelvis `Margin` och `BorderThickness` från sin Style, men bakgrundsfärgen blir ljusgrön.

Det beror på att ett lokalt värde normalt har högre prioritet än en vanlig `Setter` i en Style.

Det här är en del av WPF:s system för **Dependency Properties**, där värden kan komma från flera olika källor.

## 3. ResourceDictionary

När en applikation växer kan det bli opraktiskt att ha alla Styles direkt i varje fönster.

Då kan vi flytta resurserna till en separat XAML-fil.

### Skapa Styles.xaml

Lägg till en ny **Resource Dictionary** i projektet och kalla den `Styles.xaml`.

```xml
<ResourceDictionary
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <Style TargetType="Button">
        <Setter Property="Background" Value="LightBlue"/>
        <Setter Property="BorderBrush" Value="Blue"/>
        <Setter Property="BorderThickness" Value="3"/>
        <Setter Property="Margin" Value="10"/>
    </Style>

    <Style x:Key="DangerButton"
           TargetType="Button"
           BasedOn="{StaticResource {x:Type Button}}">
        <Setter Property="Background" Value="Tomato"/>
        <Setter Property="BorderBrush" Value="Red"/>
    </Style>

</ResourceDictionary>
```

En `ResourceDictionary` är helt enkelt en samling resurser.

### Koppla in filen i App.xaml

För att göra stilarna tillgängliga i hela applikationen kan vi koppla in filen i `App.xaml`:

```xml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="Styles.xaml"/>
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

`MergedDictionaries` låter oss slå ihop resurser från flera olika filer.

Vi kan exempelvis lägga till fler filer senare:

```xml
<ResourceDictionary.MergedDictionaries>
    <ResourceDictionary Source="Styles.xaml"/>
    <ResourceDictionary Source="MoreStyles.xaml"/>
</ResourceDictionary.MergedDictionaries>
```

Nu kan alla fönster i applikationen använda våra Styles.

En fördel är att vi kan samla gemensamt utseende på ett ställe och hålla XAML-filerna för våra vyer enklare.

## 4. Vanliga C#-objekt som Resources

Resources behöver inte vara färger eller Styles.

Vi kan även skapa vanliga C#-objekt direkt i XAML.

Vi börjar med en klass:

```csharp
public class Country
{
    public string Name { get; set; } = "";
    public string Capital { get; set; } = "";
}
```

För att använda klassen i XAML behöver vi ett namespace som pekar på vårt projekt:

```xml
xmlns:local="clr-namespace:L048_Styles___Templates"
```

Nu kan vi skapa ett objekt i `Grid.Resources`:

```xml
<local:Country
    x:Key="Country"
    Name="Sweden"
    Capital="Stockholm"/>
```

Det motsvarar ungefär att skapa ett objekt i C#:

```csharp
var country = new Country
{
    Name = "Sweden",
    Capital = "Stockholm"
};
```

### Använd objektet som resurs

Vi kan exempelvis sätta objektet som innehåll på en knapp:

```xml
<Button Content="{StaticResource Country}"/>
```

Eftersom `Country` inte har någon särskild visuell presentation visas objektets standardrepresentation, normalt klassens fullständiga typnamn.

Men vi kan också binda till en specifik property:

```xml
<Button
    Content="{Binding Capital, Source={StaticResource Country}}"/>
```

Här används databinding, men i stället för att hämta objektet från `DataContext` anger vi en explicit källa:

```xml
Source={StaticResource Country}
```

Då blir knappens innehåll `Stockholm`.

## 5. Samlingar som Resources

Vi kan även skapa en array direkt i XAML med `x:Array`:

```xml
<x:Array x:Key="Countries"
         Type="{x:Type local:Country}">
    <local:Country Name="Sweden" Capital="Stockholm"/>
    <local:Country Name="Norway" Capital="Oslo"/>
    <local:Country Name="Denmark" Capital="Copenhagen"/>
</x:Array>
```

`Type` anger vilken typ av objekt arrayen innehåller.

Här skapar vi tre `Country`-objekt.

Sedan kan vi använda arrayen som `ItemsSource` för en `ListBox`:

```xml
<ListBox ItemsSource="{StaticResource Countries}"/>
```

ListBoxen får nu tre objekt att visa.

Men hur bestämmer vi **hur varje objekt ska se ut**?

Det är här `DataTemplate` kommer in.

## 6. DataTemplates

En **DataTemplate** beskriver hur ett dataobjekt ska presenteras visuellt.

Vi har en lista med `Country`-objekt, men vill inte bara visa deras standardrepresentation.

Vi vill visa landets namn och huvudstad bredvid varandra.

### Skapa en DataTemplate

```xml
<DataTemplate x:Key="ListTemplate">
    <StackPanel Orientation="Horizontal">
        <CheckBox Margin="0,0,5,0"/>
        <TextBlock FontWeight="Bold"
                   Text="{Binding Name}"/>
        <TextBlock Text=" - "/>
        <TextBlock Text="{Binding Capital}"/>
    </StackPanel>
</DataTemplate>
```

En DataTemplate kan innehålla flera olika kontroller.

Här använder vi:

- `StackPanel` för att placera innehållet horisontellt.
- `CheckBox` för att visa en kryssruta.
- `TextBlock` för att visa landets namn i fet stil.
- `TextBlock` för att visa en avskiljare.
- `TextBlock` för att visa huvudstaden.

### Använd DataTemplate i en ListBox

```xml
<ListBox
    ItemsSource="{StaticResource Countries}"
    ItemTemplate="{StaticResource ListTemplate}"/>
```

`ItemsSource` anger **vilka objekt** listan innehåller.

`ItemTemplate` anger **hur varje objekt ska visas**.

Resultatet blir ungefär:

```text
☐ Sweden - Stockholm
☐ Norway - Oslo
☐ Denmark - Copenhagen
```

### DataContext inuti en DataTemplate

En viktig detalj är hur databinding fungerar inuti en `DataTemplate`.

```xml
<TextBlock Text="{Binding Name}"/>
```

Vi har inte angett någon `Source`, och vi har inte manuellt satt `DataContext` för varje TextBlock.

Det fungerar ändå.

När en `ListBox` använder en `DataTemplate` får varje instans av mallen sitt aktuella dataobjekt som `DataContext`.

För första raden är `DataContext` alltså objektet:

```csharp
new Country
{
    Name = "Sweden",
    Capital = "Stockholm"
};
```

För andra raden är det objektet för Norge, och så vidare.

Därför fungerar samma bindings för alla objekt i listan.

Det är en av de viktigaste idéerna bakom DataTemplates.

## 7. DataTemplate direkt i kontrollen

Vi behöver inte alltid lägga vår DataTemplate i en ResourceDictionary.

Vi kan också definiera den direkt inuti en kontroll:

```xml
<ListBox ItemsSource="{StaticResource Countries}">
    <ListBox.ItemTemplate>
        <DataTemplate>
            <StackPanel Orientation="Horizontal">
                <CheckBox Margin="0,0,5,0"/>
                <TextBlock FontWeight="Bold"
                           Text="{Binding Name}"/>
                <TextBlock Text=" - "/>
                <TextBlock Text="{Binding Capital}"/>
            </StackPanel>
        </DataTemplate>
    </ListBox.ItemTemplate>
</ListBox>
```

Det här ger samma resultat som när vi använder en namngiven DataTemplate.

Skillnaden är var mallen definieras.

Om den bara används på ett enda ställe kan det vara rimligt att lägga den direkt i kontrollen.

Om den ska återanvändas på flera ställen är det ofta bättre att lägga den som en resurs.

## 8. Implicit DataTemplate

Precis som med Styles kan vi skapa DataTemplates som väljs automatiskt.

I stället för att använda `x:Key` kan vi ange `DataType`:

```xml
<DataTemplate DataType="{x:Type local:Country}">
    <StackPanel Orientation="Horizontal">
        <TextBlock FontWeight="Bold"
                   Text="{Binding Name}"/>
        <TextBlock Text=" - "/>
        <TextBlock Text="{Binding Capital}"/>
    </StackPanel>
</DataTemplate>
```

Den här mallen gäller för objekt av typen `Country` inom resursens räckvidd.

Vi kan då skriva:

```xml
<ListBox ItemsSource="{StaticResource Countries}"/>
```

utan att ange `ItemTemplate`.

WPF hittar automatiskt en lämplig DataTemplate utifrån objektens typ.

Det här är särskilt användbart när samma typ av objekt visas på flera ställen i applikationen.

## 9. Skillnaden mellan Style och DataTemplate

Både Styles och DataTemplates används för att styra utseendet, men de har olika uppgifter.

### Style

En Style anger egenskaper för en kontroll.

Exempel:

```xml
<Style TargetType="Button">
    <Setter Property="Background" Value="LightBlue"/>
    <Setter Property="Margin" Value="10"/>
</Style>
```

Vi bestämmer hur knappar ska se ut.

### DataTemplate

En DataTemplate bestämmer hur ett dataobjekt ska presenteras.

Exempel:

```xml
<DataTemplate DataType="{x:Type local:Country}">
    <StackPanel Orientation="Horizontal">
        <TextBlock Text="{Binding Name}"/>
        <TextBlock Text="{Binding Capital}"/>
    </StackPanel>
</DataTemplate>
```

Vi bestämmer hur ett `Country`-objekt ska visas.

En enkel tumregel:

- **Style:** Hur ska en kontroll se ut?
- **DataTemplate:** Hur ska ett dataobjekt visas?

Det finns även `ControlTemplate`, som används för att ändra själva den visuella uppbyggnaden av en kontroll, exempelvis om vi vill ge en knapp rundade hörn. Det går vi inte in på i detalj här.

## 10. Hur hänger detta ihop med MVVM?

I våra exempel skapade vi objekt och arrayer direkt i XAML för att fokusera på Resources och DataTemplates.

I en riktig MVVM-applikation kommer objekten oftast från en ViewModel.

Exempel:

```csharp
public ObservableCollection<Country> Countries { get; } = new()
{
    new Country { Name = "Sweden", Capital = "Stockholm" },
    new Country { Name = "Norway", Capital = "Oslo" },
    new Country { Name = "Denmark", Capital = "Copenhagen" }
};
```

Då kan vi använda en vanlig binding:

```xml
<ListBox ItemsSource="{Binding Countries}"
         ItemTemplate="{StaticResource ListTemplate}"/>
```

Vår `DataTemplate` behöver inte ändras alls.

Den bryr sig inte om varifrån objekten kommer, utan bara om vilka properties de har.

En `ObservableCollection<T>` har dessutom fördelen att listkontrollen kan uppdateras automatiskt när objekt läggs till eller tas bort.

En vanlig array från `x:Array` har inte den funktionen.


## 11. Öppna nya fönster i MVVM

I WPF kan vi öppna nya fönster med `Show()` eller `ShowDialog()`.

- `Show()` öppnar ett vanligt fönster som användaren kan växla mellan.
- `ShowDialog()` öppnar ett **modalt dialogfönster**. Användaren måste stänga dialogen innan hen kan fortsätta använda dess ägarfönster.

I vårt exempel ville vi öppna ett `SettingsDialog` när användaren klickade på en knapp i `MainWindow`.

### Hur gör vi detta med MVVM?

Vi vill helst inte att vår ViewModel ska behöva känna till specifika WPF-fönster eller själv anropa `ShowDialog()`.

Därför kombinerade vi **Commands och Events**:

1. Knappen i `MainWindow` är bunden till `OpenSettingsCommand`.
2. När kommandot körs skickar ViewModel ett event, `OpenSettings`.
3. `MainWindow` prenumererar på eventet.
4. När eventet skickas skapar och öppnar `MainWindow` ett `SettingsDialog`.

I ViewModel:

```csharp
public event EventHandler? OpenSettings;

public RelayCommand OpenSettingsCommand { get; }

private void RaiseOpenSettings()
{
    OpenSettings?.Invoke(this, EventArgs.Empty);
}
```

Kommandot kopplas till `RaiseOpenSettings()` i ViewModels konstruktor.

I `MainWindow` prenumererar vi på eventet:

```csharp
_viewModel.OpenSettings += _viewModel_OpenSettings;
```

Och öppnar dialogen i eventhanteraren:

```csharp
var dialog = new SettingsDialog
{
    DataContext = _viewModel,
    Owner = this,
    WindowStartupLocation = WindowStartupLocation.CenterOwner
};

dialog.ShowDialog();
```

Några viktiga detaljer:

- **DataContext:** Dialogen använder samma ViewModel som huvudfönstret.
- **Owner:** Anger att dialogen tillhör huvudfönstret.
- **CenterOwner:** Placerar dialogen i mitten av huvudfönstret.
- **ShowDialog():** Öppnar dialogen modalt och väntar på att den stängs.

Det här är ett exempel på att **code-behind inte är förbjudet i MVVM**. Att skapa och visa ett fönster är UI-relaterad kod och passar bra i View.

ViewModel behöver bara meddela *att* användaren vill öppna inställningarna, inte *hur* fönstret ska öppnas.

## 12. Dela data mellan fönster med DataContext

Vi hade en property `ButtonName` i vår ViewModel, som bestämde texten på knappen i huvudfönstret.

I `SettingsDialog` skapade vi en `TextBox` med följande binding:

```xml
<TextBox Text="{Binding ButtonName, UpdateSourceTrigger=PropertyChanged}"/>
```

Eftersom båda fönstren använder **samma ViewModel-objekt** arbetar de också med samma `ButtonName`.

När användaren skriver i dialogens TextBox uppdateras därför knappens text i huvudfönstret.

`UpdateSourceTrigger=PropertyChanged` innebär att värdet uppdateras vid varje textändring, i stället för först när TextBoxen tappar fokus.

Tack vare `INotifyPropertyChanged` får huvudfönstrets knapp automatiskt veta när `ButtonName` ändras.

Vi behöver alltså inte manuellt skicka tillbaka texten från dialogen.

### Vad händer om användaren klickar Cancel?

I vårt exempel ändras `ButtonName` direkt medan användaren skriver.

Det innebär att en Cancel-knapp inte automatiskt skulle ångra ändringarna.

Om vi vill kunna avbryta behöver vi exempelvis:

1. Kopiera `ButtonName` till en tillfällig property, exempelvis `TemporaryButtonName`, när dialogen öppnas.
2. Binda dialogens TextBox till `TemporaryButtonName`.
3. Kopiera tillbaka värdet till `ButtonName` endast om användaren klickar OK.

I större dialoger kan det vara lämpligt att använda en separat ViewModel för de värden som redigeras.

## 13. DialogResult och användarvänliga dialogfönster

Vi tittade också på några enkla sätt att förbättra användarupplevelsen (**UX**, User Experience).

### DialogResult

När vi öppnar ett fönster med `ShowDialog()` kan vi använda `DialogResult` för att ange hur dialogen avslutades.

```csharp
DialogResult = true;
```

När vi sätter `DialogResult` stängs det modala fönstret automatiskt.

`ShowDialog()` returnerar ett `bool?`, så vi kan kontrollera resultatet:

```csharp
if (dialog.ShowDialog() == true)
{
    // Användaren klickade OK
}
```

Om användaren stänger dialogen med krysset blir resultatet normalt `false`.

### IsDefault och IsCancel

Vi kan göra dialogen enklare att använda med tangentbordet:

```xml
<Button Content="OK" IsDefault="True"/>
<Button Content="Cancel" IsCancel="True"/>
```

- **IsDefault:** Användaren kan aktivera OK-knappen med Enter.
- **IsCancel:** Användaren kan avbryta med Escape. I en modal dialog kan knappen också stänga dialogen med resultatet `false`.

Det här är vanliga beteenden som användare förväntar sig av dialogfönster.

### Autofokus och markera befintlig text

När vår inställningsdialog öppnas vill vi att användaren ska kunna börja skriva direkt, utan att först behöva klicka i TextBoxen.

Vi använde fönstrets `Loaded`-event:

```csharp
Loaded += (_, _) =>
{
    textBox.Focus();
    textBox.SelectAll();
};
```

- `Focus()` placerar tangentbordsfokus i TextBoxen.
- `SelectAll()` markerar all befintlig text.

Användaren kan då direkt skriva ett nytt namn och ersätta den gamla texten.

Vi väntar på `Loaded` eftersom fokus och textmarkering inte alltid fungerar som tänkt om de sätts direkt i konstruktorn innan fönstret har laddats.

### TabIndex och IsTabStop

Användaren ska också kunna navigera mellan kontroller med Tab-tangenten.

- **TabIndex:** Bestämmer i vilken ordning kontrollerna får fokus när användaren trycker Tab.
- **IsTabStop="False":** Gör att en kontroll hoppas över vid Tab-navigering.

```xml
<TextBox TabIndex="1"/>
<TextBox TabIndex="2"/>
<TextBox IsTabStop="False"/>
<Button Content="OK" TabIndex="3"/>
```

En bra dialog bör gå att använda utan mus. Det är både bekvämt och viktigt för tillgängligheten.

## 14. Converters i WPF

Ibland vill vi binda en property till en annan property som har en **annan datatyp**.

Exempelvis:

- En `CheckBox` har `IsChecked`, som är en `bool?`.
- En `Button` har `Visibility`, som är en enum med värdena `Visible`, `Hidden` och `Collapsed`.

Vi behöver därför omvandla värdet från checkboxen innan vi använder det för knappens synlighet.

Det är vad en **Converter** gör.

### BooleanToVisibilityConverter

WPF har en inbyggd converter som heter `BooleanToVisibilityConverter`.

Vi skapade den som en resurs:

```xml
<BooleanToVisibilityConverter x:Key="boolToVisibility"/>
```

Sedan använde vi den i en binding:

```xml
<Button
    Visibility="{Binding IsChecked,
                         ElementName=myCheckBox,
                         Converter={StaticResource boolToVisibility}}"/>
```

Här anger vi:

- **IsChecked:** Vilket värde vi vill läsa.
- **ElementName:** Vilken kontroll värdet kommer från.
- **Converter:** Hur värdet ska omvandlas.

Resultatet blir:

- `true` → `Visible`
- `false` → `Collapsed`

`Collapsed` innebär att kontrollen både döljs och slutar ta upp plats i layouten.

Det skiljer sig från `Hidden`, där kontrollen är osynlig men fortfarande tar upp plats.

### Egna converters och IValueConverter

Vi kan även skapa egna converters i C#.

Då skapar vi en klass som implementerar interfacet `IValueConverter`.

Det innehåller två metoder:

- **Convert:** Omvandlar värdet från bindningens källa till målegenskapen.
- **ConvertBack:** Omvandlar värdet åt andra hållet, exempelvis vid TwoWay-binding.

En egen converter kan exempelvis användas för att visa olika färger beroende på ett värde eller för att presentera data på ett annat sätt än den lagras.

Vi implementerade ingen egen converter under lektionen, men principen är densamma som för den inbyggda `BooleanToVisibilityConverter`.

## Sammanfattning

- **Resources** låter oss återanvända objekt och värden i XAML.
- **StaticResource** används för att referera till en resurs med dess nyckel.
- Resurser kan placeras på olika nivåer, exempelvis Grid, Window och Application.
- **Styles** samlar gemensamma egenskaper för kontroller.
- En implicit Style används automatiskt för sin kontrolltyp.
- En explicit Style refereras med sin `x:Key`.
- **BasedOn** låter en Style ärva egenskaper från en annan Style.
- **ResourceDictionary** gör det möjligt att samla resurser i separata XAML-filer.
- **DataTemplates** bestämmer hur dataobjekt presenteras.
- Inuti en DataTemplate är `DataContext` normalt det aktuella objektet i listan.
- `ItemsSource` anger vilka objekt som visas, medan `ItemTemplate` anger hur de visas.
- DataTemplates fungerar lika bra med data från en ViewModel som med objekt skapade direkt i XAML.
- En ViewModel kan använda **Commands och Events** för att begära att ett nytt fönster öppnas, utan att själv känna till fönstret.
- Code-behind är lämpligt för UI-relaterade uppgifter som att öppna och stänga fönster.
- Flera fönster kan dela samma ViewModel genom att använda samma `DataContext`.
- `ShowDialog()` öppnar ett modalt fönster, och `DialogResult` anger hur det avslutades.
- `IsDefault`, `IsCancel`, `Focus()`, `SelectAll()`, `TabIndex` och `IsTabStop` hjälper oss skapa mer användarvänliga dialoger.
- **Converters** omvandlar värden vid databinding.
- WPF har inbyggda converters, men vi kan också skapa egna genom att implementera `IValueConverter`.