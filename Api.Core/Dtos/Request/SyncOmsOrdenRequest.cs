using System;

namespace Api.Core.Dtos.Request
{
    public class SyncOmsOrdenRequest
    {
        public DateTime? DateTimeFrom { get; set; }

        public DateTime? DateTimeTo { get; set; }
    }
}
