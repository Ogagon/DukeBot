using DukeBot.Quotes;
using NetCord.Gateway;

namespace DukeBot.Events
{
#pragma warning disable CS8602
    public class DiscordEvents
    {
        private readonly Dictionary<string, Func<Message, ValueTask>> _commands
        = new();
        public void RegisterCommands()
        {
            _commands["quote"] = async msg =>
            {
                QuotesProvider quotes = new QuotesProvider();
                string quote = quotes.GetRandomQuote();
                await msg.Channel.SendMessageAsync(quote);
            };
            _commands["pong"] = async msg => await msg.Channel.SendMessageAsync("Ping!");
            _commands["roll"] = async msg => await msg.Channel.SendMessageAsync($"{new Random().Next() % 100}");
            _commands["ping"] = async msg => await msg.Channel.SendMessageAsync("Pong!");
            _commands["hello"] = async msg => await msg.Channel.SendMessageAsync($"Hello {msg.Author.GlobalName}!");
        }
        public ValueTask OnReadyAsync(ReadyEventArgs args)
        {
            Console.WriteLine($"Logged in as {args.User.Username}");
            return ValueTask.CompletedTask;
        }

        public async ValueTask OnMessageCreateAsync(Message message)
        {
            if (message.Author.IsBot || message.Channel is null) return;

            // simple prefix parsing
            if (!message.Content.StartsWith("!")) return;

            var parts = message.Content[1..].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0) return;

            var command = parts[0].ToLower();
            if (_commands.TryGetValue(command, out var handler))
            {
                try
                {
                    await handler(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in command {command}: {ex}");
                }
            }
        }
    }
}
