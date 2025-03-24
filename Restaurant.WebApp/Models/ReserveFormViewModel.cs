using Restaurant.WebApp.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.WebApp.Models
{
    public class ReserveFormViewModel
    {
        public ReserveFormViewModel()
        {
            RoomTypes = new List<RoomTypeViewModel>();
        }
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public int PersonNumber { get; set; }
        public int? ChildrenNumber { get; set; }
        public int RoomTypeId { get; set; }
        public DateTime ReservationHour { get; set; }
        public IEnumerable<RoomTypeViewModel> RoomTypes { get; set; }
    }
}
