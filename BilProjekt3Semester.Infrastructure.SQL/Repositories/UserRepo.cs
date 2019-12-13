using System;
using System.Collections.Generic;
using System.Linq;
using BilProjekt3Semester.Core.DomainServices;
using BilProjekt3Semester.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace BilProjekt3Semester.Infrastructure.SQL.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly CarShopContext db;

        public UserRepo(CarShopContext context)
        {
            db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.ToList();
        }

        public User Get(int id)
        {
            return db.Users.FirstOrDefault(b => b.Id == id);
        }

        public void Add(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
        }

        public void Edit(User entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Remove(int id)
        {
            var item = db.Users.FirstOrDefault(b => b.Id == id);
            db.Users.Remove(item);
            db.SaveChanges();
        }
    }
}