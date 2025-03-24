using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Restaurant.WebApp.Data;
using Restaurant.WebApp.Data.Entities;
using Restaurant.WebApp.Models;

namespace Restaurant.WebApp.Services
{
    public class ReserveService : IReserveService
    {
        public readonly ApplicationDbContext context;
        public ReserveService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public int Create(ReserveFormViewModel model)
        {
            var reservation = new Reservation
            {
                ReservationDate = model.ReservationDate,
                PersonNumber = model.PersonNumber,
                ChildrenNumber = model.ChildrenNumber,
                RoomTypeId = model.RoomTypeId,
                ReservationHour = model.ReservationHour,
            };

            this.context.Reservations.Add(reservation);
            this.context.SaveChanges();
            return reservation.Id;
        }
        public IEnumerable<RoomTypeViewModel> getRoomModelTypes()
        {
            var roomType = context.RoomTypes.Select(v => new RoomTypeViewModel
            {
                Id = v.Id,
                Name = v.Name,
            }).ToList();
            return roomType;
        }
    }
}
