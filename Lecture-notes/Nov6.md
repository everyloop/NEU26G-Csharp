# November 6

[Här är länk](https://github.com/everyloop/NEU26G-Csharp/blob/master/Resources/CleanCode.pdf) till presentationen vi gick igenom idag.

Följande anteckningar sammanfattar och kompleterar presentationen:

## 1. Vad är Clean Code?

Clean Code handlar om att skriva kod som är lätt att förstå, underhålla och vidareutveckla.

Kod ska inte bara fungera idag. Den ska också vara begriplig för andra utvecklare, och för oss själva när vi återvänder till den längre fram.

Några viktiga utgångspunkter:

- Använd tydliga och beskrivande namn på variabler, metoder och klasser.
- Håll metoder fokuserade och undvik att blanda flera olika ansvarsområden.
- Undvik onödig komplexitet och upprepning.
- Försök skriva kod som förklarar sig själv, snarare än att förlita dig på kommentarer.
- Strukturera koden så att förändringar inte får oväntade konsekvenser på andra ställen.

Begreppet **code smell** används för sådant i koden som kan tyda på designproblem. Det behöver inte innebära att koden är felaktig, men kan vara ett tecken på att den borde förbättras.

## 2. Designprinciper och design patterns

**Designprinciper** är generella riktlinjer för hur vi bör strukturera och organisera vår kod.

Några vanliga principer är:

- **DRY (Don't Repeat Yourself):** Undvik onödig upprepning av kunskap och logik.
- **KISS (Keep It Simple, Stupid):** Välj enkla lösningar när de är tillräckliga.
- **YAGNI (You Ain't Gonna Need It):** Implementera inte funktionalitet som du ännu inte behöver.
- **SOLID:** Fem principer som hjälper oss att skapa kod som är lättare att förändra och vidareutveckla.

**Design patterns (designmönster)** är i stället återkommande lösningar på vanliga designproblem.

Skillnaden är alltså att designprinciper ger oss riktlinjer för *hur vi bör tänka*, medan designmönster beskriver *hur vi kan lösa vissa typer av problem*.

## 3. SOLID – fem principer för förändringsbar kod

SOLID är ett samlingsnamn för fem designprinciper:

| Princip | Grundtanke |
|---|---|
| **S – Single Responsibility** | Samla kod som hör till samma ansvar och förändras av samma anledning. |
| **O – Open/Closed** | Gör det möjligt att utöka beteenden utan att behöva ändra befintlig kod. |
| **L – Liskov Substitution** | Subklasser och implementationer måste uppfylla sina basklassers eller interfaces förväntade kontrakt. |
| **I – Interface Segregation** | Tvinga inte klasser att vara beroende av funktionalitet de inte behöver. |
| **D – Dependency Inversion** | Låt högnivåkod bero på abstraktioner i stället för konkreta implementationsdetaljer. |

Principerna hänger ihop och hjälper oss att minska konsekvenserna när kraven på ett program förändras.

### Ansvarsfördelning och beroenden

Två viktiga begrepp i sammanhanget är:

- **Cohesion (sammanhållning):** Hur väl olika delar inom en klass hör ihop. Vi eftersträvar hög sammanhållning.
- **Coupling (koppling):** Hur starkt olika klasser eller moduler är beroende av varandra. Vi eftersträvar generellt lös koppling (*loose coupling*).

Detta hänger också ihop med **Separation of Concerns** – att dela upp ett system i tydligt avgränsade ansvarsområden.

MVVM är ett exempel på hur vi redan arbetar med sådan uppdelning: View, ViewModel och Model har olika roller.

## 4. Design handlar om avvägningar

En viktig poäng är att SOLID inte är en uppsättning absoluta regler som måste tillämpas överallt.

Varje designbeslut innebär en avvägning mellan **enkelhet idag och flexibilitet inför framtida förändringar**.

Genom att exempelvis introducera ett interface kan vi göra det enklare att byta ut en implementation senare. Men vi får också fler delar att förstå och underhålla.

Det innebär att vi behöver fundera på:

- Vilka delar av systemet kommer sannolikt att förändras?
- Var skulle en förändring riskera att påverka mycket annan kod?
- Vilka abstraktioner och uppdelningar ger oss faktiskt något värde?
- När är en enkel, konkret lösning tillräcklig?

Vi kan inte förutse alla framtida krav. Att försöka förbereda systemet för varje tänkbar förändring riskerar därför att skapa onödig komplexitet.

**Målet är inte maximal flexibilitet, utan en rimlig balans mellan enkelhet, tydlighet och förändringsbarhet.**

## 5. Koddemonstration – löst kopplade beroenden

**Code-along:**  
[L051_SOLID](https://github.com/everyloop/NEU26G-Csharp/blob/master/Code-alongs/L051_SOLID/Program.cs)  

Vi avslutade med en enkel konsolapplikation där en `ShoppingCart` kunde ladda produkter från olika datakällor.

Vi skapade två implementationer:

- `DatabaseReader` – simulerade inläsning från en databas.
- `FileReader` – simulerade inläsning från en fil.

Båda implementerade interfacet `IReader`, som definierade metoden `LoadProducts()`.

I stället för att låta `ShoppingCart` själv skapa en specifik läsare skickade vi in en implementation via konstruktorn. Detta är ett exempel på **Dependency Injection**.

Eftersom `ShoppingCart` arbetade mot interfacet `IReader` behövde den inte veta om produkterna kom från en fil eller en databas.

Demonstrationen knöt därmed ihop flera av dagens begrepp:

- **Loose coupling:** `ShoppingCart` är inte direkt beroende av en viss datakälla.
- **Dependency Inversion:** `ShoppingCart` använder en abstraktion i stället för en konkret läsarimplementation.
- **Open/Closed:** Vi kan lägga till ytterligare datakällor genom att skapa nya implementationer av `IReader`, utan att ändra `ShoppingCart`.
- **Dependency Injection:** Vi bestämmer utanför `ShoppingCart` vilken läsare den ska använda.

Poängen är att vi kan förändra *hur* produkterna laddas utan att behöva förändra den kod som använder dem.

## 6. Att ta med sig

Clean Code och SOLID handlar inte om att skriva så avancerad eller abstrakt kod som möjligt.

Det handlar om att skriva **tydlig, välorganiserad kod där framtida förändringar blir enklare att genomföra utan att skapa nya problem**.

Ibland innebär det fler klasser, interfaces och tydligare ansvarsfördelning. Andra gånger är den bästa lösningen att behålla koden enkel.