# Övningsuppgifter med data binding

Använd data binding (inte event handlers) för att lösa följande uppgifter:

## 1. Ändra textstorleken

Sätt ut en Slider som kan variera mellan 10 och 50, och en TextBlock med texten "Sample Text".

Gör så att textens storlek ändras när man drar i slidern.

## 2. Aktivera/inaktivera slidern

Lägg till en förikryssad CheckBox med texten "Enable slider".

Gör så att slidern blir inaktiverad när checkboxen inte är ikryssad.

## 3. Visa sliderns värde

Ändra TextBlock så att den visar sliderns aktuella värde i stället för "Sample Text".

Textstorleken ska fortfarande styras av slidern.

## 4. Avrunda värdet

Ändra bindningen så att TextBlock visar värdet avrundat till två decimaler.

***Tips:*** *Kolla upp StringFormat.*

## 5. Visa en beskrivande text

Ändra så att det exempelvis står:

Font size: 27

***Tips:*** *Du kan kombinera vanlig text med ett formaterat värde i StringFormat.*

## 6. Lägg till ytterligare en slider

Lägg till ytterligare en Slider med värden mellan 10 och 50.

Koppla dess Value till den första sliderns Value med data binding.

Experimentera med olika värden på Mode:
- Mode=OneTime 
- Mode=OneWay 
- Mode=OneWayToSource
- Mode=TwoWay

Vad blir skillnaden?

## 6 Ändra textens genomskinlighet

Ta helt bort data binding från den andra slider ochh uppdatera så den istället tar värden mellan 0 och 1.

Använd databindning för att låta den styra Opacity på din TextBlock.

Nu ska användaren kunna ändra både textstorlek och genomskinlighet med två olika sliders.


## 7. Skapa en User-klass och sätt DataContext

Skapa en klass som heter `User` med följande properties:

```csharp
public class User
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
}
```

Skapa ett `User`-objekt i fönstrets konstruktor, efter `InitializeComponent()`.

Ge användaren valfria värden på `FirstName`, `LastName` och `Age`.

Sätt sedan fönstrets `DataContext` till ditt `User`-objekt.

## 8. Visa användarens uppgifter

Lägg till en `StackPanel` som innehåller en `TextBlock`.

Använd flera `Run` inuti din `TextBlock` för att kombinera vanlig text med databindningar till användarens properties.

Resultatet ska exempelvis bli:

**Anna Andersson är 25 år gammal.**

Använd databindning för att hämta `FirstName`, `LastName` och `Age` från objektet som ligger i `DataContext`.

## 9. Redigera användarens uppgifter

Lägg till två `TextBox` och en `Slider` i din `StackPanel`.

- Den första textrutan ska vara bunden till `FirstName`.
- Den andra textrutan ska vara bunden till `LastName`.
- Slidern ska vara bunden till `Age` och kunna variera mellan 0 och 120.

Använd `Mode=TwoWay` där det behövs så att ändringar kan skrivas tillbaka till `User`-objektet.

**Experimentera med `UpdateSourceTrigger`:**

1. Börja med att använda standardvärdet för `UpdateSourceTrigger` på textrutorna.
2. Ändra sedan till `UpdateSourceTrigger=PropertyChanged`.
3. Undersök när objektets properties uppdateras i de två fallen.

**Undersök också:**

- Vad händer med meningen i `TextBlock` när du skriver i textrutorna?
- Vad händer när du drar i slidern?
- Uppdateras även värdena i ditt `User`-objekt?

## 10. Experimentera med DataContext

Ge din `StackPanel` ett namn så att du kan komma åt den från C#.

Ändra sedan konstruktorn så att du skapar två olika `User`-objekt och sätter olika `DataContext` på din `StackPanel` och ditt fönster:

```csharp
stackPanel.DataContext = new User
{
    FirstName = "Anders",
    LastName = "Andersson",
    Age = 22
};

DataContext = new User
{
    FirstName = "Bertil",
    LastName = "Bertilsson",
    Age = 34
};
```

**Undersök:**

- Vilken användare visas i gränssnittet?
- Varför visas just den användaren?
- Vad händer om du tar bort raden som sätter `stackPanel.DataContext`?

Lägg sedan tillbaka raden och skapa en knapp med texten `"Clear DataContext"` i din `StackPanel`.

När man klickar på knappen ska följande kod köras:

```csharp
stackPanel.ClearValue(FrameworkElement.DataContextProperty);
```

**Undersök:**

- Vad händer med värdena som visas i gränssnittet?
- Varifrån kommer de nya värdena?

**Bonus:** Testa att ersätta `ClearValue(...)` med:

```csharp
stackPanel.DataContext = null;
```

Vad blir skillnaden? Varför?

## 11. Ändra användarens namn från C#

Ta bort koden som ändrar `DataContext` i uppgift 10 och återgå till att använda ett enda `User`-objekt.

Spara objektet i ett fält i din `MainWindow`-klass så att du kan komma åt det från flera metoder.

Sätt fönstrets `DataContext` till detta objekt.

Ändra sedan knappens text till `"Change name"` och uppdatera dess `Click`-event handler så att den ändrar användarens `FirstName` till `"Erik"`.

**Undersök:**

- Ändras värdet på `FirstName` i ditt `User`-objekt?
- Uppdateras texten i `TextBlock`?
- Uppdateras textrutan som är bunden till `FirstName`?

**Fundera:** Varför fungerade uppdateringen när du skrev i textrutan, men inte när du ändrade samma property direkt från C#?