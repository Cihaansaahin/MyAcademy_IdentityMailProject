using System.ComponentModel.DataAnnotations;

namespace IdentityMail.web.DTOs.UserMessageDtos
{
    public class SendMailDto
    {
        [Required(ErrorMessage = "Alıcı mail adresi boş bırakılamaz.")]
        public string ReceiverMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
