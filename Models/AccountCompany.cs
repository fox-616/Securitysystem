using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI_3.Models;

public partial class AccountCompany
{
    [Key]
    public int ACSN { get; set; }

    [Display(Name = "會員編號")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(20, ErrorMessage = "會員編號最多20個字")]
    public string AccountID { get; set; } = null!;

    [Display(Name = "公司代碼")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(10, ErrorMessage = "公司代碼最多10個字")]
    public string CompanyID { get; set; } = null!;

    [JsonIgnore]
    public virtual Accounts? Account { get; set; }

    [JsonIgnore]
    public virtual Company? Company { get; set; }
}
