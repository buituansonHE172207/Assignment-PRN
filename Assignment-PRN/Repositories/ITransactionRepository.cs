using Assignment_PRN.Entities;

namespace Assignment_PRN.Repositories;

public interface ITransactionRepository : IRepositoryBase<Transaction, string>
{
    Task<Transaction?> GetByTransactionReference(string transactionId);
}