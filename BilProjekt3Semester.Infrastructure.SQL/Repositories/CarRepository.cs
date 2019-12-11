using System;
using System.Collections.Generic;
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
        public FilteredList<Car> ReadAllCars(Filter filter)
        {
            var filteredList = new FilteredList<Car>();

            if (filter.CurrentPage > 0 && filter.ItemsPrPage > 0)
            {
                filteredList.List = _carShopContext.Cars.Include(c => c.CarAccessories)
                    .Include(c => c.CarDetails)
                    .Include(c => c.CarSpecs)
                    .Include(c => c.PictureLinks).Skip((filter.CurrentPage - 1)
                                                       * filter.ItemsPrPage).Take(filter.ItemsPrPage);

                filteredList.Count = _carShopContext.Cars.Count();

                return filteredList;
            }
            filteredList.List = _carShopContext.Cars.Include(c => c.CarAccessories)
                .Include(c => c.CarDetails)
                .Include(c => c.CarSpecs)
                .Include(c => c.PictureLinks);
            filteredList.Count = _carShopContext.Cars.Count();

            return filteredList;

        }

        public Car CreateCar(Car car)
        {
            _carShopContext.Attach(car).State = EntityState.Added;
            _carShopContext.SaveChanges();
            return car;
        }

        public Car ReadById(int id)
        {
            return _carShopContext.Cars
                .Include(c => c.CarAccessories)
                .Include(c => c.CarDetails)
                .Include(c => c.CarSpecs)
                .Include(c => c.PictureLinks)
                .FirstOrDefault(c => c.CarId == id);
        }

        public int Count()
        {
            return _carShopContext.Cars.Count();
        }

        public void CheckAndDeleteOldCars()
        {
            var cars = _carShopContext.Cars;
            foreach (var car in cars)
            {
                if (car.Sold)
                {
                    if (car.SoldDate.Value.AddDays(3) < DateTime.Now)
                    {
                        DeleteCar(car.CarId);
                    }
                }
            }
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
            _carShopContext.Entry(carToUpdate).Collection(c => c.PictureLinks).IsModified = true;
            _carShopContext.SaveChanges();
            return carToUpdate;
        }
    }
}