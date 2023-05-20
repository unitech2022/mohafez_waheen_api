using System;
namespace mohafezApi.Dtos
{
	public class UserForLogin
	{
		public string? Email{ get; set; }

		public string? Password{ get; set; }
		public string? DeviceToken { get; set; }
	
	}
}

