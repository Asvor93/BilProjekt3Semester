namespace BilProjekt3Semester.Core.Entity
{
    public class Filter
    {
        public int CurrentPage { get; set; }
        public int ItemsPrPage { get; set; }
        public string SearchBrandNameQuery { get; set; }
        public int minPrice { get; set; }
        public int maxPrice { get; set; }
    }
}