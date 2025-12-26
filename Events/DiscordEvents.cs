using DukeBot.Commands;
using NetCord;
using NetCord.Gateway;
using NetCord.Rest;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace DukeBot.Events
{
#pragma warning disable CS8602
    public class DiscordEvents
    {
        private readonly CommandRouter _router;
        private readonly SlashCommandRouter _slashRouter;
        private ulong _guildId;
        public DiscordEvents(CommandRouter router, SlashCommandRouter slashRouter)
        {
            _router = router;
            _slashRouter = slashRouter;
        }
        public ValueTask OnReadyAsync(ReadyEventArgs args)
        {
            Console.WriteLine($"Logged in as {args.User.Username}");
            _guildId = 1450445272961519818;
            return ValueTask.CompletedTask;
        }

        public async ValueTask OnMessageCreateAsync(Message message)
        {
            if (message.Author.IsBot || message.Channel is null)
                return;

            await _router.RouteAsync(message);
        }
        public async ValueTask OnInteractionCreate(Interaction interaction)
        {
            if (interaction.User is null || interaction.Channel is null || interaction.User.IsBot) return;
            await _slashRouter.RouteAsync(interaction);
        }

        public async Task RegisterSlashCommandsAsync(GatewayClient client, ulong appId)
        {
            await client.Rest.CreateGlobalApplicationCommandAsync(
                appId,
                new SlashCommandProperties(
                    "schedule",
                    "Schedule an event"
                )
                {
                }
            );
            await client.Rest.CreateGuildApplicationCommandAsync(
                appId,
                _guildId,
                new SlashCommandProperties(
                    "schedule",
                    "Schedule an event"
                )
            );
        }
    }
}
