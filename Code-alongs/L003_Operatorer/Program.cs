
// Operatorer är special-tecken i C# som används för att utföra en operation på (oftast) två operander.

// Aritmetiska operatorer (räkneoperationer)

Console.WriteLine("Aritmetiska operationer:");
Console.WriteLine(5 + 3);
Console.WriteLine(5 - 3);
Console.WriteLine(5 * 3);
Console.WriteLine(5 / 3);
Console.WriteLine(5 % 3);

// Delar man två heltal så får man hur många gånger nämnaren går i täljaren samt en rest som man får ut med % (modulus).

Console.WriteLine("\nDivision med heltal och flyttal:");
Console.WriteLine(9 / 4);
Console.WriteLine(9 % 4); // Modulus (Remainder, Rest)
Console.WriteLine(9 / 4.0);
Console.WriteLine(9.0 / 4);
Console.WriteLine(9.0 / 4.0);


// Tilldelningsoperator:

int myInt = 50; // <= Tilldeningsoperator (Assignment) ett enkelt likamedtecken (=)


// Jämförelseoperator (Comparisson)
Console.WriteLine("\nJämförelseoperatorer:");
Console.WriteLine(myInt == 5);  // Jämför om två värden är exakt lika, dubbla likamedtecken (==)
Console.WriteLine(myInt != 5);  // Inte lika med
Console.WriteLine(myInt > 5);   // Större än
Console.WriteLine(myInt < 5);   // Mindre än
Console.WriteLine(myInt >= 5);  // Större än eller lika med
Console.WriteLine(myInt <= 5);  // Mindre än eller lika med


// Logiska operatorer
Console.WriteLine("\nLogiska eoperatorer:");

Console.WriteLine("\nNOT: !");
Console.WriteLine(!true);
Console.WriteLine(!false);

Console.WriteLine("\nAND: &&");
Console.WriteLine(false && false);
Console.WriteLine(false && true);
Console.WriteLine(true && false);
Console.WriteLine(true && true);

Console.WriteLine("\nOR: ||");
Console.WriteLine(false || false);
Console.WriteLine(false || true);
Console.WriteLine(true || false);
Console.WriteLine(true || true);


