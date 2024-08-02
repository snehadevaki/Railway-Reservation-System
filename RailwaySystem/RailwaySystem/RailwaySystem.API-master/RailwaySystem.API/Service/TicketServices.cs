﻿using RailwaySystem.API.Model;
using RailwaySystem.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Services
{
    public class TicketServices
    {
        private ITicketRepo _ITicket;
        public TicketServices(ITicketRepo ITicket)
        {
            _ITicket = ITicket;
        }
        public string UpdateTicket(Tickets Ticket)
        {
            return _ITicket.UpdateTicket(Ticket);
        }
        public Tickets GetTicket(int TicketId)
        {
            return _ITicket.GetTicket(TicketId);
        }
        public List<Tickets> GetAllTickets()
        {
            return _ITicket.GetAllTickets();
        }
        public string SaveTicket(int PassengerId, int BookingId, int TrainId)
        {
            return _ITicket.SaveTicket(PassengerId, BookingId, TrainId);
        }

        public IEnumerable<TicketModel> GetTicket(int PassengerId, int BookingId, int TrainId)
        {
            return _ITicket.GetTicket(PassengerId, BookingId, TrainId);
        }
    }
}