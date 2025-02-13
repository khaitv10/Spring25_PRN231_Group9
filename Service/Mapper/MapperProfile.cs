using AutoMapper;
using BOs.Models;
using BOs.RequestModels.Child;
using BOs.RequestModels.Vaccine;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.Vaccine;
using BOs.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOs.ResponseModels.VaccineStock;
using BOs.RequestModels.VaccineStock;

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
            CreateMap<VaccineStock, VaccineStockResponseModel>();
            CreateMap<VaccineStockCreateModel, Vaccine>();
            CreateMap<VaccineStockUpdateModel, Vaccine>();
            CreateMap<Vaccine, VaccineInfoResponseModel>();
            CreateMap<VaccineCreateModel, Vaccine>();

        }
    }
}
