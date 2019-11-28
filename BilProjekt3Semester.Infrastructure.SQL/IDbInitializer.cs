namespace BilProjekt3Semester.Infrastructure.SQL
{
    public interface IDbInitializer
    {
        void SeedDb(CarShopContext context);
    }
}