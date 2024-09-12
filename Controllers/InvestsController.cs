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
    public class InvestsController : ControllerBase
    {
        private readonly SecuritiesSystemContext _context;

        public InvestsController(SecuritiesSystemContext context)
        {
            _context = context;
        }

        // GET: api/Invests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Object>>> GetInvest()
        {
            var result = _context.Invest
                .Join(_context.Shareholder,
                i => i.SSN,
                s => s.SSN,
                (i, s) => new { i, s })
                .Join(_context.Company,
                iscombined => iscombined.i.CompanyID,
                c => c.CompanyID,
                (iscombined, c) => new
                {
                    iscombined.i.SSN,
                    iscombined.s.ShareholderName,
                    iscombined.i.CompanyID,
                    c.CompanyName,
                    iscombined.i.OwnershipPercentage
                });

            return await result.ToListAsync();

            //return await _context.Invest.ToListAsync();
        }

        // GET: api/Invests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetInvest(string id)
        {
            //var invest = await _context.Invest.FindAsync(id);

            var result = await _context.Invest
                .Join(_context.Shareholder,
                i => i.SSN,
                s => s.SSN,
                (i, s) => new { i, s })
                .Join(_context.Company,
                iscombined => iscombined.i.CompanyID,
                c => c.CompanyID,
                (iscombined, c) => new
                {
                    iscombined.i.SSN,
                    iscombined.s.ShareholderName,
                    iscombined.i.CompanyID,
                    c.CompanyName,
                    iscombined.i.OwnershipPercentage,
                    iscombined.s.ShareholderReportingDate
                }).FirstOrDefaultAsync(iscombined2 => iscombined2.CompanyID == id);


            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        // PUT: api/Invests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvest(string id, Invest invest)
        {
            if (id != invest.CompanyID)
            {
                return BadRequest();
            }

            _context.Entry(invest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestExists(id))
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

        // POST: api/Invests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invest>> PostInvest(Invest invest)
        {
            _context.Invest.Add(invest);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvestExists(invest.CompanyID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInvest", new { id = invest.CompanyID }, invest);
        }

        // DELETE: api/Invests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvest(string id)
        {
            var invest = await _context.Invest.FindAsync(id);
            if (invest == null)
            {
                return NotFound();
            }

            _context.Invest.Remove(invest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvestExists(string id)
        {
            return _context.Invest.Any(e => e.CompanyID == id);
        }
    }
}
