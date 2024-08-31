using Assignment_PRN.Enums;
using Assignment_PRN.Models;

namespace Assignment_PRN.Services;

public interface IFilmService
{
    Page<FilmItemList> GetLatestFilm();
    Page<FilmItemList> GetPopularFilm();
    Page<FilmItemList> GetFeatureFilm();
    Page<FilmItemList> GetFilmList(string? search, int? genre, int? country, FilmType? type, string? sort, int? pageNo);
    Task<FilmItemDetail?> GetFilmDetail(int id);
    Task<FilmItemCreate> AddFilm(FilmItemCreate film);
    Task<FilmItemUpdate?> UpdateFilm(FilmItemUpdate film);
    Task<bool> DeleteFilm(int id);
    Task<FilmItemDetail?> GetFilmDetailWithURL(int id);
}