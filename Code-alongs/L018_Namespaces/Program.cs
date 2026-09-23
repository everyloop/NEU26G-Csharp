
// Namespace används för att organisera klasser och andra definitioner i olika områden ("mappar").
// Detta används för att namnen inte ska korocka, och för att organisera sin kod.

// Jämför med hur mappar används i filsystemet för att organisera filer, doch för att filnamn inte ska krocka.

// Using används högst upp i filer för att ange vilka namespace som används i filen.

using MyNameSpace.MyOtherNameSpace;
// using MyNameSpace;  // <= Om denna också används har vi två klasser "Car" i olika namespaces som krockar.

// Utan using behöver man skriva hela "sökvägen"
MyNameSpace.MyOtherNameSpace.Car.Engine = null;

// ... men eftersom vi använder using MyNameSpace.MyOtherNameSpace; ovan så räcker:
Car.Engine = null;

// File scoped name space - Det vanliga i modern C#; anger namespace för allt innehåll i filen (behöver ligga överst i filen)
// namespace MyNameSpace.MyOtherNameSpace;

// Block scoped name space
namespace MyNameSpace
{
    class Car
    {
        public static string Model;
    }

    // En name space kan ligga i en annan name space, men gör det inte med denna syntax (nested code blocks)
    namespace MyOtherNameSpace
    {
        class Car
        {
            public static string Engine;
        }

    }
}

// .. utan på detta sättet; Både Boat och klassen Car ovan ligger i samma namespace: MyNameSpace.MyOtherNameSpace
namespace MyNameSpace.MyOtherNameSpace
{
    class Boat
    {

    }
}
