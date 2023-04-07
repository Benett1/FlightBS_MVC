using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightBookingSystem.Models
{
	public class AirlineModel : BaseClass
	{
		public String? Name { get; set; }
		public Guid CountryOfOrigin { get; set; }
		public virtual LocationModel? LocationModel{ get; set; }
	}
}

