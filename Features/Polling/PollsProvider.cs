using NetCord.Gateway;
using NetCord.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DukeBot.Features.Polling
{
    public class PollsProvider
    {
        public async Task CreatePoll(Message msg)
        {
            if (msg.Channel is null) return;

            // Example poll
            string question = "What's your favorite color?";
            string[] options = { "🔴 Red", "🟢 Green", "🔵 Blue" };

            // Create the embed
            var embed = new EmbedProperties
            {
                Title = "📊 Poll",
                Description = question,
                Fields = new[]
                {
                        new EmbedFieldProperties { Name = "Options", Value = string.Join("\n", options), Inline = false }
                    },
                Footer = new EmbedFooterProperties { Text = $"Poll created by {msg.Author.GlobalName ?? msg.Author.Username}" },
                Timestamp = DateTimeOffset.Now
            };

            var pollMessage = await msg.Channel.SendMessageAsync(new MessageProperties
            {
                Embeds = new[] { embed }
            });

            // Add reactions for voting
            foreach (var option in options)
            {
                var emoji = option.Split(' ')[0]; // "🔴", "🟢", etc.
                await pollMessage.AddReactionAsync(emoji);
            }
        }
    }
}
