# MovieApp

MovieApp is a web application built with ASP.NET Core that allows users to manage their movie collection and favorite lists. The application provides features for managing movies, genres, and user-specific favorite lists, along with a responsive user interface.

## Features

- **User Authentication:** Register, login, and logout functionality for users.
- **Role-Based Authorization:** Different access levels for admins and regular users.
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
- **Import Data:** Import Genre lists from Excel formats.

## Screenshots

- **Unauthenticated View:** The landing page for users who are not logged in.
  ![Screenshot 2024-08-07 143801](https://github.com/user-attachments/assets/35915166-aa2b-4c11-a281-80aefa8ad940)
- **Register Page:** The registration page for new users to sign up.
  ![Screenshot 2024-08-07 143955](https://github.com/user-attachments/assets/cb04c867-d8d6-4b1d-a106-1203e30761fc)
- **Login Page:** The login page for users to access their accounts.
  ![image](https://github.com/user-attachments/assets/9089eb5a-7867-4cb0-90b0-e1ba1aab740e)
- **Admin View:** Admin interface for managing the application.
  ![image](https://github.com/user-attachments/assets/6a77d3b0-c46f-4e75-aef5-44862080051a)
- **Admin Dashboard:** Dashboard showing various administrative controls.
  ![image](https://github.com/user-attachments/assets/0281cc68-5745-4a8b-8063-d57b169080a9)
- **Import Genre Type from Excel File:** Interface for importing genres from an Excel file.
  ![image](https://github.com/user-attachments/assets/08621482-0bfe-4eca-90e5-69da81ef6142)
- **Add New User by Admin with Role:** Admin page for adding a new user with a specific role.
  ![image](https://github.com/user-attachments/assets/7a5a28b6-40c1-40d0-91c1-3d5e8d4f1894)
- **Create New Movie by Admin:** Interface for admins to create a new movie entry.
  ![image](https://github.com/user-attachments/assets/c353adf7-fc4a-4384-a6d5-f38de32b51ed)
- **Edit Existing Movie by Admin:** Interface for admins to edit existing movie details.
  ![image](https://github.com/user-attachments/assets/c774ede1-a306-4365-9a70-6031e6526938)
- **User View:** The main interface for regular users.
  ![image](https://github.com/user-attachments/assets/543e2a69-09ef-42cc-9551-3ce44249f901)
- **Favorite Page for User:** The interface where users can manage their favorite movies.
  ![image](https://github.com/user-attachments/assets/629bc355-6474-47f2-be6c-0bc7cf3b926f)
- **Details for Movie:** Detailed view of a movie's information.
  ![image](https://github.com/user-attachments/assets/e21cdb53-6bb5-4845-8809-d6306eac3040)

---
