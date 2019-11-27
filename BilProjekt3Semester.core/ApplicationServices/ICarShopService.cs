using System.Collections.Generic;
using BilProjekt3Semester.Core.Entity;

namespace BilProjekt3Semester.Core.ApplicationServices
{
    public interface ICarShopService
    {
        List<Car> GetCars();
    }
}