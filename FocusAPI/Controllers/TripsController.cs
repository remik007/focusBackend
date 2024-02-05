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
    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripsController(ITripService tripService)
        {
            _tripService = tripService;
        }

        // GET: api/Trips
        [HttpGet]
        public ActionResult<IEnumerable<TripDto>> GetAll()
        {
            var tripDtos = _tripService.GetAll();
            return Ok(tripDtos);
        }

        // GET: api/Trips/5
        [HttpGet("{id}")]
        public ActionResult<TripDto> GetById([FromRoute] int id)
        {
            var tripDto = _tripService.GetById(id);
            return Ok(tripDto);
        }

        // GET: api/Trips/header
        [HttpGet("header")]
        public ActionResult<HeaderDto> GetHeader()
        {
            var header = _tripService.GetHeader();
            return Ok(header);
        }

        // GET: api/Trips/Highlighted
        [HttpGet("highlighted")]
        public ActionResult<TripDto> GetHighlightedTrips()
        {
            var tripDto = _tripService.GetHighlightedTrips();
            return Ok(tripDto);
        }
    }
}
