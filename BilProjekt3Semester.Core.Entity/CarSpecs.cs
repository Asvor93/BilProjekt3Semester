using System.Reflection.Metadata;

namespace BilProjekt3Semester.Core.Entity
{
    public class CarSpecs
    {
        public string Type { get; set; }
        public bool Servo { get; set; }
        public int  Gear { get; set; }
        public double AfgiftPrHalvår { get; set; }
        public int Nypris { get; set; }
        public int LasteEvne {get; set;}
        public int Tank { get; set; }
        public int Ventiler { get; set; }
        public int Cylinder { get; set; }
        public int Længde { get; set; }
        public int Bredde { get; set; }
        public double Maxvægt { get; set; }
        public double MaxPåhængsVægt {get; set;}
        public int Vægt { get; set; }


    }
}