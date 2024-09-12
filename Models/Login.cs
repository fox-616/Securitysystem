using System.ComponentModel.DataAnnotations;

namespace WebAPI_3.Models
{
    public class Login
    {
        [Key]
        [Display(Name = "會員編號")]
        [Required(ErrorMessage = "請輸入帳號")]
        [StringLength(20, ErrorMessage = "會員編號最多20個字")]
        public string AccountID { get; set; } = null!;

        [Display(Name = "密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [StringLength(30)]
        [MinLength(8, ErrorMessage = "密碼最少8碼")]
        [MaxLength(30, ErrorMessage = "密碼最多30碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
