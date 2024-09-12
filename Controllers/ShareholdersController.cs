using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_3.Models;

namespace WebAPI_3.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class ShareholdersController : ControllerBase
    {
        private readonly SecuritiesSystemContext _context;

        public ShareholdersController(SecuritiesSystemContext context)
        {
            _context = context;
        }

        // GET: api/Shareholders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shareholder>>> GetShareholder()
        {
            return await _context.Shareholder.ToListAsync();
        }

        // GET: api/Shareholders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shareholder>> GetShareholder(string id)
        {
            var shareholder = await _context.Shareholder.FindAsync(id);

            if (shareholder == null)
            {
                return NotFound();
            }

            return shareholder;
        }

        // PUT: api/Shareholders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShareholder(string id, Shareholder shareholder)
        {
            if (id != shareholder.SSN)
            {
                return BadRequest();
            }

            _context.Entry(shareholder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShareholderExists(id))
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

        // POST: api/Shareholders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shareholder>> PostShareholder(Shareholder shareholder)
        {
            _context.Shareholder.Add(shareholder);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShareholderExists(shareholder.SSN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetShareholder", new { id = shareholder.SSN }, shareholder);
        }

        // DELETE: api/Shareholders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShareholder(string id)
        {
            var shareholder = await _context.Shareholder.FindAsync(id);
            if (shareholder == null)
            {
                return NotFound();
            }

            _context.Shareholder.Remove(shareholder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShareholderExists(string id)
        {
            return _context.Shareholder.Any(e => e.SSN == id);
        }
    }
}
