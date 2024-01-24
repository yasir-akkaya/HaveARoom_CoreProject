using HaveARoom_CoreProject.Controllers;
using HaveARoom_CoreProject.Data;
using HaveARoom_CoreProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace HaveARoom_CoreProject.Areas.User.Conttollers
{

    public class HotelController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ILogger<HomeController> _logger;

        public HotelController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            var list = await db.Hotels.ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> DateSelection(int id)
        {
            ViewData["hotelId"] = id;

            return View();
        }

        //[HttpPost]
        //public IActionResult DateSelection(DateTime date1, DateTime date2, int hotelId)
        //{
        //    var activeHotelRooms = db.Rooms.Where(x => x.HotelId == hotelId).ToList();
        //    List<Room> availableRooms = new List<Room>();

        //    foreach (var room in activeHotelRooms)
        //    {
        //        var overlappingReservations = db.Reservations
        //            .Where(r => r.RoomId == room.Id &&
        //                        !(date2 < r.StartDate || date1 > r.EndDate))
        //            .ToList();

        //        if (!overlappingReservations.Any())
        //        {
        //            availableRooms.Add(room);
        //        }
        //    }

        //    return RedirectToAction("HotelRooms", availableRooms);
        //}
        //int hotelId = Request.Form["hotelId"];
        //DateTime date1 = Request.Form["date1"];
        //DateTime date2 = Request.Form["date2"];

        [HttpPost]
        public async Task<IActionResult> HotelRooms(DateTime date1, DateTime date2, int hotelId)
        {
            if (hotelId != 0)
            {

                var activeHotelRooms = db.Rooms.Where(x => x.HotelId == hotelId).ToList();
                List<Room> availableRooms = new List<Room>();

                foreach (var room in activeHotelRooms)
                {
                    var overlappingReservations = db.Reservations
                        .Where(r => r.RoomId == room.Id &&
                                    !(date2.Day < r.StartDate.Day && date1.Day > r.EndDate.Day)).ToList();

                    if (!overlappingReservations.Any())
                    {
                        availableRooms.Add(room);
                    }
                }
                return View(availableRooms);
            }
            return RedirectToAction("DateSelection");

        }


    }
}
