using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using Api.Core.Entities;
using Api.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Admin
{

    public class ClienteAdmin : BaseAdmin<int, Entities.Cliente, Dtos.Cliente, FilterCliente>
    {
        public override IQueryable GetQuery(FilterCliente filter)
        {
            var query = MyContext.Clientes.Include(MyContext.GetIncludePaths(typeof(Entities.Cliente)))
                .Where(e => !e.Deleted)
                .OrderByDescending(x => x.CreateDate)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
                query = query.Where(e => e.RazonSocial.StartsWith(filter.MultiColumnSearchText, StringComparison.InvariantCultureIgnoreCase) ||
                                         e.NombreUsuario.Contains(filter.MultiColumnSearchText, StringComparison.InvariantCultureIgnoreCase) ||
                                         e.Nombre.Contains(filter.MultiColumnSearchText, StringComparison.OrdinalIgnoreCase) ||
                                         e.NumeroDeDocumento.StartsWith(filter.MultiColumnSearchText, StringComparison.InvariantCultureIgnoreCase)).AsQueryable();
            }

            if (filter.TipoCliente.HasValue)
            {
                query = query.Where(x => x.TipoCliente == filter.TipoCliente.Value).AsQueryable();
            }

            return query;
        }

        public override PagedListResponse<Dtos.Cliente> GetByFilter(FilterCliente filter)
        {
            try
            {
                var query = GetQuery(filter).OfType<Cliente>();

                var pageSize = filter.PageSize ?? 10;
                var currentPage = filter.CurrentPage ?? 1;

                if (filter.EstadoFacturacion.HasValue)
                {
                    var results = query.ToList();

                    var data = results.Where(x => x.GetEstadosFacturacion().Contains((int)filter.EstadoFacturacion));

                    var dataPaginated = data
                        .Skip(pageSize * (currentPage - 1))
                        .Take(pageSize)
                        .ToList();

                    return new PagedListResponse<Dtos.Cliente>
                    {
                        Count = data.Count(),
                        Data = Mapper.Map<IList<Cliente>, IList<Dtos.Cliente>>(dataPaginated)
                    };
                }
                else
                {
                    var data = query.Skip(pageSize * (currentPage - 1)).Take(pageSize).ToList();

                    return new PagedListResponse<Dtos.Cliente>
                    {
                        Count = query.Count(),
                        Data = Mapper.Map<IList<Cliente>, IList<Dtos.Cliente>>(data)
                    };
                }
            }
            catch (Exception)
            {
                return new PagedListResponse<Dtos.Cliente>
                {
                    Count = 0,
                    Data = new List<Dtos.Cliente>()
                };
            }
        }

        public async Task<int> SaveIngresosBrutosArchivo(int id, Dtos.IngresosBrutosArchivoPost request)
        {
            var entity = await MyContext.Clientes.Include(MyContext.GetIncludePaths(typeof(Cliente))).SingleAsync(e => e.Id == id);

            using (var memoryStream = new MemoryStream())
            {
                request.Archivo.CopyTo(memoryStream);

                entity.IngresosBrutosArchivos.Add(new IngresosBrutosArchivo
                {
                    Nombre = request.Archivo.FileName,
                    Archivo = memoryStream.ToArray(),
                    Posicion = request.Posicion,
                    Enabled = true,
                    CreateDate = DateTime.Now,
                    CreatedBy = CurrentUser?.user_id
                });
            }

            return await MyContext.SaveChangesAsync(); 
        }

        public async Task RemoveIngresosBrutosArchivo(int id, int archivoId)
        {
            var entity = await MyContext.Clientes.Include(MyContext.GetIncludePaths(typeof(Cliente))).SingleAsync(e => e.Id == id);

            var ingresosBrutosArchivoToRemove = entity.IngresosBrutosArchivos.First(x => x.Id == archivoId);

            entity.IngresosBrutosArchivos.Remove(ingresosBrutosArchivoToRemove);

            await MyContext.SaveChangesAsync();
        }

        public async Task<Dtos.IngresosBrutosArchivoDownload> GetIngresosBrutosArchivo(int id, int archivoId)
        {
            var cliente = await MyContext.Clientes.Include(MyContext.GetIncludePaths(typeof(Cliente))).SingleAsync(e => e.Id == id);

            var ingresosBrutosArchivo = cliente.IngresosBrutosArchivos.First(x => x.Id == archivoId);

            return new Dtos.IngresosBrutosArchivoDownload
            {
                Nombre = ingresosBrutosArchivo.Nombre,
                Contenido = ingresosBrutosArchivo.Archivo
            };
        }

        public async Task<List<Dtos.IngresosBrutosArchivo>> GetAllIngresosBrutosArchivo(int id)
        {
            var cliente = await MyContext.Clientes.Include(MyContext.GetIncludePaths(typeof(Cliente))).SingleAsync(e => e.Id == id);

            var ingresosBrutosArchivo = cliente.IngresosBrutosArchivos;

            return Mapper.Map<List<IngresosBrutosArchivo>, List<Dtos.IngresosBrutosArchivo>>(ingresosBrutosArchivo.ToList());            
        }

        public override Entities.Cliente ToEntity(Dtos.Cliente dto)
        {
            var entity = new Entities.Cliente();;

            if (dto.Id.HasValue)
            {
                entity = MyContext.Clientes.Include(MyContext.GetIncludePaths(typeof(Cliente))).Single(e => e.Id == dto.Id.Value);

                var impuestosToDelete = entity.Impuestos.Where(x => !dto.Impuestos.Any(y => y.ImpuestoId == x.ImpuestoId)).ToList();

                if (impuestosToDelete.Any())
                {
                    MyContext.RemoveRange(impuestosToDelete);
                }

                foreach (var impuesto in dto.Impuestos)
                {
                    var clienteImpuesto = entity.Impuestos.FirstOrDefault(x => x.ImpuestoId == impuesto.ImpuestoId);

                    if (clienteImpuesto == null)
                    {
                        var newClienteImpuesto = new ClienteImpuesto
                        {
                            ImpuestoId = impuesto.ImpuestoId,
                            PorcentajeExencion = impuesto.PorcentajeExencion,
                            ExencionDesde = impuesto.ExencionDesde,
                            ExencionHasta = impuesto.ExencionHasta,
                            CreateDate = DateTime.Now,
                            CreatedBy = CurrentUser?.user_id,
                            Enabled = true
                        };

                        entity.Impuestos.Add(newClienteImpuesto);
                    }
                    else
                    {
                        clienteImpuesto.PorcentajeExencion = impuesto.PorcentajeExencion;
                        clienteImpuesto.ExencionDesde = impuesto.ExencionDesde;
                        clienteImpuesto.ExencionHasta = impuesto.ExencionHasta;
                        clienteImpuesto.UpdateDate = DateTime.Now;
                        clienteImpuesto.UpdatedBy = CurrentUser?.user_id;
                    }
                }
                //PSD FIX005 - Asigno el vacio al email
                entity.Nombre = dto.Nombre;
                entity.Email = dto.Email ?? " ";
                entity.Telefono = dto.Telefono;
                entity.NombreUsuario = dto.NombreUsuario;
                entity.RazonSocial = dto.RazonSocial;
                entity.NumeroDeDocumento = dto.NumeroDeDocumento;
                entity.CondicionPagoId = dto.CondicionPago?.Id;
                entity.Calle = dto.Calle;
                entity.Altura = dto.Altura;
                entity.LocalidadId = dto.Localidad?.Id;
                entity.CodigoPostal = dto.CodigoPostal;
                entity.MetodosEnvio = JsonConvert.SerializeObject(dto.MetodosEnvio);
                entity.EnvioPorEmail = dto.EnvioPorEmail;
                entity.NumeroIngresosBrutos = dto.NumeroIngresosBrutos;
                entity.TipoDocumentoId = dto.TipoDocumento.Id;
                entity.TipoImpuestoId = dto.TipoImpuesto.Id;
                entity.UpdateDate = DateTime.Now;
            }

            return entity;
        }

        public override Dtos.Response.ResultMessage<Dtos.Cliente> Validate(Dtos.Cliente dto, int Type)
        {
            // Type: 1-Create, 2-Update, 3-Delete 
            return new Dtos.Response.ResultMessage<Dtos.Cliente>() { Content = dto, Successful = true, Message = "OK" };
        }
    }
}