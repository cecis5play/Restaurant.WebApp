using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebApp.Data.Entities
{
    public class RoomType
    {
        public RoomType()
        {
            Reservations = new HashSet<Reservation>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public HashSet<Reservation> Reservations { get; set; }
    }
}
