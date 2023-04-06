using System;
namespace FlightBookingSystem.Models
{
	public class RoleModel
	{
		public int Id { get; set; }
		public String? RoleName { get; set; }
		public int AirlineId { get; set; }
	}
}

