using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Dtos.Response
{
    public class ResultMessage<T>
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        public T Content { get; set; }
    }
}
