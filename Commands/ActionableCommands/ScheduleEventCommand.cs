using DukeBot.Features.Event_Scheduling;
using NetCord.Gateway;

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
