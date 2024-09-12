using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_3.Models;

public partial class Securities
{
    [Key]
    [Display(Name = "股票代號")]
    [StringLength(10, ErrorMessage = "股票代號最多10個字")]
    public string SecurityID { get; set; } = null!;

    [Display(Name = "股票名稱")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(30, ErrorMessage = "股票名稱最多30個字")]
    public string SecurityName { get; set; } = null!;

    [Display(Name = "當前價格")]
    [Required(ErrorMessage = "必填欄位")]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal MarketPrice { get; set; }

    [Display(Name = "開盤價")]
    [Required(ErrorMessage = "必填欄位")]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal OpeningPrice { get; set; }

    [Display(Name = "收盤價")]
    [Required(ErrorMessage = "必填欄位")]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal ClosingPrice { get; set; }

    [Display(Name = "最高價")]
    [Required(ErrorMessage = "必填欄位")]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal HighestPrice { get; set; }

    [Display(Name = "最低價")]
    [Required(ErrorMessage = "必填欄位")]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal LowestPrice { get; set; }

    [Display(Name = "漲跌幅")]
    [Required(ErrorMessage = "必填欄位")]
    public decimal PriceChange { get; set; }

    [Display(Name = "成交量")]
    [Required(ErrorMessage = "必填欄位")]
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public long TradeVolume { get; set; }

    [Display(Name = "成交額")]
    [Required(ErrorMessage = "必填欄位")]
    [DisplayFormat(DataFormatString = "{0:N0}")]
    public long TradeValue { get; set; }

    [Display(Name = "交易時間")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime TradeTime { get; set; }

    [Display(Name = "公司代碼")]
    [StringLength(10, ErrorMessage = "公司代碼最多10個字")]
    public string CompanyID { get; set; } = null!;

    public virtual ICollection<AccountSecurity> AccountSecurity { get; set; } = new List<AccountSecurity>();

    public virtual Company Company { get; set; } = null!;
}
