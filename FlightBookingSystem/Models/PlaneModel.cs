using System;
namespace FlightBookingSystem.Models
{
	public class PlaneModel : BaseClass
	{
		public String? Name { get; set; }
		public String? Model { get; set; }
		public String? Manufacturer { get; set; }
		public int Seat { get; set; }
		public Double Payload { get; set; }
		public Double Weight { get; set; }
		public Guid AirlineId { get; set; }

		public virtual AirlineModel? AirlineModel { get; set; }
	}
}

