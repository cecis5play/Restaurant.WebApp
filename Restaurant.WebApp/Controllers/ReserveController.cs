using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApp.Models;
using Restaurant.WebApp.Services;

namespace Restaurant.WebApp.Controllers
{
    public class ReserveController : Controller
    {
        private readonly IReserveService service;
        private readonly UserManager<IdentityUser> userManager;

        public ReserveController(IReserveService service, UserManager<IdentityUser> userManager)
        {
            this.service = service;
            this.userManager = userManager;
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
                ReservationDate = DateTime.Now,
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
            var userId = userManager.GetUserId(User);
            var newReservation = this.service.Create(model, userId);

            TempData["ReservationCreated"] = "Резервацията беше успешно създадена!";
            return RedirectToAction("UserReservations");

        }
        [Authorize]
        public IActionResult UserReservations()
        {
            var userId = userManager.GetUserId(User);
            var model = service.GetUserReservations(userId);
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Cancel(int id)
        {
            var userId = userManager.GetUserId(User);
            service.CancelReservation(id, userId);
            TempData["SuccessMessage"] = "Резервацията беше успешно отказана.";
            return RedirectToAction("UserReservations");
        }
    }
}
