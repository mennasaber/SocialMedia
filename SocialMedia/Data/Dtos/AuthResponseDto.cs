using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Data.Dtos
{
    public class AuthResponseDto
    {
        public int Status { get; set; }
        public bool Succeeded { get; set; }
        public string Token { get; set; }
        public ICollection<string> Errors { get; set; }
        public UserDto User { get; set; }
    }
}
