using Api.Core.Entities;
using Api.Core.Enums;
using System.Threading.Tasks;

namespace Api.Core.Services.Interfaces
{
    public interface IOmsSyncLogService
    {
        Task AddLogAsync(string log, OsmJobType omsJobType);
    }
}
