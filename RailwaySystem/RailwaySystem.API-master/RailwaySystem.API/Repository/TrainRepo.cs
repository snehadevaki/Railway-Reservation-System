using Microsoft.EntityFrameworkCore;
using RailwaySystem.API.Data;
using RailwaySystem.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Repository
{
    public class TrainRepo : ITrainRepo
    {
        private TrainDbContext _trainDb;
        public TrainRepo(TrainDbContext trainDbContext)
        {
            _trainDb = trainDbContext;
        }
        #region DeleteTrain
        /// <summary>
        /// Deactivates the Train when this fuction is invoked
        /// </summary>
        /// <param name="TrainId"></param>
        /// <returns>If the TrainId is present then isActive is changed to false</returns>
        public string DeleteTrain(int TrainId)
        {

            string Result = string.Empty;
            Train delete;

            try
            {
                delete = _trainDb.trains.Find(TrainId);

                if (delete != null)
                {
                    //_trainDb.trainsDb.Remove(delete);
                    delete.isActive = false;
                    _trainDb.SaveChanges();
                    Result = "200";
                }
            }
            catch (Exception ex)
            {
                throw;
                Result = "400";
            }
            finally
            {
                delete = null;
            }
            return Result;
        }
        #endregion

        #region GetAllTrains
        /// <summary>
        /// When the function is invoked we get the list of all Trains 
        /// </summary>
        /// <returns>List of train</returns>
        public List<Train> GetAllTrains()
        {
            string Result = string.Empty;
            List<Train> trains = null;
            try
            {
                trains = _trainDb.trains.ToList();
                return trains;

            }
            catch (Exception ex)
            {
                throw;
            }
            return trains;
        }
        #endregion

        #region GetTrain
        /// <summary>
        /// When this function is invocked we get the trains by Id
        /// </summary>
        /// <param name="TrainId"></param>
        /// <returns>Finds the Id of the Train</returns>
        public Train GetTrain(int TrainId)
        {
            Train train = null;
            try
            {
                train = _trainDb.trains.Find(TrainId);
            }
            catch (Exception ex)
            {
                throw;
            }
            return train;
        }
        #endregion

        #region AddTrain
        /// <summary>
        /// When this function is invoked we can Add a train
        /// </summary>
        /// <param name="train"></param>
        /// <returns></returns>
        public string SaveTrain(Train train)
        {
            string stCode = string.Empty;
            try
            {
            _trainDb.trains.Add(train);
            _trainDb.SaveChanges();

            stCode = "200";
            }
            catch (Exception ex)
            {
                throw;
                return ex.Message;
                stCode = "400";
            }
            return stCode;
        }
        #endregion

        #region UpdateTrain
        /// <summary>
        /// When this function is invoked we will be able to Update Train details
        /// </summary>
        /// <param name="train"></param>
        /// <returns>Updated Train Details</returns>
        public string UpdateTrain(Train train)
        {
            string stCode = string.Empty;
            try
            {
                _trainDb.Entry(train).State = EntityState.Modified;
                _trainDb.SaveChanges();
                stCode = "200";
            }
            catch(Exception ex)
            {
                throw;
                stCode = "400";
            }
            return stCode;

        }
        #endregion

       #region GetTrainList2

        public IEnumerable<SearchTrainModel> GetTrains2(string ArrivalStation, string DepartureStation, DateTime date)
        {
            var Result = (from t in _trainDb.trains
                          join s in _trainDb.seat on t.TrainId equals s.TrainId
                          select new SearchTrainModel
                          {
                              TrainId = t.TrainId,
                              Name = t.Name,
                              ArrivalTime = t.ArrivalTime,
                              DepartureTime = t.DepartureTime,
                              ArrivalDate = t.ArrivalDate,
                              DepartureDate = t.DepartureDate,
                              DepartureStation = t.DepartureStation,
                              ArrivalStation = t.ArrivalStation,
                              distance = t.distance,
                              FirstAC = s.FirstAC,
                              SecondAC = s.SecondAC,
                              Sleeper = s.Sleeper,
                              Total = s.Total,
                          }).ToList().Where(q => q.ArrivalStation == ArrivalStation && q.DepartureStation == DepartureStation && q.DepartureDate == date);
            return Result;
        }

        #endregion
    }
}