namespace Restaurant.WebApp.Models
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int PersonNumber { get; set; }
        public int? ChildrenNumber { get; set; }
        public string RoomTypeName { get; set; }
        public string Status { get; set; }
        public string CustomerName { get; set; }
    }
}
