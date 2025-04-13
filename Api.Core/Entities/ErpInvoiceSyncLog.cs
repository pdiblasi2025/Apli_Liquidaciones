using System;
namespace Api.Core.Entities
{
    public class ErpInvoiceSyncLog
    {   
        public int Id { get; set; }
        public int ErpId { get; set; }
        public int LiquidacionId { get; set; }
        public string Message { get; set; }
        public DateTime SyncDate { get; set; }
    }
}
