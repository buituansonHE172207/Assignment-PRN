
using Assignment_PRN.Data;
using Assignment_PRN.Entities;
using Assignment_PRN.Enums;
using Assignment_PRN.Models;
using Assignment_PRN.Services;
using System.Linq.Expressions;

namespace Assignment_PRN.Serivices.Implementation;

public class FavoriteService : IFavoriteService
{
    private readonly IUnitOfWork _unitOfWork;
    private const int HomeFilmListSize = 18;
    private readonly IStorageService _storageService;

    public FavoriteService(IUnitOfWork unitOfWork, IStorageService storageService)
    {
        _unitOfWork = unitOfWork;
        _storageService = storageService;
    }

    public Page<FilmItemList> GetFilmFavoriteList(string? search, string? userId, int? genre, int? country, FilmType? type, string? sort,
        int? pageNo)
    {
        var pageRequest = new PageRequest<Film>
        {
            PageNumber = pageNo ?? 1,
            Size = HomeFilmListSize,
            Sort = sort
        };
        Expression<Func<Film, bool>> predicate = f =>
            (
                string.IsNullOrEmpty(search)
                || f.Title.Contains(search)
                || (!string.IsNullOrEmpty(f.OtherTitle) && f.OtherTitle.Contains(search))
                || (!string.IsNullOrEmpty(f.Actor) && f.Actor.Contains(search))
                || (!string.IsNullOrEmpty(f.Director) && f.Director.Contains(search))
                
            )
            && (!genre.HasValue || f.Genres.Any(g => g.Id == genre))
            && (!country.HasValue || f.CountryId == country)
            && (!type.HasValue || f.Type == type)
            && (!string.IsNullOrEmpty(userId) && f.Followers.Any(u => u.Id == userId));
        var films = _unitOfWork.Films.GetFilmList(pageRequest, predicate);
        foreach (var film in films.Data)
        {
            film.PosterUrl = _storageService.GetImageUrl(film.PosterUrl).Result;
        }
        return films;
    }

    public void AddFilmToFavoriteList(User user, int filmId)
    {
        var film = _unitOfWork.Films.GetById(filmId) ?? throw new Exception("Film not found");
        _unitOfWork.Films.AddFilmToFavoriteList(user, film);
        _unitOfWork.Commit();
    }

    public void RemoveFilmFromFavoriteList(User user, int filmId)
    {
        var film = _unitOfWork.Films.GetById(filmId) ?? throw new Exception("Film not found");
        _unitOfWork.Films.RemoveFilmFromFavoriteList(user, film);
        _unitOfWork.Commit();
    }

    public async Task ToggleFavoriteFilm(int id, string userId)
    {
        await _unitOfWork.Films.ToggleFavoriteFilm(userId, id);
        await _unitOfWork.CommitAsync();
    }

    public Task<bool> IsFavoriteFilm(FilmItemDetail existFilm, string userId)
    {
        return _unitOfWork.Films.IsFavoriteFilm(existFilm.Id, userId);
    }
}