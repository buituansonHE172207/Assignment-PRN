using Assignment_PRN.Enums;
using Assignment_PRN.Models;

namespace Assignment_PRN.Services;

public interface IBalanceService
{
    Task<TransactionModel> CreateTransactionAsync(decimal amountPoint, TransactionType type, string userId);

    Task ConfirmTransactionAsync(string transactionReference);
}