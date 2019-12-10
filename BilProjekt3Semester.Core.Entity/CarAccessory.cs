using System.Reflection.Metadata;

namespace BilProjekt3Semester.Core.Entity
{
    public class CarAccessory
    {
        public int CarAccessoryId { get; set; }
        public int? NrOfAirbags { get; set; }
        public bool? AirCondition { get; set; }
        public bool? AbsBrakes { get; set; }
        public string TowBar { get; set; }
        public bool? AluRims { get; set; }
        public bool? ElectricSideMirror { get; set; }
        public bool? ElectricWindows { get; set; }
        public bool? CentralLock { get; set; }
        public bool? RemoteCentralLock { get; set; }
        public bool? CruiseControl { get; set; }
        //KlimaAnlæg
        public bool? AirCon { get; set; }
        public bool? RadioWithCd { get; set; }
        public bool? HeatedSeats { get; set; }
        public bool? FogLights { get; set; }
        public bool? SplitBackSeat { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }

    }
}