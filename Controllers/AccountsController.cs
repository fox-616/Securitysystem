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
    [Route("api[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly SecuritiesSystemContext _context;

        public AccountsController(SecuritiesSystemContext context)
        {
            _context = context;
        }

        // GET: apiAccounts
        //加入 DTO 縮減物件功能
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountsDTO>>> GetAccounts()
        {
            var accountID = HttpContext.Session.GetString("accountID");

            if (string.IsNullOrEmpty(accountID))
            {
                // 使用 Unauthorized 方法返回 401 狀態碼
                var error = new { message = "未登入" };
                return Unauthorized(error);

            }

            // accountID 若是 admin ，則顯示所有資料
            if (accountID == "admin")
            {
                var resultadmin = _context.Accounts
                .Select(a => new AccountsDTO
                {
                    AccountID = a.AccountID,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    IdentityNumber = a.IdentityNumber,
                    SecuritiesAccountID = a.SecuritiesAccountID,
                    AccStatus = a.AccStatus
                });

                return await resultadmin.ToListAsync();
            }


            //var result = _context.Accounts
            var result = _context.Accounts.Where(a => a.AccountID == accountID)
                .Select(a => new AccountsDTO
                {
                    AccountID = a.AccountID,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    IdentityNumber = a.IdentityNumber,
                    SecuritiesAccountID = a.SecuritiesAccountID,
                    AccStatus = a.AccStatus
                });


            return await result.ToListAsync();
        }

        // GET: apiAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Accounts>> GetAccounts(string id)
        {
            var accounts = await _context.Accounts.FindAsync(id);

            if (accounts == null)
            {
                return NotFound();
            }

            return accounts;
        }

        // PUT: apiAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccounts(string id,Accounts accounts)
        {
            if (id != accounts.AccountID)
            {
                return BadRequest();
            }

            _context.Entry(accounts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountsExists(id))
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

        // POST: apiAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Accounts>> PostAccounts(Accounts accounts)
        {
            _context.Accounts.Add(accounts);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountsExists(accounts.AccountID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccounts", new { id = accounts.AccountID }, accounts);
        }

        // DELETE: api  Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccounts(string id)
        {
            var accounts = await _context.Accounts.FindAsync(id);
            if (accounts == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(accounts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountsExists(string id)
        {
            return _context.Accounts.Any(e => e.AccountID == id);
        }
    }
}
