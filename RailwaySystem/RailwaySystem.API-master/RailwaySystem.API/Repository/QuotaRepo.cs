using Microsoft.EntityFrameworkCore;
using RailwaySystem.API.Data;
using RailwaySystem.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Repository
{
    public class QuotaRepo : IQuotaRepo
    {
        private TrainDbContext _trainDb;
        public QuotaRepo(TrainDbContext trainDbContext)
        {
            _trainDb = trainDbContext;
        }
        public string SaveQuota(Quota quota)
        {
            string stCode = string.Empty;
            try
            {
                _trainDb.quotas.Add(quota);
                _trainDb.SaveChanges();
                stCode = "200";
            }
            catch
            {
                stCode = "400";
                throw;
            }
            return stCode;
        }

        public string DeactQuota(int QuotaId)
        {
            string Result = string.Empty;
            Quota delete;

            try
            {
                delete = _trainDb.quotas.Find(QuotaId);

                if (delete != null)
                {
                    delete.isActive = false;
                    _trainDb.SaveChanges();
                    Result = "Deactivated";
                }
            }
            catch (Exception ex)
            {
                Result = "400";
                throw;
            }
            finally
            {
                delete = null;
            }
            return Result;
        }

        public string UpdateQuota(Quota quota)
        {
            string Result = string.Empty;
            try
            {
                _trainDb.Entry(quota).State = EntityState.Modified;
                _trainDb.SaveChanges();
                Result = "200";
            }
            catch (Exception ex)
            {
                
                Result = "400";
                throw;
            }
            return Result;
        }

        public Quota GetQuota(int QuotaId)
        {
            Quota quota = null;
            try
            {
                quota = _trainDb.quotas.Find(QuotaId);
            }
            catch (Exception ex)
            {
                throw;
            }
            return quota;
        }

        public List<Quota> GetAllQuotas()
        {

            List<Quota> quotas = null;
            try
            {
                quotas = _trainDb.quotas.ToList();

            }
            catch (Exception ex)
            {
                throw;
            }
            return quotas;

        }


    }
}