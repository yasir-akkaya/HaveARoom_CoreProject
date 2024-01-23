namespace HaveARoom_CoreProject.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public double FoodScore { get; set; }
        public double RoomsScore { get; set; }
        public double ServiceScore { get; set; }

    }
}
