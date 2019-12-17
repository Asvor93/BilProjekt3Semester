using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BilProjekt3Semester.core.ApplicationServices;
using BilProjekt3Semester.Core.DomainServices;
using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.Core.ApplicationServices.Services
{
    public class CarService : ICarService
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

        public Car ReadById(int id)
        {
            return _carRepository.ReadById(id);
        }

        public Car CreateCar(Car carToCreate)
        {
            ValidateId(carToCreate.CarId);
            ValidateSpecValues(carToCreate.CarSpecs);
            ValidateAccessoryValues(carToCreate.CarAccessories);
            ValidateDetailValues(carToCreate.CarDetails);
          

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
            ValidateSpecValues(car.CarSpecs);
            ValidateAccessoryValues(car.CarAccessories);
            ValidateDetailValues(car.CarDetails);
            return _carRepository.UpdateCar(car);
        }

        public int Count()
        {
            return _carRepository.Count();
        }

        public void CheckAndDeleteOldCars()
        {
            var cars = GetFilteredCars(new Filter());
            foreach (var car in cars.List)
            {
                if (car.Sold)
                {
                    if (car.SoldDate.AddDays(3) < DateTime.Now)
                    {
                        _carRepository.CheckAndDeleteOldCars(car);
                    }
                }
            }
        }

        public void ValidateAccessoryValues(CarAccessory carAccessory)
        {
            if (carAccessory == null)
            {
                throw new InvalidDataException("CarAccessories can't be null when trying to create a car");
            }

            if (carAccessory.NrOfAirbags < 0)
            {
                throw new InvalidDataException("Number of airbags has to be higher than or equal to zero");
            }
        }

        public void ValidateDetailValues(CarDetail carDetail)
        {
            if (carDetail == null)
            {
                throw new InvalidDataException("CarDetails can't be null when trying to create a car");
            }

            if (carDetail.Color != null)
            {
                if (carDetail.Color.Any(char.IsDigit))
                {
                    throw new InvalidDataException("Color can not contain numbers");
                }
            }
            

            if (carDetail == null)
            {
                throw new InvalidDataException("CarDetails can't be null when trying to create a car");
            }

            if (carDetail.Kilometer < 0)
            {
                throw new InvalidDataException("Kilometer has to be higher than or equal to zero");
            }

            if (carDetail.Doors < 0)
            {
                throw new InvalidDataException("Doors has to be higher than or equal to zero");
            }

            if (carDetail.HorsePower < 0)
            {
                throw new InvalidDataException("HorsePower has to be higher than or equal to zero");
            }

            if (carDetail.KmPrLiter < 0)
            {
                throw new InvalidDataException("KmPrLiter has to be higher than or equal to zero");
            }

            if (carDetail.MotorSize < 0)
            {
                throw new InvalidDataException("MotorSize has to be higher than or equal to zero");
            }

            if (carDetail.Torque < 0)
            {
                throw new InvalidDataException("Torque has to be higher than or equal to zero");
            }

            if (carDetail.Year < 0)
            {
                throw new InvalidDataException("Year has to be higher than or equal to zero");
            }

            if (carDetail.TopSpeed < 0)
            {
                throw new InvalidDataException("Top speed has to be higher than or equal to zero");
            }
        }

        public void ValidateId(int id)
        {
            if (id < 0)
            {
                throw new InvalidDataException("Id has to be higher than zero");
            }
        }


        public void ValidateSpecValues(CarSpec carSpecs)
        {
            if (carSpecs == null)
            {
                throw new InvalidDataException("CarSpecs can't be null when trying to create a car");
            }

            if (carSpecs.Tonnage < 0)
            {
                throw new InvalidDataException("Tonnage has to be higher than or equal to zero");
            }

            if (carSpecs.Tank < 0)
            {
                throw new InvalidDataException("Tank has to be higher than or equal to zero");
            }

            if (carSpecs.NewPrice < 0)
            {
                throw new InvalidDataException("New price has to be higher than or equal to zero");
            }

            if (carSpecs.Width < 0)
            {
                throw new InvalidDataException("Width has to be higher than or equal to zero");
            }

            if (carSpecs.Valves < 0)
            {
                throw new InvalidDataException("Valves has to be higher than or equal to zero");
            }

            if (carSpecs.MaxWeight < 0)
            {
                throw new InvalidDataException("Max weight has to be higher than or equal to zero");
            }

            if (carSpecs.Length < 0)
            {
                throw new InvalidDataException("Length has to be higher than or equal to zero");
            }

            if (carSpecs.Cylinder < 0)
            {
                throw new InvalidDataException("Cylinder has to be higher than or equal to zero");
            }

            if (carSpecs.CostPrSixMonths < 0)
            {
                throw new InvalidDataException("Cost per six months has to be higher than or equal to zero");
            }

            if (carSpecs.Weight < 0)
            {
                throw new InvalidDataException("Weight has to be higher than or equal to zero");
            }

            if (carSpecs.MaxTrailerWeight < 0)
            {
                throw new InvalidDataException("Nax trailer weight has to be higher than or equal to zero");
            }
        }
    }
}