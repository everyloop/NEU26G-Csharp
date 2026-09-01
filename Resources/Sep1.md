# September 1

## Git och Github

[Git](https://git-scm.com/) är ett lokalt versionskontrollsystem som låter dig hålla koll på din historik inom dina projekt, samt sparar dessa olika versioner individuellt. [GitHub](https://github.com/) är en molntjänst där du kan spara Git-projekt på nätet och dela med dig av dem till andra människor.

### Open source
Klicka [här](https://choosealicense.com/licenses/) för att läsa mer om olika open-source-licenser.

## Markdown

Läs [här](https://www.markdownguide.org/getting-started/) om markdown, och kolla in deras [cheat sheet.](https://www.markdownguide.org/cheat-sheet/)

## Powershell

PowerShell är ett terminalprogram från Microsoft som används för uppgiftsautomatisering och konfigurationshantering. Det bygger på .NET och exponerar även alla bibliotek och klasser från .NET så att dessa kan användas direkt i powershell.

Liknande terminal/konsoll/shell finns även för andra plattformar som t.ex Linux och Apple. Även om dessa inte bygger på .NET, så är många av de vanligaste kommandona samma.

### Vanliga kommandon i Powershell

| Kommando | Betydelse |Förklaring  | 
|--------------|------|-------------|
| ls | list | lista filer i aktuell mapp  |
| cd | change directory | byta mapp
| pwd | print working directory | Visar absolut sökväg för mappen man är i|
| cat | concatenate | printar innehållet i fil
| mkdir | make directory | skapa en ny mapp
| cp | copy | koperia &lt;source&gt; &lt;destination&gt;
| mv | move | flytta eller byta namn på fil
| rm | remove | ta bort filer och mappar
| ni | new item | skapa en ny tom fil
| code | vs code | öppnar visual studio code
| start | | "start ." öppar aktuell mapp i file explorer
| exit | | stänger ner powershell fönstret

### Absoluta och relativa sökvägar

Platsen där en fil lagras anges ofta genom att man beskriver vilka mappar och undermappar som leder till filen. Detta kallas en sökväg. Du kan ange sökvägen på två olika sätt: som en absolut eller relativ sökväg.

**En absolut sökväg** är en komplett sökväg till en fil eller katalog från rotkatalogen. I Windows anges diskenheter som en bokstav följt av ett kolon (t.ex C: som är den primära lagringsenheten i din dator). Rotkatalogen skrivs då som enheten följt av ett backslash, t.ex C:\

Exempel, absolut sökväg: "C:\Users\Fredrik\Desktop"

**En relativ sökväg** anger platsen för en fil eller katalog i förhållande till den aktuella katalogen. Relativa sökvägar börjar inte med rotkatalogen och är vanligtvis kortare än absoluta sökvägar.

Exempel, relativ sökväg: ".\Fredrik\Desktop"

### Specialtecken

Det finns även ett antal specialtecken med specifik betydelse som används för speciella ändamål i PowerShell (och andra terminalprogram):

|Namn|Tecken|Beskrivning|
|------|----|-----------|
|punkt|.|referens till mappen man står i|
|punktpunkt|..|referens till överordnad mapp (parent folder)|
|stjärna|\*|wildcard för filterering i filnamn/sökvägar|
|redirection|\>|skriver output från kommando till en fil (obs! skiver över)|
|append|\>\>|skriver output till en fil (lägger till i slutet av filen)|
|pipe|\||skickar output som input till nästa kommando|
