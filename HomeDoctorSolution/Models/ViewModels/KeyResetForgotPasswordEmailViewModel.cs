using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HomeDoctorSolution.Models.ViewModel
{
    public class KeyResetForgotPasswordEmailViewModel
    {
        public string Key { get; set; }
        [Required(ErrorMessage = "Chưa điền mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Chưa điền xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận khác với mật khẩu")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }
        public bool KeyUpToDate { get; set; } = true;
        public string Hash { get; set; }
    }
}