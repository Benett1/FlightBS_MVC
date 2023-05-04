using System;
using Microsoft.EntityFrameworkCore;
using FlightBookingSystem.Models;
using Microsoft.AspNetCore.Http;

namespace FlightBookingSystem
{
	public static class GlobalState
	{
        private static readonly DBContext _context;

		public static UserModel User { get; set; }

		public static String UserRole { get; set; } = "Guest";
	}
}

