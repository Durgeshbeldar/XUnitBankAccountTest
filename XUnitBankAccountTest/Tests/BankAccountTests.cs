using XUnitBankAccountTest.Controllers;
using XUnitBankAccountTest.Exceptions;

namespace XUnitBankAccountTest.Tests
{
    public class BankAccountTest
    {
        AccountManager accountManager;

        public BankAccountTest()
        {
           accountManager = new AccountManager();
        }

        // Test Case 1: For Deposit Money in Bank Account...

        [Theory]
        [InlineData(1,100, 1100)]
        public void DoesPositiveAmountDeposited(int accountNo, double amountToTest, double expectedBalance)
        {
            accountManager.Deposit(accountNo, amountToTest);
            double actualBalance = accountManager.GetBalance(accountNo);
            Assert.Equal(expectedBalance, actualBalance);
        }

        // Test Case 2: For Withdraw Money From Bank Account...

        [Theory]
        [InlineData(1,100, 900)]
        public void DoesPositiveAmountWithdrawls(int accountNo,double amountToTest, double expectedBalance)
        {
            accountManager.Withdraw(accountNo, amountToTest);
            double actualBalance = accountManager.GetBalance(accountNo);

            Assert.Equal(expectedBalance, actualBalance);
        }



        // Test Case 3: For Negative Amount Deposited

        [Theory]
        [InlineData(1,-100)]
        public void Deposit_NegativeAmount_ThrowsInvalidAmountException(int accountNo, double amountToTest)
        {
            Assert.Throws<InvalidAmountException>(() => accountManager.Deposit(accountNo, amountToTest));
        }

        // Test Case 4: For Negative Amount Withdrawl
        [Theory]
        [InlineData(1,-100)]
        public void Withdraw_NegativeAmount_ThrowsInvalidAmountException(int accountNo, double amountToTest)
        {
            Assert.Throws<InvalidAmountException>(() => accountManager.Withdraw(accountNo, amountToTest));
        }

        // Test Case 5: Insufficient balance 
        [Theory]
        [InlineData(1,1000)]
        public void Withdraw_InsufficientBalance_ThrowsInsufficientBalanceException(int accountNo, double amountToTest)
        {
            Assert.Throws<InsufficientBalanceException>(() => accountManager.Withdraw(accountNo, amountToTest));
        }

        // Test Case 6: To Check TransferMoney Method For Reciever Account

        [Theory]
        [InlineData(1,2,800,200)] 

        // Sender AccountNo = 1, Reciever AccountNo = 2, TransactionAmount = 200
        // Expected Result for Reciever Balance = 800

        public void Does_Money_Transffered_Successfully_To_Reciever_Account
            (int senderAccNo, int recieverAccNo, double expectedRecieverBalance, double amountToTest)
        {

            accountManager.TransferMoney(senderAccNo, recieverAccNo, amountToTest);

            double actualBalanceOfReciever = accountManager.GetBalance(recieverAccNo);

            Assert.Equal(expectedRecieverBalance, actualBalanceOfReciever);
        }

        // Test Case 7: To Check Weather Money Deducted From Sender account or Not.

        [Theory]
        [InlineData(1,2,800,200)]

        public void Does_Money_Transffered_Successfully_And_DeductedFromSenderAccount
            (int senderAccNo, int recieverAccNo, double expectedSenderBalance, double amountToTest)
        {
            accountManager.TransferMoney(senderAccNo, recieverAccNo, amountToTest);
            double actualBalanceOfSender = accountManager.GetBalance(senderAccNo);
            Assert.Equal(expectedSenderBalance, actualBalanceOfSender);

        }

        // Test Case 8: Transfer Money Negative Amount 

        [Theory]
        [InlineData(1,2,-100)]
        public void TransferMoney_NegativeAmount_ThrowsInvalidAmountException(int senderAccNo, int recieverAccNo, double amountToTest)
        {
            Assert.Throws<InvalidAmountException>(() => accountManager.TransferMoney(senderAccNo,recieverAccNo,amountToTest));
        }

        // Test Case 9: InsufficientBalanceForTransfer

        [Theory]
        [InlineData(1,2,1000)]
        public void InsufficientBalanceForTransfer(int senderAccNo,int recieverAccNo, double amountToTest)
        {
            Assert.Throws<InsufficientBalanceException>(() => accountManager.TransferMoney(senderAccNo, recieverAccNo, amountToTest));
        }

        // Test Case 10 : Invalid Account Or Account Not Found...

        [Theory]
        [InlineData(4,2,1000)] // Sender AccountNo 4  is Not Exist in Our Dummy Data

        public void DoesInvalidAccount(int senderAccNo, int recieverAccNo,  int amountToTest)
        {
            Assert.Throws<AccountNotFoundException>(() => accountManager.TransferMoney(senderAccNo, recieverAccNo, amountToTest));
        }
    }
}