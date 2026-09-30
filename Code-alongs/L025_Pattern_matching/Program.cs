
// Mönstermatchning handlar om att testa om en bit data har en viss "form",
// och om den har det, plocka ut datan och köra logik på den i ett och samma steg.

// Stödjer dataorienterad programmering: Det gör det extremt smidigt att skriva logik som
// ska agera på rena datastrukturer (som records och structs), där logiken inte ligger inuti klassen själv.

// Tvättar bort "pyramidkod": Det eliminerar nästlade if-satser inuti if-satser och städar bort klumpiga typkastningar (cast/as).

// Keyword 'is' används för att matcha ett värde eller objekt mot ett mönster, genom att:
// 1) Inspektera formen: kollar om värdet till vänster matchar kriterierna till höger.
// 2) Hantera null: Om värdet till vänster är null, returnerar is alltid false (förutom om du uttryckligen matchar mot mönstret null)


Console.WriteLine("*** Pattern matching med 'is':\n");

int x = 5;
//double x = 15;

Console.WriteLine($"{x.GetType().Name} x = {x}\n");

Console.WriteLine($"x is 6 => {x is 6}");
Console.WriteLine($"x is not 6 => {x is not 6}");
Console.WriteLine($"x is < 6 => {x is < 6}");
Console.WriteLine($"x is > 0 and < 6 => {x is > 0 and < 6}");
Console.WriteLine($"x is < 10 or > 100 => {x is < 10 or > 100}");
Console.WriteLine($"x is int => {x is int}");
Console.WriteLine($"x is double => {x is double}");

//Console.WriteLine($"x > 0 && x < 6 => {x > 0 && x < 6}");

Animal animal = new Dog();
//Animal animal = new Cat();

Console.WriteLine($"\nAnimal animal = new {animal.GetType().Name}();\n");

Console.WriteLine($"animal is Dog => {animal is Dog}");
Console.WriteLine($"animal is not Dog => {animal is not Dog}");
Console.WriteLine($"animal is Cat or Mouse => {animal is Cat or Mouse}");

//if (animal is not Cat cat) // <= 'cat' på slutet säger till kompilatorn att skapa en Cat-referens och casta (Cat)animal till denna.
//{
//    cat.Mew();
//}
//else
//{
//    cat.Mew();
//}

// SWITCH EXPRESSIONS (Det moderna sättet att skriva switch-satser)
// Syfte: Ersätter klumpiga 'case' och 'break' med en kompakt tabell, när vi vill välja ett värde - inte ett kodblock.

// Regler: 
// 1) Uttrycket returnerar ALLTID ett värde direkt (eller kastar en SwitchExpressionException)
// 2) Det använder pil-syntax (=>) istället för kolon (:).
// 3) Mönstren utvärderas uppifrån och ner. Det första mönstret som är sant vinner - resten ingoreras.
// 4) _ (Discard) matchar allt, och kan användas sist för att fånga upp allt som inget annat mönster matchat.
// 5) Kompilatorn varnar om inte alla utfall täckts; och under körning kastas exception om inget mönster matchar värdet.


Console.WriteLine("\n\n*** Pattern matching med 'switch expressions':\n");

int temperatur = 15;

Console.WriteLine($"{x.GetType().Name} temperatur = {temperatur}\n");

string väderBeskrivning = temperatur switch
{
    < 0 => "Det är minusgrader",
    0 => "Det är precis på fryspunkten",
    > 0 and < 10 => "Det är ganska svalt ute",
    _ => "Det är behagligt eller varmt" // Default/Fallback
};

Console.WriteLine($"\n=>{väderBeskrivning}");


class Animal { }
class Dog : Animal { }
class Cat : Animal { public void Mew() { Console.WriteLine("Meow!"); } }
class Mouse : Animal { }



