namespace WebAPI_3.DTO
{
    public class AccountsDTO
    {
        public string AccountID { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string IdentityNumber { get; set; } = null!;

        public string SecuritiesAccountID { get; set; } = null!;

        public int AccStatus { get; set; }

    }
}
