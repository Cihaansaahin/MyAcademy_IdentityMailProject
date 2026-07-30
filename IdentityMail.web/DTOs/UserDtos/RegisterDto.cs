namespace IdentityMail.web.DTOs.UserDtos
{
    public class RegisterDto
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
