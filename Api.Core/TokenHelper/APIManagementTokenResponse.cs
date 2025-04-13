using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.TokenHelper
{
    public class APIManagementTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
    }
}
