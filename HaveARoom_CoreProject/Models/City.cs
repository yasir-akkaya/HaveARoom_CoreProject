namespace HaveARoom_CoreProject.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }
        public List<Hotel>? Hotels { get; set; }
    }
}
