using RailwaySystem.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Repository
{
    public interface ITrainRepo
    {
        public string SaveTrain(Train train);
        public string UpdateTrain(Train train);
        public string DeleteTrain(int TrainId);
        Train GetTrain(int TrainId);
        List<Train> GetAllTrains();
        public IEnumerable<SearchTrainModel> GetTrains2(string ArrivalStation, string DepartureStation, DateTime date);
    }
}