using System;

namespace Api.Core.Dtos.Request
{
    public class SyncOmsEnvioRequest
    {
        public DateTime DateTimeFrom { get; set; }

        public DateTime? DateTimeTo { get; set; }
    }
}
