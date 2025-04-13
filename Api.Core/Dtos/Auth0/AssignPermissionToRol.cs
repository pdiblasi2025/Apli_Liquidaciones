using System.Collections.Generic;

namespace Api.Core.Dtos
{
    public class AssignPermissionToRol
    {
       

        public List<permissionsAsigned> permissions { get; set; }
    }

    public class permissionsAsigned
    {
        public string permission_name { get; set; }

        public string resource_server_identifier { get; set; }
    }
}