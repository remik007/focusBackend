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

        // POST: api/Admin/Reservations
        [HttpPost("reservations")]
        public ActionResult<ReservationDto> CreateReservation([FromBody] ReservationDto reservationDto)
        {
            var reservationId = _adminService.CreateReservation(reservationDto);
            return Created($"/api/admin/reservations/{reservationId}", null);
        }

        // DELETE: api/Admin/Reservations/5
        [HttpDelete("reservations/{id}")]
        public ActionResult<ReservationDto> DeleteReservation([FromRoute] int id)
        {
            _adminService.DeleteReservation(id);
            return Created($"/api/admin/reservations", null);
        }

        // PUT: api/Admin/Reservations/5
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

        // POST: api/Admin/Trips
        [HttpPost("trips")]
        public ActionResult<TripDto> CreateTrip([FromBody] TripDto tripDto)
        {
            var tripId = _adminService.CreateTrip(tripDto);
            return Created($"/api/admin/trips/{tripId}", null);
        }

        // DELETE: api/Admin/Trips/5
        [HttpDelete("trips/{id}")]
        public ActionResult<TripDto> DeleteTrip([FromRoute] int id)
        {
            _adminService.DeleteTrip(id);
            return Created($"/api/admin/trips", null);
        }

        // PUT: api/Admin/Trips/5
        [HttpPut("{id}")]
        public ActionResult<TripDto> UpdateTrip([FromRoute] int id, [FromBody] TripDto tripDto)
        {
            var tripId = _adminService.UpdateTrip(id, tripDto);
            return Created($"/api/admin/trips/{tripId}", null);
        }

        //SubPages---------------------------------------------------------------------------------------------------------
        // GET: api/Admin/SubPages
        [HttpGet("subpages")]
        public ActionResult<IEnumerable<SubPageDto>> GetAllSubPages()
        {
            var subPageDtos = _adminService.GetAllSubPages();
            return Ok(subPageDtos);
        }

        // GET: api/Admin/SubPages/5
        [HttpGet("subpages/{id}")]
        public ActionResult<SubPageDto> GetSubPageById([FromRoute] int id)
        {
            var subPageDto = _adminService.GetSubPageById(id);
            return Ok(subPageDto);
        }

        // POST: api/Admin/SubPages
        [HttpPost("subpages")]
        public ActionResult<SubPageDto> CreateSubPage([FromBody] SubPageDto subPageDto)
        {
            var subPageId = _adminService.CreateSubPage(subPageDto);
            return Created($"/api/admin/subpages/{subPageId}", null);
        }

        // DELETE: api/Admin/SubPages/5
        [HttpDelete("subpages/{id}")]
        public ActionResult<SubPageDto> DeleteSubPage([FromRoute] int id)
        {
            _adminService.DeleteSubPage(id);
            return Created($"/api/admin/subpages", null);
        }

        // PUT: api/Admin/SubPages/5
        [HttpPut("subpages/{id}")]
        public ActionResult<SubPageDto> UpdateTrip([FromRoute] int id, [FromBody] SubPageDto subPageDto)
        {
            var subPageId = _adminService.UpdateSubPage(id, subPageDto);
            return Created($"/api/admin/subpages/{subPageId}", null);
        }

        //Contacts---------------------------------------------------------------------------------------------------------
        // GET: api/Admin/Contacts
        [HttpGet("contacts")]
        public ActionResult<IEnumerable<ContactDto>> GetContact()
        {
            var contactDto = _adminService.GetContact();
            return Ok(contactDto);
        }

        // PUT: api/Admin/Contacts
        [HttpPut("contacts")]
        public ActionResult<ContactDto> UpdateContact([FromBody] ContactDto contactDto)
        {
            _adminService.UpdateContact(contactDto);
            return Created($"/api/admin/contacts", null);
        }

        //TripCategories---------------------------------------------------------------------------------------------------------
        // GET: api/Admin/Categories
        [HttpGet("categories")]
        public ActionResult<IEnumerable<TripCategoryDto>> GetAllCategories()
        {
            var tripCategoryDtos = _adminService.GetAllCategories();
            return Ok(tripCategoryDtos);
        }

        // GET: api/Admin/Categories/5
        [HttpGet("categories/{id}")]
        public ActionResult<TripCategoryDto> GetCategoryById([FromRoute] int id)
        {
            var tripCategoryDto = _adminService.GetCategoryById(id);
            return Ok(tripCategoryDto);
        }

        // POST: api/Admin/Categories
        [HttpPost("categories")]
        public ActionResult<TripCategoryDto> CreateCategory([FromBody] TripCategoryDto tripCategoryDto)
        {
            var tripCategoryId = _adminService.CreateCategory(tripCategoryDto);
            return Created($"/api/admin/categories/{tripCategoryId}", null);
        }

        // DELETE: api/Admin/Categories/5
        [HttpDelete("categories/{id}")]
        public ActionResult<TripCategoryDto> DeleteCategory([FromRoute] int id)
        {
            _adminService.DeleteCategory(id);
            return Created($"/api/admin/categories", null);
        }

        // PUT: api/Admin/Categories/5
        [HttpPut("categories/{id}")]
        public ActionResult<TripCategoryDto> UpdateCategory([FromRoute] int id, [FromBody] TripCategoryDto tripCategoryDto)
        {
            var tripCategoryId = _adminService.UpdateCategory(id, tripCategoryDto);
            return Created($"/api/admin/categories/{tripCategoryId}", null);
        }
    }
}
