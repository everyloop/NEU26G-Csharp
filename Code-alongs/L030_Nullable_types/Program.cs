

string? name = "Fredrik";

if (name is not null)
{
    Console.WriteLine(name.Length);
}


// Null conditional operator ?. låter oss utföra en operation endast om värdet inte är null.
Console.WriteLine(name?.Length);

//name = null;

Console.WriteLine(name?.ToUpper());

// Null coalescing operator ?? låter oss ange ett alternativ värde om ett utryck är null.

string displayName = name ?? "Unknown";

Console.WriteLine($"displayName: {displayName}");

string input = Console.ReadLine() ?? "";

// null conditional och null coalescing används ofta ihop: om name inte är null => length = null.Length, annars length = 0.
int length = name?.Length ?? 0;

// Detta motsvarar:
//int length;

//if (name is null)
//{
//    length = 0;
//}
//else
//{
//    length = name.Length;
//}







int? myValueType = null;


string firstName = GetName();
string? middleName = GetName();

Console.WriteLine(firstName);

static string GetName()
{
    return null;
}