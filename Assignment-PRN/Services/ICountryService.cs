using Assignment_PRN.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment_PRN.Services;

public interface ICountryService
{
    IEnumerable<SelectListItem> GetCountryOptionsList();
    IEnumerable<HomeItem> GetCountryHomeItems();
}