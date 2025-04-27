using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpPost]
        public int Create(ReserveFormViewModel model, string userId)
        {
            var reservation = new Reservation
            {
                ReservationDate = model.ReservationDate,
                PersonNumber = model.PersonNumber,
                ChildrenNumber = model.ChildrenNumber,
                RoomTypeId = model.RoomTypeId,
                UserId = userId
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
        public IEnumerable<ReservationViewModel> GetUserReservations(string userId)
        {
            var now = DateTime.Now;

            var expired = context.Reservations
                .Where(r => r.UserId == userId && r.ReservationDate < now)
                .ToList();

            if (expired.Any())
            {
                context.Reservations.RemoveRange(expired);
                context.SaveChanges();
            }

           
            return context.Reservations
                .Where(r => r.UserId == userId)
                .Select(r => new ReservationViewModel
                {
                    Id = r.Id,
                    ReservationDate = r.ReservationDate,
                    PersonNumber = r.PersonNumber,
                    ChildrenNumber = r.ChildrenNumber,
                    RoomTypeName = r.RoomType.Name,
                })
                .OrderBy(r => r.ReservationDate)
                .ToList();
        }
        public void CancelReservation(int id, string userId)
        {
            var reservation = context.Reservations
                .FirstOrDefault(r => r.Id == id && r.UserId == userId);

            if (reservation != null)
            {
                context.Reservations.Remove(reservation);
                context.SaveChanges();
            }
        }
        public IEnumerable<ReservationViewModel> GetAllReservations(string emailFilter = null)
        {
            var query = context.Reservations
                .Include(r => r.RoomType)
                .Include(r => r.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(emailFilter))
            {
                query = query.Where(r => r.User.Email.ToLower().Contains(emailFilter.ToLower()));
            }

            return query
                .Select(r => new ReservationViewModel
                {
                    Id = r.Id,
                    ReservationDate = r.ReservationDate,
                    PersonNumber = r.PersonNumber,
                    ChildrenNumber = r.ChildrenNumber,
                    RoomTypeName = r.RoomType.Name,
                    CustomerName = r.User.Email
                })
                .ToList();
        }

        public void DeleteReservation(int id)
        {
            var reservation = context.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation != null)
            {
                context.Reservations.Remove(reservation);
                context.SaveChanges();
            }
        }

    }
}
