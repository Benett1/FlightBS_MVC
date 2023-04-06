using System;

namespace FlightBookingSystem.Models
{
	public class BaseClass
	{
        public Guid Id { get; set; }
		public BaseClass(){
			Id = Guid.NewGuid();
        }
	}
}

