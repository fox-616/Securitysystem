using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_3.Controllers
{
    [Route("apiAuthController")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // 檢查用戶是否登入
        [HttpGet("checkStatus")]
        public IActionResult CheckStatus()
        {

            var accountID = HttpContext.Session.GetString("accountID");

            if (string.IsNullOrEmpty(accountID))
            {
                return Unauthorized(new { isLoggedIn = false });
            }

            bool isAdmin = accountID == "admin";


            return Ok(new { isLoggedIn = true , isAdmin});
        }

        // 登出
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("accountID"); // 移除 Session
            return Ok(new { message = "登出成功" });
        }
    }
}