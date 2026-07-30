using Microsoft.AspNetCore.Identity;
using System.Security.AccessControl;

namespace IdentityMail.web.Entities
{
    public class AppUser: IdentityUser<int>
    {
        public String  FirstName { get; set; }
        public String  LastName { get; set; }
        public string?  ProfileImageUrl { get; set; }

        public List<UserMessage> SentMessages { get; set; }
        public List<UserMessage> ReceivedMessages { get; set; }

    }
}
