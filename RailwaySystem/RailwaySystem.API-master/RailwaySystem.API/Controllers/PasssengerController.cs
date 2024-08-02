using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RailwaySystem.API.Model;
using RailwaySystem.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private PassengerServices passengerS;
        public PassengerController(PassengerServices _passengerS)
        {
            passengerS = _passengerS;
        }
        [HttpPost("AddPassenger")]
        public IActionResult Addpassenger(Passenger passenger)
        {
            return Ok(passengerS.AddPassenger(passenger));
        }
        [HttpDelete("DeletePassenger")]
        public IActionResult DeletePassenger(int passengerId)
        {
            return Ok(passengerS.DeletePassenger(passengerId));
        }
        [HttpPut("UpdatePassenger")]
        public IActionResult UpdatePassenger(int PassengerId, Passenger passenger)
        {
            return Ok(passengerS.UpdatePassenger(PassengerId, passenger));
        }
        [HttpGet("GetPassenger")]
        public IActionResult Getpassenger(int passengerId)
        {
            return Ok(passengerS.GetPassenger(passengerId));
        }

        [HttpGet("GetAllPassengers")]
        public List<Passenger> GetAllpassengers()
        {
            return passengerS.GetAllPassengers();
        }
        [HttpGet("GetReport")]
        public IEnumerable<Report> GetReport(int TrainId)
        {
            return passengerS.GetReport(TrainId);
        }
        [HttpGet("GetReportStat")]
        public IEnumerable<Report> GetReportStat(int TrainId, string Status)
        {
            return passengerS.GetReportStat(TrainId, Status);
        }
    }
}