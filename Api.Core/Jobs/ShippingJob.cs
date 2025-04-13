using Api.Core.Repositories;
using Api.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Jobs
{
    [DisallowConcurrentExecution]
    public class ShippingJob : IJob
    {
        private readonly MyContext _dbContext;
        private readonly IOmsEnvioService _omsEnvioService;
        private static bool _firstRun = true;

        public ShippingJob(MyContext dbContext, IOmsEnvioService omsEnvioService)
        {
            _dbContext = dbContext;
            _omsEnvioService = omsEnvioService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (_firstRun)
            {
                var latestEntry = await _dbContext.DetalleLiquidacionPos
                    .AsNoTracking()
                    .OrderByDescending(x => x.CreateDate)
                    .FirstOrDefaultAsync();

                var dateFrom = latestEntry?.CreateDate.AddDays(-2) ?? DateTime.Now.AddDays(-1);

                await _omsEnvioService.Sync(dateFrom);

                _firstRun = false;
            }
            else
            {
                var dateFrom = DateTime.Now.AddDays(-2);

                await _omsEnvioService.Sync(dateFrom);
            }
        }

    }
}
