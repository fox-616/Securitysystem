using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI_3.Models;

public partial class Shareholder
{
    [Key]
    [Display(Name = "股東社會安全碼")]
    public string SSN { get; set; } = null!;

    [Display(Name = "股東名稱")]
    public string ShareholderName { get; set; } = null!;

    [Display(Name = "出表日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime ShareholderReportingDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Invest> Invest { get; set; } = new List<Invest>();
}
