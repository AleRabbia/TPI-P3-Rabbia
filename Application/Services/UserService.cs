using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            _userRepository.Add(user);
        } 

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUserById(int id, int? userId)
        {
            if (userId != null && id != userId)
            {    
                throw new UnauthorizedAccessException("El usuario no tiene permiso para acceder a estos datos.");
            }
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                throw new KeyNotFoundException("El usuario no existe.");
            }

            return user;
        }
          public void DeleteUserLogic(User user)
        {
            _userRepository.DeleteUserLogic(user);
        }
         
        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            var existingUser = _userRepository.GetById(id);
        if (existingUser == null)
        {
            throw new KeyNotFoundException("El usuario no existe.");
        }

        _userRepository.Delete(id);
        }


        public IEnumerable<UserDto> GetEnabledUsers()
        {
            var enabledUsers = _userRepository.GetEnabledUsers();
            return enabledUsers.Select(user => UserDto.Create(user));
        }
    }
}
