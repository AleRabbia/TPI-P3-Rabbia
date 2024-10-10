using Domain.Entities;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Update(User user);
        void DeleteUserLogic(User user);
        void Delete(int id);
        User GetByMail(string email);
        IEnumerable<User> GetEnabledUsers();
    }
}
