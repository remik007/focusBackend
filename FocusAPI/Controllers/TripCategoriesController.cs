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
using FocusAPI.Data.Migrations;

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
        // GET: api/Categories/Search
        [HttpGet("Search")]
        public ActionResult<IEnumerable<TripCategoryDetailsDto>> Search([FromQuery] SearchDto? searchDto)
        {
            var tripCategoryDetailsDto = _categoriesService.Search(searchDto);
            return Ok(tripCategoryDetailsDto);
        }

        // GET: api/Categories/Images/{category}
        [HttpGet("images/{category}")]
        public ActionResult<IEnumerable<GetImageDto>> GetCategoryImages([FromRoute] string category)
        {
            var getImageDtos = _categoriesService.GetCategoryImages(category);
            return Ok(getImageDtos);
        }

        // GET: api/Categories/Images/Search
        [HttpGet("images/search")]
        public ActionResult<IEnumerable<GetImageDto>> GetSearchImages([FromQuery] SearchDto? searchDto)
        {
            var getImageDtos = _categoriesService.GetSearchImages(searchDto);
            return Ok(getImageDtos);
        }

        // GET: api/Categories/{category}
        [HttpGet("{category}")]
        public ActionResult<IEnumerable<TripCategoryDetailsDto>> GetByName([FromRoute] string category)
        {
            var tripCategoryDetailsDto = _categoriesService.GetByName(category);
            return Ok(tripCategoryDetailsDto);
        }
    }
}
