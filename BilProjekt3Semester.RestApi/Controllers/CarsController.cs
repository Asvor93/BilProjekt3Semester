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
    }
}