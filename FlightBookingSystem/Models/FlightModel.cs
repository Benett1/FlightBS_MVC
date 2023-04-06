using System;
namespace FlightBookingSystem.Models
{
	public class FlightModel : BaseClass
	{
		public Guid PlaneId { get; set; }
		public Guid DepartureAirport { get; set; }
		public Guid ArrivalAirport { get; set; }
		public DateTime DateTime { get; set; }
	}
}

