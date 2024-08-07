# MovieApp

MovieApp is a web application built with ASP.NET Core that allows users to manage their movie collection and favorite lists. The application provides features for managing movies, genres, and user-specific favorite lists, along with a responsive user interface.

## Features

- **User Authentication:** Register, login, and logout functionality for users.
- **Role-Based Authorization:** Different access levels for admin and regular users.
- **Movie Management:** Add, edit, and delete movie information.
- **Favorite Management:** Create and manage users' favorite movie lists.
- **Genre Management:** Categorize movies by genres.
- **Search Functionality:** Search for movies and favorite lists.
- **Data Export:** Export favorite lists to various formats.
- **Validation:** Client-side and server-side validation for forms.

## Project Structure

- **MovieApp.Core:** Contains core domain entities, DTOs (Data Transfer Objects), enumerations, helpers, services, and service contracts.
  - **Entities:** Domain entities like `Movie`, `Favourite`, `Genre`, `MovieFavourite`.
  - **DTO:** Data Transfer Objects for communication between layers.
  - **Services:** Business logic for managing movies and favorites.
  - **ServiceContracts:** Interfaces for services.
  - **Helper:** Utility classes like `ValidationModel`.

- **MovieApp.Infrastructure:** Contains infrastructure-related classes like application context, configuration, migrations, and repositories.
  - **ApplicationDbContext:** Database context class.
  - **Repositories:** Data access logic for movies and favorites.

- **MovieApp.UI:** The main web application project containing controllers, views, filters, middleware, and other UI-related components.
  - **Controllers:** Handle HTTP requests and return views or data.
  - **Views:** Razor views for displaying data.
  - **wwwroot:** Static files like CSS, JS, and images.

## Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/MovieApp.git
   ```

2. Navigate to the project directory:
   ```sh
   cd MovieApp
   ```

3. Restore the dependencies:
   ```sh
   dotnet restore
   ```

4. Update the database:
   ```sh
   dotnet ef database update --project MovieApp.Infrastructure
   ```

5. Run the application:
   ```sh
   dotnet run --project MovieApp.UI
   ```

## Usage

- **Register a new user:** Go to the `/Account/Register` endpoint and create a new user.
- **Login:** Go to the `/Account/Login` endpoint to login with your registered credentials.
- **Manage Movies:**
  - **Add a new movie:** Navigate to the movies section and click on the "Add Movie" button.
  - **Edit a movie:** Click on the "Edit" button next to a movie in the list.
  - **Delete a movie:** Click on the "Delete" button next to a movie in the list.
- **Manage Favorites:**
  - **Create a new favorite list:** Go to the favorites section and click on the "Create Favorite List" button.
  - **Add movies to favorites:** Open a favorite list and click on the "Add Movie" button.
  - **Remove movies from favorites:** Click on the "Remove" button next to a movie in the favorite list.
- **Export Data:** Import Genre lists from Excel formats.

## Screenshots


## Contributing

We welcome contributions! Please fork the repository and submit pull requests for any features, bug fixes, or improvements.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.

## Contact

For any questions or issues, please open an issue on GitHub or contact the maintainers.
```
