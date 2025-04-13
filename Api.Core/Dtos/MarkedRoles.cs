using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos
{
    public class RoleEnable
    {
        public string userId { get; set; }

        public string userEmail { get; set; }
        public string roleId { get; set; }
        public string roleName { get; set; }

        public bool Enable { get; set; }
    }
}