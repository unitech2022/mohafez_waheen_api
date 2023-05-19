using System;
namespace mohafezApi.ViewModels
{
	public class UserDetailResponse
	{
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        // public string? Email { get; set; }
        // public string? ProfileImage { get; set; }
        public string? Role { get; set; }
        public string? DeviceToken { get; set; }
        // public string? Status { get; set; }
        // public string? Code { get; set; }
       public string? Gender { get; set; }
        public string? Country { get; set; }
         public int Classroom { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

