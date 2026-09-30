using Microsoft.EntityFrameworkCore;

namespace AspCoreWithEF
{
    public class AppUserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppUserContext(DbContextOptions<AppUserContext> options) : base(options)
        { 
            Database.EnsureCreated(); //создание БД
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Tom", Age = 20 },
                new User { Id = 2, Name = "Bob", Age = 21 },
                new User { Id = 3, Name = "Sam", Age = 22 }
            );
        }

    }
}
