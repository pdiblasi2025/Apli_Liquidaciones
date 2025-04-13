using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.Request
{
    public class BlockUserRequest
    {
        public string user_id { get; set; }

        public bool block { get; set; }
    }
}
