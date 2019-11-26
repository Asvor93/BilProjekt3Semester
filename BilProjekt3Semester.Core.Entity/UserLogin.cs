using System;
namespace BilProjekt3Semester.Core.Entity
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public int Id { get; set; }
    }
}
