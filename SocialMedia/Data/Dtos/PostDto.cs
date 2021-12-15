using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Data.Dtos
{
    public class PostDto:BasePostDto
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
