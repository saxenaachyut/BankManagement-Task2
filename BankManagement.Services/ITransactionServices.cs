namespace Bank
{
    public interface ITransactionServices
    {
        Transaction GetTransaction(AccountHolder accountHolder, string transactionID);
        double GetTrasferAmount(Bank bank, FundTransferOption bankOption, double amount);
        bool IsTransactionExists(AccountHolder accountHolder, string transactionID);
        bool RevertTransaction(AccountHolder accountHolder, Transaction transaction);
    }
}