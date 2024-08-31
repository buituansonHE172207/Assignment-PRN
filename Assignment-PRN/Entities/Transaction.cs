using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Assignment_PRN.Enums;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN.Entities;

public class Transaction
{
    [Key] public string Id { get; init; } = Guid.NewGuid().ToString();
    [StringLength(450)] public string UserId { get; set; } = null!;
    [Unicode] [StringLength(1000)] public string? Description { get; set; }
    [Precision(18, 2)] public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [StringLength(50)] public string TransactionReference { get; set; } = null!;
    public TransactionType Type { get; set; }
    public TransactionStatus Status { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; } = null!;
}