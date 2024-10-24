using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserService
    {
        void AddUser(User user); 
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id, int? userId);
        void UpdateUser(User user);
        void DeleteUserLogic(User user);
        void DeleteUser(int id);
        IEnumerable<UserDto> GetEnabledUsers();
    }
}

