using Assignment_PRN.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Assignment_PRN.Data
{
    public class AssignmentPRNContext : IdentityDbContext<User>
    {
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Episode> Episodes { get; set; } = null!;
        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;

        public AssignmentPRNContext(DbContextOptions<AssignmentPRNContext> options) : base(options)
        {
        }
     
    }
}
