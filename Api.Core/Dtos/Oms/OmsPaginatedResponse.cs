using System.Collections.Generic;

namespace Api.Core.Dtos.Oms
{
    public class OmsPaginatedResponse<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<T> results { get; set; }
        public Pagination pagination { get; set; }
    }

    public class Pagination
    {
        public int total { get; set; }
        public int pages { get; set; }
        public int current { get; set; }
        public int limit { get; set; }
    }
}
