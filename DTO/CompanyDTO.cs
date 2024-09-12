namespace WebAPI_3.DTO
{
    public class CompanyDTO
    {
        public string CompanyID { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string? CompanyPhone { get; set; }

        public string Industry { get; set; } = null!;

        public string Chairman { get; set; } = null!;

        public string? Website { get; set; }

    }
}
