using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class UserService
    {
        [Key]
        public int ServiceId { get; set; }
        [Required]
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public List<UserData> Users { get; } = new List<UserData>();
    }
}