using System;
using System.Threading.Tasks;

namespace Api.Core.Services.Interfaces
{
    public interface IOmsOrdenService
    {
        Task Sync(DateTime? dateFrom = null, DateTime? dateTo = null);
    }
}
