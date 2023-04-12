using System.ComponentModel.DataAnnotations;

namespace Core.DTO
{
    public class EventAdministratorDTO
    {
        public int Id { get; set; }
        [Required]
        [MinLength(4), MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(4), MaxLength(50)]
        public string LastName { get; set; }
    }
}
