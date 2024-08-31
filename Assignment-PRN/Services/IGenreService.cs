using Assignment_PRN.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment_PRN.Services;

public interface IGenreService
{
    IEnumerable<SelectListItem> GetGenresHomeList();
    IEnumerable<HomeItem> GetGenreHomeItems();
}