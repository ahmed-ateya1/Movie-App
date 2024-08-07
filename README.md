# MovieApp

**MovieApp** is a web application designed to manage a collection of movies and users' favorite lists. The application is built using ASP.NET Core and follows a layered architecture, ensuring separation of concerns and maintainability.

## Features

- Manage Movies: Add, update, and delete movie information.
- User Favorites: Create and manage users' favorite movie lists.
- Genre Management: Categorize movies by genres.
- Responsive UI: User-friendly web interface for easy navigation and interaction.

## Project Structure

The project is divided into three main layers:

1. **MovieApp.Core**: Contains the core business logic and domain entities.
2. **MovieApp.Infrastructure**: Manages data access and database interactions.
3. **MovieApp.UI**: The presentation layer with the web interface.

## Getting Started

1. **Clone the Repository**

   ```sh
   git clone https://github.com/yourusername/MovieApp.git
   cd MovieApp
   ```

2. **Setup Database**

   Configure your database connection string in `appsettings.json` and run the following command to apply migrations:

   ```sh
   dotnet ef database update
   ```

3. **Run the Application**

   ```sh
   dotnet run
   ```

4. **Access the Application**

   Open your browser and navigate to `http://localhost:5000`.

## Contributing

We welcome contributions! Please fork the repository and submit pull requests for any features, bug fixes, or improvements.
