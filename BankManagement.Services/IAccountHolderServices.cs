namespace Bank
{
    public interface IAccountHolderServices
    {
        bool AddAccountHolder(Bank bank, AccountHolder accountHolder);
        bool DepositAmount(AccountHolder accountHolder, Transaction transaction);
        double GetAccountBalance(AccountHolder customer);
        AccountHolder GetAccountHolder(Bank bank, string username);
        AccountHolder GetAccountHolderThroughID(Bank bank, string accountID);
        bool IsAccountHolderExists(Bank bank, string username);
        bool IsSufficientFundsAvailable(AccountHolder accountHolder, double amount);
        bool RemoveAccountHolder(Bank bank, string username);
        bool TransferFunds(AccountHolder sender, AccountHolder beneficiary, Transaction transaction);
        bool WithdrawAmount(AccountHolder accountHolder, Transaction transaction);
    }
}