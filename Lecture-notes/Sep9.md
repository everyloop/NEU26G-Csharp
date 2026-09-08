# September 9

## Algoritmer
En algoritm är, inom matematiken och datavetenskapen, en ändlig uppsättning (mängd) otvetydiga instruktioner som efter exekvering löser ett problem.

Algoritmer liknas ofta vid ett recept som talar om för en dator hur den ska lösa en uppgift. Algoritmen definierar startpunkten och vilka steg som ska tas, i vilken ordning, ifall stegen ska upprepas eller inte, vid vilka situationer, osv, för att nå ett visst resultat.

[Introduktion till algoritmer](https://www.khanacademy.org/computing/computer-science/algorithms/intro-to-algorithms/v/what-are-algorithms)

### Big-O Notation
Big-O notation beskriver hur en algoritms tids- eller minnesbehov växer när mängden data ökar. Det används för att jämföra hur väl olika algoritmer skalar.

[Läs mer här!](https://dev.to/b0nbon1/understanding-big-o-notation-with-javascript-25mc)

### Exempel på sorteringsalgoritmer
Det finns många standardkategorier av problem som behöver lösas; t.ex. sortering av data.

För varje sådan kategori finns vanligtvis mängder av olika algoritmer att välja på.

[Bubble sort](https://www.youtube.com/watch?v=hahrx5WUeNI) är ett exempel på en sorteringsalgoritm som är enkel att förstå, men ineffektiv för stora datamängder jämfört med effektivare algoritmer som till exempel [Quick sort](https://www.youtube.com/watch?v=WprjBK0p6rw).

[Här är en video som visuellt jämför 24 vanliga sorteringsalgoritmer (Varning, sänk ljudet)](https://www.youtube.com/watch?v=BeoCbJPuvSE)

För den som vill nörda ner sig lite extra finns här en video som förklarar [Merge sort](https://www.youtube.com/watch?v=3j0SWDX4AtU) samt visar hur den kan implementeras i Java (som är ett språk ganska likt C#, så ni kommer känna igen er).

***OBS!*** *När du skriver kod, undersök alltid om det finns färdiga lösningar/algoritmer för problemet du försöker lösa. För standardproblem, som t.ex sortering, så finns effektiva algoritmer inbyggda i .NET; lägg inte tid på att försöka skriva dina egna. (Om det inte är i studiesyfte, för att bättre förstå tekniken).*

## Project Euler
På [projecteuler.net](https://projecteuler.net/archives) hittar ni en serie utmanande matematik/programmerings-problem ni kan prova på för att utveckla er förmåga att lösa problem med hjälp av programmering.

## Problemlösning med datorn som verktyg (a.k.a. programmering)
För att lösa ett problem med en dator behöver du skriva steg-för-steg instruktioner (kod) som datorn förstår och kan följa. Det datorn är bra på, och kan hjälpa dig med, är att utföra dina instruktioner snabbt och exakt. 

När datorn väl har instruktionerna kan den lösa problemet, samt alla liknande problem, mycket snabbare än någon människa någonsin kan göra. 

Datorn löser inte problem på samma sätt som en människa. Den arbetar utifrån program, modeller och instruktioner som människor har skapat.

### För att korrekt lösa ett problem behöver vi kunna:
- Förstå problemet (Problemformulering)
- Förstå stegen som behövs för att lösa problemet (Ta fram en algoritm)
- Skriva kod som löser problemet (Implementering)
- Granska lösningen kritiskt
- Kontrollera att lösningen är korrekt (Verifiera resultatet)

**Problem → Algoritm → Implementation → Granskning → Verifiering**

Uppgifterna nedan kan hjälpa oss att förstå hur vi kan tänka för att hitta en korrekt lösning till ett problem vi står inför.

Som exempel när vi går igenom uppgifterna använder vi oss av [Project Euler 8: Largest Product in a Series](https://projecteuler.net/problem=8), men samma strategi kan användas för de flesta typer av programmeringsproblem.

### Övning 1: Förstå uppgiften
För att kunna lösa ett problem måste vi först förstå vad själva problemet som ska lösas är. 

I vissa fall, som i en "Projekt Euler"-uppgift eller en beställning från kund med medföljande specifikation, så har vi en (mer eller mindre) färdig beskrivning av problemet som vi behöver tolka och förstå. Är det t.ex en specifikation finns ju oftast någon man kan fråga och klargöra att man förstått rätt. Man kan också behöva göra viss research, t.ex för en del Project Euler uppgifter behöver ni googla begrepp.

I andra fall behöver man själv formulera problemet. Kanske utvecklar man ett eget spel/app och behöver kunna hitta en lösning på något t.ex hur vet vi om man krockar med en fiende i spelet. Vad är det för typ av problem, och exakt vad är det vi behöver lösa?

I takt med att AI-verktyg blir bättre på att föreslå och implementera lösningar så blir problemformulering och verifiering ännu viktigare. AI kan hjälpa dig att lösa ett problem – men du behöver fortfarande avgöra om det är rätt problem, om förutsättningarna är korrekta och om lösningen faktiskt håller.

När svar blir enklare att få blir förmågan att ställa rätt frågor och bedöma svaren allt viktigare.

### Övning 2: Tänk manuellt igenom stegen som behövs.
Första steget för att kunna bygga ditt program är att du själv förstår och kan lösa problemet. Om du inte förstår principen för hur problemet kan lösas, så kommer du inte heller kunna skriva koden datorn behöver för att lösa problemet. Du behöver inte kunna utföra hela lösningen manuellt, men du bör kunna förklara stegen och varför de fungerar.

Som en andra övning föreslår jag därför att du lägger undan datorn, och plockar fram papper och penna (om det behövs, alternativt kan du tänka igenom stegen i huvudet). Kolla på de tusen siffrorna i uppgiften. Hur kan du manuellt hitta/beräkna den största möjliga produkten?

Tänk på att om ett problem inte har en uppenbar lösning, så kan du ofta bryta ner problemet i enklare delmål, för att på så vis ta dig vidare.

Hur hittar du den första möjliga produkten bland talen? Hur hittar du nästa? Vilken är den sista produkten? När du hittat alla, hur kan du veta vilken som är störst? Tänk inte i termer av C# eller kod än. Kan du själv (manuellt) hitta alla möjliga produkter bland siffrorna för att avgöra vilken som är störst?

OBS! Att faktiskt hitta/beräkna alla produkter manuellt skulle förmodligen ta dig väldigt lång tid. Det vi är intresserade av här är bara att förstå principen för hur du hittar första, andra, tredje etc. produkten.

### Övning 3: Formulera en algoritm.
Fundera på hur du tänkte för att hitta/beräkna de olika produkterna? När du väl kommit på hur du skulle hitta den första produkten, så gick det förmodligen fortare att hitta nästa? Finns det steg som du upprepar om och om igen?

Det här är ett bra sätt att tänka när man försöker ta fram en algoritm. Om uppgiften består av många upprepningar behöver man inte göra alla manuellt så länge man listat ut vilka steg som behöver upprepas, och hur de påverkar nästa upprepning (i de fall man använder output från en iteration som input i nästa). 

Börja med "happy path" – det normala fallet. Fundera sedan på vad som händer vid gränserna och när förutsättningarna inte är som förväntat – så kallade "edge cases": Vilken är den sista produkten? Hur vet vi när vi är klara? Siffrorna i exemplet representeras som en ruta på 20x50 tecken; behöver hänsyn tas när vi kommer till en radbrytning? Vad händer om två produkter är lika? Annat?

### Övning 4: Instruera en kompis i hur problemet löses.
Som en övning: försök förklara för en kompis/familjemedlem hur de ska gå tillväga steg-för-steg för att hitta den högsta möjliga produkten bland siffrorna. 

När du förklarar för någon kommer de (förmodligen) ifrågasätta om det verkar helt ologiskt eller om de inte får ut rätt resultat från dina instruktioner. En dator ifrågasätter dock inte dina instruktioner, utan försöker utföra dem som de är skrivna. Därför behöver du själv kontrollera att resultatet faktiskt blir det du avsåg.

### Övning 5 - Pseudokod
Innan du börjar försöka skriva programmet i C#; som en femte övning, se om du kan skriva ner i punktform, ett ”recept” för hur man hittar delsträngarna. Ungefär som när du förklarade för en kompis alltså, men nu ska det vara tydliga instruktioner som vem som helst ska kunna
följa genom att läsa dem, utan att du är med och kan svara på eventuella (följd-)frågor.

Man brukar använda sig av pseudokod för att beskriva algoritmer. Pseudokod är ingen riktig kod från något riktigt programmeringspråk, och det finns ingen universell syntax för pseudokod. Istället försöker man förklara stegen strukturerat med ”vanliga ord”. 

Man använder visserligen ofta ord som loop och if-else (eftersom dessa är allmänna konstruktioner inom programmering), men tanken är att tydligt kommunicera en lösning till en kollega eller AI utan att fastna i ett visst programmeringsspråks syntax. Vem som helst med lite programmeringsvana ska kunna följa instruktionerna, inte bara de som jobbat i ett visst språk.

Se om du kan beskriva din algoritm i pseudokod.

### Övning 6 - Implementera i C#
Om vi har gjort stegen ovan så har vi nu förstått problemet och formulerat en algoritm.  Kanske har vi också skrivit ner pseudokod för algoritmen. Nu är det dags att översätta algoritmen i kod.

Det är först i detta steg som vi behöver ha specifika kunskaper om språket som vi ska implementera algoritmen i. (Genom dessa övningar blir det förhoppningsvis tydligare att programmering handlar om att omvandla problem till lösningar; och att skriva kod bara är en del av den processen.)

En stor del av programmering handlar om att lära sig ”tänka som en programmerare”; hur vi angriper ett problem och bryter upp det i små logiska steg (algoritm) som en dator kan följa.

Kursen vi läser nu har visserligen stort fokus på syntax och specifika konstruktioner som har med språket C# att göra. Men mycket av det vi lär oss ”mellan raderna”, iterationer, villkor, logik, uttryck, datatyper etc är saker vi finner i de flesta språk. 

Förståelsen för hur vi kombinerar dessa för att lösa komplexa problem är en stor del av hantverket. När man har mer erfarenhet av programmering och olika språk, kan det också ingå i det här steget att välja det språk/verktyg som är bäst lämpat för uppgiften. Men för denna kurs skriver vi såklart koden i C#.

Som nybörjare är det lätt att man hoppar direkt till implementeringssteget och försöker börja skriva kod. Men man
får inte glömma de första stegen i processen. Att förstå problemet, skapa en algoritm och eventuellt pseudokod. 

Först när vi vet vilka steg som behövs för att lösa uppgiften kan vi börja fundera på hur man skriver C#.

### Övning 7 - Granska lösningen kritiskt

Nu har vi en fungerande implementation. Innan vi nöjer oss med den behöver vi granska lösningen kritiskt.

Är lösningen rimlig? Finns det onödigt komplicerade delar? Har vi gjort antaganden som kanske inte stämmer? Finns det specialfall vi har missat? Finns det ett enklare eller bättre sätt att lösa problemet?

Om någon annan hade skrivit den här lösningen – vad hade du ifrågasatt?

Om hela eller delar av lösningen kommer från AI är det fortfarande du som behöver förstå och kunna bedöma den. Att koden ser övertygande ut eller går att kompilera betyder inte att lösningen är bra eller korrekt.

Var beredd att ändra eller kasta delar av lösningen om du upptäcker ett bättre sätt – även om du redan lagt mycket tid på den.

### Övning 8 - Försök motbevisa att lösningen är korrekt

Nu tror vi att lösningen fungerar. Istället för att försöka bekräfta det, försök hitta ett fall där den **inte** fungerar.

Först kan man fundera över vad som är rimligt. I vårt exempel inom vilket spann borde resultat ligga?

Finns det någon testdata, med känt resultat, som vi kan provköra vår algoritm på?

Kan vi jämföra resultatet med en manuell beräkning?

**Testa gränsfall och specialfall.**
- Fungerar det för ett enkelt normalfall?
- Vad händer med minsta möjliga input?
- Vad händer med ovanligt stor input?
- Vad händer om input innehåller nollor?
- Vad händer vid början och slutet av serien?

På vilka andra sätt kan vi verifiera resultatet?

### Sammanfattning

**Börja inte med koden. Börja med problemet.** Förstå vad som ska lösas, formulera en lösning och implementera den först när du förstår stegen.

**Och sluta inte när koden fungerar.** Granska lösningen kritiskt, förenkla och städa upp koden, och försök hitta situationer där den inte fungerar.

**Målet är inte bara kod som fungerar – utan en lösning som är begriplig, hållbar och som du kan lita på.**

## OBS! Återuppfinn inte hjulet
Tänk på att för de flesta standardproblem finns det mängder av färdiga lösningar. Om du behöver utföra vanliga operationer som t.ex komprimera, sortera eller söka i data, så finns detta inbyggt i .NET med väldigt optimerade algoritmer.

Chansen att du som nybörjare ska lyckas ta fram nya algoritmer som är mer optimerade än dessa är, typ noll.

Använd i första hand de inbyggda lösningarna. Om det du behöver inte finns i .NET, undersök om det finns ett etablerat bibliotek som löser problemet.