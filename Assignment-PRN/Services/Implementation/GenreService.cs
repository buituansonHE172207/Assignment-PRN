using Assignment_PRN.Data;
using Assignment_PRN.Models;
using Assignment_PRN.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment_PRN.Serivices.Implementation;

public class GenreService : IGenreService
{
    private readonly IUnitOfWork _unitOfWork;

    public GenreService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<SelectListItem> GetGenresHomeList()
    {
        return _unitOfWork.Genres.GetAllGenresHomeModel();
    }

    public IEnumerable<HomeItem> GetGenreHomeItems()
    {
        return _unitOfWork.Genres.GetAllGenreHomeItems();
    }
}