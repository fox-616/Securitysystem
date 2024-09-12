using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_3.Models;
using WebAPI_3.DTO;
using Microsoft.Identity.Client;

namespace WebAPI_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountCompaniesController : ControllerBase
    {
        private readonly SecuritiesSystemContext _context;

        public AccountCompaniesController(SecuritiesSystemContext context)
        {
            _context = context;
        }

        // GET: api/AccountCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountCompanyDTO>>> GetAccountCompany()
        {
            var accountID = HttpContext.Session.GetString("accountID");

            if (string.IsNullOrEmpty(accountID))
            {
                // 使用 Unauthorized 方法返回 401 狀態碼
                var error = new { message = "未登入" };
                return Unauthorized(error);

            }


            var result = _context.AccountCompany
                .Join(_context.Company,
                a => a.CompanyID,
                c => c.CompanyID,
                (a, c) => new AccountCompanyDTO
                {
                    ACSN=a.ACSN,
                    AccountID = a.AccountID,
                    CompanyID = a.CompanyID,
                    CompanyName = c.CompanyName,
                    CompanyPhone = c.CompanyPhone,
                    Industry = c.Industry,
                    Chairman = c.Chairman,
                    Website = c.Website
                }).Where(a => a.AccountID == accountID);

            return await result.ToListAsync();
        }

        // GET: api/AccountCompanies/5
        [HttpGet("{id}/{companyID}")]
        public async Task<ActionResult<Object>> GetAccountCompany(string id, string companyID)
        {
            //var accountCompany = await _context.AccountCompany.FindAsync(id);

            var result =_context.AccountCompany
                .Join(_context.Company,
                a => a.CompanyID,
                c => c.CompanyID,
                (a, c) => new
                {
                    a.AccountID,
                    a.CompanyID,
                    c.CompanyName,
                    c.CompanyPhone,
                    c.CompanyAddress,
                    c.Industry,
                    c.Spokesperson,
                    c.SpokespersonTitle,
                    c.SpokespersonPhone,
                    c.SpokespersonEmail,
                    c.DeputySpokesperson,
                    c.Chairman,
                    c.Description,
                    c.Website,
                    c.AccountingFirmName,
                    c.CompanyReportingDate,
                    c.FinancialYear,
                    c.Quarter
                });


            if (result == null)
            {
                return NotFound();
            }

            return await result.FirstOrDefaultAsync(a => a.AccountID ==id && a.CompanyID == companyID);

        }

        // PUT: api/AccountCompanies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountCompany(int id, AccountCompany accountCompany)
        {
            if (id != accountCompany.ACSN)
            {
                return BadRequest();
            }

            _context.Entry(accountCompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountCompanyExists(id))
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

        // POST: api/AccountCompanies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountCompany>> PostAccountCompany(AccountCompany accountCompany)
        {
            _context.AccountCompany.Add(accountCompany);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountCompanyExists(accountCompany.ACSN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccountCompany", new { id = accountCompany.ACSN }, accountCompany);
        }

        // DELETE: api/AccountCompanies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountCompany(int id)
        {
            var accountCompany = await _context.AccountCompany.FindAsync(id);
            if (accountCompany == null)
            {
                return NotFound();
            }

            _context.AccountCompany.Remove(accountCompany);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountCompanyExists(int id)
        {
            return _context.AccountCompany.Any(e => e.ACSN == id);
        }
    }
}
