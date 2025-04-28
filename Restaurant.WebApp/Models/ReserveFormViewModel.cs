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
        [Required(ErrorMessage = "Трябва да сложиш дата")]
        [CustomDate(ErrorMessage = "Можеш да запазиш максимално 1 година за напред")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = false, DataFormatString = "{0:yyyy-MM-dd HH}")]
        public DateTime ReservationDate { get; set; }
        [Range(1, 20, ErrorMessage = "Няма маса за по-малко от 1 човек и повече от 20 човека")]
        public int PersonNumber { get; set; }
        public int? ChildrenNumber { get; set; }
        public int RoomTypeId { get; set; }
        public IEnumerable<RoomTypeViewModel> RoomTypes { get; set; }
    }
}
