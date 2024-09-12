using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI_3.Models;

public partial class Invest
{
    [Display(Name = "公司代碼")]
    public string CompanyID { get; set; } = null!;

    [Display(Name = "股東社會安全碼")]
    public string SSN { get; set; } = null!;

    [Display(Name = "持股比例")]
    public decimal? OwnershipPercentage { get; set; }

    [JsonIgnore]
    public virtual Company? Company { get; set; } = null!;

    [JsonIgnore]
    public virtual Shareholder? SSNNavigation { get; set; } = null!;
}
