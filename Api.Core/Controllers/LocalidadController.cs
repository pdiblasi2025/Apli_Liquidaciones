using Api.Core.Dtos;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using Api.Core.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LocalidadController : ControllerBase
    {
        public IMapper Mapper;
        private readonly ILocalidadService _localidadService;

        public LocalidadController(ILocalidadService localidadService)
        {
            _localidadService = localidadService;
            Mapper = new Mapper(BootStrapper.MapperConfiguration);
        }

        [HttpGet]
        public async Task<ActionResult<PagedListResponse<Localidad>>> GetAsync([FromQuery] FilterLocalidad filter)
        {
            return await _localidadService.GetAllAsync(filter);
        }
    }
}
