using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI_3.Models;

public partial class Financial
{
    [Display(Name = "財務年度")]
    [Required(ErrorMessage = "必填欄位")]
    [StringLength(20, ErrorMessage = "會員編號最多20個字")]
    public int FinancialYear { get; set; }

    [Display(Name = "季度")]
    [Required(ErrorMessage = "必填欄位")]
    public byte Quarter { get; set; }

    [Display(Name = "營業收入")]
    [Required(ErrorMessage = "必填欄位")]
    public decimal OperatingRevenue { get; set; }

    [Display(Name = "營業成本")]
    [Required(ErrorMessage = "必填欄位")]
    public decimal OperatingCosts { get; set; }

    [Display(Name = "業外收支")]
    [Required(ErrorMessage = "必填欄位")]
    public decimal NonOperatingIncomeExpenses { get; set; }

    [Display(Name = "毛利")]
    [Required(ErrorMessage = "必填欄位")]
    public decimal GrossProfit { get; set; }

    [Display(Name = "營業費用")]
    [Required(ErrorMessage = "必填欄位")]
    public decimal OperatingExpenses { get; set; }

    [Display(Name = "所得稅費用")]
    [Required(ErrorMessage = "必填欄位")]
    public decimal IncomeTaxExpense { get; set; }

    [Display(Name = "營業利益")]
    [Required(ErrorMessage = "必填欄位")]
    public decimal OperatingIncome { get; set; }

    [Display(Name = "稅前利潤")]
    [Required(ErrorMessage = "必填欄位")]
    public decimal ProfitBeforeTax { get; set; }

    [Display(Name = "稅後利潤")]
    [Required(ErrorMessage = "必填欄位")]
    public decimal NetProfitAfterTax { get; set; }

    [Display(Name = "每股盈餘")]
    [Required(ErrorMessage = "必填欄位")]
    public decimal EPS { get; set; }

    [Display(Name = "財務報告日期")]
    [Required(ErrorMessage = "必填欄位")]
    public DateTime FinancialReportDate { get; set; }

    [Display(Name = "季度")]
    [Required(ErrorMessage = "必填欄位")]
    public string CompanyID { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;
}
