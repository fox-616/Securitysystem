using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI_3.Models;

public partial class Dividend
{
    [Display(Name = "公司代碼")]
    public string CompanyID { get; set; } = null!;

    [Display(Name = "除息日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime ExDividendDate { get; set; }

    [Display(Name = "股利金額")]
    public decimal DividendPerShare { get; set; }

    [Display(Name = "股利類型")]
    public string DividendType { get; set; } = null!;

    [JsonIgnore]
    public virtual Company Company { get; set; } = null!;
}
