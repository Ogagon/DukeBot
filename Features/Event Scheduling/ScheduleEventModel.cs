using NetCord.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
