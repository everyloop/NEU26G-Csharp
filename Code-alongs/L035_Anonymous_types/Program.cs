

var data = new { x = 3.0f, firstName = "Fredrik", z = true, myCat = new Cat() };

// I en array måste alla element vara samma anonyma datatyp.
var myArray = new[]
{
    new { x = 3.0f, firstName = "Fredrik", z = true, myCat = new Cat() },
    new { x = 3.5f, firstName = "Anna", z = false, myCat = new Cat() },
    new { x = 302.0f, firstName = "Maria", z = true, myCat = new Cat() }
};

var person = new
{
    name = "Anders andersson",
    age = 45,
    contactInfo = new { email = "anders@gmail.com", phone = "0702348645" }
};


Console.WriteLine(data);

Console.WriteLine();

Console.WriteLine(myArray);

Console.WriteLine();

Console.WriteLine(person);

Console.WriteLine();

Console.WriteLine(person.contactInfo.email);


class Cat() { };