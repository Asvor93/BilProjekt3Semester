namespace BilProjekt3Semester.Core.Entity
{
    public class CarDetails
    {
        public string Mærke { get; set; }
        public int Kilometer { get; set; }
        public int Årgang { get; set; }
        public bool Plader { get; set; }
        public int Døre { get; set; }
        public double KmPrLiter { get; set; }
        public string Brændstof { get; set; }
        public double TopHastighed { get; set; }
        public string VarensStand { get; set; }
        public string Variant { get; set; }
        public string Farve { get; set; }
        public bool ServiceBog { get; set; }
        public int HK { get; set; }
        public double Motorstørrelse { get; set; }
        public int Moment { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}