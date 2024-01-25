using HaveARoom_CoreProject.Areas.Admin.Interfaces;
using HaveARoom_CoreProject.Areas.Admin.Services;
using HaveARoom_CoreProject.Data;
using HaveARoom_CoreProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HaveARoom_CoreProject.Controllers
{
    /*[Area("User")]*/
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            var list = await db.Cities.ToListAsync();
            return View(list);
        }
        public async Task<IActionResult> CityHotels(int id)
        {
            var list = await db.Hotels.Where(x=>x.CityId==id).ToListAsync();
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
