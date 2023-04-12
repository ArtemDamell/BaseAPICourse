using System.ComponentModel.DataAnnotations;

namespace Core.VMs
{
    public class UserVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
