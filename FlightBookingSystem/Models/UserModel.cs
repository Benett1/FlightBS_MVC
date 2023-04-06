using System;
namespace FlightBookingSystem.Models
{
	public class UserModel : BaseClass
	{
		public String? Name { get; set; }
		public String? Surname { get; set; }
		public int Age { get; set; }
		public String? Password { get; set; }
		public Guid RoleId { get; set; }
	}
}

