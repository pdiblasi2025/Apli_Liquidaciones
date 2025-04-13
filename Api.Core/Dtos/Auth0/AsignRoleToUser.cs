using System.Collections.Generic;

namespace Api.Core.Dtos
{
    public class AsignRoleToUser
    {
      public  string userId { get; set; }
      public  List<string> roles { get; set; }
    
    }

    public class AsignRoleToUserAuth0
    {
        public List<string> roles { get; set; }

    }
}