using Microsoft.EntityFrameworkCore;
using RailwaySystem.API.Data;
using RailwaySystem.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Repository
{
    public class BankCredRepo : IBankCredRepo
    {
        private TrainDbContext _trainDb;

        public BankCredRepo(TrainDbContext trainDb)
        {
            _trainDb = trainDb;
        }
        public string DeactBankCred(int BankCredId)
        {
            string Result = string.Empty;
            BankCred delete;

            try
            {
                delete = _trainDb.bankCred.Find(BankCredId);

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
                Result = "400";
                throw;
            }
            finally
            {
                delete = null;
            }
            return Result;
        }
        #region GetAllBankCred
        public List<BankCred> GetAllBankCreds()
        {
            List<BankCred> bankcred = null;
            try
            {
                bankcred = _trainDb.bankCred.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return bankcred;

        }
        #endregion


        #region GetBankCred
        public BankCred GetBankCred(int BankCredId)
        {

            BankCred bankcred = null;
            try
            {
                bankcred = _trainDb.bankCred.Find(BankCredId);
                return bankcred;
            }
            catch (Exception ex)
            {
                throw;
            }
            return bankcred;
        }
        #endregion

        #region SaveBankCred
        public string SaveBankCred(BankCred bankcred)
        {
            try
            {

                _trainDb.bankCred.Add(bankcred);
                _trainDb.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return "Saved";
        }
        #endregion

        #region UpdateBankCred
        public string UpdateBankCred(BankCred bankcred)
        {
            try
            {
                _trainDb.Entry(bankcred).State = EntityState.Modified;
                _trainDb.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return "Updated";
        }
        #endregion
    }
}