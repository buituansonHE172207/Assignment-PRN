
using Assignment_PRN.Data;
using Assignment_PRN.Entities;
using Assignment_PRN.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Assignment_PRN.Repositories.Implementation;

public class FilmRepository : RepositoryBase<Film, int>, IFilmRepository
{
    private readonly AutoMapper.IConfigurationProvider _mapper;

    public FilmRepository(AssignmentPRNContext appDbContext, AutoMapper.IConfigurationProvider mapper) : base(appDbContext)
    {
        _mapper = mapper;
    }

    public Page<FilmItemList> GetFilmList(PageRequest<Film> pageRequest, Expression<Func<Film, bool>>? predicate)
    {
        var skip = (pageRequest.PageNumber - 1) * pageRequest.Size;
        var rawData = DbSet
            .Include(f => f.Genres)
            .Where(predicate ?? (_ => true));
        var totalElement = rawData.Count();
        var data = rawData
            .OrderBy(pageRequest.Sort ?? "Id desc")
            .Skip(skip).Take(pageRequest.Size)
            .ProjectTo<FilmItemList>(_mapper);
        return new Page<FilmItemList>
        {
            PageNumber = pageRequest.PageNumber,
            PageSize = pageRequest.Size,
            TotalElement = totalElement,
            Data = totalElement == 0 ? new List<FilmItemList>() : data.ToList(),
            Sort = pageRequest.Sort
        };
    }

    public bool ExistsById(int id)
    {
        return DbSet.Any(f => f.Id == id);
    }

    public void AddFilmToFavoriteList(User user, Film film)
    {
        user.FavoriteFilms.Add(film);
    }

    public void RemoveFilmFromFavoriteList(User user, Film film)
    {
        user.FavoriteFilms.Remove(film);
    }

    public Task<FilmItemDetail?> GetFilmDetail(int id)
    {
        return DbSet
            .Include(f => f.Genres)
            .Include(f => f.Country)
            .Include(f => f.Episodes)
            .Where(f => f.Id == id)
            .ProjectTo<FilmItemDetail>(_mapper)
            .FirstOrDefaultAsync();
    }

    public void DeleteFilm(Film film)
    {
        DbSet.Remove(film);
    }

    public Film? GetFilmByIdWithGenresAndCountry(int id)
    {
        return DbSet.Include(f => f.Genres)
            .Include(f => f.Country)
            .FirstOrDefault(f => f.Id == id);
    }

    public Task ToggleFavoriteFilm(string userId, int filmId)
    {
        var film = DbSet.Include(f => f.Followers).FirstOrDefault(f => f.Id == filmId);
        var user = DbContext.Users.FirstOrDefault(u => u.Id == userId)!;
        if (film!.Followers.Any(u => u.Id == userId))
            film.Followers.Remove(user);
        else
            film.Followers.Add(user);
        DbSet.Update(film);
        return Task.CompletedTask;
    }

    public Task<bool> IsFavoriteFilm(int existFilmId, string userId)
    {
        return DbSet.AnyAsync(f => f.Id == existFilmId && f.Followers.Any(u => u.Id == userId));
    }
}