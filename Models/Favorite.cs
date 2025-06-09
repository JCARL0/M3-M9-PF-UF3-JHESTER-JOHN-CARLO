using System.ComponentModel.DataAnnotations; // Required for [Key] attribute

namespace PokeTCG.Models
{
    // Represents a favorite Pokémon TCG card stored in the local SQLite database.
    public class Favorite
    {
        [Key] // Denotes Id as the primary key in the database table
        public int Id { get; set; } // Primary key for the Favorite entry in the database
        public string CardId { get; set; } // The original ID of the card from the Pokémon TCG API
        public string Name { get; set; } // Name of the favorite card
        public string ImageUrl { get; set; } // URL of the card's image
        public string Rarity { get; set; } // Rarity of the card
        public string Type { get; set; } // Primary type of the card
    }
}