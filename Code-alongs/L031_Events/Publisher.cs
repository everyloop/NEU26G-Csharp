class Publisher
{
    // Definierar vilken signatur metoder som hanterar eventet måste ha.
    // Metoden måste returnera void och ta emot två parametrar:
    // objektet som signalerar eventet (sender) och information om eventet (args).
    public delegate void MessageEvent(object sender, MessageEventArgs args);

    // Deklarerar ett event som använder delegate-typen MessageEvent.
    // ? eftersom eventet kan sakna subscribers och då är null.
    public event MessageEvent? Message;

    // Istället för att definera en egen delegattyp (som ovan), så kan vi använda fördefinerade EventHandler och generisak EventHandler<T>
    //public event EventHandler? Message;
    //public event EventHandler<MessageEventArgs>? Message; 

    // Signalerar eventet och anropar alla registrerade event handlers.
    // ?. gör att Invoke endast anropas om någon prenumererar på eventet.
    // this skickas som sender eftersom detta objekt signalerar eventet.
    // EventArgs.Empty används eftersom vi inte har någon extra information
    // om eventet att skicka med.
    public void SendMessage(string message)
    {
        Message?.Invoke(this, new MessageEventArgs(message));
    }
}