class Subscriber
{
    // Namnet används för att identifiera vilken subscriber som reagerar på eventet.
    private string name;

    public Subscriber(string name)
    {
        this.name = name;
    }

    // Event handler för Message-eventet.
    // Metodens signatur måste matcha delegate-typen MessageEvent.
    //
    // sender är objektet som signalerade eventet.
    // args innehåller eventuell information om eventet.
    public void OnMessageRecieved(object sender, MessageEventArgs args)
    {
        // Körs när Publisher signalerar Message-eventet.
        Console.WriteLine($"{name} got the message: {args.Message}");
    }
}