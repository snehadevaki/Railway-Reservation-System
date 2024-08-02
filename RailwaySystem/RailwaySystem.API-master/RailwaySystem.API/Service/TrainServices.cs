using RailwaySystem.API.Model;
using RailwaySystem.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Services
{
    public class TrainServices
    {
        private ITrainRepo _ITrain;
        public TrainServices(ITrainRepo Itrain)
        {
            _ITrain = Itrain;
        }
        public string SaveTrain(Train train)
        {
            return _ITrain.SaveTrain(train);
        }
        public string DeleteTrain(int TrainId)
        {
            return _ITrain.DeleteTrain(TrainId);
        }
        public string UpdateTrain(Train train)
        {
            return _ITrain.UpdateTrain(train);
        }
        public Train GetTrain(int TrainId)
        {
            return _ITrain.GetTrain(TrainId);
        }
        public List<Train> GetAllTrains()
        {
            return _ITrain.GetAllTrains();
        }
        public IEnumerable<SearchTrainModel> GetTrains2(string ArrivalStation, string DepartureStation, DateTime date)
        {
            return _ITrain.GetTrains2(ArrivalStation, DepartureStation, date);
        }
    }
}