using Assignment_PRN.Entities;
using Assignment_PRN.Enums;
using Assignment_PRN.Models;

namespace Assignment_PRN.Services;

public interface IFavoriteService
{
    Page<FilmItemList> GetFilmFavoriteList(string? search, string userId, int? genre, int? country, FilmType? type, string? sort, int? pageNo);
    void AddFilmToFavoriteList(User user, int filmId);
    void RemoveFilmFromFavoriteList(User user, int filmId);
    Task ToggleFavoriteFilm(int id, string userId);
    Task<bool> IsFavoriteFilm(FilmItemDetail existFilm, string userId);
}