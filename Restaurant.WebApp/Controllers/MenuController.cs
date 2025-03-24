using Microsoft.AspNetCore.Mvc;
using Restaurant.WebApp.Services;

namespace Restaurant.WebApp.Controllers
{
    public class MenuController : Controller
    {
            private readonly IMenuService service;

            public MenuController(IMenuService service)
            {
                this.service = service;
            }
            public IActionResult Index()
            {
                return View();
            }
            public IActionResult All(string searchString)
            {
                var products = service.GetAll();
                return View(products);
            }
            public IActionResult ProductDetails(int id)
            {
                var product = service.GetProductDetails(id);

                if (product == null)
                {
                    return BadRequest();
                }
                return View(product);
            }
            /*
            public IActionResult Edit(int id)
            {
                var vehicle = this.service.GetVehiclesDetails(id);

                if (!this.service.Exists(id))
                {
                    return BadRequest();
                }

                var vTypes = service.getVModelTypes();
                var vehicleModel = new VehicleFormModel()
                {
                    Id = vehicle.Id,
                    VModel = vehicle.VModel,
                    RegNumber = vehicle.RegNumber,
                    Thirdpartyliabilityinsurance = vehicle.Thirdpartyliabilityinsurance,
                    Casko = vehicle.Casko,
                    Vignette = vehicle.Vignette,
                    Area = vehicle.Area,
                    VTypes = vTypes,
                };

                return View(vehicleModel);
            }
            [HttpPost]
            public IActionResult Edit(VehicleFormModel model, int id)
            {
                if (!this.service.Exists(id))
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                this.service.Edit(model);

                return RedirectToAction(nameof(VehiclesDetails), new { id = id });

            }
            [HttpGet]
            public IActionResult Delete(int id)
            {

                if (!this.service.Exists(id))
                {
                    return BadRequest();
                }

                var vehicle = this.service.GetVehiclesDetails(id);

                var model = new VehicleDetailViewModel()
                {
                    Id = vehicle.Id,
                    VModel = vehicle.VModel,
                    RegNumber = vehicle.RegNumber
                };
                return View(model);
            }
            [HttpPost]
            public IActionResult Delete(VehicleDetailViewModel model)
            {
                if (!this.service.Exists(model.Id))
                {
                    return BadRequest();
                }

                this.service.Delete(model.Id);

                return RedirectToAction(nameof(All));
            }

            [HttpGet]
            public IActionResult Add()
            {

                var vTypes = service.getVModelTypes();
                var vehicleModel = new VehicleFormModel()
                {
                    VTypes = vTypes,
                };

                return View(vehicleModel);
            }
            [HttpPost]
            public IActionResult Add(VehicleFormModel model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var newVehicle = this.service.Create(model);


                return RedirectToAction(nameof(VehiclesDetails), new { id = newVehicle });

            }
            */

        }
    }
