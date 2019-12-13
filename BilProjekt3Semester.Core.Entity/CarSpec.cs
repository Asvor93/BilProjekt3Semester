using System.Reflection.Metadata;

namespace BilProjekt3Semester.Core.Entity
{
    public class CarSpec
    {
        public int CarSpecId { get; set; }
        public string Type { get; set; }
        public bool PowerSteering { get; set; }
        public int Gear { get; set; }
        public double CostPrSixMonths { get; set; }
        public int NewPrice { get; set; }
        public int Tonnage { get; set; }
        public int Tank { get; set; }
        public int Valves { get; set; }
        public int Cylinder { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public double MaxWeight { get; set; }
        public double MaxTrailerWeight { get; set; }
        public int Weight { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
    }
}