using Assignment_PRN.Entities;
using Assignment_PRN.Enums;
using Assignment_PRN.Models;
using Assignment_PRN.Pages.Film;
using Assignment_PRN.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment_PRN.Areas.Identity.Pages.Account.Manage
{
    public class FavoritesModel : PageModel
    {
        private readonly ICountryService _countryService;
        private readonly IFavoriteService _favoriteService;
        private readonly IGenreService _genreService;
        private readonly ILogger<FilmIndexModel> _logger;
        private readonly UserManager<User> _userManager;

        public FavoritesModel(ICountryService countryService, IFavoriteService favoriteService, IGenreService genreService,
            ILogger<FilmIndexModel> logger, UserManager<User> userManager)
        {
            _countryService = countryService;
            _favoriteService = favoriteService;
            _genreService = genreService;
            _logger = logger;
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)] public string? Search { get; set; }
        [BindProperty(SupportsGet = true)] public int? Genre { get; set; }
        [BindProperty(SupportsGet = true)] public int? Country { get; set; }
        [BindProperty(SupportsGet = true)] public string? Sort { get; set; } = "CreatedAt DESC";
        [BindProperty(SupportsGet = true)] public int? PageNo { get; set; }
        [BindProperty(SupportsGet = true)] public FilmType? Type { get; set; }
        public IEnumerable<SelectListItem> GenreOptions { get; set; } = null!;
        public IEnumerable<SelectListItem> CountryOptions { get; set; } = null!;

        public IEnumerable<SelectListItem> SortOptions { get; set; } = new List<SelectListItem>
    {
        new("Latest Update", "CreatedAt DESC"),
        new("New release", "ReleaseYear DESC, CreatedAt DESC"),
        new("Most Popular", "TotalView DESC, CreatedAt DESC"),
        new("Highest Rated", "AverageRating DESC, CreatedAt DESC")
    };

        public IEnumerable<SelectListItem> TypeOptions { get; set; } = new List<SelectListItem>
    {
        new("Movie and TV Series", ""),
        new("Movie", FilmType.Movie.ToString()),
        new("TV Series", FilmType.Series.ToString())
    };

        public Page<FilmItemList> Films { get; set; } = null!;

        public void OnGet()
        {
            GenreOptions = _genreService.GetGenresHomeList();
            CountryOptions = _countryService.GetCountryOptionsList();
            var user = _userManager.GetUserAsync(User).Result;
            var userId = _userManager.GetUserIdAsync(user).Result;
            Films = _favoriteService.GetFilmFavoriteList(Search, userId, Genre, Country, Type, Sort, PageNo);
            _logger.LogInformation(Films.ToString());
        }
    }
}
