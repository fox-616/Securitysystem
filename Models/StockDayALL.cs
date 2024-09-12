using System.ComponentModel.DataAnnotations;

namespace WebAPI_3.Models
{
    public class StockDayALL
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string TradeVolume { get; set; }
        public string TradeValue { get; set; }
        public string OpeningPrice { get; set; }
        public string HighestPrice { get; set; }
        public string LowestPrice { get; set; }
        public string ClosingPrice { get; set; }
        public string Change { get; set; }
        public string Transaction { get; set; }


    }
}

//Code    string
//證券代號

//Name	string
//證券名稱

//TradeVolume	string
//成交股數

//TradeValue	string
//成交金額

//OpeningPrice	string
//開盤價

//HighestPrice	string
//最高價

//LowestPrice	string
//最低價

//ClosingPrice	string
//收盤價

//Change	string
//漲跌價差

//Transaction	string
//成交筆數