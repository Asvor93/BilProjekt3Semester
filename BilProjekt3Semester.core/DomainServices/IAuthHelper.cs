using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.core.ApplicationServices
{
    public interface IAuthHelper
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
        string GenerateToken(User user);
    }
}