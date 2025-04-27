namespace Restaurant.WebApp.Models
{
    public class ContactRequest
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
