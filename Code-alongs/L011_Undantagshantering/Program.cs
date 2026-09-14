
// OverflowException (Kastas bara om en overflow omges av ett checked-block.)
//checked
//{
//    byte myByte = 200;
//    myByte += 100;
//    Console.WriteLine(myByte);
//}

// IndexOutOfRangeException
//int[] myArray = new int[10];
//myArray[51] = 3;

// FormatException
//Int32.Parse("Fem");

// DivideByZeroException
//int x = 0;
//int y = 1 / x;

static void FunctionB()
{
    int number = int.Parse(Console.ReadLine());

    Console.WriteLine(2 / number);

    //int[] myArray = new int[10];
    //myArray[51] = 3;
}

static void FunctionA()
{
    Console.Write("Mata in ett tal:");

    try
    {
        FunctionB();
    }
    //catch (FormatException ex)
    //{
    //    Console.WriteLine("Du måste skriva ett heltal.");
    //    Console.WriteLine(ex.Message);
    //}
    //catch (DivideByZeroException ex)
    //{
    //    Console.WriteLine("Du får inte skriva talet 0.");
    //}
    catch (Exception ex)
    {
        //Console.WriteLine("Något annat konstigt gick fel!");
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
    }
    finally
    {
        Console.WriteLine("Denna kod körs alltid");
    }

    Console.WriteLine("The end!");
}

FunctionA();