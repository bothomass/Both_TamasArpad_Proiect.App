using SQLite;
using System.ComponentModel.DataAnnotations;

namespace Both_TamasArpad_Proiect.Models
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }
    }
}
