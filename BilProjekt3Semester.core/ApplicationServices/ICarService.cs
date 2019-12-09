using System.Collections.Generic;
using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.Core.ApplicationServices
{
    public interface ICarService
    {
        List<Car> GetCars();
        Car CreateCar(Car car);
        Car DeleteCar(Car carToDelete);
        Car ReadById(int id);
    }
}