using System.Collections.Generic;
using System.Linq;
using BilProjekt3Semester.core.ApplicationServices;
using BilProjekt3Semester.Core.Entity;

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
            return _carShopContext.Cars;
        }
    }
}