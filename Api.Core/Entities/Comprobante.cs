namespace Api.Core.Entities
{
    public class Comprobante : EntityBase<int>
    {
        public string Nombre { get; set; }

        public byte[] Archivo { get; set; }

        public int LiquidacionId { get; set; }

        public Liquidacion Liquidacion { get; set; }

        public int Posicion { get; set; }
    }
}
