using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Domain.IdentityEntites;
using MovieApp.Core.Domain.RepositoryContracts;
using MovieApp.Core.Services;
using MovieApp.Core.ServicesContracts;
using MovieApp.Infrastructure.ApplicationDbContext;
using MovieApp.Infrastructure.Repositories;
using MovieApp.UI.FileServices;

namespace MovieApp.UI.StartupExtensions
{
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection ServiceConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with SQL Server
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("connstr"));
            });

            // Configure Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 5;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, AppDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, AppDbContext, Guid>>();
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser().Build();    
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LogoutPath = "/Account/Login";
            });
            // Add MVC services
            services.AddControllersWithViews();

            // Register application services and repositories
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreServices, GenreServices>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieServices, MovieServices>();
            services.AddScoped<IFileServices, FileService>();
            services.AddScoped<IFavouriteRepository, FavouriteRepository>();
            services.AddScoped<IFavouriteServices, FavouriteServices>();
            services.AddScoped<IMovieFavouriteRepository, MovieFavouriteRepository>();
            services.AddScoped<IMovieFavouriteServices, MovieFavouriteServices>();
            return services;
        }
    }
}
