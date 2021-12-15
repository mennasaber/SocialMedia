using SocialMedia.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Data.ResponeViewModels
{
    public class AuthResponse<TEntityDto> : BaseResponse where TEntityDto : class
    {
        public string Token { get; set; }
        public TEntityDto User { get; set; }
    }
}
