using Api.Core.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.Filter
{
    public class FilterLicencia : FilterBase
    {
        public int idCompania { get; set; }
        public int idPlanes { get; set; }
    }
}
