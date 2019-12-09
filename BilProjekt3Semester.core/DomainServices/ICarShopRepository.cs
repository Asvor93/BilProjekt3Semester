using System.Collections.Generic;
using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.core.ApplicationServices
{
    public interface ICarShopRepository
    {
        IEnumerable<Car> ReadAllCars();
        Car CreateCar(Car car);
        Car DeleteCar(int id);
        Car UpdateCar(Car carToUpdate);
    }
}