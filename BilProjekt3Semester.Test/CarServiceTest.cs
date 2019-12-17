using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BilProjekt3Semester.core.ApplicationServices;
using BilProjekt3Semester.Core.ApplicationServices;
using BilProjekt3Semester.Core.ApplicationServices.Services;
using BilProjekt3Semester.Core.DomainServices;
using BilProjekt3Semester.Core.Entity;
using BilProjekt3Semester.Infrastructure.SQL.Helper;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace BilProjekt3Semester.Test
{
    public class CarServiceTest
    {
       

        [Fact]
        public void TestCreateShouldThrowExceptionIfCarHasNoCarDetails()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory(),
                CarSpecs = new CarSpec()
            };

            var mockCarRepo = new Mock<ICarRepository>();
            ICarService service = new CarService(mockCarRepo.Object);

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

            var mockCarRepo = new Mock<ICarRepository>();
            ICarService service = new CarService(mockCarRepo.Object);

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

            var mockCarRepo = new Mock<ICarRepository>();
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method and assert
            Exception ex = Assert.Throws<InvalidDataException>(() => service.CreateCar(mockCar));
            Assert.Equal("CarSpecs can't be null when trying to create a car", ex.Message);
        }

        [Fact]
        public void TesDeleteCarOnlyCallsRepositoryOnce()
        {
            //setup
            var mockCar = new Car
            {
                CarSpecs = new CarSpec(),
                CarDetails = new CarDetail()
            };

            var mockCarRepo = new Mock<ICarRepository>();
            mockCarRepo.Setup(r => r.DeleteCar(mockCar.CarId));
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            service.DeleteCar(mockCar);

            //Assert
            mockCarRepo.Verify(s => s.DeleteCar(mockCar.CarId), Times.Once);
        }

        [Fact]
        public void TestUpdateCarOnlyCallsRepositoryOnce()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory {AbsBrakes = false},
                CarSpecs = new CarSpec(),
                CarDetails = new CarDetail()
            };

            var mockCarRepo = new Mock<ICarRepository>();
            mockCarRepo.Setup(r => r.UpdateCar(It.IsAny<Car>())).Returns(mockCar);
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            service.UpdateCar(mockCar);

            //Assert
            mockCarRepo.Verify(s => s.UpdateCar(It.IsAny<Car>()), Times.Once);
        }

        [Fact]
        public void TestSearchCarsByBrandName()
        {
            //setup
            var carList = new List<Car>();
            for (int i = 0; i < 20; i++)
            {
                carList.Add(new Car()
                {
                    CarAccessories = new CarAccessory {AbsBrakes = false},
                    CarSpecs = new CarSpec(),
                    CarDetails = new CarDetail() {BrandName = "Audi" + i}
                });
                carList.Add(new Car()
                {
                    CarAccessories = new CarAccessory {AbsBrakes = false},
                    CarSpecs = new CarSpec(),
                    CarDetails = new CarDetail() {BrandName = "BMW" + i}
                });
            }

            var filter = new Filter()
            {
                SearchBrandNameQuery = "Audi19"
            };

            var expectedList = new FilteredList<Car>()
            {
                List = carList.Where(c => c.CarDetails.BrandName.Contains(filter.SearchBrandNameQuery)),
                Count = carList.Count
            };

            var mockCarRepo = new Mock<ICarRepository>();
            mockCarRepo.Setup(r => r.ReadAllCars(filter)).Returns(expectedList);
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            var foundCars = service.GetFilteredCars(filter);

            //Assert
            Assert.Equal(expectedList, foundCars);
        }

        [Fact]
        public void TestSearchCarsByBrandNameReturnsEmptyListIfNoMatch()
        {
            //setup
            var carList = new List<Car>();
            for (int i = 0; i < 20; i++)
            {
                carList.Add(new Car()
                {
                    CarAccessories = new CarAccessory { AbsBrakes = false },
                    CarSpecs = new CarSpec(),
                    CarDetails = new CarDetail() { BrandName = "Audi" + i }
                });
                carList.Add(new Car()
                {
                    CarAccessories = new CarAccessory { AbsBrakes = false },
                    CarSpecs = new CarSpec(),
                    CarDetails = new CarDetail() { BrandName = "BMW" + i }
                });
            }

            var filter = new Filter()
            {
                SearchBrandNameQuery = "Skoda"
            };

            var expectedList = new FilteredList<Car>()
            {
                List = carList.Where(c => c.CarDetails.BrandName.Contains(filter.SearchBrandNameQuery)),
                Count = carList.Count
            };

            var mockCarRepo = new Mock<ICarRepository>();
            mockCarRepo.Setup(r => r.ReadAllCars(filter)).Returns(expectedList);
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            var foundCars = service.GetFilteredCars(filter);

            //Assert
            Assert.Equal(expectedList, foundCars);
        }

        [Fact]
        public void TestSearchCarsByBrandNameReturnsAllCarsIfFilterIsNull()
        {
            //setup
            var carList = new List<Car>();
            for (int i = 0; i < 20; i++)
            {
                carList.Add(new Car()
                {
                    CarAccessories = new CarAccessory { AbsBrakes = false },
                    CarSpecs = new CarSpec(),
                    CarDetails = new CarDetail() { BrandName = "Audi" + i }
                });
                carList.Add(new Car()
                {
                    CarAccessories = new CarAccessory { AbsBrakes = false },
                    CarSpecs = new CarSpec(),
                    CarDetails = new CarDetail() { BrandName = "BMW" + i }
                });
            }

            var filter = new Filter();
        

            var expectedList = new FilteredList<Car>()
            {
                List = new List<Car>(),
                Count = carList.Count
            };

            var mockCarRepo = new Mock<ICarRepository>();
            mockCarRepo.Setup(r => r.ReadAllCars(filter)).Returns(expectedList);
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            var foundCars = service.GetFilteredCars(filter);
         
            //Assert
            Assert.Equal(expectedList, foundCars);
        }

        [Fact]
        public void TestCreateCarCanNotCreateIfWeightIsLessThanZero()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory {AbsBrakes = false},
                CarSpecs = new CarSpec {Weight = -1},
                CarDetails = new CarDetail()
            };

            var mockCarRepo = new Mock<ICarRepository>();
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            //Assert
            Assert.Throws<InvalidDataException>(() => service.CreateCar(mockCar));
            mockCarRepo.Verify(s => s.CreateCar(It.IsAny<Car>()), Times.Never);
        }

        [Fact]
        public void TestCreateCarCanNotCreateIfMaxWeightIsLessThanZero()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory {AbsBrakes = false},
                CarSpecs = new CarSpec {MaxWeight = -1},
                CarDetails = new CarDetail()    
            };

            var mockCarRepo = new Mock<ICarRepository>();
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            //Assert
            Assert.Throws<InvalidDataException>(() => service.CreateCar(mockCar));
            mockCarRepo.Verify(s => s.CreateCar(It.IsAny<Car>()), Times.Never);
        }

        [Fact]
        public void TestCreateCarCanNotCreateIfWidthIsZeroIsLessThanZero()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory {AbsBrakes = false},
                CarSpecs = new CarSpec {Width = -1},
                CarDetails = new CarDetail()
            };

            var mockCarRepo = new Mock<ICarRepository>();
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            //Assert
            Assert.Throws<InvalidDataException>(() => service.CreateCar(mockCar));
            mockCarRepo.Verify(s => s.CreateCar(It.IsAny<Car>()), Times.Never);
        }

        [Fact]
        public void TestCreateCarCanNotCreateIfLengthIsLessThanZero()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory {AbsBrakes = false},
                CarSpecs = new CarSpec {Length = -1},
                CarDetails = new CarDetail()
            };

            var mockCarRepo = new Mock<ICarRepository>();
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            //Assert
            Assert.Throws<InvalidDataException>(() => service.CreateCar(mockCar));
            mockCarRepo.Verify(s => s.CreateCar(It.IsAny<Car>()), Times.Never);
        }

        [Fact]
        public void TestCreateCarCanNotCreateIfCylinderIsLessThanZero()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory {AbsBrakes = false},
                CarSpecs = new CarSpec {Cylinder = -1},
                CarDetails = new CarDetail()
            };

            var mockCarRepo = new Mock<ICarRepository>();
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            //Assert
            Assert.Throws<InvalidDataException>(() => service.CreateCar(mockCar));
            mockCarRepo.Verify(s => s.CreateCar(It.IsAny<Car>()), Times.Never);
        }

        [Fact]
        public void TestCreateCarCanNotCreateIfValveIsLessThanZero()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory {AbsBrakes = false},
                CarSpecs = new CarSpec {Valves = -1},
                CarDetails = new CarDetail()
            };

            var mockCarRepo = new Mock<ICarRepository>();
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            //Assert
            Assert.Throws<InvalidDataException>(() => service.CreateCar(mockCar));
            mockCarRepo.Verify(s => s.CreateCar(It.IsAny<Car>()), Times.Never);
        }

        [Fact]
        public void TestCreateCarCanNotCreateIfTankIsLessThanZero()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory {AbsBrakes = false},
                CarSpecs = new CarSpec {Tank = -1},
                CarDetails = new CarDetail()
            };

            var mockCarRepo = new Mock<ICarRepository>();
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            //Assert
            Assert.Throws<InvalidDataException>(() => service.CreateCar(mockCar));
            mockCarRepo.Verify(s => s.CreateCar(It.IsAny<Car>()), Times.Never);
        }

        [Fact]
        public void TestCreateCarCanNotCreateIfTonnageIsLessThanZero()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory {AbsBrakes = false},
                CarSpecs = new CarSpec {Tonnage = -1},
                CarDetails = new CarDetail()
            };

            var mockCarRepo = new Mock<ICarRepository>();
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            //Assert
            Assert.Throws<InvalidDataException>(() => service.CreateCar(mockCar));
            mockCarRepo.Verify(s => s.CreateCar(It.IsAny<Car>()), Times.Never);
        }

        [Fact]
        public void TestCreateCarCanNotCreateIfNewPriceIsLessThanZero()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory {AbsBrakes = false},
                CarSpecs = new CarSpec {NewPrice = -1},
                CarDetails = new CarDetail()
            };

            var mockCarRepo = new Mock<ICarRepository>();
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            //Assert
            Assert.Throws<InvalidDataException>(() => service.CreateCar(mockCar));
            mockCarRepo.Verify(s => s.CreateCar(It.IsAny<Car>()), Times.Never);
        }

        [Fact]
        public void TestCreateCarCanNotCreateIfCostPrSixMonthsIsLessThanZero()
        {
            //setup
            var mockCar = new Car
            {
                CarAccessories = new CarAccessory {AbsBrakes = false},
                CarSpecs = new CarSpec {CostPrSixMonths = -1},
                CarDetails = new CarDetail()
            };

            var mockCarRepo = new Mock<ICarRepository>();
            ICarService service = new CarService(mockCarRepo.Object);

            //Calling method
            //Assert
            Assert.Throws<InvalidDataException>(() => service.CreateCar(mockCar));
            mockCarRepo.Verify(s => s.CreateCar(It.IsAny<Car>()), Times.Never);
        }
    }
}