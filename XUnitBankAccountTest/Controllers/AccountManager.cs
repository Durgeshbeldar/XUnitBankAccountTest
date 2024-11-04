using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUnitBankAccountTest.Exceptions;
using XUnitBankAccountTest.Models;

namespace XUnitBankAccountTest.Controllers
{
    internal class AccountManager
    {
        private List<Account> _accounts;
        static double MIN_BALANCE = 500;
        public AccountManager()
        {
            // Dummy Data For Testing Purpose...

            _accounts = new List<Account>()
            {
                new Account(1,"Shyam",1000),
                new Account(2,"Manish", 600),
                new Account(3,"Mohan", 200)
            };
        }

        Account FindAccountByAccountNo(int accountNo)
        {
            Account account = _accounts.FirstOrDefault(account => account.AccountNumber == accountNo);
            if (account == null)
                throw new AccountNotFoundException("Invalid Account OR Account Not Found!");
            return account;
        }
        public void Deposit(int accountNo , double amount)
        {
            if (amount < 0)
                throw new InvalidAmountException("Deposit Amount Must be Positive..");
            Account account = FindAccountByAccountNo(accountNo);
            account.DepositMoney(amount); 
        }

        public void Withdraw(int accountNo, double amount)
        {

            if (amount < 0)
                throw new InvalidAmountException("Deposit Amount Must be Positive..");

            Account account = FindAccountByAccountNo(accountNo);
            if ( account.Balance - amount < MIN_BALANCE)
                throw new InsufficientBalanceException("Transaction Failed, Due to Insufficient Balance..");

            account.WithdrawMoney(amount);  
        }

        public double GetBalance(int accountNo)
        {
            Account account = FindAccountByAccountNo(accountNo) ;
            return account.Balance; 
        }
        public void TransferMoney(int senderAccountNo ,  int recieverAccountNo, double amount)
        {
            if (amount < 0)
                throw new InvalidAmountException("Deposit Amount Must be Positive..");

            Account senderAccount = FindAccountByAccountNo(senderAccountNo);    
            if (senderAccount.Balance - amount < MIN_BALANCE)
                throw new InsufficientBalanceException("Transaction Failed, Due to Insufficient Balance..");
            Account recieverAccount = FindAccountByAccountNo(recieverAccountNo);

            senderAccount.WithdrawMoney(amount);
            recieverAccount.DepositMoney(amount);
        }
    }
}
