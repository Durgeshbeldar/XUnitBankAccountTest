using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XUnitBankAccountTest.Exceptions;

namespace XUnitBankAccountTest.Models
{
    internal class Account
    {
        public int AccountNumber { get; set; }
        public string Name { get; set; }    
        public double Balance { get; set; }

        const double MIN_BALANCE = 500;

        public Account(int accountNumber, string name, double balance)
        {
            AccountNumber = accountNumber;
            Name = name;
            Balance = (balance > MIN_BALANCE) ? balance : MIN_BALANCE;
        }

        // Account Related Methods...
        public void DepositMoney(double amount)
        {
            Balance = Balance + amount;
        }

        public void WithdrawMoney(double amount)
        {
            Balance = Balance - amount;
        }
    }
}
