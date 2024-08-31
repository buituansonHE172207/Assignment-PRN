using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment_PRN.Entities;

public class Rating
{
    public int Id { get; set; }
    public int FilmId { get; set; }
    [StringLength(450)] public string UserId { get; set; } = null!;
    public int Rate { get; set; }
    [ForeignKey(nameof(FilmId))]
    public virtual Film Film { get; set; } = null!;
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;
}