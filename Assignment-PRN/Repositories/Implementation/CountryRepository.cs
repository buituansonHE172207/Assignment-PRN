
using Assignment_PRN.Data;
using Assignment_PRN.Entities;
using Assignment_PRN.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment_PRN.Repositories.Implementation;

public class CountryRepository : RepositoryBase<Country, int>, ICountryRepository
{
    public CountryRepository(AssignmentPRNContext appDbContext) : base(appDbContext)
    {
    }

    public IEnumerable<SelectListItem> GetAllCountryOptions()
    {
        return DbSet.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();
    }

    public IEnumerable<HomeItem> GetAllCountryHomeItems()
    {
        return DbSet.Select(c => new HomeItem
        {
            Id = c.Id,
            Name = c.Name,
            Image = c.Image,
            TotalItem = c.Films.Count
        }).ToList();
    }
}