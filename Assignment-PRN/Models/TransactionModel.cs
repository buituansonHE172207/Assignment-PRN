using Assignment_PRN.Enums;

namespace Assignment_PRN.Models;

public class TransactionModel
{
    public string TransactionReference { get; init; } = null!;
    public string Info { get; init; } = string.Empty;
    public decimal Amount { get; init; }
}