using System;
namespace FlightBookingSystem.Models.DBGetModels
{
	public class FlightFormatted
	{
        public FlightFormatted(Guid id, string departureAirport, string arrivalAirport, DateTime dateTime, string airline)
        {
            this.id = id;
            this.departureAirport = departureAirport;
            this.arrivalAirport = arrivalAirport;
            this.dateTime = dateTime;
            this.airline = airline;
        }

        public Guid id { get;set;}
        public string departureAirport { get; set; }
        public string arrivalAirport { get; set; }
        public DateTime dateTime { get; set; }
        public String airline { get; set; }
    }
}

