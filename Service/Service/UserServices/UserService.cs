﻿using AutoMapper;
using BOs.Models;
using BOs.RequestModels.User;
using BOs.ResponseModels.User;
using Repository.Repositories.UserRepositories;
using Service.Services.EmailServices;
using Service.Ultis;
using Service.Utils;

namespace Service.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IMapper mapper, IEmailService emailService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task CreateStaffAccount(StaffCreateModel model)
        {
            var listUser = await _userRepository.GetAllUserExceptAdmin();
            if (listUser.Where(l => l.Email.ToLower().Equals(model.Email.ToLower())).Any())
            {
                throw new Exception("This email is already existed!");
            }
            User newStaff = _mapper.Map<User>(model);
            await _userRepository.Insert(newStaff);
        }

        public async Task<List<UserInfoResponseModel>> GetAllUserExceptAdmin()
        {
            var list = await _userRepository.GetAllUserExceptAdmin();
            return _mapper.Map<List<UserInfoResponseModel>>(list);
        }

        public async Task<UserInfoResponseModel> GetUserInfoById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            return _mapper.Map<UserInfoResponseModel>(user);
        }

        public async Task<UserInfoResponseModel> GetOwnUserInfo(string token)
        {
            var userId = JwtDecode.DecodeTokens(token, "UserId");
            int parsedUserId;
            if (!int.TryParse(userId, out parsedUserId))
            {
                throw new ArgumentException("Invalid user ID format.");
            }
            var user = await _userRepository.GetUserById(parsedUserId);

            return _mapper.Map<UserInfoResponseModel>(user);
        }

        public async Task EditOwnInfo(string token, UserInfoUpdateModel model)
        {
            var userId = JwtDecode.DecodeTokens(token, "UserId");
            int parsedUserId;
            if (!int.TryParse(userId, out parsedUserId))
            {
                throw new ArgumentException("Invalid user ID format.");
            }
            var user = await _userRepository.GetUserById(parsedUserId);

            _mapper.Map(model, user);
            await _userRepository.Update(user);
        }

        public async Task UpdateUserStatus(int id, bool status)
        {
            var user = await _userRepository.GetUserById(id);
            user.Status = status;
            await _userRepository.Update(user);
        }

        public async Task SendEmailWhenForgotPassword(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception("User not found!");
            }
            var randomPass = RandomPassword.GenerateRandomPassword();

            var htmlBody = EmailTemplate.CreatePasswordResetEmail(user.FullName, user.Email, randomPass);
            bool sendEmailSuccess = await _emailService.SendEmail(user.Email, "Reset Password", htmlBody);

            if (!sendEmailSuccess)
            {
                throw new Exception("Error in sending email");
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(randomPass);
            await _userRepository.Update(user);
        }

    }
}
