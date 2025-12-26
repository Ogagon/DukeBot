using DukeBot.Commands.Command_Interfaces;
using NetCord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukeBot.Commands
{
    public class SlashCommandRouter
    {
        private readonly Dictionary<string, ISlashCommand> _commands = new();
        private readonly Dictionary<string, IAutocompleteProvider> _autocomplete = new();
        public void Register(string name, ISlashCommand command) => _commands[name] = command;
        public void RegisterAutocomplete(string name, IAutocompleteProvider provider) => _autocomplete[name] = provider;
        public async ValueTask RouteAsync(Interaction interaction)
        {
            switch (interaction)
            {
                case SlashCommandInteraction slash:
                    if (_commands.TryGetValue(slash.Data.Name, out var command))
                    {
                        await command.ExecuteAsync(slash);
                    }
                    break;
                case AutocompleteInteraction autocomplete:
                    if (_autocomplete.TryGetValue(autocomplete.Data.Name, out var provider)){
                        await provider.HandleAsync(autocomplete);
                    }
                    break;
            }
        }
    }
}
