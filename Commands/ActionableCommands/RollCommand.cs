using NetCord.Gateway;

namespace DukeBot.Commands.ActionableCommands
{
    public sealed class RollCommand : ICommand
    {
        private readonly Random _random = new Random();
        public async ValueTask ExecuteAsync(Message message)
        {
            if (message.Channel is null) return;
            await message.Channel.SendMessageAsync($"{_random.Next() % 100}");
        }
    }
}
