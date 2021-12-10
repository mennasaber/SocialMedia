using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Data.Dtos
{
    public class RegisterDto : BaseUserDto
    {
        [Required]
        public string Password { get; set; }
    }
}
