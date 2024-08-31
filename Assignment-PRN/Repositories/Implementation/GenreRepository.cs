using Assignment_PRN.Data;
using Assignment_PRN.Entities;
using Assignment_PRN.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment_PRN.Repositories.Implementation;

public class GenreRepository : RepositoryBase<Genre, int>, IGenreRepository
{
    public GenreRepository(AssignmentPRNContext appDbContext) : base(appDbContext)
    {
    }

    public IEnumerable<SelectListItem> GetAllGenresHomeModel()
    {
        return DbSet.Select(g => new SelectListItem
        {
            Value = g.Id.ToString(),
            Text = g.Name
        }).ToList();
    }

    public IEnumerable<HomeItem> GetAllGenreHomeItems()
    {
        return DbSet.Select(g => new HomeItem
        {
            Id = g.Id,
            Name = g.Name,
            Image = g.Image,
            TotalItem = g.Films.Count
        }).ToList();
    }

    public IEnumerable<Genre> GetGenreByIds(IEnumerable<int> ids)
    {
        return DbSet.Where(g => ids.Contains(g.Id)).ToList();
    }
}