using System.Collections.Generic;

namespace Api.Core.Dtos
{
    public class UnassignPermissionToRol
    { 
            public string rolId { get; set; }

            public List<string> permissions { get; set; }
         
    }
}