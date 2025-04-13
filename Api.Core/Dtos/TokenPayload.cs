using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class TokenPayload
    { 
            public string iss { get; set; }
            public string sub { get; set; }
            public List<string> aud { get; set; }
            public int iat { get; set; }
            public int exp { get; set; }
            public string azp { get; set; }
            public string scope { get; set; }
            public List<string> permissions { get; set; }
       
    }
}
