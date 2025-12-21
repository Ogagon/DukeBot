using NetCord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukeBot.Commands
{
    public interface ICommand
    {
        ValueTask ExecuteAsync(Message message);
    }
}
