using System.ComponentModel.DataAnnotations;

namespace IdentityMail.web.DTOs.UserDtos.UserMessageDtos
{
    public class SendMailDto
    {
        [Required(ErrorMessage = "Alıcı mail adresi boş bırakılamaz.")]
        public string ReceiverMail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
    }
}
