using NetCord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukeBot.Commands
{
    public sealed class CommandRouter
    {
        private readonly Dictionary<string, ICommand> _commands = new();

        public void Register(string name, ICommand command)
            => _commands[name] = command;

        public async ValueTask RouteAsync(Message message)
        {
            if (!message.Content.StartsWith("!")) return;

            var parts = message.Content[1..].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return;

            if (_commands.TryGetValue(parts[0].ToLower(), out var command))
            {
                await command.ExecuteAsync(message);
            }
        }
    }

}
