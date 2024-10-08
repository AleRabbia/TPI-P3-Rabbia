﻿using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            var user = _context.Users.Find(id)?? throw new Exception($"No se encontró un usuario con ID {id}.");
            return user;
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
        
        public void DeleteUserLogic(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        } 

        public User GetByMail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email)  ?? throw new Exception($"No se encontró un usuario el correo {email}.");
            return user;
        }
        public IEnumerable<User> GetEnabledUsers()
        {
            return _context.Users.Where(u => u.Enabled).ToList();
        }
    }
}
