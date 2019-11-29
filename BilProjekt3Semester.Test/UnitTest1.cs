using System;
using System.Collections.Generic;
using System.IO;
using BilProjekt3Semester.core.ApplicationServices;
using BilProjekt3Semester.Core.ApplicationServices;
using BilProjekt3Semester.Core.ApplicationServices.Services;
using BilProjekt3Semester.Core.Entity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using Xunit;

namespace BilProjekt3Semester.Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestCreateCarOnlyCallsRepositoryOnce()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory { AbsBremser = false },
                CarSpecs = new CarSpec(),
                CarDetails = new CarDetail()
            };

            var mockCarRepo = new Mock<ICarShopRepository>();
            mockCarRepo.Setup(r => r.CreateCar(It.IsAny<Car>())).Returns(mockCar);
            ICarShopService service = new CarService(mockCarRepo.Object);
            
            //Calling method
            service.CreateCar(mockCar);

            //Assert
            mockCarRepo.Verify(s => s.CreateCar(It.IsAny<Car>()), Times.Once);
        }

        [Fact]
        public void TestCreateShouldThrowExceptionIfCarHasNoCarDetails()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory(),
                CarSpecs = new CarSpec()
            };

            var mockCarRepo = new Mock<ICarShopRepository>();
            ICarShopService service = new CarService(mockCarRepo.Object);

            //Calling method and assert
            Exception ex = Assert.Throws<InvalidDataException>(() => service.CreateCar(mockCar));
            Assert.Equal("CarDetails can't be null when trying to create a car", ex.Message);
        }

        [Fact]
        public void TestCreateShouldThrowExceptionIfCarHasNoCarAccessory()
        {
            //setup
            var mockCar = new Car
            {
                CarSpecs = new CarSpec(),
                CarDetails = new CarDetail()
            };

            var mockCarRepo = new Mock<ICarShopRepository>();
            ICarShopService service = new CarService(mockCarRepo.Object);

            //Calling method and assert
            Exception ex = Assert.Throws<InvalidDataException>(() => service.CreateCar(mockCar));
            Assert.Equal("CarAccessories can't be null when trying to create a car", ex.Message);
        }

        [Fact]
        public void TestCreateShouldThrowExceptionIfCarHasNoCarSpecs()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory(),
                CarDetails = new CarDetail()
            };

            var mockCarRepo = new Mock<ICarShopRepository>();
            ICarShopService service = new CarService(mockCarRepo.Object);

            //Calling method and assert
            Exception ex = Assert.Throws<InvalidDataException>(() => service.CreateCar(mockCar));
            Assert.Equal("CarSpecs can't be null when trying to create a car", ex.Message);
        }

    }
}
