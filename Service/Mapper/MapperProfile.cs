using AutoMapper;
using BOs.Models;
using BOs.RequestModels.Child;
using BOs.ResponseModels.Child;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //Child
            CreateMap<Child, ChildResponseModel>();
            CreateMap<ChildCreateModel, Child>()
                .ForMember(dest => dest.ParentId, opt => opt.Ignore());

        }
    }
}
