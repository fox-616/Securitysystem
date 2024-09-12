namespace WebAPI_3.DTO
{
    public class AccountSecuritiesDTO
    {
        public string AccountID { get; set; } = null!;

        public string SecurityID { get; set; } = null!;

        public string SecurityName { get; set; } = null!;

        public decimal MarketPrice { get; set; }

        //public decimal OpeningPrice { get; set; }

        //public decimal PriceChange { get; set; }

        public int? SharesOwned { get; set; }

        public decimal? UnrealizedProfitLoss { get; set; }

        public decimal? ProfitLossPercentage { get; set; }


    }
}
