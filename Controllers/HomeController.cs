using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PokeTCG.Models;
using System.Net.Http.Headers;

namespace PokeTCG.Controllers
{
    // HomeController handles all requests related to displaying and managing Pokémon TCG cards.
    public class HomeController : Controller
    {
        // Private field for database context (SQLite)
        private readonly AppDbContext _context;
        // Private field for making HTTP requests to external APIs
        private readonly HttpClient _httpClient;
        // API Key for the Pokémon TCG API
        private const string apiKey = "5cbfadf5-0974-41f9-b9c5-d4a5bf5cb790";

        // Constructor for HomeController, uses dependency injection for AppDbContext
        public HomeController(AppDbContext context)
        {
            _context = context; // Initialize database context
            _httpClient = new HttpClient(); // Initialize HttpClient
            // Add the API key to the default request headers for all outgoing requests
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
        }

        // Action method for the main index page, displays a list of cards
        // Supports filtering by search term, type, set, series, and rarity
        public async Task<IActionResult> Index(string search, string type, string set, string series, string rarity)
        {
            // Base URL for the Pokémon TCG API cards endpoint, requesting 100 cards per page
            var baseUrl = "https://api.pokemontcg.io/v2/cards?pageSize=100";
            // List to hold query filters
            List<string> filters = new();

            // Add filters if search parameters are provided
            if (!string.IsNullOrEmpty(search))
                filters.Add($"name:{search}");

            if (!string.IsNullOrEmpty(type))
                filters.Add($"types:{type}");

            if (!string.IsNullOrEmpty(set))
                // Note: Set names with spaces need quotes in the API query
                filters.Add($"set.name:\"{set}\"");

            if (!string.IsNullOrEmpty(series))
                // Note: Series names with spaces need quotes in the API query
                filters.Add($"set.series:\"{series}\"");

            if (!string.IsNullOrEmpty(rarity))
                // Note: Rarity names with spaces need quotes in the API query
                filters.Add($"rarity:\"{rarity}\"");

            // If any filters are applied, append them to the base URL
            if (filters.Any())
                baseUrl += $"&q={string.Join(" ", filters)}"; // Join filters with space (API requires space for AND logic)

            // Make API request to get cards
            var cardResponse = await _httpClient.GetAsync(baseUrl);
            // Read the JSON response content
            var cardJson = await cardResponse.Content.ReadAsStringAsync();
            // Deserialize the JSON response into an ApiResponse object
            var root = JsonConvert.DeserializeObject<ApiResponse>(cardJson);

            // Fetch filter options dynamically from the API to populate dropdowns in the view
            var types = await GetFilterValues("https://api.pokemontcg.io/v2/types");
            var sets = await GetSetNames();
            // Extract distinct series names from the fetched sets
            var seriesList = sets.Select(s => s.Series).Distinct().ToList();
            var rarities = await GetFilterValues("https://api.pokemontcg.io/v2/rarities");

            // Pass filter options to the view using ViewBag
            ViewBag.TypeOptions = types;
            ViewBag.SetOptions = sets.Select(s => s.Name).ToList(); // Only pass set names
            ViewBag.SeriesOptions = seriesList;
            ViewBag.RarityOptions = rarities;

            // Return the Index view with the list of cards
            return View(root.Data);
        }

        // Action method to display details of a specific card
        public async Task<IActionResult> Details(string id)
        {
            // Make API request to get details for a specific card ID
            var response = await _httpClient.GetAsync($"https://api.pokemontcg.io/v2/cards/{id}");
            // Read the JSON response content
            var json = await response.Content.ReadAsStringAsync();
            // Deserialize the JSON response into a CardResponse object
            var root = JsonConvert.DeserializeObject<CardResponse>(json);

            // Return the Details view with the card data
            return View(root.Data);
        }

        // Action method to display the list of favorite cards from the database
        public IActionResult Favorites()
        {
            // Retrieve all favorite cards from the SQLite database
            return View(_context.Favorites.ToList());
        }

        // POST action method to add a card to favorites
        // Expects a Favorite object in the request body (JSON)
        [HttpPost]
        public IActionResult AddToFavorites([FromBody] Favorite card)
        {
            // Check if the card is not already in favorites to avoid duplicates
            if (!_context.Favorites.Any(f => f.CardId == card.CardId))
            {
                _context.Favorites.Add(card); // Add the new favorite card to the database context
                _context.SaveChanges(); // Save changes to the database
            }
            return Ok(); // Return a 200 OK status
        }

        // POST action method to remove a card from favorites
        // Expects the favorite card's database ID in the request body (JSON)
        [HttpPost]
        public IActionResult RemoveFavorite([FromBody] int id)
        {
            // Find the favorite card by its database ID
            var card = _context.Favorites.Find(id);
            if (card != null) // Check if the card exists
            {
                _context.Favorites.Remove(card); // Remove the card from the database context
                _context.SaveChanges(); // Save changes to the database
            }
            return Ok(); // Return a 200 OK status
        }

        // Action method to display the type weakness chart
        public IActionResult Chart()
        {
            // Simply returns the Chart view
            return View();
        }

        // Private helper method to fetch generic filter values (types, rarities) from the API
        private async Task<List<string>> GetFilterValues(string url)
        {
            var response = await _httpClient.GetAsync(url); // Make API request
            var json = await response.Content.ReadAsStringAsync(); // Read response content
            // Deserialize response into a FilterResponse object
            var result = JsonConvert.DeserializeObject<FilterResponse>(json);
            return result.Data; // Return the list of filter values
        }

        // Private helper method to fetch set names and series from the API
        private async Task<List<Set>> GetSetNames()
        {
            var response = await _httpClient.GetAsync("https://api.pokemontcg.io/v2/sets"); // Make API request
            var json = await response.Content.ReadAsStringAsync(); // Read response content
            // Deserialize response into a SetListResponse object
            var result = JsonConvert.DeserializeObject<SetListResponse>(json);
            return result.Data; // Return the list of Set objects
        }

        // Nested classes to help deserialize API responses
        // Represents the root structure for a list of cards
        public class ApiResponse { public List<Card> Data { get; set; } }
        // Represents the root structure for a single card
        public class CardResponse { public Card Data { get; set; } }
        // Represents the root structure for lists of string filters (types, rarities)
        public class FilterResponse { public List<string> Data { get; set; } }
        // Represents the root structure for a list of sets
        public class SetListResponse { public List<Set> Data { get; set; } }
    }
}














