using DukeBot.Commands;
using NetCord.Gateway;

namespace DukeBot.Events
{
#pragma warning disable CS8602
    public class DiscordEvents
    {
        private readonly CommandRouter _router;
        public DiscordEvents(CommandRouter router)
        {
            _router = router;
        }
        public ValueTask OnReadyAsync(ReadyEventArgs args)
        {
            Console.WriteLine($"Logged in as {args.User.Username}");
            return ValueTask.CompletedTask;
        }

        public async ValueTask OnMessageCreateAsync(Message message)
        {
            if (message.Author.IsBot || message.Channel is null)
                return;

            await _router.RouteAsync(message);
        }
    }
}
