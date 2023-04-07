using System;
namespace FlightBookingSystem.Models
{
	public class FlightModel : BaseClass
	{
        public FlightModel() { }

        public FlightModel(Guid planeId, Guid departureAirport, Guid arrivalAirport, DateTime dateTime, PlaneModel? planeModel)
        {
            PlaneId = planeId;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            DateTime = dateTime;
            PlaneModel = planeModel;
        }

        public Guid PlaneId { get; set; }
		public Guid DepartureAirport { get; set; }
		public Guid ArrivalAirport { get; set; }
		public DateTime DateTime { get; set; }

		public virtual PlaneModel? PlaneModel { get; set; }
		public virtual AirportModel? DepartureAirportv { get; set; }
        public virtual AirportModel? ArrivalAirportv { get; set; }
    }
}

