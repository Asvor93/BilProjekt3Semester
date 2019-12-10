namespace BilProjekt3Semester.Core.Entity
{
    public class CarDetail
    {
        public int CarDetailId { get; set; }
        public string BrandName { get; set; }
        public int? Kilometer { get; set; }
        public int? Year { get; set; }
        public bool? NumberPlates { get; set; }
        public int? Doors { get; set; }
        public double? KmPrLiter { get; set; }
        public string Fuel { get; set; }
        public double? TopSpeed { get; set; }
        public string ConditionOfProduct { get; set; }
        public string Variant { get; set; }
        public string Color { get; set; }
        public bool? ServiceBook { get; set; }
        public int? HorsePower { get; set; }
        public double? MotorSize { get; set; }
        public int? Torque { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}