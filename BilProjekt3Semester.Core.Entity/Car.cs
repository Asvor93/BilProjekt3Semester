namespace BilProjekt3Semester.Core.Entity
{
    public class Car
    {
        public int CarId { get; set; }
        public bool Sold { get; set; }
        public DateTime SoldDate { get; set; }
        public CarAccessories CarAccessories { get; set; }
        public CarDetails CarDetails { get; set; }
        public CarSpecs CarSpecs { get; set; }
    }
}