using System.Linq.Expressions;
using Assignment_PRN.Entities;
using Assignment_PRN.Models;

namespace Assignment_PRN.Repositories;

public interface IFilmRepository : IRepositoryBase<Film, int>
{
    Page<FilmItemList> GetFilmList(PageRequest<Film> pageRequest, Expression<Func<Film, bool>>? predicate);
    bool ExistsById(int id);
    void AddFilmToFavoriteList(User user, Film film);
    void RemoveFilmFromFavoriteList(User user, Film film);
    Task<FilmItemDetail?> GetFilmDetail(int id);
    void DeleteFilm(Film film);
    Film? GetFilmByIdWithGenresAndCountry(int id);
    Task ToggleFavoriteFilm(string userId, int filmId);
    Task<bool> IsFavoriteFilm(int existFilmId, string userId);
}