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
        public ActionResult<Car> Get()
        {
            try
            {
                return Ok(_carService.GetCars());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
        public ActionResult<Car> Delte(int id)
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
    }
}