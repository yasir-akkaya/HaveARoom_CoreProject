using HaveARoom_CoreProject.Areas.Admin.Interfaces;
using HaveARoom_CoreProject.Data;
using HaveARoom_CoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HaveARoom_CoreProject.Areas.Admin.Services
{
    public class ReservationService : IReservationService
    {
        ApplicationDbContext db;
        public ReservationService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Task<bool> AddReservationAsync(Reservation reservation)
        {
            bool result = false;
            if (reservation != null)
            {
                db.Reservations.AddAsync(reservation);
                db.SaveChanges();
                result = true;
            }
            return Task.FromResult(result);
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            var reservation = await db.Reservations.FirstOrDefaultAsync(c => c.Id == id);
            bool result = false;
            if (reservation != null)
            {
                db.Remove(reservation);
                db.SaveChanges();
                result = true;
            }
            return result;
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            var list = await db.Reservations.ToListAsync();
            return list;
        }

        public Task<Reservation> GetReservationByIdAsync(int id)
        {
            var reservation = db.Reservations.FirstOrDefaultAsync(x => x.Id == id);

            return reservation;
        }
        public async Task<bool> UpdateReservationAsync(Reservation reservation, int reservationId)
        {
            var editedReservation = await db.Reservations.FirstOrDefaultAsync(x => x.Id == reservationId);
            bool result = false;
            if (editedReservation != null)
            {
                
                editedReservation.FoodScore = reservation.FoodScore;
                editedReservation.RoomsScore = reservation.RoomsScore;
                editedReservation.ServiceScore = reservation.ServiceScore;

                db.SaveChanges();
                result = true;
            }
            return result;
        }
    }
}
