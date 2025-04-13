using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class CreateUserRequest
    {
        public string email { get; set; }
        public bool blocked { get; set; }
        public bool email_verified { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string name { get; set; }
        public string nickname { get; set; }
        public string picture { get; set; }
        public string user_id { get; set; }
        public string connection { get; set; }
        public string password { get; set; }
        public bool verify_email { get; set; }
    }
}
