using Assignment_PRN.Data;
using Assignment_PRN.Interfaces.Services;
using Assignment_PRN.Repositories.Implementation;
using Assignment_PRN.Repositories;
using Assignment_PRN.Serivices.Implementation;
using Assignment_PRN.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Assignment_PRN.Data.Implementation;
using Assignment_PRN.Services.Implementation;

namespace Assignment_PRN.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.RegisterData();
            services.RegisterApplicationServices();
        }

        private static void RegisterData(this IServiceCollection services)
        {
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IEpisodeRepository, EpisodeRepository>();
            services.AddScoped<IFilmRepository, FilmRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IStorageService, CloudinaryService>();
            services.AddScoped<IPaymentService, VnPayService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IFavoriteService, FavoriteService>();
            services.AddScoped<IBalanceService, BalanceService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
