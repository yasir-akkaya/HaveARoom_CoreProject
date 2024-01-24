using HaveARoom_CoreProject.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace HaveARoom_CoreProject.Areas.User.Conttollers
{
        [Area("User")]
    public class CityController : Controller
    {
        private readonly CityService cityService;
        public CityController(CityService cityService)
        {
            this.cityService= cityService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await cityService.GetAllCitiesAsync();
            return View(list);
        }
    }
}
