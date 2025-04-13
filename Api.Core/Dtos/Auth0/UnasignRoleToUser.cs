using System.Collections.Generic;

namespace Api.Core.Dtos
{
    public class UnasignRoleToUser
    { 
            public string userId { get; set; }
            public List<string> roles { get; set; }
 
    }
}