using RailwaySystem.API.Model;
using RailwaySystem.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Services
{
    public class BookingServices
    {
        private IBookingRepo _Ibooking;
        public BookingServices(IBookingRepo Ibooking)
        {
            _Ibooking = Ibooking;
        }
        public string SaveBooking(Booking Booking)
        {
            return _Ibooking.SaveBooking(Booking);
        }
        public string DeactBooking(int BookingId , int TrainId)
        {
            return _Ibooking.DeactBooking(BookingId , TrainId);
        }
        public string UpdateBooking(Booking Booking)
        {
            return _Ibooking.UpdateBooking(Booking);
        }
        public Booking GetBooking(int BookingId)
        {
            return _Ibooking.GetBooking(BookingId);
        }
        public List<Booking> GetAllBookings()
        {
            return _Ibooking.GetAllBookings();
        }
        public double CalculateFare(int TrainId, string Class, int PassengerId, int UserId)
        {
            return _Ibooking.CalculateFare(TrainId,Class,PassengerId, UserId);
        }
        public Booking ConfirmBooking(int BookingId)
        {
            return _Ibooking.ConfirmBooking(BookingId);
        }
        public IEnumerable<Booking> GetBookingByUserID(int UserId)
        {
            return _Ibooking.GetBookingByUserID(UserId);
        }
        public int GetBookingId(int PassengerId)
        {
            return _Ibooking.GetBookingId(PassengerId);
        }
    }
}