using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FocusAPI.Data;

namespace FocusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubPagesController : ControllerBase
    {
        private readonly FocusDbContext _context;

        public SubPagesController(FocusDbContext context)
        {
            _context = context;
        }

        // GET: api/SubPages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubPage>>> GetSubPages()
        {
          if (_context.SubPages == null)
          {
              return NotFound();
          }
            return await _context.SubPages.ToListAsync();
        }

        // GET: api/SubPages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubPage>> GetSubPage(int id)
        {
          if (_context.SubPages == null)
          {
              return NotFound();
          }
            var subPage = await _context.SubPages.FindAsync(id);

            if (subPage == null)
            {
                return NotFound();
            }

            return subPage;
        }

        // PUT: api/SubPages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubPage(int id, SubPage subPage)
        {
            if (id != subPage.Id)
            {
                return BadRequest();
            }

            _context.Entry(subPage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubPageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SubPages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubPage>> PostSubPage(SubPage subPage)
        {
          if (_context.SubPages == null)
          {
              return Problem("Entity set 'FocusDbContext.SubPages'  is null.");
          }
            _context.SubPages.Add(subPage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubPage", new { id = subPage.Id }, subPage);
        }

        // DELETE: api/SubPages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubPage(int id)
        {
            if (_context.SubPages == null)
            {
                return NotFound();
            }
            var subPage = await _context.SubPages.FindAsync(id);
            if (subPage == null)
            {
                return NotFound();
            }

            _context.SubPages.Remove(subPage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubPageExists(int id)
        {
            return (_context.SubPages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
