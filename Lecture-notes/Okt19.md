# Oktober 19

Vi började med att gemensamt gå igenom "clean code"-exempel från labb 2.

Om du vill repetera det, så hittar du exemplen [här](https://github.com/everyloop/NEU26G-Csharp/blob/master/Resources/CleanCode-Labb2.pdf).

# Introduktion till WPF och XAML

Idag började vi arbeta med **WPF (Windows Presentation Foundation)**, ett ramverk i .NET för att utveckla grafiska Windows-applikationer.

Vi tittade framför allt på hur man bygger ett användargränssnitt med **XAML**, hur XAML och C# samverkar och hur man kan reagera på användarens interaktioner, exempelvis knapptryckningar.

## 1. WPF – grafiska applikationer i C#

Hittills har vi framför allt arbetat med konsolapplikationer, där användaren interagerar med programmet genom text i konsolfönstret.

Med WPF kan vi i stället bygga applikationer med exempelvis:

- Fönster
- Knappar
- Textfält
- Bilder
- Menyer
- Listor

WPF använder vanligtvis två olika språk:

- **XAML** för att beskriva användargränssnittets struktur och utseende.
- **C#** för att skriva programmets logik och hantera exempelvis knapptryckningar.

Den här uppdelningen gör det möjligt att arbeta med gränssnittets utseende utan att behöva skapa alla kontroller manuellt i C#.

---

## 2. Vad är XAML?

**XAML (eXtensible Application Markup Language)** är ett XML-baserat språk som används för att beskriva objekt och deras egenskaper.

Exempel:

```xml
<Button Content="Klicka här!"
        Width="150"
        Height="40"
        FontSize="18" />
```

Detta skapar en knapp med angiven text, storlek och textstorlek.

XAML består av **element**, **attribut** och **nästlade element**.

### Element och attribut

```xml
<Button Content="Hej!" Width="150" />
```

- `Button` är ett element som representerar ett objekt av klassen `Button`.
- `Content` och `Width` är properties på objektet.
- Värdena anges som attribut i XAML.

### Start- och sluttaggar

Ett element kan skrivas med en starttagg och en sluttagg:

```xml
<Button>
    Hej!
</Button>
```

Om elementet inte behöver något innehåll mellan taggarna kan det vara självstängande:

```xml
<Button Content="Hej!" />
```

XAML måste följa XML:s syntaxregler, exempelvis att element nästlas korrekt och att attributvärden omges av citationstecken.

---

## 3. Olika sätt att ange properties

Vi tittade på tre olika sätt att ange innehållet i en knapp.

### Attribute syntax

En property anges som ett attribut:

```xml
<Button Content="Hej!" />
```

### Property element syntax

En property anges som ett nästlat element:

```xml
<Button>
    <Button.Content>
        Hej!
    </Button.Content>
</Button>
```

### Content syntax

För vissa kontroller kan man skriva innehållet direkt mellan start- och sluttaggen:

```xml
<Button>
    Hej!
</Button>
```

Alla tre exemplen ger en knapp med texten `Hej!`.

Det sista fungerar eftersom `Content` är knappens **content property**.

### När använder man vilken syntax?

För enkla värden är attribut oftast smidigast:

```xml
<Button Width="150" Height="40" Content="Hej!" />
```

När en property ska innehålla ett mer komplext objekt behöver man ofta använda nästlade element.

Exempelvis kan en knapp innehålla en bild och text:

```xml
<Button>
    <StackPanel Orientation="Vertical">
        <Image Source="logo.png" Width="50" />
        <TextBlock Text="Klicka här!" />
    </StackPanel>
</Button>
```

Det fungerar eftersom en `Button` kan innehålla ett objekt som sitt `Content`, inte bara en textsträng.

I detta fall innehåller knappen en `StackPanel`, som i sin tur innehåller två andra element.

---

## 4. Element kan innehålla andra element

WPF-gränssnitt byggs upp som en hierarki av objekt.

Ett enkelt fönster kan exempelvis se ut så här:

```xml
<Window ...>
    <Grid>
        <Button Content="Hej!" />
    </Grid>
</Window>
```

Här har vi:

- Ett `Window`, som representerar fönstret.
- En `Grid`, som används för att placera andra element.
- En `Button`, som ligger inuti vår Grid.

En `Window` kan bara ha ett direkt innehållsobjekt. Därför använder man ofta en layoutpanel som `Grid` som innehåll i fönstret.

En panel kan sedan innehålla flera kontroller.

Vi kommer att titta närmare på olika layoutpaneler och kontroller under kommande lektioner.

---

## 5. Vanliga egenskaper för placering och utseende

Vi experimenterade med några vanliga properties som påverkar hur kontroller visas.

### HorizontalAlignment och VerticalAlignment

Dessa bestämmer hur en kontroll placeras inom det utrymme som dess förälder tilldelar den.

```xml
<Button Content="Hej!"
        HorizontalAlignment="Center"
        VerticalAlignment="Top" />
```

Vanliga värden är:

- `Left`, `Center`, `Right` och `Stretch` för `HorizontalAlignment`.
- `Top`, `Center`, `Bottom` och `Stretch` för `VerticalAlignment`.

`Stretch` innebär att kontrollen kan sträckas ut för att fylla det tillgängliga utrymmet, om andra inställningar tillåter det.

### Width och Height

Anger kontrollens bredd och höjd:

```xml
<Button Content="Hej!"
        Width="150"
        Height="40" />
```

Om man inte anger någon storlek kan WPF:s layoutsystem beräkna storleken utifrån innehåll och tillgängligt utrymme.

### Margin och Padding

Två viktiga begrepp när vi arbetar med layout:

**Margin** är utrymmet *utanför* kontrollens kant, alltså avståndet till omgivningen.

**Padding** är utrymmet *innanför* kontrollens kant, mellan kanten och innehållet.

```xml
<Button Content="Hej!"
        Margin="20"
        Padding="15" />
```

Man kan också ange olika värden för olika sidor:

```xml
<Button Content="Hej!"
        Margin="10,20,30,40" />
```

Ordningen är:

**Left, Top, Right, Bottom**

Man kan även ange två värden:

```xml
<Button Content="Hej!"
        Margin="10,20" />
```

Det betyder 10 till vänster och höger, och 20 upptill och nedtill.

---

## 6. XAML och code-behind

När vi skapar ett WPF-fönster får vi normalt två filer:

```text
MainWindow.xaml
MainWindow.xaml.cs
```

**MainWindow.xaml** innehåller beskrivningen av användargränssnittet.

**MainWindow.xaml.cs** innehåller C#-kod som hör till fönstret, exempelvis event handlers.

I XAML finns en koppling till C#-klassen:

```xml
<Window x:Class="MyApp.MainWindow"
        ...>
```

I code-behind har vi:

```csharp
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
}
```

### Varför är klassen partial?

Nyckelordet `partial` betyder att en klass kan vara uppdelad på flera delar.

En del av klassen skriver vi själva i `MainWindow.xaml.cs`.

En annan del genereras automatiskt av WPF:s byggverktyg utifrån XAML-filen.

Dessa delar kombineras till **en och samma klass** vid kompileringen.

Den genererade koden hjälper bland annat till att koppla ihop XAML med code-behind och skapa åtkomst till namngivna element.

### Vad gör InitializeComponent()?

```csharp
InitializeComponent();
```

Den här metoden använder den kompilerade XAML-informationen för att bygga upp användargränssnittet och koppla ihop det med vårt fönster.

Det är därför vi normalt anropar `InitializeComponent()` i konstruktorn innan vi börjar arbeta med kontrollerna.

---

## 7. x:Name – ge en kontroll ett namn

Vi kan ge en kontroll ett namn i XAML:

```xml
<Button x:Name="MyButton"
        Content="Klicka här!" />
```

Det gör att vi kan referera till kontrollen från code-behind:

```csharp
MyButton.Content = "Ny text!";
```

På så sätt kan vi ändra gränssnittet från C#.

`x:Name` är en XAML-direktivsyntax som används för att namnge objekt.

---

## 8. Events – reagera på användarens handlingar

En grafisk applikation är ofta **händelsestyrd**.

Det innebär att programmet reagerar på händelser, exempelvis att användaren klickar på en knapp.

Vi kan koppla en knapp till en event handler i XAML:

```xml
<Button Content="Klicka här!"
        Click="Button_Click" />
```

Och skriva metoden i code-behind:

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    MessageBox.Show("Hej!");
}
```

När användaren klickar på knappen anropas metoden.

### Vad är sender?

Parametern `sender` refererar till objektet som utlöste händelsen.

Vi kan använda den för att exempelvis ändra den knapp som klickades:

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    if (sender is Button button)
    {
        button.Content = "Du klickade på mig!";
    }
}
```

