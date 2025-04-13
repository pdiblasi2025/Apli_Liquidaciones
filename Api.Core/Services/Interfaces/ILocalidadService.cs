using Api.Core.Dtos;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Services.Interfaces
{
    public interface ILocalidadService
    {
        Task<PagedListResponse<Localidad>> GetAllAsync(FilterLocalidad filter);
    }
}
