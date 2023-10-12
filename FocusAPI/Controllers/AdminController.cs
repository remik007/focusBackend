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

        //RESERVATIONS---------------------------------------------------------------------------------------------------------

        // GET: api/Admin/Reservations
        [HttpGet("reservations")]
        public ActionResult<IEnumerable<ReservationDto>> GetAllReservations()
        {
            var reservationDtos = _adminService.GetAllReservations();
            return Ok(reservationDtos);
        }

        // GET: api/Admin/Reservations/5
        [HttpGet("reservations/{id}")]
        public ActionResult<ReservationDto> GetReservationById([FromRoute] int id)
        {
            var reservationDto = _adminService.GetReservationById(id);
            return Ok(reservationDto);
        }

        // POST: api/Admin/Reservations/5
        [HttpPost("reservations/{id}")]
        public ActionResult<ReservationDto> CreateReservation([FromBody] ReservationDto reservationDto)
        {
            var reservationId = _adminService.CreateReservation(reservationDto);
            return Created($"/api/admin/reservations/{reservationId}", null);
        }

        // POST: api/Admin/Reservations/5
        [HttpDelete("reservations/{id}")]
        public ActionResult<ReservationDto> DeleteReservation([FromRoute] int id)
        {
            _adminService.DeleteReservation(id);
            return Created($"/api/admin/reservations", null);
        }

        // POST: api/Admin/Reservations/5
        [HttpPut("reservations/{id}")]
        public ActionResult<ReservationDto> UpdateReservation([FromRoute] int id, [FromBody] ReservationDto reservationDto)
        {
            var reservationId = _adminService.UpdateReservation(id, reservationDto);
            return Created($"/api/admin/reservations/{reservationId}", null);
        }

        //TRIPS---------------------------------------------------------------------------------------------------------

        // GET: api/Admin/Trips
        [HttpGet("trips")]
        public ActionResult<IEnumerable<TripDto>> GetAllTrips()
        {
            var tripDtos = _adminService.GetAllTrips();
            return Ok(tripDtos);
        }

        // GET: api/Admin/Trips/5
        [HttpGet("trips/{id}")]
        public ActionResult<TripDto> GetTripById([FromRoute] int id)
        {
            var tripDto = _adminService.GetTripById(id);
            return Ok(tripDto);
        }

        // POST: api/Admin/Trips/5
        [HttpPost("trips/{id}")]
        public ActionResult<TripDto> CreateTrip([FromBody] TripDto tripDto)
        {
            var tripId = _adminService.CreateTrip(tripDto);
            return Created($"/api/admin/trips/{tripId}", null);
        }

        // POST: api/Admin/Trips/5
        [HttpDelete("trips/{id}")]
        public ActionResult<TripDto> DeleteTrip([FromRoute] int id)
        {
            _adminService.DeleteTrip(id);
            return Created($"/api/admin/trips", null);
        }

        // POST: api/Admin/Trips/5
        [HttpPut("{id}")]
        public ActionResult<ReservationDto> UpdateTrip([FromRoute] int id, [FromBody] TripDto tripDto)
        {
            var tripId = _adminService.UpdateTrip(id, tripDto);
            return Created($"/api/admin/trips/{tripId}", null);
        }
    }
}
