using Api.Core.Entities;
using Api.Core.Enums;
using Api.Core.Repositories;
using Api.Core.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Api.Core.Services.Implementations
{
    public class OmsSyncLogService : IOmsSyncLogService
    {
        private readonly MyContext _context;
        public OmsSyncLogService(MyContext context)
        {
            _context = context;
        }

        public async Task AddLogAsync(string log, OsmJobType omsJobType)
        {
            var entity = new OmsSyncLog
            {
                Log = log,
                Date = DateTime.Now,
                JobType = omsJobType
            };

            _context.OmsSyncLogs.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
