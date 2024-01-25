using System.ComponentModel.DataAnnotations.Schema;

namespace HaveARoom_CoreProject.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int LuxuryLevel { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        public string Image {  get; set; }
        public int  HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public List<Reservation>? Reservations { get; set; }

        [NotMapped]
        public double? AvgScore {  get; set; }  

    }
}
