using System;
using System.Collections.Generic;
using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.Core.DomainServices
{
    public interface IUserRepo
    {
        List<User> GetAllUsers(Filter filter);
        User GetUser(int id);
        User CreateUser(User user);
        User UpdateUser(User user);
        User DeleteUser(int id);
        List<User> LogIn();
        int Count();
    }
}
