using System;
namespace FlightBookingSystem.Models
{
	public class AirportModel : BaseClass
	{
		public Guid LocationID { get; set; }
		public String? Name { get; set; }

		public virtual LocationModel? LocationModel { get; set; }
	}
}

