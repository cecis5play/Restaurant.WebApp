using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.WebApp.Data.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime ReservationDate { get; set; }
        [Required]
        [MaxLength(20)]
        public int PersonNumber { get; set; }
        [MaxLength(20)]
        public int? ChildrenNumber { get; set; }
        [Required]
        [ForeignKey(nameof(RoomType))]
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime ReservationHour { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
