using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class AsignacionModel
    {
        public List<DtoUser> Users { get; set; }
        public List<DtoRole> Roles { get; set; }

        public UserRoles SelectedUser { get; set; }
    }
    public class DtoRole
    {
       public    string id { get; set; }
        public string name  { get; set; }
        public    string description { get; set; }
        public string text { get; set; }

        public string value { get; set; }
    }

    
    public class DtoUser
    {
            public string userId { get; set; }
            public string UserEmail { get; set; }
            public string userName { get; set; }
             public string text { get; set; }

        public string value { get; set; }

    }
 

    public class UserRoles
    {
        public string userId { get; set; }
        public List<string>Roles { get; set; }
    }
}
