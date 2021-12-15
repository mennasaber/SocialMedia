using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Data.ResponeViewModels
{
    public class ListResponse<TEntityDto> :BaseResponse where TEntityDto : class
    {
        public ICollection<TEntityDto> Entities { get; set; }
    }
}
