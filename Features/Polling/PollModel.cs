using NetCord.Rest;

namespace DukeBot.Features.Polling
{
    public class PollModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<EmbedFieldProperties>? Fields { get; set; }
        public EmbedFooterProperties? Footer { get; set; }
        public EmbedAuthorProperties? Author { get; set; }
    }
}
