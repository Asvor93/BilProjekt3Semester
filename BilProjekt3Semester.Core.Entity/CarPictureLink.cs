using System.Reflection.Metadata;

namespace BilProjekt3Semester.Core.Entity
{
    public class CarPictureLink
    {
        public int CarPictureLinkId { get; set; }
        public string PictureLink { get; set; }

        public Car Car { get; set; }
        public int CarId { get; set; }
    }
}