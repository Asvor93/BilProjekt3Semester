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
                Sold = false,
                Description = "Pæne bil med meget plads. 2.4 i. 162 Hk. " +
                              "Synsfri træk 1700 kg.Årg 4 / 5.2006. Km 231000." +
                              "Synet 4 / 5.2018. Se reg nr XXC 52558.Tre nøgler. " +
                              "Undervogns behandlet.Flot stue. Kører bare godt.AC / Klima.CD. " +
                              "4 x El - ruder.Fart pilot.Sæde varme. 16 t alu med m / s dæk. " +
                              "Med nr plader, Plus 600 kr til omg."
            };

            var c2 = new Car
            {
                CarAccessories = ca2,
                CarDetails = cd2,
                CarSpecs = cs2,
                Sold = false,
                Description = "Kører bare godt. 1.6 HDI 1069 Hk. 6 Gear. " +
                "Nysynet 22 / 11.2019," +
                "Årg 24 / 9. 2009." +
                "Km 329000.Se reg nr BG 48737." +
                "Årlig vægt afgift 4000 kr.To nøgler." +
                "Synsfri træk 1000 kg.CD.AC.El - ruder." +
                "17 t alu med gode dæk." +
                "Service er ok." +
                "Med nr plader," +
                "Plus 600 kr til omg."

            };
            
            var c3 = new Car
            {
                CarAccessories = ca3,
                CarDetails = cd3,
                CarSpecs = cs3,
                Sold = false,
                Description = "Pæn St.Car 2.0 i 115 Hk. Synet 1/10.2018." +
                "Km 355000.Se reg nr CA 31456." +
                "Årg 4 / 7.2000.En nøgle." +
                "Kører bare godt." +
                "4 x El - ruder.CD." +
                "Tandrem er skiftet ved 307000 km i 2017." +
                "Med nr plader," +
                "Plus 600 kr til omg."
            };

            context.Cars.AddRange(c1,c2,c3);
            context.CarDetails.AddRange(cd1,cd2,cd3);
            context.CarAccessories.AddRange(ca1,ca2,ca3);
            context.CarSpecs.AddRange(cs1,cs2,cs3);
            context.SaveChanges();
        }
    }
}