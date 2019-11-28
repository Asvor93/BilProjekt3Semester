using System.Reflection.Metadata;

namespace BilProjekt3Semester.Core.Entity
{
    public class CarAccessory
    {
        public int CarAccessoryId { get; set; }
        public int AntalAirbags { get; set; }
        public bool AirCondition { get; set; }
        public bool AbsBremser { get; set; }
        public string Anhængertræk { get; set; }
        public bool AluFælge { get; set; }
        public bool ElSideSpejle { get; set; }
        public bool ElRuder { get; set; }
        public bool CentralLås { get; set; }
        public bool FjernbCentralLås { get; set; }
        public bool FartPilot { get; set; }
        public bool KlimaAnlæg { get; set; }
        public bool RadioMedCd { get; set; }
        public bool VarmeISæder { get; set; }
        public bool TågeLygter { get; set; }
        public bool SplitBagsæde { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }

    }
}