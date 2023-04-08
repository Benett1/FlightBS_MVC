﻿using System.Diagnostics;
using FlightBookingSystem.Models;
using FlightBookingSystem.Models.DBGetModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;

namespace FlightBookingSystem.Controllers;

public class HomeController : Controller
{
    HttpClient client = new HttpClient();
    private readonly DBContext _context;

    public HomeController(DBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> searchFlights(string departureAirportin, string arrivalAirportin, DateTime date)
    {
        List<FlightFormatted> flightsFormatted = new List<FlightFormatted>();
        var flights = await _context.Flights
            .Include(f => f.ArrivalAirportv)
            .Include(f => f.DepartureAirportv)
            .Include(f => f.PlaneModel)
            .ToListAsync();

        var departLocation = _context.Locations.FirstOrDefault(e => e.City == departureAirportin);
        var arrivalLocation =  _context.Locations.FirstOrDefault(e => e.City == arrivalAirportin);

       

        //flight.DateTime.Year == date.Year && flight.DateTime.Month == date.Month && flight.DateTime.Day == date.Day
        if (departLocation != null && arrivalLocation != null) {
            var departAirport = _context.Airports.FirstOrDefault(el => el.LocationID == departLocation.Id);
            var arrivAirport = _context.Airports.FirstOrDefault(el => el.LocationID == arrivalLocation.Id);

            foreach (var flight in flights)
            {
                if (flight.DepartureAirport == departAirport.Id && flight.ArrivalAirport == arrivAirport.Id)
                {
                    // Retrieve the departure and arrival airports from the related entities
                    var departureAirport = await _context.Airports
                        .FirstOrDefaultAsync(a => a.Id == flight.DepartureAirport);
                    var arrivalAirport = await _context.Airports
                        .FirstOrDefaultAsync(a => a.Id == flight.ArrivalAirport);
                    var plane = await _context.Planes
                        .FirstOrDefaultAsync(a => a.Id == flight.PlaneId);
                    var airline = await _context.Airlines
                        .FirstOrDefaultAsync(a => a.Id == plane!.AirlineId);


                    // Create a new FlightModel object and populate it with data
                    var flightFormatted = new FlightFormatted(flight.Id, departureAirport.Name, arrivalAirport.Name, flight.DateTime, airline.Name);

                    // Add the new FlightModel object to the list
                    flightsFormatted.Add(flightFormatted);
                }
            }
        }
       
        return View("Index", flightsFormatted);
    }


    public async Task<IActionResult> Index()
    {
        var flights = await _context.Flights.Include(f => f.ArrivalAirportv).Include(f => f.DepartureAirportv).Include(f => f.PlaneModel).ToListAsync();
        List<FlightFormatted> flightsFormatted = new List<FlightFormatted>();

        foreach (var flight in flights)
        {
            // Retrieve the departure and arrival airports from the related entities
            var departureAirport = await _context.Airports
                .FirstOrDefaultAsync(a => a.Id == flight.DepartureAirport);
            var arrivalAirport = await _context.Airports
                .FirstOrDefaultAsync(a => a.Id == flight.ArrivalAirport);
            var plane = await _context.Planes
                .FirstOrDefaultAsync(a => a.Id == flight.PlaneId);
            var airline = await _context.Airlines
                .FirstOrDefaultAsync(a => a.Id == plane!.AirlineId);


            // Create a new FlightModel object and populate it with data
            var flightFormatted = new FlightFormatted(flight.Id, departureAirport.Name, arrivalAirport.Name,flight.DateTime, airline.Name);

            // Add the new FlightModel object to the list
            flightsFormatted.Add(flightFormatted);
            }
        return View(flightsFormatted);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}