
Point p1 = new Point(3.0, 5.0);
Point p2 = new Point(3.0, 5.0);

// Exempel 1:
Console.WriteLine($"Är p1 och p2 lika? {p1.Equals(p2)}\n");
// struct: Ja, de har samma värde på både X och Y.
// class:  Nej, de är referenser till olika objekt - även om båda objekten har samma värde på X och Y.


// Exempel 2:
Point p3 = p2;
// struct: p3 är en kopia på värdet p2, om vi sedan ändrar X-värdet på p3 så påverkas inte värdet på p2.X
// class:  p3 är en kopia på referensen p2; så både p2 och p3 refererar nu till samma objekt på Heap.

p3.X = 4.0;

Console.WriteLine($"p1 = ({p1.X}; {p1.Y})");
Console.WriteLine($"p2 = ({p2.X}; {p2.Y})");
Console.WriteLine($"p3 = ({p3.X}; {p3.Y})");

// En struct kan - precis som en class - ha fields, properties, konstruktor, metoder etc.
// Skillnaden är att en struct definerar en value type, medan en class är en reference type.
// Prova att ändra keyword 'struct' till 'class' på nästa rad, och se hur programmet beter sig annorlunda.
struct Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point(double x, double y)
    {
        this.X = x;
        this.Y = y;
    }
}