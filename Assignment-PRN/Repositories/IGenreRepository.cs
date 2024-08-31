using Assignment_PRN.Entities;
using Assignment_PRN.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment_PRN.Repositories;

public interface IGenreRepository : IRepositoryBase<Genre, int>
{
    IEnumerable<SelectListItem> GetAllGenresHomeModel();
    public IEnumerable<HomeItem> GetAllGenreHomeItems();
    public IEnumerable<Genre> GetGenreByIds(IEnumerable<int> ids);
}