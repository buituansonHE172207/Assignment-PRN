﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN.Entities;

public class Genre
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [StringLength(50)] [Unicode] public string Name { get; set; } = null!;
    [StringLength(255)] public string Image { get; set; } = null!;
    public virtual ICollection<Film> Films { get; set; } = new HashSet<Film>();
}