using System.Collections.Generic;
using System.IO;
using System.Linq;
using BilProjekt3Semester.core.ApplicationServices;
using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.Core.ApplicationServices.Services
{
    public class CarService: ICarShopService
    {
        private readonly ICarShopRepository _carShopRepository;

        public CarService(ICarShopRepository carShopRepository)
        {
            _carShopRepository = carShopRepository;
        }
        public List<Car> GetCars()
        {
            if (_carShopRepository.ReadAllCars() != null)
            {
                return _carShopRepository.ReadAllCars().ToList();
            }

            return null;
        }

        public Car CreateCar(Car car)
        {
            return _carShopRepository.CreateCar(car);
        }

        public Car DeleteCar(Car carToDelete)
        {
            if (carToDelete != null)
            {
                return _carShopRepository.DeleteCar(carToDelete.CarId);
            }
            throw new InvalidDataException("The car you are trying to delete does not exist");
        }
    }
}