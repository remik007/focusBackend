using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FocusAPI.Data;
using FocusAPI.Models;
using FocusAPI.Services;

namespace FocusAPI.Controllers
{
    [Route("api/transporttypes")]
    [ApiController]
    public class TransportTypesController : ControllerBase
    {
        private readonly ITransportTypeService _transportTypeService;

        public TransportTypesController(ITransportTypeService transportTypeService)
        {
            _transportTypeService = transportTypeService;
        }

        // GET: api/transport
        [HttpGet]
        public ActionResult<IEnumerable<TripDto>> GetAll()
        {
            var tripDtos = _transportTypeService.GetAll();
            return Ok(tripDtos);
        }

    }
}
