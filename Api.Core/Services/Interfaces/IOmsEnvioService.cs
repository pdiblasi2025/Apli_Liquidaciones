using System;
using System.Threading.Tasks;

namespace Api.Core.Services.Interfaces
{
    public interface IOmsEnvioService
    {
        Task Sync(DateTime dateFrom, DateTime? dateTo = null);
    }
}
