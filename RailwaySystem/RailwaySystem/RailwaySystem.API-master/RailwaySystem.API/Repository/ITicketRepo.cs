﻿using RailwaySystem.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Repository
{
    public interface ITicketRepo
    {
        public string UpdateTicket(Tickets ticket);
        Tickets GetTicket(int TicketId);
        List<Tickets> GetAllTickets();
        public string SaveTicket(int PassengerId, int BookingId, int TrainId);
        public IEnumerable<TicketModel> GetTicket(int PassengerId, int BookingId, int TrainId);
    }
}