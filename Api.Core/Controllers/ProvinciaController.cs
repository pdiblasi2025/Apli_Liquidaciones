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
    public class ProvinciaController : ControllerBase
    {
        private readonly IProvinciaService _provinciaService;
        public IMapper Mapper;

        public ProvinciaController(IProvinciaService provinciaService)
        {
            _provinciaService = provinciaService;
            Mapper = new Mapper(BootStrapper.MapperConfiguration);
        }

        [HttpGet]
        public async Task<ActionResult<PagedListResponse<Provincia>>> GetAsync([FromQuery] FilterProvincia filter)
        {
            return await _provinciaService.GetAllAsync(filter);
        }
    }
}
