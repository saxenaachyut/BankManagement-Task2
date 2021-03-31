using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank
{
    public interface IAccountHolderServices
    {
        Task AddAccountHolder(int bankId, AccountHolder accountHolder);
        Task ChangePassword(int bankId, string username, string newPassword);
        Task DepositAmount(Transaction transaction);
        Task<double> GetAccountBalance(int bankId, int accountHolderId);
        AccountHolder GetAccountHolder(int bankId, string username);
        AccountHolder GetAccountHolderThroughID(int bankId, string accountID);
        Task<List<Transaction>> GetTransactions(int accountHolderId);
        bool IsAccountHolderExists(int bankId, string username);
        bool IsSufficientFundsAvailable(int accountHolderId, double amount);
        Task RemoveAccountHolder(int bankId, string username);
        Task TransferFunds(Transaction transaction);
        Task WithdrawAmount(Transaction transaction);
    }
}