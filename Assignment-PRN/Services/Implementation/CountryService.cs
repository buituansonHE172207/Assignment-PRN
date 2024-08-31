
using Assignment_PRN.Data;
using Assignment_PRN.Models;
using Assignment_PRN.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment_PRN.Serivices.Implementation;

public class CountryService : ICountryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CountryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<SelectListItem> GetCountryOptionsList()
    {
        return _unitOfWork.Countries.GetAllCountryOptions();
    }

    public IEnumerable<HomeItem> GetCountryHomeItems()
    {
        return _unitOfWork.Countries.GetAllCountryHomeItems();
    }
}