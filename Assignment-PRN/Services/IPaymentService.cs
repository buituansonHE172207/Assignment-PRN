using Assignment_PRN.Models;

namespace Assignment_PRN.Interfaces.Services;
public interface IPaymentService
{
    Task<string> CreatePaymentUrl(TransactionModel transaction);
    Task CheckPayment(string queryString);
    Task<string> GetTransactionReference(string queryString);
}