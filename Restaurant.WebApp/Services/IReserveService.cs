using Restaurant.WebApp.Models;

namespace Restaurant.WebApp.Services
{
    public interface IReserveService
    {
        int Create(ReserveFormViewModel model);
        public IEnumerable<RoomTypeViewModel> getRoomModelTypes();
    }
}
