using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BilProjekt3Semester.Core.ApplicationServices;
using BilProjekt3Semester.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BilProjekt3Semester.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        // GET api/cars
        [HttpGet]
        public ActionResult<FilteredList<Car>> Get([FromQuery] Filter filter)
        {
            try
            {
                return Ok(_carService.GetCars(filter));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Car> GetById(int id)
        {
            return _carService.ReadById(id);
        }

        // POST api/cars
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Car> Post([FromBody] Car car)
        {
            try
            {
                return Ok(_carService.CreateCar(car));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //DELETE api/cars
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Car> Delete(int id)
        {
            try
            {
                return Ok(_carService.DeleteCar(id > 0 ? new Car() {CarId = id} : null));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //PUT api/cars
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Car> Put(int id, [FromBody] Car carToUpdate)
        {
            try
            {
                if (id < 1 || id != carToUpdate.CarId)
                {
                    return BadRequest("The id does not match anything");
                }

                return Ok(_carService.UpdateCar(carToUpdate));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}