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
    [Route("api/categories")]
    [ApiController]
    public class TripCategoriesController : ControllerBase
    {
        private readonly ITripCategoryService _categoriesService;

        public TripCategoriesController(ITripCategoryService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // GET: api/Categories
        [HttpGet]
        public ActionResult<IEnumerable<TripDto>> GetAll()
        {
            var tripDtos = _categoriesService.GetAll();
            return Ok(tripDtos);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public ActionResult<TripDto> GetById([FromRoute] int id)
        {
            var tripDto = _categoriesService.GetById(id);
            return Ok(tripDto);
        }
    }
}
