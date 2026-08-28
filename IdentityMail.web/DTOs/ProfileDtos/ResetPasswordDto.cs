using System.ComponentModel.DataAnnotations;

namespace IdentityMail.web.DTOs.ProfileDtos
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Yeni şifre zorunludur.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}
