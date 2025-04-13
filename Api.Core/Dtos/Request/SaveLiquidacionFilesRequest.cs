using Api.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Core.Dtos.Request
{
    public class SaveLiquidacionFilesRequest
    {
        public Liquidacion Liquidacion { get; set; }
        public bool UseInterna { get; set; }
    }
}
