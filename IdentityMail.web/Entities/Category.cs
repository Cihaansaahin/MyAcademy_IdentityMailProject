namespace IdentityMail.web.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<UserMessage> UserMessages { get; set; } = new List<UserMessage>();
    }
}
