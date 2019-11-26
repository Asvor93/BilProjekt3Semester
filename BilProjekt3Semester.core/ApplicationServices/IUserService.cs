using System;
using System.Collections.Generic;
using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.Core.ApplicationServices
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUser(int id);
        User CreateUser(UserLogin userlogin);
        User UpdateUser(UserLogin userlogin);
        User DeleteUser(int id);
    }
}
