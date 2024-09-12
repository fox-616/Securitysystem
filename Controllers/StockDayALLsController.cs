using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPI_3.Models;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_3.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics;

namespace WebAPI_3.Controllers
{
    [Route("apiStockDayALLs")]
    [ApiController]
    public class StockDayALLsController : ControllerBase
    {
        //for 儲存資料使用
        private readonly SecuritiesSystemContext _context;

        public StockDayALLsController(SecuritiesSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<StockDayALL>> Get()
        //public async Task<IActionResult> Get()
        {
            //正常讀取Third API 作法
            string url = "https://openapi.twse.com.tw/v1/exchangeReport/STOCK_DAY_ALL";
            
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = Int32.MaxValue;

            var resp= await client.GetStringAsync(url);

            var collection = JsonConvert.DeserializeObject<IEnumerable<StockDayALL>>(resp);

            // 儲存資料到資料庫
            await SaveDataToDatabase(collection);

            return collection;

            //DTO 寫法
            //var collection = JsonConvert.DeserializeObject<IEnumerable<StockDayALL>>(resp);

            //var result = collection.Select(s => new StockDayALLDTO
            //{
            //    Code = s.Code,
            //    Name = s.Name,
            //    OpeningPrice = s.OpeningPrice,
            //    HighestPrice = s.HighestPrice,
            //    LowestPrice = s.LowestPrice,
            //    ClosingPrice = s.ClosingPrice
            //});
            //return result;

        }

        [HttpGet("keyword")]
        public async Task<ActionResult<IEnumerable<StockDayALL>>> SearchStocks(string keyword)
        {
            // 從資料庫中搜尋符合關鍵字的資料
            var filteredStocks = await _context.StockDayALLs
                .Where(s => s.Name.Contains(keyword) || s.Code.Contains(keyword))
                .ToListAsync();

            // 如果沒有符合的結果，可以返回 NotFound
            if (filteredStocks == null || !filteredStocks.Any())
            {
                return NotFound(new { message = "找不到符合的股票資料" });
            }

            return Ok(filteredStocks);
        }



        private async Task SaveDataToDatabase(IEnumerable<StockDayALL> stockData)
        {
            // 清空資料表，避免重複資料
            _context.StockDayALLs.RemoveRange(_context.StockDayALLs);
            await _context.SaveChangesAsync();

            // 新增資料
            _context.StockDayALLs.AddRange(stockData);
            await _context.SaveChangesAsync();
        }


    }
}
