namespace WebAPI_3.DTO
{
    public class FinancialDTO
    {
        public string CompanyID { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public int FinancialYear { get; set; }

        public byte Quarter { get; set; }

        public decimal OperatingRevenue { get; set; }

        public decimal EPS { get; set; }


    }
}
