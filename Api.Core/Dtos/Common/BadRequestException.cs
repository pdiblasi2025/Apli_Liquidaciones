using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.Common
{
    public class BadRequestException : Exception
    {
        public Result Result { get; set; }

        public BadRequestException(Result result)
            : base("Bad request exception")
        {
            Result = result;
        }
    }
}
