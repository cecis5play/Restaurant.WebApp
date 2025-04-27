using Restaurant.WebApp.Models;

namespace Restaurant.WebApp.Services
{
    public interface IReserveService
    {
        int Create(ReserveFormViewModel model, string userId);
        public IEnumerable<RoomTypeViewModel> getRoomModelTypes();
        IEnumerable<ReservationViewModel> GetUserReservations(string userId);
        void CancelReservation(int id, string userId);
        IEnumerable<ReservationViewModel> GetAllReservations(string emailFilter = null);
        void DeleteReservation(int id);
    }
}
