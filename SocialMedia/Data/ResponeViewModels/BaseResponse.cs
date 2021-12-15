using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Data.ResponeViewModels
{
    public class BaseResponse
    {
        public int Status { get; set; }
        public bool Succeeded { get; set; }
        public ICollection<string> Errors { get; set; }
    }
}
