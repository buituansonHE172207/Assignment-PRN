using Assignment_PRN.Entities;
using Assignment_PRN.Enums;
using Microsoft.AspNetCore.Http;

namespace Assignment_PRN.Models;

public class FilmItemCreate
{
    public string Title { get; set; } = null!;
    public string? OtherTitle { get; set; }
    public string? Description { get; set; }
    public IFormFile? VideoFile { get; set; }
    public string? TrailerUrl { get; set; }
    public IFormFile? ThumbnailFile { get; set; }
    public IFormFile? PosterFile { get; set; }
    public int? Duration { get; set; }
    public decimal? AverageRating { get; set; }
    public int? TotalEpisode { get; set; }
    public int? DurationPerEpisode { get; set; }
    public FilmType Type { get; set; }
    public string? Actor { get; set; }
    public string? Director { get; set; }
    public int? TotalView { get; set; } = 0;
    public int? ReleaseYear { get; set; }
    public int? CountryId { get; set; }
    public ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();
}