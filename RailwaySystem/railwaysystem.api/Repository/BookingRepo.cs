using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RailwaySystem.API.Data;
using RailwaySystem.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Repository
{
    public class BookingRepo : IBookingRepo
    {
        private TrainDbContext _trainDb;
        public BookingRepo(TrainDbContext trainDb)
        {
            _trainDb = trainDb;
        }

        #region DeactivateBookinh

        public string DeactBooking(int BookingId, int TrainId)
        {
            string Result = string.Empty;
            Booking delete = null;

            try
            {
                Seat seat = _trainDb.seat.FirstOrDefault(q => q.TrainId == TrainId);
                delete = _trainDb.bookings.Find(BookingId);
                if (delete != null && delete.Status != "CANCELLED")
                {

                    delete.Status = "CANCELLED";
                    if (delete.Classes == "FirstAC")
                    {
                        seat.FirstAC++;
                        seat.Total++;
                    }
                    if (delete.Classes == "SecondAC")
                    {
                        seat.SecondAC++;
                        seat.Total++;
                    }
                    if (delete.Classes == "Sleeper")
                    {
                        seat.Sleeper++;
                        seat.Total++;
                    }

                    _trainDb.SaveChanges();
                    Result = "200";
                }


            }
            catch (Exception ex)
            {
                Result = "400";
                throw;
            }
            return Result;

        }

        #endregion


        #region GetAllBookings

        public List<Booking> GetAllBookings()
        {
            string Result = string.Empty;
            List<Booking> bookings = null;
            try
            {
                bookings = _trainDb.bookings.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
            return bookings;
        }

        #endregion


        #region GetBooking

        public Booking GetBooking(int BookingId)
        {
            Booking booking = null;
            try
            {
                booking = _trainDb.bookings.Find(BookingId);
            }
            catch (Exception ex)
            {
                throw;
            }
            return booking;
        }

        #endregion


        #region SaveBooking

        public string SaveBooking(Booking booking)
        {
            string stCode = string.Empty;
            try
            {
                _trainDb.bookings.Add(booking);
                _trainDb.SaveChanges();
                stCode = "200";
            }
            catch (Exception ex)
            {
                stCode = "400";
                throw;
            }
            return stCode;
        }

        #endregion


        #region UpdateBooking

        public string UpdateBooking(Booking booking)
        {
            string stCode = string.Empty;
            try
            {
                _trainDb.Entry(booking).State = EntityState.Modified;
                _trainDb.SaveChanges();
                stCode = "200";
            }
            catch (Exception ex)
            {
                stCode = "400";
                throw;
            }
            return stCode;
        }

        #endregion


        #region CalculateFare

        public double CalculateFare(int TrainId, string Class, int PassengerId, int UserId)
        {
            double fare = 0.00;
            var train = _trainDb.trains.Find(TrainId);
            int distance = (int)train.distance;
            Seat seat = _trainDb.seat.FirstOrDefault(q => q.TrainId == TrainId);
            if (Class == "FirstAC")
            {
                fare = ((8 * distance) + 250 + 70) * 0.18;
                seat.FirstAC = seat.FirstAC - 1;
                seat.Total = seat.Total - 1;
            }
            if (Class == "SecondAC")
            {
                fare = ((6 * distance) + 150 + 50) * 0.18;
                seat.SecondAC = seat.SecondAC - 1;
                seat.Total = seat.Total - 1;
            }
            if (Class == "Sleeper")
            {
                fare = ((4 * distance) + 50 + 30) * 0.18;
                seat.Sleeper = seat.Sleeper - 1;
                seat.Total = seat.Total - 1;
            }
            Random rnd = new Random();
            int seatNum = rnd.Next(1, 72);
            _trainDb.bookings.Add(new Booking { TrainId = TrainId, Classes = Class, Status = "Pending", Date = DateTime.Now, PassengerId = PassengerId, SeatNum = seatNum, fare = fare, UserId = UserId });
            _trainDb.Entry(seat).State = EntityState.Modified;//to reduce seat
            _trainDb.SaveChanges();
            return fare;
        }

        #endregion


        #region ConfirmBooking
        public Booking ConfirmBooking(int BookingId)
        {
            string Result = string.Empty;
            Booking confirm = null;

            try
            {
                confirm = _trainDb.bookings.Find(BookingId);
                if (confirm != null)
                {
                    confirm.Status = "CONFIRM";
                    _trainDb.transaction.Add(new Transaction { BookingId = BookingId, TransactionStatus = "Success", Fare = confirm.fare });
                    _trainDb.SaveChanges();
                    Result = "200";
                }

            }
            catch (Exception ex)
            {
                Result = "400";
                throw;
            }
            return confirm;

        }
        #endregion


        #region GetBookingbyUserID
        public IEnumerable<Booking> GetBookingByUserID(int UserId)
        {
            var booking = _trainDb.bookings.Where(a => a.UserId == UserId).ToList();

            return booking;
        }
        #endregion


        #region GetBookingID

        public int GetBookingId(int PassengerId)
        {
            Booking booking = _trainDb.bookings.FirstOrDefault(q => q.PassengerId == PassengerId);
            int BookingId = booking.BookingId;
            return BookingId;

        }

        #endregion


    }
}