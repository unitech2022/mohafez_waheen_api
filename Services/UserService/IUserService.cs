using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mohafezApi.Dtos;
using mohafezApi.Models;
using mohafezApi.ViewModels;

namespace mohafezApi.Services.UserService
{
    public interface IUserService
    {
        Task<object> Register(UserForRegister userForRegister);
		Task<object> IsUserRegistered(string UserName);
		// Task<object> LoginAdmin(AdminForLoginRequest adminForLogin);
		Task<object> LoginUser(UserForLogin userForLogin);
		Task<object> RegisterAdmin(UserForRegister adminForRegister);
	Task<object> UpdateUser(UserForUpdate userForUpdate);

		Task<bool> UpdateDeviceToken(string Token,string UserId);
		Task<UserDetailResponse> GetUser(string UserId);
    }
}