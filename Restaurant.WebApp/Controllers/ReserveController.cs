using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApp.Models;
using Restaurant.WebApp.Services;

namespace Restaurant.WebApp.Controllers
{
    public class ReserveController : Controller
    {
        private readonly IReserveService service;

        public ReserveController(IReserveService service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {

            var roomTypes = service.getRoomModelTypes();
            var reservationModel = new ReserveFormViewModel()
            {
                RoomTypes = roomTypes,
            };

            return View(reservationModel);
        }
        [HttpPost]
        public IActionResult Add(ReserveFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newReservation = this.service.Create(model);


            return RedirectToAction(nameof(Index));

        }
    }
}
