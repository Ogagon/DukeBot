using NetCord.Rest;

namespace DukeBot.Features.Event_Scheduling
{
    public class ScheduleEventModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<EmbedFieldProperties>? Fields { get; set; }
        public EmbedFooterProperties? Footer { get; set; }

    }
}
