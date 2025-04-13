namespace Api.Core.Dtos
{
    public class FilterAuth
    { 
            public int company_id { get; set; }
            public string MultiColumnSearchText { get; set; }
            public int? PageSize { get; set; }
            public int? CurrentPage { get; set; }
        
    }
}