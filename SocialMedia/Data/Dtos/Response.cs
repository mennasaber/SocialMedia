using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Data.Dtos
{
    public class Response
    {
        public int Status { get; set; }
        public bool Succeeded { get; set; }
        public ICollection<string> Errors { get; set; }
    }
}
