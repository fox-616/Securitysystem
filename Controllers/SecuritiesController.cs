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
    public class SecuritiesController : ControllerBase
    {
        private readonly SecuritiesSystemContext _context;

        public SecuritiesController(SecuritiesSystemContext context)
        {
            _context = context;
        }

        // GET: api/Securities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SecuritiesDTO>>> GetSecurities()
        {
            var accountID = HttpContext.Session.GetString("accountID");

            if (string.IsNullOrEmpty(accountID))
            {
                // 使用 Unauthorized 方法返回 401 狀態碼
                var error = new { message = "未登入" };
                return Unauthorized(error);

            }

            var result = _context.Securities.Select(s => new SecuritiesDTO
            {
                SecurityID=s.SecurityID,
                SecurityName=s.SecurityName,
                MarketPrice=s.MarketPrice,
                OpeningPrice=s.OpeningPrice,
                PriceChange = s.PriceChange
            });


            return await result.ToListAsync();
        }

        // GET: api/Securities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Securities>> GetSecurities(string id)
        {
            var securities = await _context.Securities.FindAsync(id);

            if (securities == null)
            {
                return NotFound();
            }

            return securities;
        }

        // PUT: api/Securities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecurities(string id, Securities securities)
        {
            if (id != securities.SecurityID)
            {
                return BadRequest();
            }

            _context.Entry(securities).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecuritiesExists(id))
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

        // POST: api/Securities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Securities>> PostSecurities(Securities securities)
        {
            _context.Securities.Add(securities);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SecuritiesExists(securities.SecurityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSecurities", new { id = securities.SecurityID }, securities);
        }

        // DELETE: api/Securities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecurities(string id)
        {
            var securities = await _context.Securities.FindAsync(id);
            if (securities == null)
            {
                return NotFound();
            }

            _context.Securities.Remove(securities);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SecuritiesExists(string id)
        {
            return _context.Securities.Any(e => e.SecurityID == id);
        }
    }
}
