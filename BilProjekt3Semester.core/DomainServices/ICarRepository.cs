using System.Collections.Generic;
using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.Core.DomainServices
{
    public interface ICarRepository
    {
        FilteredList<Car> ReadAllCars(Filter filter = null);
        Car CreateCar(Car car);
        Car DeleteCar(int id);
        Car UpdateCar(Car carToUpdate);
        Car ReadById(int id);
        int Count();
    }
}