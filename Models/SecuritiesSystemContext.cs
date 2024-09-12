using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAPI_3.Models;

namespace WebAPI_3.Models;

public partial class SecuritiesSystemContext : DbContext
{
    public SecuritiesSystemContext()
    {
    }

    public SecuritiesSystemContext(DbContextOptions<SecuritiesSystemContext> options)
        : base(options)
    {
    }
    public DbSet<StockDayALL> StockDayALLs { get; set; }

    public virtual DbSet<AccountCompany> AccountCompany { get; set; }

    public virtual DbSet<AccountSecurity> AccountSecurity { get; set; }

    public virtual DbSet<Accounts> Accounts { get; set; }

    public virtual DbSet<Company> Company { get; set; }

    public virtual DbSet<Dividend> Dividend { get; set; }

    public virtual DbSet<Financial> Financial { get; set; }

    public virtual DbSet<Invest> Invest { get; set; }

    public virtual DbSet<Securities> Securities { get; set; }

    public virtual DbSet<Shareholder> Shareholder { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=MSI\\MSSQLSERVER01;Database=SecuritiesSystem;TrustServerCertificate=True;User ID=jim;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountCompany>(entity =>
        {
            entity.HasKey(e => e.ACSN).HasName("PK__AccountC__06FD78AF2C24E18D");

            entity.Property(e => e.ACSN)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AccountID)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CompanyID)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.AccountCompany)
                .HasForeignKey(d => d.AccountID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountCo__Accou__656C112C");

            entity.HasOne(d => d.Company).WithMany(p => p.AccountCompany)
                .HasForeignKey(d => d.CompanyID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountCo__Compa__66603565");
        });

        modelBuilder.Entity<AccountSecurity>(entity =>
        {
            entity.HasKey(e => new { e.AccountID, e.SecurityID }).HasName("PK__AccountS__2D651513CD971968");

            entity.Property(e => e.AccountID)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SecurityID)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.AveragePrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.HoldingCost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MarketPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProfitLossPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UnrealizedProfitLoss).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PurchasedShares).HasColumnType("int");
            entity.Property(e => e.SharesOwned).HasColumnType("int");
            entity.Property(e => e.Isbuy).HasColumnType("bit");


            entity.HasOne(d => d.Account).WithMany(p => p.AccountSecurity)
                .HasForeignKey(d => d.AccountID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountSe__Accou__534D60F1");

            entity.HasOne(d => d.Security).WithMany(p => p.AccountSecurity)
                .HasForeignKey(d => d.SecurityID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountSe__Secur__5441852A");
        });

        modelBuilder.Entity<Accounts>(entity =>
        {
            entity.HasKey(e => e.AccountID).HasName("PK__Accounts__349DA5866250D33D");

            entity.HasIndex(e => e.SerialNumber, "UQ__Accounts__048A0008CCEB51D1").IsUnique();

            entity.HasIndex(e => e.SecuritiesAccountID, "UQ__Accounts__C2FD20C638A4F248").IsUnique();

            entity.Property(e => e.AccountID)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdentityNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SecuritiesAccountID)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyID).HasName("PK__Company__2D971C4C7912E088");

            entity.Property(e => e.CompanyID)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.AccountingFirmName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Chairman)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CompanyAddress)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CompanyPhone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CompanyReportingDate).HasColumnType("datetime");
            entity.Property(e => e.DeputySpokesperson)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Industry)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Spokesperson)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SpokespersonEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SpokespersonPhone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.SpokespersonTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Website)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Dividend>(entity =>
        {
            entity.HasKey(e => new { e.CompanyID, e.ExDividendDate }).HasName("PK__Dividend__550823A4C8EB0B96");

            entity.Property(e => e.CompanyID)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ExDividendDate).HasColumnType("datetime");
            entity.Property(e => e.DividendPerShare).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DividendType)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.Dividend)
                .HasForeignKey(d => d.CompanyID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dividend__Compan__46E78A0C");
        });

        modelBuilder.Entity<Financial>(entity =>
        {
            entity.HasKey(e => new { e.CompanyID, e.FinancialYear, e.Quarter }).HasName("PK__Financia__901AEA25CDC237B0");

            entity.Property(e => e.CompanyID)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.EPS).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FinancialReportDate).HasColumnType("datetime");
            entity.Property(e => e.GrossProfit).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.IncomeTaxExpense).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.NetProfitAfterTax).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.NonOperatingIncomeExpenses).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.OperatingCosts).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.OperatingExpenses).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.OperatingIncome).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.OperatingRevenue).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.ProfitBeforeTax).HasColumnType("decimal(15, 2)");

            entity.HasOne(d => d.Company).WithMany(p => p.Financial)
                .HasForeignKey(d => d.CompanyID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Financial__Compa__4BAC3F29");
        });

        modelBuilder.Entity<Invest>(entity =>
        {
            entity.HasKey(e => new { e.CompanyID, e.SSN }).HasName("PK__Invest__E136F4AF89F7ACBD");

            entity.Property(e => e.CompanyID)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SSN)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.OwnershipPercentage).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Company).WithMany(p => p.Invest)
                .HasForeignKey(d => d.CompanyID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invest__CompanyI__4316F928");

            entity.HasOne(d => d.SSNNavigation).WithMany(p => p.Invest)
                .HasForeignKey(d => d.SSN)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invest__SSN__440B1D61");
        });

        modelBuilder.Entity<Securities>(entity =>
        {
            entity.HasKey(e => e.SecurityID).HasName("PK__Securiti__9F8B09509C07E6B9");

            entity.Property(e => e.SecurityID)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ClosingPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CompanyID)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.HighestPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.LowestPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MarketPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.OpeningPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PriceChange).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.SecurityName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TradeTime).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.Securities)
                .HasForeignKey(d => d.CompanyID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Securitie__Compa__4E88ABD4");
        });

        modelBuilder.Entity<Shareholder>(entity =>
        {
            entity.HasKey(e => e.SSN).HasName("PK__Sharehol__CA1E8E3D258054BB");

            entity.Property(e => e.SSN)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ShareholderName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ShareholderReportingDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<WebAPI_3.Models.Login> Login { get; set; } = default!;
}
