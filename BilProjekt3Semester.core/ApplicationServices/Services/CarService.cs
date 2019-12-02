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

        public Car CreateCar(Car carToCreate)
        {
            if (carToCreate.CarDetails == null)
            {
                throw new InvalidDataException("CarDetails can't be null when trying to create a car");
            }

            if (carToCreate.CarAccessories == null)
            {
                throw new InvalidDataException("CarAccessories can't be null when trying to create a car");
            }

            if (carToCreate.CarSpecs == null)
            {
                throw new InvalidDataException("CarSpecs can't be null when trying to create a car");
            }

            return _carShopRepository.CreateCar(carToCreate);
        }

        public Car DeleteCar(Car carToDelete)
        {
            if (carToDelete == null)
            {
                throw new InvalidDataException("The car you are trying to delete does not exist");
            }
            return _carShopRepository.DeleteCar(carToDelete.CarId);
        }
    }
}