﻿using System;
namespace FlightBookingSystem.Models
{
	public class BookingsModel : BaseClass
	{
		public Guid FlightId { get; set; }
		public Guid UserId { get; set; }
        public String? Name { get; set; }
        public String? Surname { get; set; }
        public int Age { get; set; }
        public int Seat { get; set; }
		public bool baggage { get; set; }

		public FlightModel? FlightModel { get; set; }
		public UserModel? UserModel { get; set; }
	}
}

