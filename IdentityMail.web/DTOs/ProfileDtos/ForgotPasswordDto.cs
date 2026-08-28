using System.ComponentModel.DataAnnotations;

namespace IdentityMail.web.DTOs.ProfileDtos
{
    public class ForgotPasswordDto
    {
       
            [Required(ErrorMessage = "Email adresi zorunludur.")]
            [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
            public string Email { get; set; }
        }
}
