using DukeBot.Features.Jokes;
using Microsoft.VisualBasic;
using NetCord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukeBot.Commands.ActionableCommands
{
    public sealed class JokesCommand : ICommand
    {
        private readonly JokesProvider _jokes;

        public JokesCommand(JokesProvider jokes)
        {
            _jokes = jokes;
        }

        public async ValueTask ExecuteAsync(Message message)
        {
            if (message.Channel is null) return;
            var joke = _jokes.GetRandomJoke();
            await message.Channel.SendMessageAsync($"{joke.Question}\n{joke.Answer}");
        }
    }

}
