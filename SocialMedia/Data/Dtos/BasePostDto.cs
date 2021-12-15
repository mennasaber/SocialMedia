using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Data.Dtos
{
    public class BasePostDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
