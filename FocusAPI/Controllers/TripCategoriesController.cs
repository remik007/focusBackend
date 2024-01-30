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
        public ActionResult<IEnumerable<TripCategoryDto>> GetAll()
        {
            var tripCategoriesDtos = _categoriesService.GetAll();
            return Ok(tripCategoriesDtos);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public ActionResult<TripCategoryDto> GetById([FromRoute] int id)
        {
            var tripCategoryDto = _categoriesService.GetById(id);
            return Ok(tripCategoryDto);
        }

        // GET: api/Trips/{category}
        [HttpGet("{category}")]
        public ActionResult<IEnumerable<TripCategoryDetailsDto>> GetByName(string category)
        {
            var tripCategoryDetailsDto = _categoriesService.GetByName(category);
            return Ok(tripCategoryDetailsDto);
        }
    }
}
