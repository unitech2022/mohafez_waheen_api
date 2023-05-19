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
using mohafezApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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



        public async Task<User> GetUser(string UserId)
        {
            User? user = await _context.Users!.FindAsync(UserId);
            return user!;
        }

        public Task<object> IsUserRegistered(string UserName)
        {
            throw new NotImplementedException();
        }

        public async Task<object> LoginUser(UserForLogin userForLogin)
        {
            var loginUser = await userManager.FindByNameAsync(userForLogin.UserName);
          
           
            if (loginUser != null)
            {
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
                error = "رقم الهاتف مسجل من قبل";
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
    }
}