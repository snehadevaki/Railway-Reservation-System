using RailwaySystem.API.Model;
using RailwaySystem.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Services
{
    public class UserServices
    {
        private IUserRepo _userRepository;
        public UserServices(IUserRepo userRepository)
        {
            _userRepository = userRepository;
        }
        public double SaveUser(User user)
        {
            return _userRepository.SaveUser(user);
        }
        public string UpdateUser(User user)
        {
            return _userRepository.UpdateUser(user);
        }
        public User GetUser(int UserId)
        {
            return _userRepository.GetUser(UserId);
        }
        public User GetUserbyEmail(string Email)
        {
            return _userRepository.GetUserbyEmail(Email);
        }
        public List<User> GetAllUser()
        {
            return _userRepository.GetAllUser();
        }
    }
}