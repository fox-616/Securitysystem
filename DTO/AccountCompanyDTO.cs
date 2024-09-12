namespace WebAPI_3.DTO
{
    public class AccountCompanyDTO
    {
        public int ACSN { get; set; }

        public string AccountID { get; set; } = null!;

        public string CompanyID { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string? CompanyPhone { get; set; }

        public string Industry { get; set; } = null!;

        public string Chairman { get; set; } = null!;

        public string? Website { get; set; }
    }
}
