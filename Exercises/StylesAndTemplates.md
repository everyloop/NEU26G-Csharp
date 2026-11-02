# Övningsuppgifter med Resources, Styles och DataTemplates

Skapa ett nytt WPF-projekt för att lösa följande uppgifter.

## 1. Färger som resurser

Skapa tre `SolidColorBrush`-resurser: en för fönstrets bakgrund, en för textfärg och en för knapparnas bakgrund.

Använd resurserna på flera kontroller med `StaticResource`.

**Extra:** Lägg en resurs med samma `x:Key` i både `Window.Resources` och `Grid.Resources`. Vilken av resurserna används av kontrollerna i gridden?

## 2. Skapa egna Styles

Skapa ett fönster med minst fyra knappar.

- Skapa en implicit `Style` som påverkar alla knappar.
- Sätt minst fyra egenskaper, exempelvis `Background`, `Foreground`, `Padding`, `Margin` och `FontSize`.
- Skapa en namngiven `Style` med nyckeln `DangerButton` som ärver från den vanliga stilen med `BasedOn`. Ge den en röd bakgrund.
- Använd `DangerButton` på en av knapparna.
- Ge en annan knapp en egen `Background` direkt i XAML.

**Fundera:** Varför kan du ändra bakgrundsfärgen på en enskild knapp utan att ändra din Style?

## 3. Skapa en lista med DataTemplate

Skapa en klass `Movie` med följande properties:

- `Title`
- `Director`
- `ReleaseYear`

Skapa minst fyra filmer i en `x:Array` i XAML och visa dem i en `ListBox`.

Skapa en `DataTemplate` som visar:

- Filmtiteln med fet stil.
- Regissören under titeln.
- Utgivningsåret i en annan färg.

**Extra:** Lägg till en "Remove"-knapp bredvid varje film. (Den behöver inte göra något när man trycker på den.)

## 4. Flytta Styles till en egen fil

Skapa en separat `ResourceDictionary` med namnet `MyStyles.xaml`.

Flytta minst två Styles till den nya filen och koppla in den i `App.xaml` med `MergedDictionaries`.

Kontrollera att dina Styles fortfarande fungerar.

**Extra:** Skapa ett andra fönster och kontrollera att samma Styles fungerar där.

## 5. Implicit DataTemplate

Ändra din `DataTemplate` från uppgift 3 så att den använder `DataType` i stället för `x:Key`.

Ta sedan bort `ItemTemplate` från din `ListBox`.

Fungerar det fortfarande?

**Fundera:** Vad är skillnaden mellan en DataTemplate med `x:Key` och en med `DataType`?

## 6. Bonus: Quizfrågor med DataTemplate

Skapa en klass `Question` med properties för frågetext och korrekt svar.

Skapa några frågor och visa dem i en `ListBox` med hjälp av en `DataTemplate`.

Visa frågetexten med fet stil och det korrekta svaret under frågan i en annan färg.

**Extra:** Skapa en Style för `TextBlock` som du använder i din DataTemplate.