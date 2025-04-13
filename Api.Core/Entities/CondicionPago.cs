namespace Api.Core.Entities
{
    public class CondicionPago
    {
        public int Id { get; set; }
        public string ErpId { get; set; }
        public string Nombre { get; set; }
        public int TerminoPago { get; set; }
    }
}
