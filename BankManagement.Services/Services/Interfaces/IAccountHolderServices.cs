using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Services
{
    public interface IAccountHolderServices
    {
        Task<string> AddAccountHolder(string bankId, AccountHolder accountHolder);
        Task ChangePassword(string bankId, string username, string newPassword);
        Task<string> DepositAmount(Transaction transaction);
        Task<double> GetAccountBalance(string bankId, string accountHolderId);
        AccountHolder GetAccountHolder(string bankId, string username);
        AccountHolder GetAccountHolderThroughID(string bankId, string accountID);
        Task<List<Transaction>> GetTransactions(string accountHolderId);
        bool IsAccountHolderExists(string bankId, string username);
        bool IsSufficientFundsAvailable(string accountHolderId, double amount);
        Task RemoveAccountHolder(string bankId, string username);
        Task<string> TransferFunds(Transaction transaction);
        Task<string> WithdrawAmount(Transaction transaction);
    }
}