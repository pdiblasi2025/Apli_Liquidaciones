using Api.Core.Enums;
using System;

namespace Api.Core.Entities
{
    public class OmsSyncLog
    {
        public int Id { get; set; }
        public string Log { get; set; }
        public DateTime Date { get; set; }
        public OsmJobType JobType { get; set; }
    }
}
