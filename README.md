# Pokémon TCG Explorer

A Pokedex-style web app for exploring and tracking Pokémon Trading Card Game cards.

---

## 🚀 Getting Started

1. Open your terminal in the project’s root folder.  
2. Run the application:

    ```bash
    dotnet run
    ```

3. Once it’s running, open your web browser and navigate to:

    ```text
    http://localhost:5259
    ```

> ⚠️ The specific port may differ depending on your environment.

---

## 🎯 Motivation

Pokemon has been a huge part of our lives—from classic video games to the fast-paced Trading Card Game. This project is our way of building a “Pokedex-like” app specifically for the Pokemon TCG, letting you explore and track your favorite cards with ease.

---

## 🏗️ Architecture Overview

This project follows the **MVC (Model-View-Controller)** design pattern, implemented with .NET Core.

- **Models**  
  Represent Pokémon TCG card data, used to communicate with external APIs and our local SQLite database.

- **Views**  
  Built with .cshtml Razor Pages, styled with custom CSS for a clean, modern look.

- **Controllers**  
  Serve as intermediaries between Models and Views; handle requests, retrieve data, and send it to the view layer.

---

## 🔍 MVC Breakdown

### 📂 Controllers

- **HomeController**  
  - Search and filter cards  
  - View card details  
  - Manage favorite cards  
  - Connects to external Web APIs and the local SQLite database via Entity Framework Core

### 📂 Models

- **External Models**  
  - `Card`, `Images`, etc. for data fetched from the Pokémon TCG API

- **Local Models**  
  - `FavoriteCard` for items saved by the user (Entity Framework Core)

### 📂 Views

- Razor views for:  
  - Card lists  
  - Search results  
  - Card detail pages  
  - User favorites

---

## 🗄️ Database

- Local storage via SQLite  
- Managed with Entity Framework Core  
- Automatically created/migrated on first run

---

## 🔁 Serialization

- Uses **Newtonsoft.Json** for smooth JSON handling when communicating with external APIs.

---

## 💡 Future Ideas & Improvements

- **User Accounts**: Allow users to create accounts and save their favorites.  
- **Advanced Filters**: Add more detailed search and sorting options (by rarity, expansion, HP, etc.).  
- **Mobile Optimization**: Improve usability and layout on phones and tablets.