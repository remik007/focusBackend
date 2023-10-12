using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FocusAPI.Data;
using FocusAPI.Services;
using FocusAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace FocusAPI.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    [Authorize]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // GET: api/Reservations
        [HttpGet]
        public ActionResult<IEnumerable<ReservationDto>> GetAll()
        {
            var reservationDtos = _reservationService.GetAll();
            return Ok(reservationDtos);
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public ActionResult<ReservationDto> GetById([FromRoute] int id)
        {
            var reservationDto = _reservationService.GetById(id);
            return Ok(reservationDto);
        }

        // POST: api/Reservations/5
        [HttpPost("{id}")]
        public ActionResult<ReservationDto> Create([FromBody] ReservationDto reservationDto)
        {
            var reservationId = _reservationService.Create(reservationDto);
            return Created($"/api/reservations/{reservationId}", null);
        }
    }
}
