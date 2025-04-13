using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    
        public class PermisosDto 
        {
            public string id { get; set; }
            public string name { get; set; }
            public string identifier { get; set; }
            public bool allow_offline_access { get; set; }
            public bool skip_consent_for_verifiable_first_party_clients { get; set; }
            public int token_lifetime { get; set; }
            public int token_lifetime_for_web { get; set; }
            public string signing_alg { get; set; }
            public List<Scope> scopes { get; set; }
            public bool enforce_policies { get; set; }
            public string token_dialect { get; set; }
        }
        public class Scope
        {
            public string value { get; set; }
            public string description { get; set; }
        }

   
}
