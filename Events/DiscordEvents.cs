using DukeBot.Commands;
using DukeBot.Features.Event_Scheduling;
using DukeBot.Features.Polling;
using DukeBot.Jokes;
using DukeBot.Quotes;
using NetCord.Gateway;
using NetCord.Rest;

namespace DukeBot.Events
{
#pragma warning disable CS8602
    public class DiscordEvents
    {
        private readonly Dictionary<string, Func<Message, ValueTask>> _commands
        = new();
        private QuotesProvider _quotesProvider;
        private JokesProvider _jokesProvider;
        private static Random _random = new Random();
        private PollsProvider _pollsProvider;
        private ScheduleEventProvider _scheduleEventsProvider;
        private readonly CommandRouter _router;
        public DiscordEvents()
        {
            _quotesProvider = new QuotesProvider();
            _jokesProvider = new JokesProvider();
            _pollsProvider = new PollsProvider();
            _scheduleEventsProvider = new ScheduleEventProvider();
        }
        public void RegisterCommands()
        {
            _commands["quote"] = async msg =>
            {
                string quote = _quotesProvider.GetRandomQuote();
                await msg.Channel.SendMessageAsync(quote);
            };
            _commands["poll"] = async msg =>
            {
                await _pollsProvider.CreatePoll(msg);
            };
            _commands["embed"] = async msg =>
            {
                await _scheduleEventsProvider.CreateEvent(msg);
            };
            _commands["joke"] = async msg =>
            {
                var joke = _jokesProvider.GetRandomJoke();
                await msg.Channel.SendMessageAsync($"{joke.Question}\n{joke.Answer}");
            };
            _commands["pong"] = async msg => await msg.Channel.SendMessageAsync("Ping!");
            _commands["roll"] = async msg => await msg.Channel.SendMessageAsync($"{_random.Next() % 100}");
            _commands["ping"] = async msg => await msg.Channel.SendMessageAsync("Pong!");
            _commands["hello"] = async msg => await msg.Channel.SendMessageAsync($"Hello {msg.Author.GlobalName ?? msg.Author.Username}!");
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
