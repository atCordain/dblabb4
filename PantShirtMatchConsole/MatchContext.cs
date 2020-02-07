using Microsoft.EntityFrameworkCore;

namespace PantShirtMatchConsole
{
    public class MatchContext : DbContext
    {
        public DbSet<Shirt> Shirts { get; set; }

        public DbSet<Pant> Pants { get; set; }

        public DbSet<Look> Looks { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<RatingCategory> RatingCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                   "https://localhost:8081",
                   "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                   databaseName: "matchdb");
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}