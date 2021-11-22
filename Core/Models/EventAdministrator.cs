using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    // 67. Модель для DTO
    public class EventAdministrator
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2), MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2), MaxLength(100)]
        public string LastName { get; set; }
        public int Age { get; set; }
        [Phone]
        public string Phone { get; set; }
        [MinLength(3), MaxLength(100)]
        public string Address { get; set; }
        public int? ProjectId { get; set; }
        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }
    }
}
