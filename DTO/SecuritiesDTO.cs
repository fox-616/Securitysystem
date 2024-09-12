namespace WebAPI_3.DTO
{
    public class SecuritiesDTO
    {
        public string SecurityID { get; set; } = null!;

        public string SecurityName { get; set; } = null!;

        public decimal MarketPrice { get; set; }

        public decimal OpeningPrice { get; set; }


        public decimal PriceChange { get; set; }

    }
}
