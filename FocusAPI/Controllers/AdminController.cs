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
using Microsoft.AspNetCore.Authorization;

namespace FocusAPI.Controllers
{
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // GET: api/Admin/Reservations
        [HttpGet]
        public ActionResult<IEnumerable<ReservationDto>> GetAllReservations()
        {
            var reservationDtos = _adminService.GetAllReservations();
            return Ok(reservationDtos);
        }

        // GET: api/Admin/Reservations/5
        [HttpGet("{id}")]
        public ActionResult<ReservationDto> GetReservationById([FromRoute] int id)
        {
            var reservationDto = _adminService.GetById(id);
            return Ok(reservationDto);
        }

        // POST: api/Admin/Reservations/5
        [HttpPost("{id}")]
        public ActionResult<ReservationDto> CreateReservation([FromBody] ReservationDto reservationDto)
        {
            var reservationId = _adminService.Create(reservationDto);
            return Created($"/api/reservations/{reservationId}", null);
        }

        // POST: api/Admin/Reservations/5
        [HttpDelete("{id}")]
        public ActionResult<ReservationDto> DeleteReservation([FromRoute] int id)
        {
            _adminService.DeletReservation(id);
            return Created($"/api/reservations", null);
        }

        // POST: api/Admin/Reservations/5
        [HttpPatch("{id}")]
        public ActionResult<ReservationDto> UpdateReservation([FromRoute] int id, [FromBody] ReservationDto reservationDto)
        {
            var reservationId = _adminService.UpdateReservation(id, reservationDto);
            return Created($"/api/reservations/{reservationId}", null);
        }

    }
}
