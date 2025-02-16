using AutoMapper;
using BOs.Models;
using BOs.RequestModels.Child;
using BOs.RequestModels.User;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.User;
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
                    .ForMember(dest => dest.Dob, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Dob)))
                    .ForMember(dest => dest.ParentId, opt => opt.Ignore());


            //User
            CreateMap<User, UserInfoResponseModel>();

            CreateMap<StaffCreateModel, User>();

        }
    }
}
