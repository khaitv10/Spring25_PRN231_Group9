using BOs.Models;
using BOs.RequestModels.Child;
using BOs.ResponseModels.Child;
using FFilms.Application.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.ChildServices
{
    public interface IChildService
    {
        Task<ChildResponseModel> GetChildDetail(int id);
        Task<List<ChildResponseModel>> GetAllChilds();
        Task<List<ChildResponseModel>> GetAllChildByParentId(int id);
        Task CreateChild(ChildCreateModel request, int userId);
        Task UpdateChild(int id, ChildUpdateModel request);
        Task DeleteChild(int id);

    }
}
