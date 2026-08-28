namespace IdentityMail.web.Entities
{
    public class UserMessage
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime SendDate { get; set; }
        public bool IsRead { get; set; }
        public bool IsImportant { get; set; }

        // Yeni Eklenen Özellikler
        public bool IsStarred { get; set; }
        public bool IsDeleted { get; set; }

        // Kategori İlişkisi (Opsiyonel / Nullable)
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        // Gönderici ve Alıcı İlişkileri
        public int SenderId { get; set; }
        public AppUser Sender { get; set; } = null!;

        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; } = null!;
    }
}