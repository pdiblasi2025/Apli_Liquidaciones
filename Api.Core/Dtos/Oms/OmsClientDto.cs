namespace Api.Core.Dtos.Oms
{
    public class OmsClientDto
    {
        public int id { get; set; }
        public string company_name { get; set; }
        public string client_cuit { get; set; }
        //PSD
        public string cuit { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool? prepaid { get; set; }
        public bool active { get; set; }
        public string username { get; set; }
    }

}
