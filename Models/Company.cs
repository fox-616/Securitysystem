using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_3.Models;

public partial class Company
{
    [Key]
    [Display(Name = "公司代碼")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(10, ErrorMessage = "公司代碼最多10個字")]
    public string CompanyID { get; set; } = null!;

    [Display(Name = "公司名稱")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(50, ErrorMessage = "公司名稱最多50個字")]
    public string CompanyName { get; set; } = null!;

    [Display(Name = "公司連絡電話")]
    [StringLength(15, ErrorMessage = "公司連絡電話最多15個字")]
    public string? CompanyPhone { get; set; }

    [Display(Name = "公司地址")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(200, ErrorMessage = "公司地址最多200個字")]
    public string CompanyAddress { get; set; } = null!;

    [Display(Name = "行業別")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(50, ErrorMessage = "行業別最多50個字")]
    public string Industry { get; set; } = null!;

    [Display(Name = "發言人")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(50, ErrorMessage = "發言人最多50個字")]
    public string Spokesperson { get; set; } = null!;

    [Display(Name = "發言人職稱")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(50, ErrorMessage = "發言人職稱最多50個字")]
    public string SpokespersonTitle { get; set; } = null!;

    [Display(Name = "發言人電話")]
    [StringLength(15, ErrorMessage = "發言人電話最多15個字")]
    public string? SpokespersonPhone { get; set; }

    [Display(Name = "發言人電子信箱")]
    [StringLength(100, ErrorMessage = "發言人電子信箱最多100個字")]
    public string? SpokespersonEmail { get; set; }

    [Display(Name = "代理發言人")]
    [StringLength(50, ErrorMessage = "代理發言人最多50個字")]
    public string? DeputySpokesperson { get; set; }

    [Display(Name = "董事長")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(50, ErrorMessage = "董事長最多50個字")]
    public string Chairman { get; set; } = null!;

    [Display(Name = "公司簡介")]
    [DataType(DataType.MultilineText)]
    public string? Description { get; set; }

    [Display(Name = "公司網址")]
    [StringLength(100, ErrorMessage = "公司網址最多100個字")]
    public string? Website { get; set; }

    [Display(Name = "會計事務所")]
    [StringLength(100, ErrorMessage = "會計事務所最多100個字")]
    public string? AccountingFirmName { get; set; }

    [Display(Name = "出表日期")]
    [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
    public DateTime? CompanyReportingDate { get; set; }

    [Display(Name = "財務年度")]
    public int? FinancialYear { get; set; }

    [Display(Name = "季度")]
    public byte? Quarter { get; set; }

    public virtual ICollection<AccountCompany> AccountCompany { get; set; } = new List<AccountCompany>();

    public virtual ICollection<Dividend> Dividend { get; set; } = new List<Dividend>();

    public virtual ICollection<Financial> Financial { get; set; } = new List<Financial>();

    public virtual ICollection<Invest> Invest { get; set; } = new List<Invest>();

    public virtual ICollection<Securities> Securities { get; set; } = new List<Securities>();
}
