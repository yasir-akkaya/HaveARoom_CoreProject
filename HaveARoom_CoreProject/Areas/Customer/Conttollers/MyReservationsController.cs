using HaveARoom_CoreProject.Areas.Admin.Services;
using HaveARoom_CoreProject.Controllers;
using HaveARoom_CoreProject.Data;
using HaveARoom_CoreProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HaveARoom_CoreProject.Areas.Customer.Conttollers
{

    public class MyReservationsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<HaveARoom_CoreProject.Models.User> userManager;


        public MyReservationsController(UserManager<HaveARoom_CoreProject.Models.User> userManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }
        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userReservations = db.Reservations.Where(u => u.UserId.Equals(userId));

            return View(userReservations);
        }

        [HttpPost]
        public IActionResult Index(int foodScore, int serviceScore, int roomsScore, int reservationId)
        {
            var updatedReservation = db.Reservations.FirstOrDefault(x => x.Id == reservationId);

            if (updatedReservation != null)
            {
                updatedReservation.FoodScore = foodScore;
                updatedReservation.RoomsScore = serviceScore;
                updatedReservation.ServiceScore = roomsScore;

                db.SaveChanges();
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userReservations = db.Reservations.Where(u => u.UserId.Equals(userId));
            return View(userReservations);

        }

        //public IActionResult IndexFoodScore()
        //{
        //    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var userReservations = db.Reservations.Where(u => u.UserId.Equals(userId));

        //    return View(userReservations);
        //}
        //public IActionResult IndexFoodScore()
        //{
        //    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    var userReservations = db.Reservations.Where(u => u.UserId.Equals(userId));

        //    return View(userReservations);
        //}
    }
}
