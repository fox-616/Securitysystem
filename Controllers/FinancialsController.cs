using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_3.Models;
using WebAPI_3.DTO;

namespace WebAPI_3.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class FinancialsController : ControllerBase
    {
        private readonly SecuritiesSystemContext _context;

        public FinancialsController(SecuritiesSystemContext context)
        {
            _context = context;
        }

        // GET: api/Financials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinancialDTO>>> GetFinancial()
        {
            var result = _context.Financial
                .Join(_context.Company,
                f => f.CompanyID,
                c => c.CompanyID,
                (f, c) => new FinancialDTO
                {
                    CompanyID = f.CompanyID,
                    CompanyName = c.CompanyName,
                    FinancialYear = f.FinancialYear,
                    Quarter = f.Quarter,
                    OperatingRevenue = f.OperatingRevenue,
                    EPS = f.EPS
                });

            return await result.ToListAsync();

            //return await _context.Financial.ToListAsync();
        }

        // GET: api/Financials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetFinancial(string id)
        {
            //var financial = await _context.Financial.FindAsync(id);

            var result = await _context.Financial
                .Join(_context.Company,
                f => f.CompanyID,
                c => c.CompanyID,
                (f, c) => new
                {
                    f.CompanyID,
                    c.CompanyName,
                    f.FinancialYear,
                    f.Quarter,
                    f.FinancialReportDate,
                    f.OperatingRevenue,
                    f.OperatingCosts,
                    f.NonOperatingIncomeExpenses,
                    f.GrossProfit,
                    f.OperatingExpenses,
                    f.IncomeTaxExpense,
                    f.OperatingIncome,
                    f.ProfitBeforeTax,
                    f.NetProfitAfterTax,
                    f.EPS
                }).FirstOrDefaultAsync(f => f.CompanyID == id);


            if (result == null)
            {
                return NotFound();
            }

            return result;

        }

        // PUT: api/Financials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinancial(string id, Financial financial)
        {
            if (id != financial.CompanyID)
            {
                return BadRequest();
            }

            _context.Entry(financial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancialExists(id))
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

        // POST: api/Financials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Financial>> PostFinancial(Financial financial)
        {
            _context.Financial.Add(financial);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FinancialExists(financial.CompanyID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFinancial", new { id = financial.CompanyID }, financial);
        }

        // DELETE: api/Financials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinancial(string id)
        {
            var financial = await _context.Financial.FindAsync(id);
            if (financial == null)
            {
                return NotFound();
            }

            _context.Financial.Remove(financial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinancialExists(string id)
        {
            return _context.Financial.Any(e => e.CompanyID == id);
        }
    }
}
