using BilProjekt3Semester.core.ApplicationServices;
using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.Infrastructure.SQL
{
    public class DbInitializer: IDbInitializer
    {
        private static IAuthHelper _authHelper;

        public DbInitializer(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public void SeedDb(CarShopContext context)
        {
            context.Database.EnsureCreated();
            
            //User
            string password = "1234";
            byte[] passwordHashAdmin, passwordSaltAdmin;
            _authHelper.CreatePasswordHash(password,out passwordHashAdmin,out passwordSaltAdmin);

            var user = context.Users.Add(new User
            {
                Username = "admin",
                PasswordHash = passwordHashAdmin,
                PasswordSalt = passwordSaltAdmin,
                IsAdmin = true
            });

            //Car accessories
            var ca1 = new CarAccessory
            {
                AbsBremser = true
            };

            var ca2 = new CarAccessory
            {
                AbsBremser = false
            };

            var ca3 = new CarAccessory
            {
                AbsBremser = true
            };

            //Car details
            var cd1 = new CarDetail
            {
                Døre = 5
            };

            var cd2 = new CarDetail
            {
                Døre = 3
            };

            var cd3 = new CarDetail
            {
                Døre = 5
            };

            //Car specs
            var cs1 = new CarSpec
            {
                Gear = 6
            };

            var cs2 = new CarSpec
            {
                Gear = 5
            };

            var cs3 = new CarSpec
            {
                Gear = 6
            };

            //Cars
            var c1 = new Car
            {
                CarAccessories = ca1,
                CarDetails = cd1,
                CarSpecs = cs1,
                Sold = false
            };

            var c2 = new Car
            {
                CarAccessories = ca2,
                CarDetails = cd2,
                CarSpecs = cs2,
                Sold = false
            };
            
            var c3 = new Car
            {
                CarAccessories = ca3,
                CarDetails = cd3,
                CarSpecs = cs3,
                Sold = false
            };

            context.Cars.AddRange(c1,c2,c3);
            context.CarDetails.AddRange(cd1,cd2,cd3);
            context.CarAccessories.AddRange(ca1,ca2,ca3);
            context.CarSpecs.AddRange(cs1,cs2,cs3);
            context.SaveChanges();
        }
    }
}