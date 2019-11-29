using System;
using System.Collections.Generic;
using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.Core.DomainServices
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        void Add(User entity);
        void Edit(User entity);
        void Remove(int id);
    }
}
