using DukeBot.Quotes;
using NetCord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
