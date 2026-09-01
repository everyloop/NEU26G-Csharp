# Augusti 27

## The Big Picture - Introduktionsföreläsning

- Vad är en dator?​
- Vad är programmering?​
- Vad är C#?​
- Vad är .NET?​
- Vad är den röda tråden i utbildningen?​
- Vad menas med frontend, backend, och fullstack?​
- Vad gör en utvecklare?​
- Vad ska ni EGENTLIGEN lära er?​
- Hur tänker en professionell utvecklare?​

[Länk till slide](https://github.com/everyloop/NEU26G-Csharp/blob/master/Resources/Introduktion.pdf)

## Visual studio installer
Visual studio installer används för att installera eller uppdatera befintlig installation med fler komponenter.

I installern kan man välja att istallera enskilda komponenter, men det vanligaste är att man väljer ett paket, så kallat "workload", för den typ av utveckling man ska göra. För denna kursen använder vi workload ".NET Desktop Development"

I denna kurs använder vi [Visual Studio 2022 Community](https://visualstudio.microsoft.com/downloads/)

## Visual studio

### Solution & Projects

En solution i Visual Studio kan innehålla ett eller flera projekt. Konfigurationen för lösningen, bland annat vilka projekt som ingår lagras i en .sln fil.

Varje projekt lagras sedan i en undermapp med samma namn som projektet. I projektmappen ligger en .csproj fil som innehåller konfiguration för projektet (t.ex vilken version av dotnet). I samma mapp ligger även alla .cs filer som hör till projektet.

### Build, Rebuild, Clean

För att bygga ett projekt, högerklicka på projektet i Solution Explorer och välj "Build". Detta kommer kompilera koden och bygga en körbar .exe fil.

Kolla i undermappen bin/Debug/net8.0 för att se de filer som du byggde.

Om du istället högerklickar på din solution i Solution Explorer och väljer "Build Solution" så kommer visual studio bygga samtliga projekt (som uppdaterats sedan senaste bygg) som ingår i din solution.

### Debug / Release build

Brevid den gröna "run"-knappen i Visual Studio finns en drop-down där man kan välja mellan Debug och Release. Detta är olika konfigurationer för hur projektet ska byggas, och output för dessa hamnar i två separata mappar under ditt projekts "bin"-mapp.

När du bygger för "Debug" optimeras inte din kod lika väl, samt den tar med meta-data som används för att kunna debugga/felsöka koden.

När du bygger för "Release" så kommer inte den meta-datan med, plus att den optimeras mer.

Använd "debug" under utveckling, och "release" när du vill släppa en färdig version som ska delas med användare.

