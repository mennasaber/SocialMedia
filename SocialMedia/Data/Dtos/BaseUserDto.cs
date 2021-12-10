using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Data.Dtos
{
    public class BaseUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
    }
}
