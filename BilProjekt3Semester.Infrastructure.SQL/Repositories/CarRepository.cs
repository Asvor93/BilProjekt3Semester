using System.Collections.Generic;
using System.Linq;
using BilProjekt3Semester.core.ApplicationServices;
using BilProjekt3Semester.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace BilProjekt3Semester.Infrastructure.SQL.Repositories
{
    public class CarRepository: ICarShopRepository
    {
        private readonly CarShopContext _carShopContext;

        public CarRepository(CarShopContext carShopContext)
        {
            _carShopContext = carShopContext;
        }
        public IEnumerable<Car> ReadAllCars()
        {
            return _carShopContext.Cars
                .Include(c => c.CarAccessories)
                .Include(c => c.CarDetails)
                .Include(c => c.CarSpecs)
                .Include(c => c.PictureLinks);
        }

        public Car CreateCar(Car car)
        {
            _carShopContext.Attach(car).State = EntityState.Added;
            _carShopContext.SaveChanges();
            return car;
        }
    }
}