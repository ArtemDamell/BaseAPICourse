using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Core.Models
{
    public class Project
    {
        public int Id { get; set; }
        [StringLength(250)]
        public string ClientName { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public List<Ticket>? Tickets { get; set; }
    }
}
