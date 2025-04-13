using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class Identity
    {
        public string connection { get; set; }
        public string provider { get; set; }
        public string user_id { get; set; }
        public bool isSocial { get; set; }
    }

    public class UserMetadata
    {
        public UserMetadata()
        {
        }

        public int company_id { get; set; }

 
        public bool is_owner { get; set; }

        public string roles { get; set; }
    }

    public class AppMetadata
    {
        public List<string> roles { get; set; }
    }

    public class AuthUser
    {
        public AuthUser()
        {
            user_metadata = new UserMetadata();
        }

        public string roles { get; set; }
        public DateTime created_at { get; set; }
        public string email { get; set; }
        public bool email_verified { get; set; }
        public List<Identity> identities { get; set; }
        public string name { get; set; }
        public string nickname { get; set; }
        public string picture { get; set; }
        public DateTime updated_at { get; set; }
        public string user_id { get; set; }
        public UserMetadata user_metadata { get; set; }
        public AppMetadata app_metadata { get; set; }
        public string last_ip { get; set; }
        public DateTime last_login { get; set; }
        public int logins_count { get; set; }
        public bool blocked { get; set; }
        public List<object> blocked_for { get; set; }
        public List<object> guardian_authenticators { get; set; }
        public string given_name { get; set; }

        public string family_name { get; set; }
    }
}
