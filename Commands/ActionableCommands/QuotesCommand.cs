using DukeBot.Features.Quotes;
using NetCord.Gateway;

namespace DukeBot.Commands.ActionableCommands
{
    public sealed class QuotesCommand : ICommand
    {
        private readonly QuotesProvider _quotes;
        public QuotesCommand(QuotesProvider quotes)
        {
            _quotes = quotes;
        }
        public async ValueTask ExecuteAsync(Message message)
        {
            if (message.Channel is null) return;
            string quote = _quotes.GetRandomQuote();
            await message.Channel.SendMessageAsync(quote);
        }
    }
}
