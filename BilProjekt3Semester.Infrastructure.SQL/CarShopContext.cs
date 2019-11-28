using BilProjekt3Semester.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BilProjekt3Semester.Infrastructure.SQL
{
    public class CarShopContext : DbContext
    {
        public CarShopContext(DbContextOptions opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarPictureLink>()
                .HasOne(pl => pl.Car)
                .WithMany(c => c.PictureLinks)
                .HasForeignKey(pl => pl.CarId);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.CarAccessories)
                .WithOne(ca => ca.Car)
                .HasForeignKey<CarAccessory>(ca => ca.CarId);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.CarSpecs)
                .WithOne(cs => cs.Car)
                .HasForeignKey<CarSpec>(cs => cs.CarId);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.CarDetails)
                .WithOne(cd => cd.Car)
                .HasForeignKey<CarDetail>(cd => cd.CarId);

            modelBuilder.Entity<CarAccessory>().HasKey(ca => ca.CarAccessoryId);

            modelBuilder.Entity<User>()
                .HasKey(us => us.Id);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarAccessory> CarAccessories { get; set; }
        public DbSet<CarDetail> CarDetails { get; set; }
        public DbSet<CarSpec> CarSpecs { get; set; }
        public DbSet<CarPictureLink> CarPictureLinks { get; set; }
    }

    
}