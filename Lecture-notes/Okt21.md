
# Oktober 21

Idag fortsatte vi med WPF och XAML. Vi tittade först på olika [**layoutpaneler**](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt21.md#1-layoutpaneler), alltså hur vi kan placera kontroller i ett fönster. Därefter undersökte vi några vanliga [**kontroller**](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt21.md#2-vanliga-kontroller-i-wpf), hur deras properties fungerar och hur vi kan reagera på användarens interaktioner.

## 1. Layoutpaneler

**Code-along:**  
[L042_Panels](https://github.com/everyloop/NEU26G-Csharp/tree/master/Code-alongs/L042_Panels)

En layoutpanel är ett element som ansvarar för att **placera och arrangera sina barn** (*children*).

Olika paneler har olika regler för hur barnen placeras. Vilken panel vi väljer påverkar därför hur gränssnittet beter sig, exempelvis när fönstrets storlek ändras.

Vi tittade på:

- [`Grid`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt21.md#grid--rader-och-kolumner)
- [`StackPanel`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt21.md#stackpanel--staplar-elementen)
- [`WrapPanel`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt21.md#wrappanel--radbryter-automatiskt)
- [`DockPanel`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt21.md#dockpanel--docka-mot-kanterna)
- [`Canvas`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt21.md#canvas--absoluta-positioner)

### Grid – rader och kolumner

En `Grid` delar upp ett område i rader och kolumner.

```xml
<Grid ShowGridLines="True">
    <Grid.RowDefinitions>
        <RowDefinition Height="*" />
        <RowDefinition Height="*" />
    </Grid.RowDefinitions>

    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*" />
        <ColumnDefinition Width="*" />
    </Grid.ColumnDefinitions>

    <Button Grid.Row="0" Grid.Column="0" Content="Övre vänster" />
    <Button Grid.Row="0" Grid.Column="1" Content="Övre höger" />
    <Button Grid.Row="1" Grid.Column="0" Content="Nedre vänster" />
    <Button Grid.Row="1" Grid.Column="1" Content="Nedre höger" />
</Grid>
```

Här skapar vi två rader och två kolumner, vilket ger fyra celler.

`Grid.Row` och `Grid.Column` anger vilken cell ett element ska placeras i. Numreringen börjar på **0**.

Om vi inte anger någon rad eller kolumn hamnar elementet i rad 0, kolumn 0.

#### Storlekar i Grid

Vi kan ange rad- och kolumnstorlekar på olika sätt:

| Värde | Betydelse |
|---|---|
| `100` | Fast storlek, 100 enhetsoberoende pixlar |
| `Auto` | Anpassa storleken efter innehållet |
| `*` | Ta en andel av det återstående utrymmet |
| `2*` | Ta dubbelt så stor andel som `*` |

Exempel:

```xml
<Grid.ColumnDefinitions>
    <ColumnDefinition Width="100" />
    <ColumnDefinition Width="*" />
    <ColumnDefinition Width="2*" />
</Grid.ColumnDefinitions>
```

Första kolumnen är 100 bred. De andra två delar på det återstående utrymmet i proportionerna 1:2.

`ShowGridLines="True"` är praktiskt när vi experimenterar med layouten, eftersom det gör cellgränserna synliga.

---

### StackPanel – staplar elementen

En `StackPanel` placerar sina barn efter varandra, antingen **vertikalt** eller **horisontellt**.

```xml
<StackPanel Orientation="Vertical">
    <Button Content="1" />
    <Button Content="2" />
    <Button Content="3" />
</StackPanel>
```

Med `Orientation="Vertical"` placeras knapparna ovanför varandra.

```xml
<StackPanel Orientation="Horizontal">
    <Button Content="1" />
    <Button Content="2" />
    <Button Content="3" />
</StackPanel>
```

Med `Orientation="Horizontal"` placeras de bredvid varandra.

`Vertical` är standardvärdet.

#### Lägga till element från C#

Vi kan skapa nya kontroller direkt i C# och lägga till dem i en panel.

```csharp
var newButton = new Button()
{
    Content = "Ny knapp",
    Width = 40,
    Height = 40,
    Margin = new Thickness(10, 10, 0, 0)
};

stackPanel.Children.Add(newButton);
```

`Children` är samlingen av element som ligger i panelen.

Några användbara operationer:

```csharp
stackPanel.Children.Add(newButton);    // Lägg till
stackPanel.Children.Remove(newButton); // Ta bort
stackPanel.Children.Clear();           // Ta bort alla

int count = stackPanel.Children.Count;
```

Vi kan också undersöka var ett element ligger i samlingen:

```csharp
int index = stackPanel.Children.IndexOf(myButton);
```

Precis som i andra samlingar börjar index på 0.

#### Koppla event handlers till nya kontroller

När vi skapar en knapp i XAML kan vi skriva:

```xml
<Button Content="Klicka" Click="Button_Click" />
```

Skapar vi knappen i C# kan vi koppla eventet så här:

```csharp
newButton.Click += Button_Click;
```

Det är samma typ av eventhantering, men vi gör kopplingen i C# i stället för i XAML.

#### Ta reda på vilken knapp som klickades

Event handlerns `sender` innehåller objektet som utlöste eventet.

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    if (sender is Button button)
    {
        stackPanel.Children.Remove(button);
    }
}
```

Det gör att samma event handler kan användas av flera knappar.

Vi kan också ändra panelens egenskaper från C#:

```csharp
stackPanel.Orientation = Orientation.Horizontal;
```

---

### WrapPanel – radbryter automatiskt

En `WrapPanel` liknar en `StackPanel`, men med en viktig skillnad:

**När elementen inte längre får plats fortsätter de på nästa rad eller kolumn.**

```xml
<WrapPanel Orientation="Horizontal">
    <Button Content="1" Width="80" Height="40" />
    <Button Content="2" Width="80" Height="40" />
    <Button Content="3" Width="80" Height="40" />
    <Button Content="4" Width="80" Height="40" />
</WrapPanel>
```

Om panelen blir för smal flyttas några knappar automatiskt till nästa rad.

Med `Orientation="Vertical"` fylls i stället en kolumn innan elementen fortsätter i nästa.

Vi kan lägga till element på samma sätt som i en `StackPanel`:

```csharp
wrapPanel.Children.Add(newButton);
```

**Skillnaden mellan StackPanel och WrapPanel:**

- `StackPanel` fortsätter i samma riktning även om elementen inte får plats.
- `WrapPanel` börjar på en ny rad eller kolumn när utrymmet tar slut.

Det är särskilt tydligt när man ändrar storlek på fönstret.

---

### DockPanel – docka mot kanterna

En `DockPanel` placerar element längs panelens kanter.

Vi kan välja mellan:

- `Left`
- `Top`
- `Right`
- `Bottom`

```xml
<DockPanel>
    <Button Content="Vänster" DockPanel.Dock="Left" />
    <Button Content="Överst" DockPanel.Dock="Top" />
    <Button Content="Höger" DockPanel.Dock="Right" />
    <Button Content="Innehåll" />
</DockPanel>
```

Barnen behandlas i den ordning de ligger i panelen. Varje element tar en del av det utrymme som återstår.

#### LastChildFill

En `DockPanel` har en property som heter `LastChildFill`.

Den är `True` som standard.

Det innebär att **det sista barnet fyller hela det återstående utrymmet**, oavsett vad dess `DockPanel.Dock` säger.

Det är användbart när man exempelvis vill bygga en applikation med:

- Meny längst upp
- Sidopanel till vänster
- Statusfält längst ned
- Huvudinnehåll som fyller resten

Om vi sätter `LastChildFill="False"` dockas även det sista barnet enligt sin `DockPanel.Dock`.

#### Attached properties

`DockPanel.Dock` är en så kallad **attached property**.

Det betyder att propertyn definieras av `DockPanel`, men kan sättas på dess barn.

En `Button` har alltså inte en vanlig property som heter `Dock`.

I XAML:

```xml
<Button DockPanel.Dock="Left" />
```

I C# använder vi i stället en metod på `DockPanel`:

```csharp
DockPanel.SetDock(myButton, Dock.Left);
```

Vi kan läsa värdet med:

```csharp
Dock position = DockPanel.GetDock(myButton);
```

Även `Grid.Row`, `Grid.Column` och `Canvas.Left` är exempel på attached properties.

#### Slumpmässig dockning

I vår demo skapade vi knappar som fick en slumpmässig dockningsposition:

```csharp
Dock dockPosition = (Dock)Random.Shared.Next(4);

var newButton = new Button()
{
    Content = dockPosition.ToString()[0]
};

DockPanel.SetDock(newButton, dockPosition);
dockPanel.Children.Add(newButton);
```

`Random.Shared.Next(4)` ger ett heltal från 0 till 3.

Vi omvandlar talet till enum-typen `Dock`, vars värden är `Left`, `Top`, `Right` och `Bottom`.

Det här fungerar eftersom dessa enum-värden motsvarar talen 0–3.

---

### Canvas – absoluta positioner

Till skillnad från de andra panelerna placerar `Canvas` sina barn med **koordinater**.

```xml
<Canvas>
    <Button Content="Test"
            Canvas.Left="50"
            Canvas.Top="100" />

    <Rectangle Width="100"
               Height="70"
               Fill="Red"
               Canvas.Left="20"
               Canvas.Bottom="50" />
</Canvas>
```

Vi kan använda:

- `Canvas.Left` – avstånd från vänsterkanten
- `Canvas.Top` – avstånd från överkanten
- `Canvas.Right` – avstånd från högerkanten
- `Canvas.Bottom` – avstånd från nederkanten

Värdena anges i WPF:s enhetsoberoende pixlar (*device-independent pixels*).

En `Canvas` är användbar för exempelvis ritprogram, diagram, spel och andra gränssnitt där vi vill placera objekt på bestämda koordinater.

Däremot är den ofta mindre lämplig för vanliga formulär, eftersom positionerna inte automatiskt anpassas när fönstret ändrar storlek.

#### Rectangle och Ellipse

Vi använde också två former:

```xml
<Rectangle Width="100"
           Height="70"
           Fill="Red"
           Stroke="Black"
           StrokeThickness="10" />

<Ellipse Width="100"
         Height="70"
         Fill="Green"
         Stroke="Black"
         StrokeThickness="10" />
```

Viktiga properties:

- `Width` och `Height` – storlek
- `Fill` – fyllnadsfärg
- `Stroke` – kantlinjens färg
- `StrokeThickness` – kantlinjens tjocklek

`Rectangle` och `Ellipse` är exempel på WPF:s `Shape`-element.

---

## 2. Vanliga kontroller i WPF

**Code-along:**  
[L043_Kontroller](https://github.com/everyloop/NEU26G-Csharp/tree/master/Code-alongs/L043_Kontroller)

Efter panelerna tittade vi på några vanliga kontroller:

- [`TextBlock`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt21.md#textblock--visa-text)
- [`TextBox`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt21.md#textbox--skriva-och-redigera-text)
- [`Button`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt21.md#button--reagera-p%C3%A5-klick)
- [`CheckBox`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Okt21.md#checkbox--ett-val-som-kan-vara-p%C3%A5-eller-av)

Vi undersökte också hur man kan koppla ihop dem med event handlers.

### TextBlock – visa text

`TextBlock` används för att visa text som användaren normalt inte ska redigera.

```xml
<TextBlock Text="Hello world"
           FontSize="30"
           Foreground="Blue" />
```

Några viktiga properties:

- `Text`
- `FontSize`
- `FontFamily`
- `FontWeight`
- `FontStyle`
- `Foreground`
- `TextWrapping`

#### Formatera delar av texten med Run

En `TextBlock` kan innehålla flera `Run`-element.

Det gör att olika delar av texten kan ha olika formatering.

```xml
<TextBlock>
    <Run Text="Hello " FontWeight="Bold" />
    <Run Text="world" Foreground="Red" FontStyle="Italic" />
    <Run Text="!!!" />
</TextBlock>
```

`Run` har en `Text`-property, men kan också ha egen formatering.

Vi kan ge ett `Run` ett namn och ändra texten från C#:

```xml
<Run x:Name="redRun" Foreground="Red" Text="world" />
```

```csharp
redRun.Text = "Ny text";
```

#### Radbrytningar

Vill vi lägga in en radbrytning kan vi använda:

```xml
<TextBlock>
    Första raden
    <LineBreak />
    Andra raden
</TextBlock>
```

Vi kan också använda en numerisk teckenreferens i ett attribut:

```xml
<Run Text="Första raden&#10;Andra raden" />
```

`&#10;` representerar Unicode-tecknet med kodpunkt 10, alltså LF (*line feed*).

Det är XML-syntax, medan `\n` är den syntax vi är vana vid från C#-strängar.

#### Automatisk radbrytning

```xml
<TextBlock Width="200"
           TextWrapping="Wrap"
           Text="En lång text som radbryts när den inte får plats." />
```

`TextWrapping="Wrap"` innebär att texten kan delas upp på flera rader när utrymmet tar slut.

---

### TextBox – skriva och redigera text

`TextBox` används när användaren ska kunna skriva text.

```xml
<TextBox x:Name="textBox"
         FontSize="30"
         TextChanged="TextBox_TextChanged" />
```

Texten finns i propertyn `Text`.

Vi kan läsa och ändra den från C#:

```csharp
string input = textBox.Text;

textBox.Text = "Hej!";
```

#### TextChanged

Eventet `TextChanged` inträffar när texten ändras.

```csharp
private void TextBox_TextChanged(
    object sender,
    TextChangedEventArgs e)
{
    redRun.Text = textBox.Text;
}
```

I vår demo lät vi alltså texten i en `TextBox` uppdatera texten i ett `Run`.

Det visar hur olika kontroller kan påverka varandra.

**Observera:** `TextChanged` kan också utlösas när vi ändrar `Text` från C#, inte bara när användaren skriver.

#### TextBox med flera rader

En `TextBox` är normalt avsedd för en rad, men vi kan tillåta flera:

```xml
<TextBox Width="200"
         Height="200"
         AcceptsReturn="True"
         TextWrapping="Wrap"
         VerticalScrollBarVisibility="Auto" />
```

Viktiga properties:

| Property | Funktion |
|---|---|
| `AcceptsReturn` | Tillåter Enter för att skapa en ny rad |
| `TextWrapping` | Styr automatisk radbrytning |
| `VerticalScrollBarVisibility` | Styr den vertikala scrollbaren |
| `HorizontalScrollBarVisibility` | Styr den horisontella scrollbaren |

För scrollbars finns bland annat värdena:

- `Visible` – scrollbar visas alltid
- `Hidden` – scrollbar visas inte, men innehållet kan fortfarande scrollas på andra sätt
- `Disabled` – scrolling i den riktningen är avstängd
- `Auto` – scrollbar visas när den behövs

`Wrap` och `NoWrap` styr om långa textrader ska radbrytas automatiskt.

#### KeyDown – reagera på tangenttryckningar

Vi kan också reagera på tangentbordet:

```xml
<TextBox KeyDown="textBox_KeyDown" />
```

```csharp
private void textBox_KeyDown(object sender, KeyEventArgs e)
{
    if (e.Key == Key.Enter)
    {
        anotherRun.Text = textBox2.Text;
    }
}
```

Här undersöker vi vilken tangent som trycktes ned med `e.Key`.

Observera att `KeyDown` och `TextChanged` är olika events:

- `KeyDown` handlar om tangenttryckningar.
- `TextChanged` handlar om att textens innehåll förändras.

---

### Button – reagera på klick

Vi har använt `Button` tidigare, men tittade nu också på properties som påverkar om knappen kan användas eller visas.

```xml
<Button x:Name="copyButton"
        Content="Copy"
        IsEnabled="False"
        Visibility="Visible"
        Click="Button_Click" />
```

#### IsEnabled

`IsEnabled` bestämmer om kontrollen kan användas.

```csharp
copyButton.IsEnabled = true;
```

I vår demo ville vi bara tillåta kopiering när textrutan innehöll text:

```csharp
private void TextBox_TextChanged(
    object sender,
    TextChangedEventArgs e)
{
    redRun.Text = textBox.Text;

    copyButton.IsEnabled = textBox.Text != "";
}
```

När textrutan är tom är knappen inaktiverad.

När användaren skriver något blir knappen aktiv.

#### Visibility

`Visibility` bestämmer hur ett element visas.

Det finns tre möjliga värden:

| Värde | Betydelse |
|---|---|
| `Visible` | Elementet visas |
| `Hidden` | Elementet syns inte men behåller sin plats i layouten |
| `Collapsed` | Elementet syns inte och tar inte upp någon plats |

Exempel:

```csharp
copyButton.Visibility = Visibility.Collapsed;
```

#### Kopiera text mellan kontroller

I vår demo använde vi knappens `Click`-event för att kopiera text från en `TextBox` till en annan:

```csharp
private void Button_Click(object sender, RoutedEventArgs e)
{
    textBox2.Text = textBox.Text;
}
```

Det här är kopiering **mellan två kontroller**, inte till datorns urklipp.

---

### CheckBox – ett val som kan vara på eller av

En `CheckBox` används ofta för inställningar som kan aktiveras eller inaktiveras.

```xml
<CheckBox x:Name="checkBox"
          Content="Show scrollbars"
          IsChecked="True"
          Checked="CheckBox_CheckedChanged"
          Unchecked="CheckBox_CheckedChanged" />
```

Den viktigaste propertyn är `IsChecked`.

Den är av typen `bool?`, alltså en nullable boolean.

Normalt använder vi `true` och `false`, men om `IsThreeState="True"` kan en `CheckBox` också ha ett tredje tillstånd: `null`.

#### Checked och Unchecked

Vi kan koppla samma event handler till båda eventen:

```csharp
private void CheckBox_CheckedChanged(
    object sender,
    RoutedEventArgs e)
{
    if (checkBox.IsChecked == true)
    {
        textBox2.HorizontalScrollBarVisibility =
            ScrollBarVisibility.Visible;

        textBox2.VerticalScrollBarVisibility =
            ScrollBarVisibility.Visible;
    }
    else
    {
        textBox2.HorizontalScrollBarVisibility =
            ScrollBarVisibility.Hidden;

        textBox2.VerticalScrollBarVisibility =
            ScrollBarVisibility.Hidden;
    }
}
```

Här använder vi alltså en `CheckBox` för att styra två andra properties.

Att skriva `checkBox.IsChecked == true` fungerar även om värdet skulle vara `null`.

**En detalj från demon:** Texten på vår `CheckBox` var `Hide scrollbars`, men koden visade scrollbars när den var ikryssad. Texten borde alltså snarare vara `Show scrollbars`, alternativt skulle villkoret kunna vändas.

#### Viewbox – skala en kontroll

Vi placerade också vår `CheckBox` i en `Viewbox`:

```xml
<Viewbox Stretch="Uniform" Height="50">
    <CheckBox Content="Show scrollbars"
              FontSize="12" />
</Viewbox>
```

En `Viewbox` skalar sitt innehåll så att det passar i det tillgängliga utrymmet.

Med `Stretch="Uniform"` behålls proportionerna.

Det kan vara användbart eftersom en `CheckBox` består av både text och en liten kryssruta. Att öka `FontSize` gör normalt texten större, men inte nödvändigtvis själva kryssrutan. En `Viewbox` kan skala båda tillsammans.

---

## 3. Slider – välj ett numeriskt värde

En annan vanlig kontroll är `Slider`.

Den låter användaren välja ett tal genom att dra ett reglage.

```xml
<Slider x:Name="mySlider"
        Minimum="0"
        Maximum="100"
        Value="50"
        Width="250" />
```

De viktigaste properties är:

| Property | Funktion |
|---|---|
| `Minimum` | Minsta tillåtna värde |
| `Maximum` | Största tillåtna värde |
| `Value` | Det aktuella värdet |
| `Orientation` | `Horizontal` eller `Vertical` |
| `TickFrequency` | Avstånd mellan markeringar |
| `TickPlacement` | Var markeringarna ska visas |
| `IsSnapToTickEnabled` | Om reglaget ska fastna vid markeringarna |
| `SmallChange` | Stegstorlek för mindre tangentbordsändringar |
| `LargeChange` | Stegstorlek för större ändringar |

### Value är en double

`Slider.Value` är av typen `double`, även om vi bara har angett heltal i XAML.

```csharp
double value = mySlider.Value;
```

Om vi vill använda värdet som ett heltal kan vi exempelvis avrunda:

```csharp
int value = (int)Math.Round(mySlider.Value);
```

### ValueChanged

Eventet `ValueChanged` inträffar när sliderns värde ändras.

```xml
<StackPanel>
    <Slider x:Name="mySlider"
            Minimum="0"
            Maximum="100"
            Value="50"
            ValueChanged="Slider_ValueChanged" />

    <TextBlock x:Name="valueText"
               Text="50" />
</StackPanel>
```

```csharp
private void Slider_ValueChanged(
    object sender,
    RoutedPropertyChangedEventArgs<double> e)
{
    valueText.Text = e.NewValue.ToString("F0");
}
```

`e.NewValue` är det nya värdet och `e.OldValue` är det tidigare värdet.

`"F0"` formaterar talet med noll decimaler.

**Viktigt:** `ValueChanged` kan utlösas redan när XAML laddas och kontrollen initieras. Då kanske andra namngivna kontroller ännu inte har skapats. Ett sätt att undvika problem är att kontrollera att de finns:

```csharp
private void Slider_ValueChanged(
    object sender,
    RoutedPropertyChangedEventArgs<double> e)
{
    if (valueText != null)
    {
        valueText.Text = e.NewValue.ToString("F0");
    }
}
```

### Visa markeringar och välja heltalssteg

```xml
<Slider Minimum="0"
        Maximum="10"
        Value="5"
        TickFrequency="1"
        TickPlacement="BottomRight"
        IsSnapToTickEnabled="True" />
```

Här visas markeringar för varje heltal och reglaget fastnar vid dem.

Observera att `Value` fortfarande har typen `double`.

### Låt en Slider påverka en annan kontroll

En `Slider` passar bra för att exempelvis ändra storleken på text:

```xml
<StackPanel>
    <Slider x:Name="fontSlider"
            Minimum="10"
            Maximum="60"
            Value="20"
            ValueChanged="FontSlider_ValueChanged" />

    <TextBlock x:Name="sampleText"
               Text="Hello world!"
               FontSize="20" />
</StackPanel>
```

```csharp
private void FontSlider_ValueChanged(
    object sender,
    RoutedPropertyChangedEventArgs<double> e)
{
    if (sampleText != null)
    {
        sampleText.FontSize = e.NewValue;
    }
}
```

Samma princip kan användas för att styra exempelvis:

- Ett elements `Width` eller `Height`
- Ett elements `Opacity` (mellan 0 och 1)
- En `ProgressBar`-kontrolls `Value`
- Storleken på en form i en `Canvas`

---

## 4. Properties kan ärvas från föräldraelement

Vi såg också att vissa properties kan sättas på ett föräldraelement och automatiskt påverka dess barn.

Exempel:

```xml
<Window FontSize="30">
    <StackPanel>
        <TextBlock Text="Stor text" />
        <Button Content="Också stor text" />
        <TextBlock Text="Mindre text" FontSize="14" />
    </StackPanel>
</Window>
```

Här ärver de två första elementen `FontSize="30"` från fönstret.

Det sista elementet anger en egen `FontSize` och använder därför värdet 14.

Det här kallas **property value inheritance** och är en del av WPF:s system med *dependency properties*.

Några properties som kan ärvas är:

- `FontSize`
- `FontFamily`
- `FontWeight`
- `FontStyle`
- `Foreground`
- `DataContext`

Men **alla properties ärvs inte**.

Exempelvis ärvs inte `Margin`, `Width`, `Height` eller `Background`.

Om en förälder har en bakgrundsfärg kan det ändå se ut som att barnen har samma bakgrund, eftersom förälderns bakgrund syns bakom dem.

Det är inte samma sak som att barnens `Background`-property faktiskt har ärvt värdet.

---

## 5. Sammanfattning

### Layout

- `Grid` delar upp ytan i rader och kolumner.
- `StackPanel` staplar elementen vertikalt eller horisontellt.
- `WrapPanel` radbryter eller kolumnbryter när utrymmet tar slut.
- `DockPanel` placerar element längs kanterna och kan låta det sista barnet fylla resten.
- `Canvas` placerar element med koordinater.

### Kontroller

- `TextBlock` visar text och kan formatera olika delar med `Run`.
- `TextBox` låter användaren skriva text.
- `Button` kan reagera på klick och aktiveras eller inaktiveras med `IsEnabled`.
- `CheckBox` representerar ett val som kan vara på eller av.
- `Slider` låter användaren välja ett numeriskt värde.

### C# och events

Vi har också tränat på att:

- Skapa kontroller från C#.
- Lägga till och ta bort element via en panels `Children`.
- Koppla event handlers med `+=`.
- Använda `sender` för att identifiera kontrollen som utlöste ett event.
- Ändra en kontrolls properties när användaren interagerar med en annan kontroll.

Det viktiga är att förstå att **XAML beskriver hur gränssnittet är uppbyggt, medan C# kan användas för att förändra det och reagera på användarens handlingar**.