using DukeBot.Commands.Command_Interfaces;
using DukeBot.Features.Event_Scheduling;
using NetCord;
using NetCord.Gateway;
using NetCord.Rest;

namespace DukeBot.Commands.ActionableCommands
{
    public sealed class ScheduleEventCommand : ICommand, ISlashCommand
    {
        private readonly ScheduleEventProvider _provider;
        private readonly GatewayClient _client;
        public ScheduleEventCommand(ScheduleEventProvider provider, GatewayClient client)
        {
            _provider = provider;
            _client = client;
        }
        public async ValueTask ExecuteAsync(Message message)
        {
            await _provider.CreateEvent(message);
        }

        public async ValueTask ExecuteAsync(SlashCommandInteraction interaction)
        {
            await interaction.SendResponseAsync(
                InteractionCallback.Message(
                    new InteractionMessageProperties
                    {
                        Content = "Let's schedule an event!",
                        Flags = MessageFlags.Ephemeral
                    }
                    )
                );
            //Dummy event creation flow
            var dmChannel = await _client.Rest.GetDMChannelAsync(interaction.User.Id);
            
            await dmChannel.SendMessageAsync("Where will the event take place?");
            string location = await WaitForUserMessageAsync(_client, interaction.User.Id, dmChannel.Id);

            await dmChannel.SendMessageAsync("What date should the event be?");
            string date = await WaitForUserMessageAsync(_client, interaction.User.Id, dmChannel.Id);

            await dmChannel.SendMessageAsync($"Event scheduled at **{location}** on **{date}**!");
        }
        private Task<string> WaitForUserMessageAsync(GatewayClient client, ulong userId, ulong channelId)
        {
            var tcs = new TaskCompletionSource<string>();

            ValueTask Handler(Message msg)
            {
                if (msg.Author.Id == userId && msg.ChannelId == channelId)
                    tcs.TrySetResult(msg.Content);
                return ValueTask.CompletedTask;
            }

            client.MessageCreate += Handler;

            tcs.Task.ContinueWith(_ => client.MessageCreate -= Handler);

            return tcs.Task;
        }
    }
}
