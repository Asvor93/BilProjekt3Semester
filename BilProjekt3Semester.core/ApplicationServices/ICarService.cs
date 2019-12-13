using System.Collections.Generic;
using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.Core.ApplicationServices
{
    public interface ICarService
    {
        FilteredList<Car> GetFilteredCars(Filter filter);
        FilteredList<Car> GetCars();
        Car CreateCar(Car car);
        Car DeleteCar(Car carToDelete);
        Car ReadById(int id);
        Car UpdateCar(Car car);
        int Count();
        void CheckAndDeleteOldCars();
    }
}