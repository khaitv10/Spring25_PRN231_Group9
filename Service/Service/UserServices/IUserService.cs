using BOs.RequestModels.User;
using BOs.ResponseModels.User;

namespace Service.Services.UserServices
{
    public interface IUserService
    {
        Task CreateStaffAccount(StaffCreateModel model);
        Task<List<UserInfoResponseModel>> GetAllUserExceptAdmin();
        Task<UserInfoResponseModel> GetUserInfoById(int id);
        Task<UserInfoResponseModel> GetOwnUserInfo(string token);
        Task EditOwnInfo(string token, UserInfoUpdateModel model);
        Task UpdateUserStatus(int id, bool status);
        Task SendEmailWhenForgotPassword(string email);
    }
}
