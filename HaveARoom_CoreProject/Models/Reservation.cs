namespace HaveARoom_CoreProject.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int? FoodScore { get; set; }
        public int? RoomsScore { get; set; }
        public int? ServiceScore { get; set; }

    }
}
