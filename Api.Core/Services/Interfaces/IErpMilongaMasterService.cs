using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services.Interfaces
{
    public interface IErpMilongaMasterService
    {
        Task SyncMasters();
    }
}
