using Microsoft.EntityFrameworkCore;
using RailwaySystem.API.Data;
using RailwaySystem.API.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Repository
{
    public class UserRepo : IUserRepo
    {
        private TrainDbContext _trainDb;

        public UserRepo(TrainDbContext trainDb)
        {
            _trainDb = trainDb;
        }

        #region GetAllUser
        public List<User> GetAllUser()
        {
            List<User> users = null;
            try
            {
                users = _trainDb.users.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }

            return users;
        }
        #endregion

        #region GetUser


        public User GetUser(int UserId)
        {
            User user = null;
            try
            {
                user = _trainDb.users.Find(UserId);

            }
            catch (Exception ex)
            {
                throw;
            }
            return user;
        }
        #endregion

        #region GetUserbyEmail
        public User GetUserbyEmail(string Email)
        {
            User user = null;
            try
            {
                user = _trainDb.users.FirstOrDefault(q => q.Email == Email);

            }
            catch (Exception ex)
            {
                throw;
            }
            return user;
        }
        #endregion

        #region SaveUser
        public double SaveUser(User user)
        {
            double message = 0.00;
            try
            {
                User userd = GetUserbyEmail(user.Email);
                if (userd != null)
                {
                    message = 1.00;
                }
                else
                {
                    _trainDb.users.Add(user);

                    _trainDb.SaveChanges();

                    message = 0.00;

                }

              
            }
            catch (Exception ex)
            {
                throw;
            }

            return message;
        }
        #endregion

        #region UpdateUser
        public string UpdateUser(User user)
        {
            try
            {
                _trainDb.Entry(user).State = EntityState.Modified;
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