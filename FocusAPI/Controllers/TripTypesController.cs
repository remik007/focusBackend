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
    public class TripTypesController : ControllerBase
    {
        private readonly FocusDbContext _context;

        public TripTypesController(FocusDbContext context)
        {
            _context = context;
        }

        // GET: api/TripTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TripCategory>>> GetTripTypes()
        {
          if (_context.TripCategories == null)
          {
              return NotFound();
          }
            return await _context.TripCategories.ToListAsync();
        }

        // GET: api/TripTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TripCategory>> GetTripType(int id)
        {
          if (_context.TripCategories == null)
          {
              return NotFound();
          }
            var tripType = await _context.TripCategories.FindAsync(id);

            if (tripType == null)
            {
                return NotFound();
            }

            return tripType;
        }

        // PUT: api/TripTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTripType(int id, TripCategory tripType)
        {
            if (id != tripType.Id)
            {
                return BadRequest();
            }

            _context.Entry(tripType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripTypeExists(id))
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

        // POST: api/TripTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TripCategory>> PostTripType(TripCategory tripType)
        {
          if (_context.TripCategories == null)
          {
              return Problem("Entity set 'FocusDbContext.TripTypes'  is null.");
          }
            _context.TripCategories.Add(tripType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTripType", new { id = tripType.Id }, tripType);
        }

        // DELETE: api/TripTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripType(int id)
        {
            if (_context.TripCategories == null)
            {
                return NotFound();
            }
            var tripType = await _context.TripCategories.FindAsync(id);
            if (tripType == null)
            {
                return NotFound();
            }

            _context.TripCategories.Remove(tripType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TripTypeExists(int id)
        {
            return (_context.TripCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
