using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.DTO
{
    // 68. Создаём объект DTO
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
