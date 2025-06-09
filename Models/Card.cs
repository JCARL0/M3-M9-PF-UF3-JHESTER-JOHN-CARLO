using System.Collections.Generic; // Required for List<T>

namespace PokeTCG.Models
{
    // Represents a Pokémon TCG card as returned by the API.
    // This class is used for deserializing API responses.
    public class Card
    {
        public string Id { get; set; } // Unique ID of the card
        public string Name { get; set; } // Name of the card
        public List<string> Types { get; set; } // List of types (e.g., "Grass", "Fire")
        public List<string> Subtypes { get; set; } // List of subtypes (e.g., "Basic", "VMAX")
        public string Supertype { get; set; } // Supertype of the card (e.g., "Pokémon", "Trainer")
        public string Rarity { get; set; } // Rarity of the card (e.g., "Common", "Rare Holo")
        public Set Set { get; set; } // The set the card belongs to
        public string Series => Set?.Series; // Convenience property to get the series from the Set object (null-safe)
        public List<Attack> Attacks { get; set; } // List of attacks the Pokémon has
        public Images Images { get; set; } // URLs for card images
    }

    // Represents a Pokémon TCG set.
    public class Set
    {
        public string Name { get; set; } // Name of the set (e.g., "Base Set")
        public string Series { get; set; } // Series the set belongs to (e.g., "Sword & Shield")
    }

    // Represents an attack a Pokémon card can have.
    public class Attack
    {
        public string Name { get; set; } // Name of the attack
        public string Text { get; set; } // Description of the attack
        public string Damage { get; set; } // Damage value of the attack (can be text like "20+")
    }

    // Represents the image URLs for a Pokémon TCG card.
    public class Images
    {
        public string Small { get; set; } // URL for a small version of the card image
        public string Large { get; set; } // URL for a large version of the card image
    }
}