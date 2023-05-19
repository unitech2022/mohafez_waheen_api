using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mohafezApi.Dtos
{
    public class UserForRegister
    {
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Country { get; set; }
        public string? Classroom { get; set; }

        public string? Password { get; set; }
        public string? DeviceToken { get; set; }
        public string? Gender { get; set; }

        public string? Role { get; set; }

        public double? Lat { get; set; }
        public double? Lng { get; set; }
    }
}