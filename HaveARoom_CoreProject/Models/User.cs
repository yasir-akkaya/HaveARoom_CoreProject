using System.ComponentModel.DataAnnotations.Schema;

namespace HaveARoom_CoreProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }

        [NotMapped]
        public string Role { get; set; }
        public List<Reservation>? Reservations { get; set; }
    }
}
