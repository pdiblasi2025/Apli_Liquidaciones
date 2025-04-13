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
    public class OrderJob : IJob
    {
        private readonly MyContext _dbContext;
        private readonly IOmsOrdenService _omsOrdenService;
        private static bool _firstRun = true;

        public OrderJob(MyContext dbContext, IOmsOrdenService omsOrdenService)
        {
            _dbContext = dbContext;
            _omsOrdenService = omsOrdenService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (_firstRun)
            {
                var latestEntry = await _dbContext.DetalleLiquidacionPre
                    .AsNoTracking()
                    .OrderByDescending(x => x.CreateDate)
                    .FirstOrDefaultAsync();

                var dateFrom = latestEntry?.CreateDate.AddDays(-2) ?? DateTime.Now.AddDays(-1);

                await _omsOrdenService.Sync(dateFrom);

                _firstRun = false;
            }
            else
            {
                var dateFrom = DateTime.Now.AddDays(-2);

                await _omsOrdenService.Sync(dateFrom);
            }
        }

    }
}
