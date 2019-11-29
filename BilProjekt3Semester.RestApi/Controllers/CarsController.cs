using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BilProjekt3Semester.Core.ApplicationServices;
using BilProjekt3Semester.Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BilProjekt3Semester.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ICarShopService _carShopService;

        public CarsController(ICarShopService carShopService)
        {
            _carShopService = carShopService;
        }

        // GET api/cars
        [HttpGet]
        public ActionResult<Car> Get()
        {
            try
            {
                return Ok(_carShopService.GetCars());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/cars
        [HttpPost]
        public ActionResult<Car> Post([FromBody] Car car)
        {
            try
            {
                return Ok(_carShopService.CreateCar(car));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //DELETE api/cars
        [HttpDelete("{id}")]
        public ActionResult<Car> Delte(int id)
        {
            try
            {
                return Ok(_carShopService.DeleteCar(id > 0 ? new Car() {CarId = id} : null));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}