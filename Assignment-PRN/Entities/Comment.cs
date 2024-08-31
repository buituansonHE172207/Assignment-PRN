using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN.Entities;

public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [StringLength(450)] public string UserId { get; set; } = null!;
    public int FilmId { get; set; }
    [StringLength(int.MaxValue)] [Unicode] public string Content { get; set; } = null!;
    public DateTime Time { get; set; } = DateTime.UtcNow;
    [ForeignKey(nameof(FilmId))]
    public virtual Film Film { get; set; } = null!;
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;
}