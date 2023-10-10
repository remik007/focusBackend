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
        public async Task<ActionResult<IEnumerable<TripType>>> GetTripTypes()
        {
          if (_context.TripTypes == null)
          {
              return NotFound();
          }
            return await _context.TripTypes.ToListAsync();
        }

        // GET: api/TripTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TripType>> GetTripType(int id)
        {
          if (_context.TripTypes == null)
          {
              return NotFound();
          }
            var tripType = await _context.TripTypes.FindAsync(id);

            if (tripType == null)
            {
                return NotFound();
            }

            return tripType;
        }

        // PUT: api/TripTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTripType(int id, TripType tripType)
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
        public async Task<ActionResult<TripType>> PostTripType(TripType tripType)
        {
          if (_context.TripTypes == null)
          {
              return Problem("Entity set 'FocusDbContext.TripTypes'  is null.");
          }
            _context.TripTypes.Add(tripType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTripType", new { id = tripType.Id }, tripType);
        }

        // DELETE: api/TripTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTripType(int id)
        {
            if (_context.TripTypes == null)
            {
                return NotFound();
            }
            var tripType = await _context.TripTypes.FindAsync(id);
            if (tripType == null)
            {
                return NotFound();
            }

            _context.TripTypes.Remove(tripType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TripTypeExists(int id)
        {
            return (_context.TripTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
