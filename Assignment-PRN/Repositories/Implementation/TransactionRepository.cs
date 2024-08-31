
using Assignment_PRN.Data;
using Assignment_PRN.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment_PRN.Repositories.Implementation;

public class TransactionRepository : RepositoryBase<Transaction, string>, ITransactionRepository
{
    public TransactionRepository(AssignmentPRNContext appDbContext) : base(appDbContext)
    {
    }

    public Task<Transaction?> GetByTransactionReference(string transactionId)
    {
        return DbSet.Include(t => t.User).FirstOrDefaultAsync(t => t.TransactionReference == transactionId);
    }
}