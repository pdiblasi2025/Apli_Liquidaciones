using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.Filter
{
    public class FilterLocalidad : FilterBase
    {
        public int? ProvinciaId { get; set; }
    }
}
