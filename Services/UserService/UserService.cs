using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using mohafezApi.Data;
using mohafezApi.Models;
using mohafezApi.Dtos;
using mohafezApi.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using mohafezApi.ViewModels;

namespace mohafezApi.Services.UserService
{
    public class UserService : IUserService
    {


        private readonly IMapper _mapper;

        private UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly AppDBcontext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserService(IHttpContextAccessor httpContextAccessor, IConfiguration _config,
                           IMapper mapper,
                           AppDBcontext context,
                           UserManager<User> userManager,

                           RoleManager<IdentityRole> roleManager)
        {

            this._mapper = mapper;
            this._roleManager = roleManager;
            this._context = context;
            this.userManager = userManager;
            this._httpContextAccessor = httpContextAccessor;
            this._config = _config;

        }



        public async Task<UserDetailResponse> GetUser(string UserId)
        {
            User? user = await _context.Users!.FindAsync(UserId);
              UserDetailResponse userDetailResponse =_mapper.Map<UserDetailResponse>(user);
            return userDetailResponse!;
        }

        public Task<object> IsUserRegistered(string UserName)
        {
            throw new NotImplementedException();
        }

        public async Task<object> LoginUser(UserForLogin userForLogin)
        {
            var loginUser = await userManager.FindByNameAsync(userForLogin.Email);
          

           
            if (loginUser != null)
            {
                if(userForLogin.Password ==loginUser.Code){
                          
         loginUser.DeviceToken = userForLogin.DeviceToken;
            await _context.SaveChangesAsync();
                var Token = await GenerateTokenAsync(loginUser);
                return new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(Token),
                    user = _mapper.Map<UserDetailResponse>(loginUser),
                     expiration = Token.ValidTo,
                    status = true,
                    message = "تم التسجيل بنجاح"

                };
                }else{
                     return new
            {
                status = false,
                message ="البيانات خاطئة"
            };
                }
           
            }
            return new
            {
                status = false,
                message = "ليس لديك حساب"
            };
        }



        public async Task<dynamic> GenerateTokenAsync(User loginUser)
        {
            var userRoles = await userManager.GetRolesAsync(loginUser);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, loginUser.Id),
                    new Claim(ClaimTypes.Name, loginUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1000),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        public async Task<object> Register(UserForRegister userForRegister)
        {
            dynamic userExist = await IsUserExist(userForRegister);
            if (userExist != "")
            {
                return new
                {
                    message = userExist,
                    status = false
                };
            }
            userForRegister.Password = "Abc123@";
            var userToCreate = _mapper.Map<User>(userForRegister);
            userToCreate.Role = userForRegister.Role;
            if (!await _roleManager.RoleExistsAsync(userForRegister.Role))
                await _roleManager.CreateAsync(new IdentityRole(userForRegister.Role));
            var result = await userManager.CreateAsync(userToCreate, userForRegister.Password);
            await userManager.AddToRoleAsync(userToCreate, userForRegister.Role);
            // string Code = RandomNumber();
            // userToCreate.Code = Code;
            await _context.SaveChangesAsync();
            return new { message = "تم انشاء الحساب بنجاح" , status = true};
        }

        public async Task<dynamic> IsUserExist(UserForRegister userForValidate)
        {
            string error = "";
            User? user = await _context.Users!.Where(x => x.UserName == userForValidate.UserName).FirstOrDefaultAsync();
            if (user != null)
            {
                error = "الحساب   مسجل من قبل";
                return error;
            }

            return error;

        }
        public Task<object> RegisterAdmin(UserForRegister adminForRegister)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateDeviceToken(string Token, string UserId)
        {
            User user = await _context.Users.Where(x => x.Id == UserId).FirstAsync();
            user.DeviceToken = Token;
            await _context.SaveChangesAsync();
            return true;
        }


          public async Task<object> UpdateUser(UserForUpdate userForUpdate) {
            User? user = await _context.Users!.Where(x => x.Id == userForUpdate.UserId).FirstOrDefaultAsync();
            if (user == null) return false;
            if (userForUpdate.FullName !=null) {
                user.FullName = userForUpdate.FullName;
            }
            if (userForUpdate.Gender != null)
            {
                user.Gender = userForUpdate.Gender;
            }
            if (userForUpdate.Country != null)
            {
                user.Country = userForUpdate.Country;
            }

            if (userForUpdate.ProfileImage != null)
            {
                user.ProfileImage = userForUpdate.ProfileImage;
            }
            // if (userForUpdate.Birth != null)
            // {
            //     user.Birth = userForUpdate.Birth;
            // }

            await _context.SaveChangesAsync();
            UserDetailResponse userDetailResponse =_mapper.Map<UserDetailResponse>(user);
            return userDetailResponse;
        }


    }
}