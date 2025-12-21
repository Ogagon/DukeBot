using DukeBot.Features.Event_Scheduling;
using NetCord.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukeBot.Commands.ActionableCommands
{
    public sealed class ScheduleEventCommand : ICommand
    {
        private readonly ScheduleEventProvider _provider;
        public ScheduleEventCommand(ScheduleEventProvider provider)
        {
            _provider = provider;
        }
        public async ValueTask ExecuteAsync(Message message)
        {
            await _provider.CreateEvent(message);
        }
    }
}
