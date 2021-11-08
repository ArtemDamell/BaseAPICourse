using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Core.Models
{
    // 34. Реализовываем модель Project
    public class Project
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public List<Ticket>? Tickets { get; set; }
    }
}
