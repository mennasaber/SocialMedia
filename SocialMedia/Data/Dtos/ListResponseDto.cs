using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Data.Dtos
{
    public class ListResponseDto<TEntity>:ResponseDto where TEntity : class
    {
        public ICollection<TEntity> Entities { get; set; }
    }
}
