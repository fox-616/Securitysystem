using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_3.Models;

namespace WebAPI_3.Controllers
{
    [Route("apiLogins")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly SecuritiesSystemContext _context;

        public LoginsController(SecuritiesSystemContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login login)
        {
            if (login == null)
            {
                var invalidLoginResponse = new { message= "請輸入帳號及密碼" } ;
                return BadRequest(invalidLoginResponse);
            }

            var result = await _context.Accounts
                .Where(a => a.AccountID == login.AccountID && a.Password == login.Password && a.AccStatus == 1).FirstOrDefaultAsync();

            if (result == null)
            {
                var errorResponse = new { message = "您的帳號或密碼錯誤，請重新輸入" };
                return Unauthorized(errorResponse);
            }
            else    //如果帳密正確，登入到首頁
            {
                //保留使用者權限到 session
                HttpContext.Session.SetString("accountID",login.AccountID);

                var redirectResponse = new { redirectTo = "/home/index.html" };

                return Ok(redirectResponse);
            }

            
        }

    }
}
