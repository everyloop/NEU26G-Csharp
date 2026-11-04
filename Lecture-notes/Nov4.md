# November 4

Idag har vi kollat på:

- [Ostrukturerad, strukturerad och semi-strukturerad data.](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Nov4.md#1-olika-typer-av-data)
- [Json](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Nov4.md#2-vad-%C3%A4r-json)
- [Serialize / Deserialize](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Nov4.md#3-serialisering-och-deserialisering)
- [AsyncRelayCommand](https://github.com/everyloop/NEU26G-Csharp/blob/master/Lecture-notes/Nov4.md#10-asyncrelaycommand-i-mvvm) 

## 1. Olika typer av data

Vi började med att prata om hur data kan organiseras och lagras.

### Strukturerad data

Strukturerad data följer en tydlig och förutbestämd struktur.

Exempel är relationsdatabaser och kalkylblad där information organiseras i tabeller med kolumner och rader.

Varje rad följer i princip samma schema, exempelvis:

| FirstName | LastName | Age |
|---|---|---|
| Maria | Andersson | 23 |
| Peter | Svensson | 28 |

Strukturerad data är enkel att söka, filtrera och bearbeta eftersom vi vet hur informationen är organiserad.

### Ostrukturerad data

Ostrukturerad data saknar ett tydligt, förutbestämt schema.

Exempel:

- Bilder
- Ljudfiler
- Videofiler
- Fritextdokument

Det betyder inte att informationen saknar innehåll eller intern struktur, utan att den inte är organiserad enligt ett gemensamt schema med tydligt definierade fält.

### Semistrukturerad data

Semistrukturerad data har en viss organisation, men behöver inte följa ett lika strikt schema som en relationstabell.

Exempel är **JSON** och **XML**.

Datan organiseras med hjälp av exempelvis nycklar, värden och nästlade strukturer. Olika objekt kan innehålla olika uppsättningar egenskaper.

Det gör formaten flexibla och användbara när vi behöver lagra eller överföra information mellan olika system.

## 2. Vad är JSON?

**JSON (JavaScript Object Notation)** är ett textbaserat format för att lagra och överföra data.

Trots namnet är JSON inte begränsat till JavaScript. Det används i många programmeringsspråk, bland annat C#.

JSON används exempelvis för:

- Kommunikation med webb-API:er.
- Lagring av inställningar.
- Sparande och inläsning av programdata.
- Informationsutbyte mellan olika system.

XML är ett annat vanligt format för liknande ändamål.

### Hur är JSON uppbyggt?

JSON består av ett antal grundläggande datatyper:

- **Object:** En samling nycklar och värden inom `{ }`.
- **Array:** En lista med värden inom `[ ]`.
- **String:** Text inom dubbla citationstecken.
- **Number:** Numeriska värden.
- **Boolean:** `true` eller `false`.
- **null:** Avsaknad av värde.

Exempel:

```json
{
  "firstName": "Maria",
  "lastName": "Andersson",
  "age": 23,
  "isStudent": true,
  "favoriteColors": ["Red", "Blue"],
  "address": {
    "city": "Stockholm",
    "country": "Sweden"
  }
}
```

Några viktiga detaljer:

- JSON-nycklar måste vara strängar inom dubbla citationstecken.
- Objekt innehåller nyckel/värde-par separerade med kommatecken.
- Arrays kan innehålla flera värden, även objekt.
- JSON har ingen särskild datatyp för exempelvis datum eller enums.
- JSON stöder inte vanliga kommentarer.

JSON beskriver alltså **data**, inte C#-klasser eller programlogik.

## 3. Serialisering och deserialisering

**Code-along:**  
[L050_Json](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L050_Json/Program.cs)  

När vi arbetar med JSON i C# behöver vi kunna omvandla mellan C#-objekt och JSON.

Detta kallas serialisering och deserialisering.

**Serialisering:** C#-objekt → JSON

**Deserialisering:** JSON → C#-objekt

I modern .NET finns det inbyggda biblioteket `System.Text.Json` som hanterar detta.

Ett annat vanligt bibliotek är `Newtonsoft.Json` (Json.NET), som fortfarande förekommer i många projekt.

### Serialisering med JsonSerializer

Vi skapade en array med `Person`-objekt och serialiserade den:

```csharp
string json = JsonSerializer.Serialize(people);
```

Resultatet blir en JSON-sträng som innehåller informationen från objekten.

Eftersom vi serialiserar en array får vi en JSON-array med flera JSON-objekt.

### Spara JSON till fil

En JSON-sträng kan sparas som en vanlig textfil:

```csharp
File.WriteAllText("people.json", json);
```

Filen skapas i programmets aktuella arbetskatalog om vi inte anger en annan sökväg.

JSON är ett textformat och kan därför öppnas och läsas i en vanlig texteditor.

### Deserialisering

Vi kan läsa tillbaka filen:

```csharp
string json = File.ReadAllText("people.json");

List<Person>? people =
    JsonSerializer.Deserialize<List<Person>>(json);
```

`Deserialize<T>()` använder den generiska typparametern `T` för att veta vilken C#-typ JSON-datan ska omvandlas till.

I vårt exempel läser vi in en JSON-array som en `List<Person>`.

Returtypen är nullable eftersom deserialiseringen kan ge `null`, exempelvis om JSON-innehållet är `null`.

## 4. Hur skapas objekt vid deserialisering?

När `JsonSerializer` deserialiserar ett objekt måste den skapa en instans av klassen och ge den rätt värden.

Om klassen har en publik parameterlös konstruktor kan den användas för att skapa objektet, varefter publika properties med tillgängliga setters kan fyllas i.

Om klassen i stället bara har en publik konstruktor med parametrar kan `System.Text.Json` använda den.

Exempel:

```csharp
public Person(string firstName, string lastName, int age)
{
    FirstName = firstName;
    LastName = lastName;
    Age = age;
}
```

För konstruktorbaserad deserialisering måste parametrarna kunna matchas mot klassens properties vad gäller namn och typ.

Matchningen mot propertynamnen är inte skiftlägeskänslig.

En property som bara har en getter kan alltså fortfarande få sitt värde via konstruktorn.

Däremot kan en property med en privat setter normalt inte fyllas i direkt efter att objektet skapats, utan ytterligare konfiguration.

**Viktigt:** Att en property kan serialiseras innebär inte automatiskt att den också kan deserialiseras på samma sätt.

## 5. JsonSerializerOptions

Vi kan styra hur JSON ska skrivas och läsas genom att skicka ett `JsonSerializerOptions`-objekt till `Serialize()` eller `Deserialize()`.

```csharp
var options = new JsonSerializerOptions
{
    WriteIndented = true
};

string json = JsonSerializer.Serialize(people, options);
```

### WriteIndented

```csharp
WriteIndented = true
```

Gör JSON mer lättläst genom att lägga till indrag och radbrytningar.

Utan denna inställning skrivs JSON normalt mer kompakt, utan onödiga blanksteg.

### PropertyNamingPolicy

```csharp
PropertyNamingPolicy = JsonNamingPolicy.CamelCase
```

Ändrar hur propertynamn skrivs i JSON.

Exempel:

- `FirstName` → `firstName`
- `FavoriteColor` → `favoriteColor`

Det är vanligt att JSON använder camelCase, även om C#-properties normalt använder PascalCase.

### IgnoreReadOnlyProperties

```csharp
IgnoreReadOnlyProperties = true
```

Gör att vanliga skrivskyddade properties, exempelvis properties med enbart `get`, utelämnas vid serialisering.

Standardvärdet är `false`.

Inställningen handlar om **serialisering**, inte om hur konstruktorn används vid deserialisering.

### IncludeFields

```csharp
IncludeFields = true
```

Som standard serialiserar `System.Text.Json` publika properties, men inte vanliga publika fält.

Vi hade exempelvis:

```csharp
public string myField = "test";
```

Med `IncludeFields = true` kan även detta fält tas med i JSON.

### UnmappedMemberHandling

Vid deserialisering kan JSON innehålla nycklar som inte motsvarar några properties eller inkluderade fält i C#-klassen.

```csharp
UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip
```

`Skip` innebär att sådana JSON-värden ignoreras, vilket också är standardbeteendet.

Alternativet `Disallow` gör i stället att deserialiseringen kastar ett undantag om JSON innehåller okända medlemmar.

Det kan vara användbart när vi vill kontrollera att inkommande JSON följer en förväntad struktur.

## 6. Enums och JsonStringEnumConverter

Vi hade en nullable enum-property i vår `Person`-klass:

```csharp
public ConsoleColor? FavoriteColor { get; set; }
```

Som standard serialiseras enumvärden som **heltal**.

Om `FavoriteColor` är `ConsoleColor.Red` skrivs därför det numeriska värdet för `Red` i JSON.

Vi kan ändra detta genom att registrera en converter:

```csharp
var options = new JsonSerializerOptions
{
    WriteIndented = true,
    Converters = { new JsonStringEnumConverter() }
};
```

Då serialiseras enumvärden som strängar, exempelvis:

```json
{
  "FavoriteColor": "Red"
}
```

Det gör JSON mer lättläst och mindre beroende av enumvärdenas numeriska ordning.

En **JSON-converter** styr hur en viss C#-typ omvandlas till och från JSON.

Vi kan också skriva egna converters genom att ärva från `JsonConverter<T>`, men det gick vi inte in på under lektionen.

## 7. Attribut för att styra serialiseringen

Vi tittade också på hur attribut kan användas direkt på klassens properties.

Attributen kommer från `System.Text.Json.Serialization`.

### JsonPropertyName

```csharp
[JsonPropertyName("Name")]
public string FirstName { get; set; }
```

Gör att propertyn `FirstName` får namnet `"Name"` i JSON.

Attributet används både vid serialisering och deserialisering.

Det påverkar däremot inte vilket namn en konstruktorparameter måste ha – den måste fortfarande matcha C#-propertyn `FirstName`.

### JsonIgnore

```csharp
[JsonIgnore]
public string LastName { get; set; }
```

Gör att propertyn ignoreras vid vanlig serialisering och deserialisering.

Det kan vara användbart om viss information inte ska sparas i JSON.

### JsonPropertyOrder

```csharp
[JsonPropertyOrder(-1)]
public int Age { get; }
```

Styr i vilken ordning properties skrivs till JSON.

Properties med lägre ordningsvärde skrivs före properties med högre värde.

Exempelvis skrivs en property med `-1` före en property med `1`.

Det påverkar bara ordningen i JSON-resultatet, inte hur informationen representeras i C#.

## 8. Referenser och cirkulära objektgrafer

Vi experimenterade även med att låta två `Person`-objekt referera till varandra genom en `BestFriend`-property.

Exempel:

```csharp
people[0].BestFriend = people[1];
people[1].BestFriend = people[0];
```

Det skapar en **cirkulär referens**:

Maria → Peter → Maria → Peter ...

Vid vanlig serialisering kan detta orsaka problem eftersom serialiseraren fortsätter att följa referenserna.

Vi tittade därför på:

```csharp
ReferenceHandler = ReferenceHandler.Preserve
```

Detta gör att `System.Text.Json` kan bevara objektreferenser genom att lägga till särskild metadata i JSON, exempelvis `$id` och `$ref`.

På så sätt kan flera referenser peka på samma objekt när informationen deserialiseras igen.

Det är framför allt relevant när vi arbetar med mer komplexa objektgrafer.

## 9. Asynkron JSON-hantering

Vi har tidigare gått igenom `async` och `await`, och JSON-hantering kan också göras asynkront.

`System.Text.Json` innehåller bland annat:

- `JsonSerializer.SerializeAsync()`
- `JsonSerializer.DeserializeAsync()`

Dessa arbetar med streams och kan användas tillsammans med exempelvis `FileStream`.

Exempel:

```csharp
await using var stream = File.Create("people.json");

await JsonSerializer.SerializeAsync(stream, people, options);
```

Vi kan också använda vanlig serialisering och asynkron filhantering:

```csharp
string json = JsonSerializer.Serialize(people, options);

await File.WriteAllTextAsync("people.json", json);
```

Skillnaden är att `SerializeAsync()` hanterar streamskrivningen asynkront, medan `File.WriteAllTextAsync()` skriver en redan färdig JSON-sträng asynkront.

**Async innebär inte automatiskt att CPU-arbetet körs på en annan tråd.**

Om själva serialiseringen av en mycket stor objektgraf är CPU-krävande kan det fortfarande påverka UI-tråden. Då kan `Task.Run()` vara aktuellt.

För små JSON-filer behövs det normalt inte.

## 10. AsyncRelayCommand i MVVM

Som avslutning återvände vi till vår tidigare WPF-code-along och lade till en [`AsyncRelayCommand`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L049_Windows_in_MVVM/AsyncRelayCommand.cs).

**Code-along:**  
[L049_Windows_in_MVVM](https://github.com/everyloop/NEU26G-Csharp/tree/master/Code-alongs/L049_Windows_in_MVVM)  


Vi hade redan en vanlig [`RelayCommand`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L049_Windows_in_MVVM/RelayCommand.cs) som tar emot en `Action<object?>`.

Problemet är att följande kod:

```csharp
new RelayCommand(async _ =>
{
    await Task.Delay(2000);
    ButtonName += "X";
});
```

blir en **async void-lambda**, eftersom `Action` har returtypen `void`.

Kommandot får alltså ingen `Task` som det kan invänta.

Det innebär att flera klick kan starta flera överlappande operationer.

### AsyncRelayCommand

Vår nya [`AsyncRelayCommand`](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L049_Windows_in_MVVM/AsyncRelayCommand.cs) tar i stället emot en:

```csharp
Func<object?, Task>
```

Då kan kommandot invänta den asynkrona operationen och hantera eventuella undantag.

Vår implementation använder också en `_isExecuting`-flagga tillsammans med `CanExecute()` och `CanExecuteChanged`.

Det gör att knappen automatiskt inaktiveras medan kommandot körs.

### Demonstrationen

Vi använde följande kod:

```csharp
AsyncTestCommand = new AsyncRelayCommand(async _ =>
{
    await Task.Delay(3000);
    ButtonName += "X";
});
```

Med vår vanliga `RelayCommand` kunde vi klicka flera gånger och starta flera operationer som avslutades efter sina respektive fördröjningar.

Med `AsyncRelayCommand` blev knappen i stället inaktiverad tills operationen hade avslutats.

Vi tittade också på hur `try/catch/finally` används i kommandoklassen:

- `try` inväntar operationen.
- `catch` fångar eventuella undantag.
- `finally` återställer körstatusen och uppdaterar `CanExecute`.

Vi använde en `MessageBox` för att visa eventuella fel, som en enkel lösning för demonstrationssyfte.

### Varför bygger vi en egen AsyncRelayCommand?

Precis som med vår vanliga `RelayCommand` och `ViewModelBase` bygger vi den själva för att förstå hur den fungerar.

I större projekt används ofta färdiga bibliotek, exempelvis [**CommunityToolkit.Mvvm**](https://www.nuget.org/packages/CommunityToolkit.Mvvm), som innehåller mer genomarbetade implementationer med bland annat körstatus, avbrytning och olika strategier för felhantering.

## Sammanfattning

- Data kan vara strukturerad, ostrukturerad eller semistrukturerad.
- JSON är ett textbaserat, semistrukturerat format för lagring och överföring av data.
- `JsonSerializer.Serialize()` omvandlar C#-objekt till JSON.
- `JsonSerializer.Deserialize<T>()` omvandlar JSON till C#-objekt.
- `JsonSerializerOptions` styr hur JSON skrivs och läses.
- Attribut som `[JsonIgnore]`, `[JsonPropertyName]` och `[JsonPropertyOrder]` kan styra enskilda properties.
- `JsonStringEnumConverter` gör att enums kan sparas som strängar i stället för heltal.
- `ReferenceHandler.Preserve` kan användas för att hantera objektreferenser och cirkulära referenser.
- JSON kan läsas och skrivas asynkront med `SerializeAsync()` och `DeserializeAsync()`.
- En `AsyncRelayCommand` använder `Func<object?, Task>` för att kunna invänta asynkrona operationer.
- Vår implementation av `AsyncRelayCommand` förhindrar även överlappande körningar och hanterar undantag.