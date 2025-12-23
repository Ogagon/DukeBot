using NetCord.Gateway;

namespace DukeBot.Commands.ActionableCommands
{
    public sealed class PongCommand : ICommand
    {
        public async ValueTask ExecuteAsync(Message message)
        {
            if (message.Channel is null) return;
            await message.Channel.SendMessageAsync("Ping!");
        }
    }
}
