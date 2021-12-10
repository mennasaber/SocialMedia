using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Data.Dtos
{
    public class AuthResponseDto:Response
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
