using Assignment_PRN.Entities;
using Assignment_PRN.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment_PRN.Repositories;

public interface ICountryRepository : IRepositoryBase<Country, int>
{
    IEnumerable<SelectListItem> GetAllCountryOptions();
    IEnumerable<HomeItem> GetAllCountryHomeItems();
}