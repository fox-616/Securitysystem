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
    [Route("api/[controller]")]
    [ApiController]
    public class DividendsController : ControllerBase
    {
        private readonly SecuritiesSystemContext _context;

        public DividendsController(SecuritiesSystemContext context)
        {
            _context = context;
        }

        // GET: api/Dividends
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Object>>> GetDividend()
        {
            var result = _context.Dividend
                .Join(_context.Company,
                d => d.CompanyID,
                c => c.CompanyID,
                (d,c) => new
                {
                    d.CompanyID,
                    c.CompanyName,
                    d.ExDividendDate,
                    d.DividendPerShare,
                    d.DividendType
                });

            return await result.ToListAsync();

            //return await _context.Dividend.ToListAsync();
        }

        // GET: api/Dividends/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetDividend(string id)
        {
            //var dividend = await _context.Dividend.FindAsync(id);

            var result = await _context.Dividend
               .Join(_context.Company,
               d => d.CompanyID,
               c => c.CompanyID,
               (d, c) => new
               {
                   d.CompanyID,
                   c.CompanyName,
                   d.DividendPerShare,
                   d.DividendType,
                   d.ExDividendDate                   
               }).FirstOrDefaultAsync(d => d.CompanyID == id);


            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // PUT: api/Dividends/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDividend(string id, Dividend dividend)
        {
            if (id != dividend.CompanyID)
            {
                return BadRequest();
            }

            _context.Entry(dividend).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DividendExists(id))
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

        // POST: api/Dividends
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dividend>> PostDividend(Dividend dividend)
        {
            _context.Dividend.Add(dividend);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DividendExists(dividend.CompanyID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDividend", new { id = dividend.CompanyID }, dividend);
        }

        // DELETE: api/Dividends/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDividend(string id)
        {
            var dividend = await _context.Dividend.FindAsync(id);
            if (dividend == null)
            {
                return NotFound();
            }

            _context.Dividend.Remove(dividend);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DividendExists(string id)
        {
            return _context.Dividend.Any(e => e.CompanyID == id);
        }
    }
}
