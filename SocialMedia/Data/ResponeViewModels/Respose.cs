using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Data.ResponeViewModels
{
    public class Respose<TEntityDto> : BaseResponse where TEntityDto : class
    {
        public TEntityDto EntityDto { get; set; }
    }
}
