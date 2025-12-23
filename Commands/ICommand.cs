using NetCord.Gateway;

namespace DukeBot.Commands
{
    public interface ICommand
    {
        ValueTask ExecuteAsync(Message message);
    }
}
