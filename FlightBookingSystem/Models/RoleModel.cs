using System;
namespace FlightBookingSystem.Models
{
	public class RoleModel
	{
		public int Id { get; set; }
		public String? RoleName { get; set; }
		public Guid? AirlineId { get; set; }

		public virtual AirlineModel? AirlineModel{ get; set; }
	}
}

