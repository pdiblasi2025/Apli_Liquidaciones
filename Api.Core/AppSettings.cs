using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core
{
    public class AppSettings
    {
        public Dictionary<string, string> ConnectionStrings { get; set; }
        public string ApiKey { get; set; }
        public string Cors { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string NameSpace { get; set; }
    }
}
