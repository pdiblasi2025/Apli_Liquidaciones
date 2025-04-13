using System.Collections.Generic;

namespace Api.Core.Dtos
{
    public class RemoveRolesFromUserInput
    {
        public string userId { get; set; }
        public List<string> Roles { get; set; }
    }
}