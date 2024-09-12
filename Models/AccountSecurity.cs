using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI_3.Models;

public partial class AccountSecurity
{
    [Key]
    [Display(Name = "會員編號")]
    public string AccountID { get; set; } = null!;

    [Display(Name = "股票代號")]
    [StringLength(10, ErrorMessage = "股票代號最多10個字")]
    public string SecurityID { get; set; } = null!;

    [Display(Name = "購買股數")]
    //[RegularExpression(@"^\d+$", ErrorMessage = "請輸入正整數")]
    [Range(0, int.MaxValue, ErrorMessage = "購買價格不可為負數。")]
    public int? PurchasedShares { get; set; }

    [Display(Name = "購買價格")]
    [Range(0, int.MaxValue, ErrorMessage = "購買價格不可為負數。")]
    public decimal? PurchasePrice { get; set; }

    [Display(Name = "持有股數")]
    public int? SharesOwned { get; set; }

    [Display(Name = "持有成本")]
    public decimal? HoldingCost { get; set; }

    [Display(Name = "平均價格")]
    public decimal? AveragePrice { get; set; }

    [Display(Name = "市值")]
    public decimal? MarketPrice { get; set; }

    [Display(Name = "參考損益")]
    public decimal? UnrealizedProfitLoss { get; set; }

    [Display(Name = "損益率")]
    public decimal? ProfitLossPercentage { get; set; }

    [Display(Name = "買賣")]
    public bool? Isbuy { get; set; }

    [JsonIgnore]
    public virtual Accounts? Account { get; set; }

    [JsonIgnore]
    public virtual Securities? Security { get; set; }
}
