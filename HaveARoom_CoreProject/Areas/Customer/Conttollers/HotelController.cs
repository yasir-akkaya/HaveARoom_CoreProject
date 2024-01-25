using HaveARoom_CoreProject.Areas.Admin.Services;
using HaveARoom_CoreProject.Controllers;
using HaveARoom_CoreProject.Data;
using HaveARoom_CoreProject.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Claims;

namespace HaveARoom_CoreProject.Areas.User.Conttollers
{
    public class HotelController : Controller
    {
        private readonly ApplicationDbContext db;
        public HotelController(ApplicationDbContext db)
        {
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
        public async Task<IActionResult> CreateReservation(int Id)
        {
            var activeRoom = db.Rooms.FirstOrDefault(x => x.Id.Equals(Id));
            return View(activeRoom);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(DateTime date1, DateTime date2, int roomId)
        {
            double activeRoomPrice = db.Rooms.FirstOrDefault(x => x.Id == roomId).Price;
            string totalPrice = Convert.ToString((date2.Day - date1.Day) * activeRoomPrice);
            double totalPriceDouble = (date2.Day - date1.Day) * activeRoomPrice;
            
            HttpContext.Session.SetString("totalPrice", totalPrice);
            HttpContext.Session.SetInt32("roomId", roomId);

            Reservation newReservation = new Reservation()
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                StartDate = date1,
                EndDate = date2,
                RoomId = roomId,
                TotalPrice = (date2.Day - date1.Day) * activeRoomPrice
            };
            db.Reservations.Add(newReservation);
            db.SaveChanges();

            
            return RedirectToAction("Payment", "Hotel");

        }
        public IActionResult Payment()
        {
            string activeUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var activeUser = User.Identity;
            string totalPrice = HttpContext.Session.GetString("totalPrice");

            double myDoublePrice = Math.Floor(Convert.ToDouble(totalPrice));


            string _total = Convert.ToString(myDoublePrice);

            //double totalPriceDouble = Math.Ceiling(_total);
            //int totalPriceInt = (int)totalPriceDouble;
            //string realTotalStringPrice = totalPriceInt.ToString();
            ////double roundedPriceInt = Math.Round(totalPriceInt);
            int roomId = HttpContext.Session.GetInt32("roomId") ?? 0;

            //IYZICO KODLARI:::::::::

            Options options = new Options(); // Iyzico Import
            options.ApiKey = "sandbox-R687HFKzohdymXoCGttUXlGUpN6WPBum";
            options.SecretKey = "sandbox-XVN5Xae1h4tWlyqo8OuCf8MjWG2gZ4fE";
            options.BaseUrl = "Https://sandbox-api.iyzipay.com";

            


            CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = _total;
            request.PaidPrice = _total;
            request.Currency = Currency.TRY.ToString();
            request.BasketId = "B67832";
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            //DÜZENLE
            request.CallbackUrl = "https://localhost:7278/Hotel/Success";


            Buyer buyer = new Buyer();
            buyer.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            buyer.Name = activeUser.Name;
            buyer.Surname = "Akkaya";
            buyer.GsmNumber = "+901234434477";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "57702464791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2000-12-12 12:00:00";
            buyer.RegistrationAddress = "Bahçelievler Mah., No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Yasir Akkaya";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Deneme Sitesi";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Yasir Akkaya";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Deneme Sitesi";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basketProduct;
            basketProduct = new BasketItem();
            basketProduct.Id = Convert.ToString(roomId);
            basketProduct.Name = "Hotel Room";
            basketProduct.Category1 = "Hotel";
            basketProduct.Category2 = "";
            basketProduct.ItemType = BasketItemType.PHYSICAL.ToString();

            double price = myDoublePrice;
            double endPrice = myDoublePrice;
            basketProduct.Price = endPrice.ToString();
            basketItems.Add(basketProduct);

            request.BasketItems = basketItems;

            CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(request, options);
            ViewBag.pay = checkoutFormInitialize.CheckoutFormContent;


            return View();

        }


        public IActionResult Success()
        {
            return View();
        }
    }


}

