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
    public class TicketController : ControllerBase
    {
        private TicketServices ticketS;
        public TicketController(TicketServices _ticketS)
        {
            ticketS = _ticketS;
        }
        [HttpPut("UpdateTicket")]
        public IActionResult UpdateTicket(Tickets ticket)
        {
            return Ok(ticketS.UpdateTicket(ticket));
        }
        [HttpGet("GetTicket")]
        public IActionResult GetTicket(int TicketId)
        {
            return Ok(ticketS.GetTicket(TicketId));
        }

        [HttpGet("GetAllTickets")]
        public List<Tickets> GetAllTickets()
        {
            return ticketS.GetAllTickets();
        }
        [HttpGet("SaveTicket")]
        public IActionResult SaveTicket(int PassengerId, int BookingId, int TrainId)
        {
            return Ok(ticketS.SaveTicket(PassengerId, BookingId, TrainId));
        }
        [HttpGet("GetTicketModel")]
        public IEnumerable<TicketModel> GetTicket(int PassengerId, int BookingId, int TrainId)
        {
            return ticketS.GetTicket(PassengerId, BookingId, TrainId);
        }

    }
}