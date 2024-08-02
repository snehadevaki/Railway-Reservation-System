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
    public class TrainController : ControllerBase
    {
        private TrainServices trainS;
        public TrainController(TrainServices _trainS)
        {
            trainS = _trainS;
        }
        [HttpPost("SaveTrain")]
        public IActionResult SaveTrain(Train train)
        {
            return Ok(trainS.SaveTrain(train));
        }
        [HttpDelete("DeleteTrain")]
        public IActionResult DeleteTrain(int TrainId)
        {
            return Ok(trainS.DeleteTrain(TrainId));
        }
        [HttpPut("UpdateTrain")]
        public IActionResult UpdateTrain(Train train)
        {
            return Ok(trainS.UpdateTrain(train));
        }
        [HttpGet("GetTrain")]
        public IActionResult GetTrain(int TrainId)
        {
            return Ok(trainS.GetTrain(TrainId));
        }

        [HttpGet("GetAllTrains")]
        public List<Train> GetAllTrains()
        {
            return trainS.GetAllTrains();
        }
        [HttpGet("SearchTrainsSeat2")]
        public IEnumerable<SearchTrainModel> GetTrains2(string ArrivalStation, string DepartureStation, DateTime date)
        {
            return trainS.GetTrains2(ArrivalStation, DepartureStation, date);
        }
    }
}