Här använder vi pattern matching för att kontrollera om `sender` är en `Button`.

Om det är en knapp får vi en variabel `button` som refererar till objektet.

### Flera knappar kan använda samma event handler

```xml
<Button Content="Knapp 1"
        Click="Button_Click" />

<Button Content="Knapp 2"
        Click="Button_Click" />
```

Båda knapparna anropar samma metod, men med hjälp av `sender` kan vi ta reda på vilken knapp som klickades.

Vi kan exempelvis ändra dess placering:

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    if (sender is Button button)
    {
        if (button.HorizontalAlignment == HorizontalAlignment.Left)
        {
            button.HorizontalAlignment = HorizontalAlignment.Right;
        }
        else
        {
            button.HorizontalAlignment = HorizontalAlignment.Left;
        }
    }
}
```

Det här var grunden till våra små övningar under lektionen.

---

## 9. Designern i Visual Studio

Vi tittade också på hur Visual Studio kan visa en förhandsgranskning av vårt WPF-gränssnitt medan vi skriver XAML.

En intressant detalj är att XAML även kan innehålla inställningar som **bara gäller i designern**.

Exempel:

```xml
<Window ...
        FontSize="20"
        d:FontSize="40"
        d:Height="500"
        d:Width="500">
```

Här gäller:

- `FontSize="20"` används när applikationen körs.
- `d:FontSize="40"` används bara i designern.
- `d:Height` och `d:Width` kan styra storleken i designvyn utan att påverka fönstrets faktiska storlek vid körning.

Prefixet `d:` används för design-time-egenskaper.

Detta kan vara användbart när vi vill göra gränssnittet enklare att arbeta med i Visual Studio utan att påverka den färdiga applikationen.

---

## Sammanfattning

Dagens viktigaste lärdomar:

1. **WPF** används för att skapa grafiska Windows-applikationer i .NET.
2. **XAML** beskriver gränssnittets objekt, struktur och egenskaper.
3. XAML bygger på XML och använder element, attribut och nästlade element.
4. Properties kan anges med attribut eller med nästlade property-element.
5. WPF-kontroller kan innehålla andra objekt, exempelvis bilder och layoutpaneler.
6. `Margin` styr utrymmet utanför en kontroll, medan `Padding` styr utrymmet innanför.
7. **Code-behind** är C#-koden som hör till XAML-filen.
8. `partial` används eftersom en del av fönsterklassen genereras automatiskt.
9. `InitializeComponent()` bygger upp gränssnittet utifrån XAML.
10. **Events** gör det möjligt att reagera på användarens handlingar.
11. Med `sender` kan flera kontroller använda samma event handler.

### Nästa steg

Under kommande lektioner kommer vi att utforska fler av WPF:s vanligaste kontroller och layoutpaneler, och lära oss hur vi bygger mer avancerade användargränssnitt med XAML.