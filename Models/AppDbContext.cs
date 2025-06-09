using Microsoft.EntityFrameworkCore; // Required for DbContext
using System.Collections.Generic; // Required for List

namespace PokeTCG.Models
{
    // AppDbContext is the Entity Framework Core DbContext for the application.
    // It manages the database connection and configuration, and provides access to database entities.
    public class AppDbContext : DbContext
    {
        // Constructor for AppDbContext, takes DbContextOptions to configure the database connection.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet for the Favorite entity.
        // This property represents the collection of Favorite objects that can be queried from the database.
        public DbSet<Favorite> Favorites { get; set; }

        // Note: The Card, Set, Attack, and Images classes are defined in Card.cs and are used for API data mapping,
        // not directly as DbSet entities in this database context (since they are fetched from an external API).
    }
}