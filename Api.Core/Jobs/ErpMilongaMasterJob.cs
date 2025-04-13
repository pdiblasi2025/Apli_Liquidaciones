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
    public class ErpMilongaMasterJob : IJob
    {
        private readonly IErpMilongaMasterService _erpMilongaService;

        public ErpMilongaMasterJob(IErpMilongaMasterService erpMilongaService)
        {
            _erpMilongaService = erpMilongaService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _erpMilongaService.SyncMasters();
        }

    }
}
