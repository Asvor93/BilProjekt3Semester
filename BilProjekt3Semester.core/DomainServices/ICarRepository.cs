using System.Collections.Generic;
using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.Core.DomainServices
{
    public interface ICarRepository
    {
        IEnumerable<Car> ReadAllCars();
        Car CreateCar(Car car);
        Car DeleteCar(int id);
    }
}