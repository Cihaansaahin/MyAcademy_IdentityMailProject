namespace IdentityMail.web.Areas.Admin.Models
{
    internal class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalMessages { get; set; }
        public int TodayMessages { get; set; }
        public int UnreadMessages { get; set; }
        public int TrashMessages { get; set; }
    }
}