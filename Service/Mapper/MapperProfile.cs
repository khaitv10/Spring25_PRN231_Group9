using AutoMapper;
using BOs.Models;
using BOs.RequestModels.Child;
using BOs.RequestModels.DoseRecord;
using BOs.RequestModels.DoseSchedule;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.DoseRecord;
using BOs.ResponseModels.DoseSchedule;
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

            CreateMap<DoseSchedule, DoseScheduleResponseModel>();
            CreateMap<DoseScheduleCreateModel, DoseSchedule>();
            CreateMap<DoseScheduleUpdateModel, DoseSchedule>();

            CreateMap<DoseRecordCreateModel, DoseRecord>();
            CreateMap<DoseRecordUpdateModel, DoseRecord>();
            CreateMap<DoseRecord, DoseRecordResponseModel>();
        }
    }
}
