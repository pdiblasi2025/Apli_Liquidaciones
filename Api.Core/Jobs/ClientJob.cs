using Api.Core.Dtos.Oms;
using Api.Core.Entities;
using Api.Core.Enums;
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
    public class ClientJob : IJob
    {
        private readonly MyContext _dbContext;
        private readonly IOmsService _omsService;
        private readonly IOmsSyncLogService _omsSyncLogService;

        public ClientJob(MyContext dbContext, IOmsService omsService, IOmsSyncLogService omsSyncLogService)
        {
            _dbContext = dbContext;
            _omsService = omsService;
            _omsSyncLogService = omsSyncLogService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _omsSyncLogService.AddLogAsync($"Oms Client Sync process triggered", OsmJobType.Client);

            try
            {
                var currentClients = await _dbContext.Clientes.ToListAsync();

                var omsClients = await _omsService.GetAllClientsAsync();

                omsClients = omsClients.Where(x => x.active).ToList();

                var newOmsClients = omsClients.Where(x => !currentClients.Any(y => y.OmsId == x.id));

                var clientsToInsert = newOmsClients.Where(x => x.prepaid != null)
                    .Select(x => new Cliente
                    {
                        OmsId = x.id,
                        RazonSocial = x.company_name,
                        NumeroDeDocumento = x.client_cuit,
                        TipoCliente = x.prepaid.Value ? TipoCliente.Prepago : TipoCliente.Pospago,
                        CreateDate = DateTime.Now,
                        CreatedBy = "ClientJob",
                        Enabled = true,
                        Deleted = false
                    }).ToList();

                if (clientsToInsert.Any())
                {
                    await _omsSyncLogService.AddLogAsync($"{clientsToInsert.Count} client(s) will be added", OsmJobType.Client);
                    _dbContext.AddRange(clientsToInsert);
                }

                await _dbContext.SaveChangesAsync();

                await _omsSyncLogService.AddLogAsync("Oms Client Sync process finished", OsmJobType.Client);
            }
            catch (Exception ex)
            {
                await _omsSyncLogService.AddLogAsync($"Error when running Oms Client Sync process - {ex.Message}", OsmJobType.Client);
            }
        }
    }
}
