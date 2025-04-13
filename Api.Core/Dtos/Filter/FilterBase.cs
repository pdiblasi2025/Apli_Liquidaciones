using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos 
{
    public  abstract  class FilterBase
    {
        public int company_id { get; set; }
        public string MultiColumnSearchText { get; set; }
        public int? PageSize { get; set; }
        public int? CurrentPage { get; set; }
    }
}
