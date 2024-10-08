﻿
using Assignment_PRN.Models;
using Assignment_PRN.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_PRN.Areas.Admin.Pages.Catalog;

public class Delete : PageModel
{
    private readonly IFilmService _filmService;

    [BindProperty] public FilmItemDetail Film { get; set; } = default!;
    
    public Delete(IFilmService filmService)
    {
        _filmService = filmService;
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var film = await _filmService.GetFilmDetail(id.Value);
        if (film == null)
        {
            return NotFound();
        }
        Film = film;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var result = await _filmService.DeleteFilm(id.Value);
        if (!result)
        {
            return Page();
        }
        return RedirectToPage("./Index");
    }
}