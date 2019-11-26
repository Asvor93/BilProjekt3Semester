using System;
using System.Collections.Generic;
using System.Linq;
using BilProjekt3Semester.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace BilProjekt3Semester.Infrastructure.SQL.Repositories
{
    public class UserRepo
    {

        private CarShopContext _context;

        public UserRepo(CarShopContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers(Filter filter)
        {
            if (filter == null)
            {
                return _context.Users
                    .Select(u => new User { Id = u.Id, IsAdmin = u.IsAdmin, Username = u.Username })
                    .ToList(); ;
            }
            return _context.Users
                .Select(u => new User { Id = u.Id, IsAdmin = u.IsAdmin, Username = u.Username })
                .Skip((filter.CurrentPage - 1) * filter.ItemsPrPage)
                .Take(filter.ItemsPrPage)
                .ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users
                .Select(u => new User { Id = u.Id, IsAdmin = u.IsAdmin, Username = u.Username })
                .FirstOrDefault(u => u.Id == id);
        }

        public User CreateUser(User user)
        {
            _context.Attach(user).State = EntityState.Added;
            _context.SaveChanges();
            user.PasswordSalt = null;
            user.PasswordHash = null;
            return user;
        }

        public User UpdateUser(User user)
        {
            _context.Attach(user).State = EntityState.Modified;
            _context.SaveChanges();
            user.PasswordSalt = null;
            user.PasswordHash = null;
            return user;
        }

        public User DeleteUser(int id)
        {
            var user = _context.Remove(new User { Id = id }).Entity;
            _context.SaveChanges();
            user.PasswordSalt = null;
            user.PasswordHash = null;
            return user;
        }

        public List<User> LogIn()
        {
            return _context.Users.ToList();
        }

        public int Count()
        {
            return _context.Users.Count();
        }
    }
}