using System;
using System.Collections.Generic;

namespace BilProjekt3Semester.Core.Entity
{
    public class Car
    {
        public bool CarOnFrontPage { get; set; }
        public int CarId { get; set; }
        public double Price { get; set; }
        public bool Sold { get; set; }
        public DateTime SoldDate { get; set; }
        public string ThumbnailLink { get; set; }
        public List<CarPictureLink> PictureLinks { get; set; }
        public CarAccessory CarAccessories { get; set; }
        public CarDetail CarDetails { get; set; }
        public CarSpec CarSpecs { get; set; }
        public string Description { get; set; }
    }
}