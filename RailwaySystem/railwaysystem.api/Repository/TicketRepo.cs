using Microsoft.EntityFrameworkCore;
using RailwaySystem.API.Data;
using RailwaySystem.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Repository
{
    public class TicketRepo : ITicketRepo
    {
        private TrainDbContext _TicketDb;
        public TicketRepo(TrainDbContext TicketDbContext)
        {
            _TicketDb = TicketDbContext;
        }
              
        #region GetAllTickets
        /// <summary>
        /// When the function is invoked we get the list of all Tickets 
        /// </summary>
        /// <returns>List of Ticket</returns>
        public List<Tickets> GetAllTickets()
        {
            string Result = string.Empty;
            List<Tickets> Tickets = null;
            try
            {
                Tickets = _TicketDb.tickets.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
            return Tickets;
        }
        #endregion

        #region GetTicket
        /// <summary>
        /// When this function is invocked we get the Tickets by Id
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns>Finds the Id of the Ticket</returns>
        public Tickets GetTicket(int TicketId)
        {
            Tickets Ticket = null;
            try
            {
                Ticket = _TicketDb.tickets.Find(TicketId);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ticket;
        }
        #endregion

        #region UpdateTicket
        /// <summary>
        /// When this function is invoked we will be able to Update Ticket details
        /// </summary>
        /// <param name="Ticket"></param>
        /// <returns>Updated Ticket Details</returns>
        public string UpdateTicket(Tickets Ticket)
        {
            string stCode = string.Empty;
            try
            {
                _TicketDb.Entry(Ticket).State = EntityState.Modified;
                _TicketDb.SaveChanges();
                stCode = "200";
            }
            catch
            {
                stCode = "400";
            }
            return stCode;

        }
        #endregion

        #region GetTicket
        /// <summary>
        /// When this function is invocked we get the Tickets by Id
        /// </summary>
        /// <param name="TicketId"></param>
        /// <returns>Finds the Id of the Ticket</returns>
        public IEnumerable<TicketModel> GetTicket(int PassengerId, int BookingId, int TrainId)
        {

            List<TicketModel> Result;

            Result = (from p in _TicketDb.passenger
                      join b in _TicketDb.bookings on p.PassengerId equals b.PassengerId
                      join t in _TicketDb.trains on b.TrainId equals t.TrainId
                      select new TicketModel
                      {
                          TrainId = t.TrainId,
                          Name = t.Name,
                          ArrivalTime = t.ArrivalTime,
                          DepartureTime = t.DepartureTime,
                          ArrivalDate = t.ArrivalDate,
                          DepartureDate = t.DepartureDate,
                          ArrivalStation = t.ArrivalStation,
                          DepartureStation = t.DepartureStation,
                          BookingId = b.BookingId,
                          fare = b.fare,
                          Status = b.Status,
                          SeatNum = b.SeatNum,
                          PassengerId = p.PassengerId,
                          PName = p.PName,
                          Age = p.Age,
                          gender = p.gender,
                          Class = p.Class,
                      }).Where(q => q.PassengerId == PassengerId && q.BookingId == BookingId && q.TrainId == TrainId).ToList();



            return Result;
        }
        #endregion

        #region AddTicket
        /// <summary>
        /// When this function is invoked we can Add a Ticket
        /// </summary>
        /// <param name="Ticket"></param>
        /// <returns></returns>
        public string SaveTicket(int PassengerId, int BookingId, int TrainId)
        {
            string stCode = string.Empty;
            try
            {
                _TicketDb.tickets.Add(new Tickets { TrainId = TrainId, PassengerId = PassengerId, BookingId = BookingId });

                _TicketDb.SaveChanges();
                stCode = "200";
            }
            catch (Exception ex)
            {
                throw;
                stCode = "400";
            }
            return stCode;
        }


        #endregion
    }
}