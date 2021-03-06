﻿using System;
using System.Collections.Generic;
using System.Linq;
using BilProjekt3Semester.core.ApplicationServices;
using BilProjekt3Semester.Core.DomainServices;
using BilProjekt3Semester.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace BilProjekt3Semester.Infrastructure.SQL.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarShopContext _carShopContext;

        public CarRepository(CarShopContext carShopContext)
        {
            _carShopContext = carShopContext;
        }

        public FilteredList<Car> ReadAllCars(Filter filter)
        {
            var filteredList = new FilteredList<Car>();
            if (filter != null)
            {
                if (string.IsNullOrEmpty(filter.SearchBrandNameQuery))
                {
                    filter.SearchBrandNameQuery = "";
                }
            }

            if (filter != null && (filter.CurrentPage > 0 && filter.ItemsPrPage > 0))
            {
                filteredList.List = _carShopContext.Cars
                    .Include(c => c.CarAccessories)
                    .Include(c => c.CarDetails)
                    .Include(c => c.CarSpecs)
                    .Include(c => c.PictureLinks).Skip((filter.CurrentPage - 1)
                                                       * filter.ItemsPrPage).Take(filter.ItemsPrPage)
                    .Where(c => c.CarDetails.BrandName.Contains(filter.SearchBrandNameQuery));

                filteredList.Count = _carShopContext.Cars.Count();

                return filteredList;
            }

            filteredList.List = _carShopContext.Cars
                .Include(c => c.CarAccessories)
                .Include(c => c.CarDetails)
                .Include(c => c.CarSpecs)
                .Include(c => c.PictureLinks)
                .Where(c => c.CarDetails.BrandName.Contains(filter.SearchBrandNameQuery));
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

        public void CheckAndDeleteOldCars(Car car)
        {
            _carShopContext.Cars.Remove(car);
            _carShopContext.SaveChanges();
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
            _carShopContext.Entry(carToUpdate.CarDetails).State = EntityState.Modified;
            _carShopContext.Entry(carToUpdate.CarAccessories).State = EntityState.Modified;
            _carShopContext.Entry(carToUpdate.CarSpecs).State = EntityState.Modified;
            _carShopContext.Entry(carToUpdate).Collection(c => c.PictureLinks).IsModified = true;
            _carShopContext.SaveChanges();
            return carToUpdate;
        }
    }
}