using HaveARoom_CoreProject.Models;

namespace HaveARoom_CoreProject.Areas.Admin.Interfaces
{
    public interface IReservationService
    {
        public Task<bool> AddReservationAsync(Reservation reservation);
        public Task<List<Reservation>> GetAllReservationsAsync();
        public Task<Reservation> GetReservationByIdAsync(int id);
        public Task<bool> DeleteReservationAsync(int id);
        public Task<bool> UpdateReservationAsync(Reservation reservation);

    }
}
