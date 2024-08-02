using Microsoft.EntityFrameworkCore;
using RailwaySystem.API.Data;
using RailwaySystem.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Repository
{
    public class TransactionRepo : ITransactionRepo
    {
        private TrainDbContext _trainDb;

        public TransactionRepo(TrainDbContext trainDb)
        {
            _trainDb = trainDb;
        }

        #region GetAllTransactions
        public List<Transaction> GetAllTransaction()
        {
            List<Transaction> transactions = null;
            try
            {
                transactions = _trainDb.transaction.ToList();

            }
            catch (Exception ex)
            {

            }
            return transactions;
        }

        #endregion

        #region GetTransaction
        public Transaction GetTransaction(int TransactionId)
        {
            Transaction transaction = null;
            try
            {
                transaction = _trainDb.transaction.Find(TransactionId);
            }
            catch (Exception ex)
            {

            }
            return transaction;
        }
        #endregion

        #region SaveTransaction
        public string SaveTransaction(Transaction transaction)
        {
            try
            {
                _trainDb.transaction.Add(transaction);
                _trainDb.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            return "Saved";
        }
        #endregion

        #region UpdateTransaction
        public string UpdateTransaction(Transaction transaction)
        {
            try
            {
                _trainDb.Entry(transaction).State = EntityState.Modified;
                _trainDb.SaveChanges();
            }
            catch (Exception ex)
            {

            }

            return "Updated";
        }
        #endregion

    }
}