// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;

Console.WriteLine("Dispatcher is runnig");
while (true)
{
    Console.WriteLine("1-Trigger driver module");
    Console.WriteLine("2-Trigger passenger module");
    Console.WriteLine("3-Trigger both");
    Console.Write("select option:");
    var key = Console.ReadKey();
    switch (key.Key)
    {
        case ConsoleKey.NumPad1:
        case ConsoleKey.D1:
            TriggerDriver("hello Driver").Wait();
            break;

        case ConsoleKey.NumPad2:
        case ConsoleKey.D2:
            TriggerPassanger("hello passenger").Wait();
            break;

        case ConsoleKey.NumPad3:
        case ConsoleKey.D3:
            TriggerPassanger("hello passenger").Wait();
            TriggerDriver("hello Driver").Wait();
            break;

        case ConsoleKey.Escape:
            return;

        default:
            continue;

    }
    Console.WriteLine("press any key to continue");
    Console.ReadKey();
}

static async Task TriggerPassanger(string Message)
{
    await Task.Delay(10);
    var messageBroker = ConnectionMultiplexer.Connect("localhost:6379");
    var pubsub = messageBroker.GetSubscriber();
    pubsub.Publish("Passenger", Message);
}

static async Task TriggerDriver(string Message)
{
    await Task.Delay(10);
    var messageBroker = ConnectionMultiplexer.Connect("localhost:6379");
    var pubsub = messageBroker.GetSubscriber();
    pubsub.Publish("Driver", Message);
}