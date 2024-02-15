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
    [Route("api/subpages")]
    [ApiController]
    public class SubPagesController : ControllerBase
    {
        private readonly ISubPageService _subpageService;

        public SubPagesController(ISubPageService subpageService)
        {
            _subpageService = subpageService;
        }

        // GET: api/SubPages
        [HttpGet]
        public ActionResult<IEnumerable<SubPageDto>> GetAll()
        {
            var subPageDtos = _subpageService.GetAll();
            return Ok(subPageDtos);
        }

        // GET: api/SubPages/5
        [HttpGet("{subPageName}")]
        public ActionResult<GetSubPageDto> GetById([FromRoute] string subPageName)
        {
            var getSubPageDto = _subpageService.GetByName(subPageName);
            return Ok(getSubPageDto);
        }

        // GET: api/SubPages/5
        [HttpGet("images/{subPageName}")]
        public ActionResult<GetImageDto> GetImageById([FromRoute] string subPageName)
        {
            var getImageDto = _subpageService.GetImageByName(subPageName);
            return Ok(getImageDto);
        }
    }
}
