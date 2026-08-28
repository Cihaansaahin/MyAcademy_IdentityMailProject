using System.ComponentModel.DataAnnotations;

namespace IdentityMail.web.DTOs.ProfileDtos
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Mevcut Şifrenizi Girin.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Yeni Şifre Zorunlu.")]

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Yeni Şifre en az 8 karakter uzunluğunda olmalı ve en az bir büyük harf, bir küçük harf, bir rakam ve bir özel karakter içermelidir.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }
    }
}
