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
        public DiscordEvents()
        {
            _quotesProvider = new QuotesProvider();
            _jokesProvider = new JokesProvider();
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
                // Example poll
                string question = "What's your favorite color?";
                string[] options = { "🔴 Red", "🟢 Green", "🔵 Blue" };

                // Create the embed
                var embed = new EmbedProperties
                {
                    Title = "📊 Poll",
                    Description = question,
                    Fields = new[]
                    {
            new EmbedFieldProperties { Name = "Options", Value = string.Join("\n", options), Inline = false }
                    },
                    Footer = new EmbedFooterProperties { Text = $"Poll created by {msg.Author.GlobalName ?? msg.Author.Username}" },
                    Timestamp = DateTimeOffset.Now
                };

                var pollMessage = await msg.Channel.SendMessageAsync(new MessageProperties
                {
                    Embeds = new[] { embed }
                });

                // Add reactions for voting
                foreach (var option in options)
                {
                    var emoji = option.Split(' ')[0]; // "🔴", "🟢", etc.
                    await pollMessage.AddReactionAsync(emoji);
                }
            };
            _commands["embed"] = async msg =>
            {
                var embeded = new EmbedProperties
                {
                    Title = "Hello user!",
                    Description = "This is an example embed",
                    Footer = new EmbedFooterProperties
                    {
                        Text = "This is a footer area"
                    },
                    Fields = new[]
                    {
                    new EmbedFieldProperties{Inline = false, Name ="Field 1", Value="This is the first field"},
                    new EmbedFieldProperties{Inline = true, Name ="Field 2", Value="This is the second field"},
                    },
                    Author = new EmbedAuthorProperties { Name = msg.Author.GlobalName ?? msg.Author.Username }
                };
                var message = new MessageProperties
                {
                    Embeds = new[] { embeded }
                };
                await msg.Channel.SendMessageAsync(message);
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
