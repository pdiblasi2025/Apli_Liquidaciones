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
                        NombreUsuario = x.username,
                        CreateDate = DateTime.Now,
                        CreatedBy = "PsdClientJob",
                        Enabled = true,
                        Deleted = false 
                        
                        //PSD
                        //NumeroDeDocumento = x.cuit,
                        //Nombre = x.first_name,
                        //Apellido = x.last_name,
                        //Email = x.email,
                        //Telefono = x.phone,
                        

                    }).ToList();

                if (clientsToInsert.Any())
                {
                    await _omsSyncLogService.AddLogAsync($"{clientsToInsert.Count} client(s) will be added", OsmJobType.Client);
                    _dbContext.AddRange(clientsToInsert);
                }
                //PSD
                //int clientsToUpdateCount = 0;

                //foreach (var currentClient in currentClients)
                //{
                //    var omsClient = omsClients.FirstOrDefault(x => x.id == currentClient.OmsId && x.prepaid.HasValue);

                //    if (omsClient == null)
                //        continue;

                //    if (HasClientChanged(currentClient, omsClient))
                //    {
                //        currentClient.RazonSocial = omsClient.company_name;
                //        currentClient.NumeroDeDocumento = omsClient.cuit;
                //        currentClient.Nombre = omsClient.first_name;
                //        currentClient.Apellido = omsClient.last_name;
                //        currentClient.Email = omsClient.email;
                //        currentClient.Telefono = omsClient.phone;
                //        currentClient.TipoCliente = omsClient.prepaid.Value ? TipoCliente.Prepago : TipoCliente.Pospago;
                //        currentClient.NombreUsuario = omsClient.username;
                //        currentClient.UpdateDate = DateTime.Now;

                //        clientsToUpdateCount++;
                //    }
                //}

                //if (clientsToUpdateCount > 0)
                //{
                //    await _omsSyncLogService.AddLogAsync($"{clientsToUpdateCount} client(s) will be updated", OsmJobType.Client);
                //}

                await _dbContext.SaveChangesAsync();

                await _omsSyncLogService.AddLogAsync("Oms Client Sync process finished", OsmJobType.Client);
            }
            catch (Exception ex)
            {
                await _omsSyncLogService.AddLogAsync($"Error when running Oms Client Sync process - {ex.Message}", OsmJobType.Client);
            }
        //PSD
        //}

        //private bool HasClientChanged(Cliente a, OmsClientDto b)
        //{
        //    return string.Compare(a.RazonSocial, b.company_name) != 0 ||
        //           string.Compare(a.NumeroDeDocumento, b.cuit) != 0 ||
        //           string.Compare(a.Nombre, b.first_name) != 0 ||
        //           string.Compare(a.Apellido, b.last_name) != 0 ||
        //           string.Compare(a.Email, b.email) != 0 ||
        //           string.Compare(a.Telefono, b.phone) != 0 ||
        //           string.Compare(a.NombreUsuario, b.username) != 0 ||
        //           (a.TipoCliente != (b.prepaid.Value ? TipoCliente.Prepago : TipoCliente.Pospago));
        }
    }
}
