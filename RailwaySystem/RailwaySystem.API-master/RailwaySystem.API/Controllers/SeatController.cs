using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
    public class SeatController : ControllerBase
    {
        private SeatServices _seatServices;
        public SeatController(SeatServices seatServices)
        {
            _seatServices = seatServices;
        }
        [HttpOptions]
        public IActionResult Options()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "POST");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            return NoContent();
        }


        [HttpPost("SaveSeat")]
        public IActionResult SaveSeat(Seat seat)
        {
            /*
            string result = _seatServices.SaveSeat(seat);
            Console.WriteLine("************************************");
            Console.WriteLine(result);
            Console.WriteLine("************************************");
            return Ok(new { message = result });
            */
            try
            {
                string result = _seatServices.SaveSeat(seat);
                return Ok(new { message = result });
            }
            /*
            catch (SqlException ex)
            {
            //Console.WriteLine("************************************");
                //Console.WriteLine(ex.Message);
                //Console.WriteLine("************************************");
                //Console.WriteLine(ex.InnerException);
                //Console.WriteLine("************************************");
                //Console.WriteLine(ex.StackTrace);
                //Console.WriteLine("************************************");
                if (ex.Number == 2601 || ex.Number == 2627) // Duplicate key error codes
                {
                    return StatusCode(500, new
                    {
                        error = "Duplicate Record",
                        message = "Duplicate Record",
                        innerException = "",
                        stackTrace = ""
                    });
                }
                else
                {
                    return StatusCode(500, new { error = "Error occurred. Please try again.", message = ex.Message, innerException = ex.InnerException?.Message, stackTrace = ex.StackTrace });
                }
            } */
            catch (Exception ex)
            {
                throw;
                if (ex.InnerException?.Message.Contains("duplicate key", StringComparison.OrdinalIgnoreCase) == true)
                {
                    return StatusCode(500, new
                    {
                        error = "Duplicate Record",
                        message = "Duplicate Record",
                        innerException = "",
                        stackTrace = ""
                    });
                    
                }
                else if(ex.InnerException?.Message.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint", StringComparison.OrdinalIgnoreCase) == true)
                {
                    return StatusCode(500, new
                    {
                        error = "Train ID not found!",
                        message = "Train ID not found!",
                        innerException = "",
                        stackTrace = ""
                    });
                    
                }
                else{
                    return StatusCode(500, new
                    {
                        error = "Error occurred. Please try again.",
                        message = ex.Message,
                        innerException = ex.InnerException?.Message,
                        stackTrace = ex.StackTrace
                    });
                }

            }
        }
        
        [HttpPut("UpdateSeat")]
        public IActionResult UpdateSeat(int SeatId, Seat seat)
        {
            return Ok(_seatServices.UpdateSeat(SeatId,seat));
        }
        [HttpGet("GetSeat")]
        public IActionResult GetSeat(int SeatId)
        {
            return Ok(_seatServices.GetSeat(SeatId));
        }
        [HttpGet("GetAllSeats()")]
        public List<Seat> GetAllSeats()
        {
            return _seatServices.GetAllSeats();
        }
    }
}