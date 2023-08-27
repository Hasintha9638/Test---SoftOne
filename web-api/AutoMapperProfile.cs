using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Students, GetStudentsDto>();
            CreateMap<AddStudentsDto, Students>();
        }
    }
}