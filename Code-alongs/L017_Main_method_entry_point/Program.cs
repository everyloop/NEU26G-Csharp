
internal class Program
{
    // När .NET runtime laddat in ditt program kommer den leta igenom alla klasser tills den hittar
    // en statisk metod med namnet "Main". Denna används som entry point till ditt program genom att
    // runtien anropar metoden, och skickar med de argument som användes i command line vid start.

    // Det måsta alltså finnas exakt en (1) statisk metod men namn "Main".
    private static void Main(string[] args)
    {
        if (args.Length == 2)
        {
            string source = args[0];
            string destination = args[1];
        }

        Console.WriteLine($"Number of arguments: {args.Length}");

        foreach (var argument in args)
        {
            Console.WriteLine(argument);
        }


        // Detta är inte en metod, d.v.s det är ingen member på klassen "Program".
        // Detta är istället en så kallad "local function" som man kan bädda in i en metod
        // och som endast kan anropas inifrån metoden den ligger i.

        // För en "local function" har keyword static en något annorlunda betydelse:
        // Om den är static kan den inte komma åt variabler som är definerade i den omslutande metoden.

        int x = 5;

        NonStaticLocalFunctionPrintHello();
        StaticLocalFunctionPrintHello(3);

        static void StaticLocalFunctionPrintHello(int x)
        {
            Console.WriteLine(x);
        }

        void NonStaticLocalFunctionPrintHello()
        {
            Console.WriteLine(x);
        }

        StaticMethodPrintHello();
    }

    // Eftersom Main() är statisk så kan den bara anropa andra metoder i klassen som också är statiska.
    private static void StaticMethodPrintHello()
    {
        Console.WriteLine("Hello");
    }

}

// I C# 9.0 introducerades dock top level statements, som gör att man kan ha en (1) fil i sitt projekt
// där kod inte ligger i en klass. D.v.s. filen med top level statmenets kan t.ex ha endast en (exempel)
// Console.WriteLine() och ändå fungera; Det kompilatorn gör när den hittar top level statments är att
// den automatiskt skapar en klass med en Main() metod vid kompilering och lägger alla top level statements
// i denna. Det blir alltså ingen skillnad i det färdigbyggda programmet oavsett om man använder sig av
// top level statements eller inte.


