using System.Collections.Generic;
using System.IO;
using System.Linq;
using BilProjekt3Semester.core.ApplicationServices;
using BilProjekt3Semester.Core.DomainServices;
using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.Core.ApplicationServices.Services
{
    public class CarService: ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public FilteredList<Car> GetFilteredCars(Filter filter = null)
        {

                return _carRepository.ReadAllCars(filter);
        }

        public FilteredList<Car> GetCars()
        {
            return _carRepository.ReadAllCars();
        }

        public Car ReadById(int id)
        {
            return _carRepository.ReadById(id);
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

            return _carRepository.CreateCar(carToCreate);
        }

        public Car DeleteCar(Car carToDelete)
        {
            if (carToDelete == null)
            {
                throw new InvalidDataException("The car you are trying to delete does not exist");
            }
            return _carRepository.DeleteCar(carToDelete.CarId);
        }

        public Car UpdateCar(Car car)
        {
            if (car == null)
            {
                throw new InvalidDataException("The car does not exist!");
            }

            return _carRepository.UpdateCar(car);
        }

        public int Count()
        {
            return _carRepository.Count();
        }

        public void CheckAndDeleteOldCars()
        {
            _carRepository.CheckAndDeleteOldCars();
        }
    }
}