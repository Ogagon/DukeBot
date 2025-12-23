using DukeBot.Features.Polling;
using NetCord.Gateway;

namespace DukeBot.Commands.ActionableCommands
{
    public sealed class PollsCommand : ICommand
    {
        private readonly PollsProvider _polls;

        public PollsCommand(PollsProvider polls)
        {
            _polls = polls;
        }

        public async ValueTask ExecuteAsync(Message message)
        {
            await _polls.CreatePoll(message);
        }
    }

}
