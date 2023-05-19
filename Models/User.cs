using System;
using Microsoft.AspNetCore.Identity;

namespace mohafezApi.Models
{

  

	public class User:IdentityUser
	{
        public string? Role { get; set; }
        public string? FullName { get; set; }
        public string? DeviceToken { get; set; }
        public string? Status { get; set; }

        // public string? Code { get; set; }
        
        //public string? ProfileImage { get; set; }
        public string? Gender { get; set; }
        public string? Country { get; set; }
         public int Classroom { get; set; }
        // public double? Points { get; set; }

        public double? Lat { get; set; }
        public double? Lng { get; set; }

        // public double? SurveysBalance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public User() {
            CreatedAt = DateTime.UtcNow.AddHours(3);
            Status = "ACTIVE";
         
            Lat = 0.0;
             Lng = 0.0;


        }
    }
}

