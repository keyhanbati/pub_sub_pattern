using StackExchange.Redis;

namespace Subscribers;
public class Subscribe
{
    public Subscribe(string SubString)
    {
        var messageBroker = ConnectionMultiplexer.Connect("localhost:6379");
        var pubsub = messageBroker.GetSubscriber();
        pubsub.Subscribe(SubString, (channel, message) => MessageAction(message));
    }

    void MessageAction(RedisValue message)
    {
        Console.WriteLine($"message is: {message}");
    }
}
