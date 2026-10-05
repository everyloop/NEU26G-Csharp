

var publisher = new Publisher();

var subscriber1 = new Subscriber("Adam");
var subscriber2 = new Subscriber("Eva");
var subscriber3 = new Subscriber("Daniel");

// Prenumererar på Message-eventet.
// Metoderna läggs till i eventets invocation list.
publisher.Message += subscriber1.OnMessageRecieved;
publisher.Message += subscriber2.OnMessageRecieved;
publisher.Message += subscriber3.OnMessageRecieved;

// Event (till skilnad från delegat) tillåter inte enskilda subscribers att "ta över" hela subscription listan
// publisher.Message = subscriber3.OnMessageRecieved;

// Till skillnad från rena delegat, så tillåter inte event att någon utanför klassen där eventet defineras gör invoke.
// publisher.Message.Invoke(null, EventArgs.Empty);

// Publisher signalerar eventet.
// Alla tre registrerade event handlers anropas.
publisher.SendMessage("This is the first message.");

Console.WriteLine();

// Eva avprenumererar från eventet.
// Hennes metod tas bort från invocation list.
publisher.Message -= subscriber2.OnMessageRecieved;

// Signalerar eventet igen.
// Nu anropas endast Adams och Daniels event handlers.
publisher.SendMessage("Another message!!!");






