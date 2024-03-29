﻿using System;
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
            var getTripDto = _adminService.GetTripById(id);
            return Ok(getTripDto);
        }

        // GET: api/Admin/Trips/Images/5
        [HttpGet("trips/images/{id}")]
        public ActionResult<GetImageDto> GetTripImageById([FromRoute] int id)
        {
            var getImageDto = _adminService.GetTripImageById(id);
            return Ok(getImageDto);
        }

        // POST: api/Admin/Trips
        [HttpPost("trips")]
        public ActionResult<TripDto> CreateTrip([FromBody] TripDto tripDto)
        {
            var tripId = _adminService.CreateTrip(tripDto);
            return Created($"/api/admin/trips/{tripId}", tripId);
        }

        // DELETE: api/Admin/Trips/5
        [HttpDelete("trips/{id}")]
        public ActionResult<TripDto> DeleteTrip([FromRoute] int id)
        {
            _adminService.DeleteTrip(id);
            return Created($"/api/admin/trips", null);
        }

        // PUT: api/Admin/Trips/5
        [HttpPut("trips")]
        public ActionResult<TripDto> UpdateTrip([FromBody] TripDto tripDto)
        {
            var tripId = _adminService.UpdateTrip(tripDto);
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
        [HttpGet("subpages/{subPageName}")]
        public ActionResult<SubPageDto> GetSubPageByName([FromRoute] string subPageName)
        {
            var subPageDto = _adminService.GetSubPageByName(subPageName);
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
        [HttpDelete("subpages/{subpage}")]
        public ActionResult<SubPageDto> DeleteSubPage([FromRoute] string subpage)
        {
            _adminService.DeleteSubPage(subpage);
            return Created($"/api/admin/subpages", null);
        }

        // PUT: api/Admin/SubPages/5
        [HttpPut("subpages")]
        public ActionResult<SubPageDto> UpdateTrip([FromBody] SubPageDto subPageDto)
        {
            var subPageId = _adminService.UpdateSubPage(subPageDto);
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
        [HttpGet("categories/GetCategoryById/{id}")]
        public ActionResult<TripCategoryDto> GetCategoryById([FromRoute] int id)
        {
            var tripCategoryDto = _adminService.GetCategoryById(id);
            return Ok(tripCategoryDto);
        }

        // GET: api/Admin/Categories/GetCategoryByName{category}
        [HttpGet("categories/GetCategoryByName/{category}")]
        public ActionResult<TripCategoryDetailsDto> GetCategoryByName([FromRoute] string category)
        {
            var tripCategoryDetailsDto = _adminService.GetCategoryByName(category);
            return Ok(tripCategoryDetailsDto);
        }

        // GET: api/Admin/Categories/images/GetCategoryByName{category}
        [HttpGet("categories/images/GetCategoryByName/{category}")]
        public ActionResult<IEnumerable<GetImageDto>> GetCategoryImagesByName([FromRoute] string category)
        {
            var getImageDtos = _adminService.GetCategoryByName(category);
            return Ok(getImageDtos);
        }

        // POST: api/Admin/Categories
        [HttpPost("categories")]
        public ActionResult<TripCategoryDto> CreateCategory([FromBody] TripCategoryDto tripCategoryDto)
        {
            var tripCategoryId = _adminService.CreateCategory(tripCategoryDto);
            return Created($"/api/admin/categories/{tripCategoryId}", null);
        }

        // DELETE: api/Admin/Categories/5
        [HttpDelete("categories/{category}")]
        public ActionResult<TripCategoryDto> DeleteCategory([FromRoute] string category)
        {
            _adminService.DeleteCategory(category);
            return Created($"/api/admin/categories", null);
        }

        // PUT: api/Admin/Categories/5
        [HttpPut("categories")]
        public ActionResult<TripCategoryDto> UpdateCategory([FromBody] TripCategoryDto tripCategoryDto)
        {
            var tripCategoryId = _adminService.UpdateCategory(tripCategoryDto);
            return Created($"/api/admin/categories/{tripCategoryId}", null);
        }

        // GET: api/Admin/Categories/Search
        [HttpGet("categories/search")]
        public ActionResult<IEnumerable<TripCategoryDetailsDto>> Search([FromQuery] SearchDto? searchDto)
        {
            var tripCategoryDetailsDto = _adminService.Search(searchDto);
            return Ok(tripCategoryDetailsDto);
        }

        // GET: api/Admin/Categories/Search
        [HttpGet("categories/images/search")]
        public ActionResult<IEnumerable<GetImageDto>> GetSearchImages([FromQuery] SearchDto? searchDto)
        {
            var getImageDtos = _adminService.GetSearchImages(searchDto);
            return Ok(getImageDtos);
        }

        //TransportTypes---------------------------------------------------------------------------------------------------------
        // GET: api/Admin/TransportTypes
        [HttpGet("transporttypes")]
        public ActionResult<IEnumerable<TransportTypeDto>> GetAllTransportTypes()
        {
            var typeDtos = _adminService.GetAllTransportTypes();
            return Ok(typeDtos);
        }

        // GET: api/Admin/TransportTypes/5
        [HttpGet("transporttypes/{id}")]
        public ActionResult<TransportTypeDto> GetTransportTypeById([FromRoute] int id)
        {
            var typeDto = _adminService.GetTransportTypeById(id);
            return Ok(typeDto);
        }

        // POST: api/Admin/TransportTypes
        [HttpPost("transporttypes")]
        public ActionResult<TransportTypeDto> CreateTransportType([FromBody] TransportTypeDto typeDto)
        {
            var typeId = _adminService.CreateTransportType(typeDto);
            return Created($"/api/admin/transporttypes/{typeId}", null);
        }

        // DELETE: api/Admin/TransportTypes/5
        [HttpDelete("transporttypes/{id}")]
        public ActionResult<TransportTypeDto> DeleteTransportType([FromRoute] int id)
        {
            _adminService.DeleteTransportType(id);
            return Created($"/api/admin/transporttypes", null);
        }

        // PUT: api/Admin/TransportTypes/5
        [HttpPut("transporttypes/{id}")]
        public ActionResult<TransportTypeDto> UpdateTransportType([FromRoute] int id, [FromBody] TransportTypeDto typeDto)
        {
            var typeId = _adminService.UpdateTransportType(id, typeDto);
            return Created($"/api/admin/transporttypes/{typeId}", null);
        }

        //Users---------------------------------------------------------------------------------------------------------
        // GET: api/Admin/Users
        [HttpGet("users")]
        public ActionResult<IEnumerable<TransportTypeDto>> GetUsers()
        {
            var userDtos = _adminService.GetUsers();
            return Ok(userDtos);
        }
    }
}
