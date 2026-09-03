
Console.Write("Enter your name: ");
string name = Console.ReadLine();

//Console.WriteLine("Denna rad körs alltid i början!");

if (name == "fredrik")
{
    Console.WriteLine("Du heter Fredrik!");
}
else if (name == "anders")
{
    Console.WriteLine("Du heter Anders!");
}
else if (name == "kalle")
{
    Console.WriteLine("Du heter Kalle!");
}
else
{
    Console.WriteLine("Du heter varken Fredrik eller Anders eller Kalle!");
}

//Console.WriteLine("Denna rad körs alltid på slutet!");


//if (5 + 2 == 7)
//{
//    Console.WriteLine("Sju");
//}
//else if (9 + 2 == 11)   // <= Det är skillnad på om det är else if eller bara if här.
//{
//    Console.WriteLine("Elva");
//}



// Exempel på switch

//switch (name)
//{
//    case "fredrik":
//        Console.WriteLine("Du heter Fredrik!");
//        break;
//    case "anders":
//        Console.WriteLine("Du heter Anders!");
//        break;
//    case "kalle":
//        Console.WriteLine("Du heter Kalle!");
//        break;
//    default:
//        Console.WriteLine("Du heter varken Fredrik eller Anders eller Kalle!");
//        break;
//}



Console.WriteLine("\nTernery operator: ");

string lastName; // = (name == "fredrik" ? "johansson" : "unknown");

if (name == "fredrik")
{
    lastName = "johansson";
}
else
{
    lastName = "unknown";
}


//Console.WriteLine(lastName);
//Console.WriteLine(2 < 5 ? "yes" : "no");

//int x = -5;
//Console.WriteLine("x är " + (x >= 0 ? "positivt" : "negativt"));

int numberOfCookies = 1;
Console.WriteLine("\nDu har " + numberOfCookies + (numberOfCookies == 1 ? " kaka" : " kakor"));