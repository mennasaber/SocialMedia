using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Data.Dtos
{
    public class ReactDto:BaseReactDto
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
