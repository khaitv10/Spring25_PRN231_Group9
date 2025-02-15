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
using BOs.ResponseModels.Service;
using BOs.RequestModels.Service;

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
            CreateMap<BOs.Models.Service, ServiceResponseModel>()
                .ForMember(dest => dest.Vaccine, opt => opt.MapFrom(src => src.ServiceVaccines
                    .Select(sv => new ServiceVaccineResponseModel {
            NumberOfDose = sv.NumberOfDose,
            VaccineInfo = new VaccineInfoResponseModel
            {
                Id = sv.Vaccine.Id,
                Name = sv.Vaccine.Name,
                Description = sv.Vaccine.Description,
                Origin = sv.Vaccine.Origin,
                MinAge = sv.Vaccine.MinAge,
                MaxAge = sv.Vaccine.MaxAge,
                Status = sv.Vaccine.Status
            }
        }).ToList()));
            CreateMap<ServiceCreateModel, BOs.Models.Service>();
        }
    }
}
