using System.Threading.Tasks;

namespace Bank.Services
{
    public interface ITransactionServices
    {
        BankContext BankContext { get; set; }

        Transaction GetTransaction(string transactionUId);
        double GetTrasferAmount(string bankId, FundTransferOption bankOption, double amount);
        bool IsTransactionExists(string transactionUID);
        Task<string> RevertTransaction(string transactionUId);
    }
}