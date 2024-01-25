using HaveARoom_CoreProject.Data;

namespace HaveARoom_CoreProject.Models
{
    
    public class Hotel
    {
        

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
