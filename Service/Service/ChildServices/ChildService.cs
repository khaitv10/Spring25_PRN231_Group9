using AutoMapper;
using BOs.Models;
using BOs.RequestModels.Child;
using BOs.ResponseModels.Child;
using Org.BouncyCastle.Asn1.Ocsp;
using Repository.Repositories.ChildRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.ChildServices
{
    public class ChildService : IChildService
    {
        private readonly IChildRepository _childRepository;
        private readonly IMapper _mapper;

        public ChildService(IChildRepository childRepository, IMapper mapper)
        {
            _childRepository = childRepository;
            _mapper = mapper;
        }

        public async Task CreateChild(ChildCreateModel request, int userId)
        {
            Child newChild = _mapper.Map<Child>(request);
            newChild.ParentId = userId;
            await _childRepository.Insert(newChild);
        }

        public async Task DeleteChild(int id)
        {
            var child = await _childRepository.GetById(id);

            if (child == null)
            {
                throw new InvalidOperationException("Child not found");
            }

            if (child.Appointments.Any() || child.DoseRecords.Any() || child.DoseSchedules.Any())
            {
                throw new InvalidOperationException("Can not delete: Child has Appointments, DoseRecords or DoseSchedules");
            }

            await _childRepository.Delete(child);
        }


        public async Task<List<ChildResponseModel>> GetAllChilds()
        {
            var list = await _childRepository.GetAllChild();
            return _mapper.Map<List<ChildResponseModel>>(list);
        }

        public async Task<List<ChildResponseModel>> GetAllChildByParentId(int id)
        {
            var child = await _childRepository.GetAllChildByParentId(id);
            return _mapper.Map<List<ChildResponseModel>>(child);
        }

        public async Task UpdateChild(int id, ChildUpdateModel request)
        {
            var child = await _childRepository.GetById(id);
            _mapper.Map(request, child);
            await _childRepository.Update(child);
        }

        public async Task<ChildResponseModel> GetChildDetail(int id)
        {
            var child = await _childRepository.GetById(id);
            return _mapper.Map<ChildResponseModel>(child);
        }

        public async Task<ChildDetailResModel> GetDetailChild(int id)
        {
            var child = await _childRepository.GetDetailChild(id);
            return _mapper.Map<ChildDetailResModel>(child);
        }
    }
}
