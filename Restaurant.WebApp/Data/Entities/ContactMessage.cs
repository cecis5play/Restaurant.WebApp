using System;

namespace Restaurant.WebApp.Data.Entities
{
    public class ContactMessage
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public bool IsHandled { get; set; } = false;
    }
}