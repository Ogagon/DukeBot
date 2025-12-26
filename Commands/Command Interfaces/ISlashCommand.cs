using NetCord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukeBot.Commands.Command_Interfaces
{
    public interface ISlashCommand
    {
        ValueTask ExecuteAsync(SlashCommandInteraction interaction);
    }
}
