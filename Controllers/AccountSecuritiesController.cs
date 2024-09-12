using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebAPI_3.DTO;
using WebAPI_3.Models;
using System.Diagnostics;


namespace WebAPI_3.Controllers
{
    [Route("api[controller]")]
    [ApiController]
    public class AccountSecuritiesController : ControllerBase
    {
        private readonly SecuritiesSystemContext _context;

        public AccountSecuritiesController(SecuritiesSystemContext context)
        {
            _context = context;
        }

        // GET: apiAccountSecurities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountSecuritiesDTO>>> GetAccountSecurity()
        {

            var accountID = HttpContext.Session.GetString("accountID");

            if (string.IsNullOrEmpty(accountID))
            {
                // 使用 Unauthorized 方法返回 401 狀態碼
                var error = new { message = "未登入" };
                return Unauthorized(error);

            }

            var result = _context.AccountSecurity
                .Join(_context.Securities,
                a => a.SecurityID,
                s => s.SecurityID,
                (a, s) => new AccountSecuritiesDTO
                {
                    AccountID = a.AccountID,
                    SecurityID = s.SecurityID,
                    SecurityName = s.SecurityName,
                    MarketPrice = s.MarketPrice,
                    SharesOwned = a.SharesOwned,
                    //損益︰(市值 - 均價)*股數
                    UnrealizedProfitLoss = (s.MarketPrice - a.AveragePrice) * a.SharesOwned,
                    //損益率︰((市值 - 均價)*股數)/成本

                    ProfitLossPercentage = a.HoldingCost == 0 ? 0 : ((s.MarketPrice - a.AveragePrice) * a.SharesOwned) / a.HoldingCost * 100
                    //a.HoldingCost
                }).Where(a => a.AccountID == accountID);


            return await result.ToListAsync();
        }

        // GET: apiAccountSecurities/5
        //join security
        [HttpGet("{id}/{securityID}")]
        public async Task<ActionResult<Object>> GetAccountSecurity(string id, string securityID)
        {
            //var accountSecurity = await _context.AccountSecurity.FirstOrDefaultAsync(a => a.AccountID == id && a.SecurityID == securityID);
            var result = _context.AccountSecurity
                .Join(_context.Securities,
                a => a.SecurityID,
                s => s.SecurityID,
                (a, s) => new
                {
                    a.AccountID,
                    a.SecurityID,
                    s.SecurityName,
                    a.SharesOwned,
                    a.AveragePrice,
                    a.HoldingCost,
                    s.MarketPrice,
                    a.UnrealizedProfitLoss,
                    a.ProfitLossPercentage
                });


            if (result == null)
            {
                return NotFound();
            }

            return await result.FirstOrDefaultAsync(a => a.AccountID == id && a.SecurityID == securityID);
        }

        // PUT: apiAccountSecurities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountSecurity(string id, AccountSecurity accountSecurity)
        {
            if (id != accountSecurity.AccountID)
            {
                return BadRequest();
            }

            _context.Entry(accountSecurity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountSecurityExists(id))
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

        // POST: apiAccountSecurities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //買進/賣出功能
        [HttpPost]
        public async Task<ActionResult<AccountSecurity>> PostAccountSecurity(AccountSecurity accountSecurity)
        {
            //_context.AccountSecurity.Add(accountSecurity);
            var existingAccountSecurity = await _context.AccountSecurity
        .FirstOrDefaultAsync(a => a.AccountID == accountSecurity.AccountID && a.SecurityID == accountSecurity.SecurityID);

            //// 如果 IsBuy 為 true，保持 PurchasedShares 為正數；反之為負數
            accountSecurity.PurchasedShares = accountSecurity.Isbuy == true ? accountSecurity.PurchasedShares: -1 * accountSecurity.PurchasedShares;


            if (existingAccountSecurity != null)
            {
                // 持有股數︰將 SharesOwned 與 PurchasedShares 相加               
                existingAccountSecurity.SharesOwned += accountSecurity.PurchasedShares;

                //test 功能
                // 持有股數<0，警示並離開
                if (existingAccountSecurity.SharesOwned < 0)
                {
                    // 返回錯誤或警示訊息，並結束操作
                    return BadRequest(new { success = false, message = "持有股數不能小於 0" });
                }

                // 成本︰將 HoldingCost 與 此次購買的成本相加
                existingAccountSecurity.HoldingCost = existingAccountSecurity.SharesOwned == 0 ? 0 : existingAccountSecurity.HoldingCost;
                //existingAccountSecurity.HoldingCost += accountSecurity.PurchasedShares * accountSecurity.PurchasePrice;
                // 均價︰成本/股數
                existingAccountSecurity.AveragePrice = existingAccountSecurity.SharesOwned == 0 ? 0 : existingAccountSecurity.AveragePrice;
                //existingAccountSecurity.AveragePrice = existingAccountSecurity.HoldingCost / existingAccountSecurity.SharesOwned;
                // 損益︰(市值 - 均價)*股數
                existingAccountSecurity.UnrealizedProfitLoss = (existingAccountSecurity.MarketPrice - existingAccountSecurity.AveragePrice) * existingAccountSecurity.SharesOwned;
                //損益率︰損益/成本
                existingAccountSecurity.ProfitLossPercentage = existingAccountSecurity.HoldingCost == 0 ? 0 : existingAccountSecurity.ProfitLossPercentage;
                //existingAccountSecurity.ProfitLossPercentage = existingAccountSecurity.UnrealizedProfitLoss / existingAccountSecurity.HoldingCost;


                // 更新資料庫中的值
                _context.Entry(existingAccountSecurity).State = EntityState.Modified;
            }
            else
            {
                // 如果該帳戶證券不存在，新增一筆新的紀錄
                _context.AccountSecurity.Add(accountSecurity);
                // 新增 SharesOwned
                accountSecurity.SharesOwned = accountSecurity.PurchasedShares;
                // 新增 HoldingCost
                accountSecurity.HoldingCost = accountSecurity.PurchasedShares * accountSecurity.PurchasePrice;
                // 新增 AveragePrice
                accountSecurity.AveragePrice = accountSecurity.PurchasePrice;


            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AccountSecurityExists(accountSecurity.AccountID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAccountSecurity", new { id = accountSecurity.AccountID }, accountSecurity);
        }

        // DELETE: api/AccountSecurities/5
        [HttpDelete("{id}/{securityID}")]
        public async Task<IActionResult> DeleteAccountSecurity(string id,string securityID)
        {
            //var accountSecurity = await _context.AccountSecurity.FindAsync(id);
            var result = await _context.AccountSecurity.Where(a => a.AccountID == id && a.SecurityID == securityID).FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound();
            }

            _context.AccountSecurity.Remove(result);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountSecurityExists(string id)
        {
            return _context.AccountSecurity.Any(e => e.AccountID == id);
        }
    }
}
