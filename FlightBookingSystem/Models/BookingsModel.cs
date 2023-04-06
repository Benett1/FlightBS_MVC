using System;
namespace FlightBookingSystem.Models
{
	public class BookingsModel
	{
		public Guid BookingsId { get; set; }
		public Guid FlightId { get; set; }
		public Guid PassengerId { get; set; }
		public int Seat { get; set; }
	}
}

