using Bank.Services;
using System.Threading.Tasks;

namespace Bank.Console
{
    public interface IBankApplication
    {
        IAccountHolderServices AccountHolderService { get; set; }
        IBankServices BankService { get; set; }
        ITransactionServices TransactionService { get; set; }

        Task AccountHolderMenu(Bank bank, AccountHolder accountHolder);
        Task AddNewCurrency(Bank bank);
        Task BankLogin(Bank bank);
        Task CreateBank();
        Task CreateNewAccountHolder(Bank bank);
        Task DeleteCustomerAccount(Bank bank);
        Task Deposit(Bank bank, AccountHolder accountHolder);
        Task DisplayBanks();
        Task Login();
        Task MainMenu();
        Task OtherBankTransfer(Bank bank, AccountHolder accountHolder, FundTransferOption bankOption);
        Task RevertTransaction(Bank bank);
        Task SameBankTransfer(Bank bank, AccountHolder accountHolder, FundTransferOption bankOption);
        Task StaffMenu(Bank bank, BankStaff bankStaff);
        Task TransactionHistory(AccountHolder accountHolder);
        Task TransactionHistory(Bank bank);
        Task TransferFundsMenu(Bank bank, AccountHolder accountHolder);
        Task UpdateAccountHolder(Bank bank);
        Task UpdateServiceChargeOtherBank(Bank bank);
        Task UpdateServiceChargeSameBank(Bank bank);
        Task Withdraw(Bank bank, AccountHolder accountHolder);
    }
}