using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPI_3.Models;

public partial class Accounts
{
    [Display(Name = "會員編號")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(20, ErrorMessage = "會員編號最多20個字")]
    public string AccountID { get; set; } = null!;

    [Display(Name = "姓")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(20, ErrorMessage = "姓最多20個字")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "名")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(20, ErrorMessage = "名最多20個字")]
    public string LastName { get; set; } = null!;

    [Display(Name = "密碼")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(30)]
    [MinLength(8, ErrorMessage = "密碼最少8碼")]
    [MaxLength(30, ErrorMessage = "密碼最多30碼")]
    //[DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "身分證號")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(10, ErrorMessage = "身分證號必須10碼")]
    [RegularExpression("[A-Z][1-2][0-9]{8}", ErrorMessage = "格式錯誤")]
    public string IdentityNumber { get; set; } = null!;

    [Display(Name = "證券帳號")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(20, ErrorMessage = "證券帳號最多20個字")]
    public string SecuritiesAccountID { get; set; } = null!;

    [Display(Name = "憑證序號")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(20, ErrorMessage = "憑證序號最多20個字")]
    public string SerialNumber { get; set; } = null!;

    [Display(Name = "憑證起始日")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime? IssueDate { get; set; }

    [Display(Name = "憑證到期日")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
    public DateTime? ExpiryDate { get; set; }

    [Display(Name = "帳號狀態")]
    [Required(ErrorMessage = "必填欄位")]
    public int AccStatus { get; set; }

    [JsonIgnore]
    public virtual ICollection<AccountCompany>? AccountCompany { get; set; } = new List<AccountCompany>();

    [JsonIgnore]
    public virtual ICollection<AccountSecurity>? AccountSecurity { get; set; } = new List<AccountSecurity>();
}
