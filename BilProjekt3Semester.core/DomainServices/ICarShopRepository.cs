using System.Collections.Generic;
using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.core.ApplicationServices
{
    public interface ICarShopRepository
    {
        IEnumerable<Car> ReadAllCars();
    }
}