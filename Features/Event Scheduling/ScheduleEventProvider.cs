using NetCord.Gateway;
using NetCord.Rest;

namespace DukeBot.Features.Event_Scheduling
{
    public class ScheduleEventProvider
    {
        public async Task CreateEvent(Message msg)
        {
            
            if (msg.Channel is null) return;
            var embeded = new EmbedProperties
            {
                Title = "Hello user!",
                Description = "This is an example embed",
                Timestamp = DateTimeOffset.UtcNow,
                Footer = new EmbedFooterProperties
                {
                    Text = "This is a footer area"
                },
                Fields = new[]
    {
                    new EmbedFieldProperties{Inline = false, Name ="Field 1", Value="This is the first field"},
                    new EmbedFieldProperties{Inline = true, Name ="Field 2", Value="This is the second field"},
                    },
                Author = new EmbedAuthorProperties { Name = msg.Author.GlobalName ?? msg.Author.Username }
            };
            var message = new MessageProperties
            {
                Embeds = new[] { embeded }
            };
            await msg.Channel.SendMessageAsync(message);

        }
    }
}
