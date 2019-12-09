﻿using System.Collections.Generic;
using System.Linq;
using BilProjekt3Semester.core.ApplicationServices;
using BilProjekt3Semester.Core.DomainServices;
using BilProjekt3Semester.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace BilProjekt3Semester.Infrastructure.SQL.Repositories
{
    public class CarRepository: ICarRepository
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

        public Car DeleteCar(int id)
        {
            var carToDelete = _carShopContext.Remove(new Car {CarId = id}).Entity;
            _carShopContext.SaveChanges();
            return carToDelete;
        }

        public Car UpdateCar(Car carToUpdate)
        {
            _carShopContext.Attach(carToUpdate).State = EntityState.Modified;
            _carShopContext.Entry(carToUpdate).Reference(c => c.CarDetails).IsModified = true;
            _carShopContext.Entry(carToUpdate).Reference(c => c.CarAccessories).IsModified = true;
            _carShopContext.Entry(carToUpdate).Reference(c => c.CarSpecs).IsModified = true;
            _carShopContext.Entry(carToUpdate).Reference(c => c.PictureLinks).IsModified = true;
            _carShopContext.SaveChanges();
            return carToUpdate;
        }
    }
}