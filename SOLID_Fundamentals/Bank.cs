using System.Diagnostics.Contracts;

namespace SOLID_Fundamentals //LSP
{
    public interface IWithdrawable
    {
        void Withdraw(decimal amount);
        bool CanWithdraw(decimal amount);
    }
    public interface IDepositable
    {
        void Deposit(decimal amount);
    }
    public interface ITransferable : IWithdrawable, IDepositable
    {
        decimal Balance { get; }
    }
    public abstract class Account : ITransferable
    {
        public decimal Balance { get; protected set; }

        public virtual void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public abstract void Withdraw(decimal amount);
        public virtual bool CanWithdraw(decimal amount)
        {
            if (amount <= Balance)
            return true;
            else return false;
        }
        public virtual decimal CalculateInterest()
        {
            return Balance * 0.01m;
        }
    }

    public class SavingsAccount : Account
    {
        public decimal MinimumBalance { get; } = 100m;
        public override bool CanWithdraw(decimal amount)
        {
            return Balance - amount >= MinimumBalance;
        }
        public override void Withdraw(decimal amount)
        {
            if (!CanWithdraw(amount))
            {
                throw new InvalidOperationException("Cannot go below minimum balance");
            }
            Balance -= amount;
        }
    }

    public class CheckingAccount : Account
    {
        public decimal OverdraftLimit { get; } = 500m;

        public override void Withdraw(decimal amount)
        {
            if (Balance - amount < -OverdraftLimit)
            {
                throw new InvalidOperationException("Overdraft limit exceeded");
            }
            Balance -= amount;
        }
    }

    public class FixedDepositAccount : Account
    {
        public DateTime MaturityDate { get; }

        public FixedDepositAccount(DateTime maturityDate)
        {
            MaturityDate = maturityDate;
        }

        public override void Withdraw(decimal amount)
        {
            if (DateTime.Now < MaturityDate)
            {
                throw new InvalidOperationException("Cannot withdraw before maturity date");
            }

            if (amount > Balance)
            {
                throw new InvalidOperationException("Insufficient funds");
            }

            Balance -= amount;
        }

        public override decimal CalculateInterest()
        {
            return Balance * 0.05m;
        }
    }

    public class Bank
    {
        public void ProcessWithdrawal(Account account, decimal amount)
        {
            try
            {
                account.Withdraw(amount);
                Console.WriteLine($"Successfully withdrew {amount}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Withdrawal failed: {ex.Message}");
            }
        }

        public void Transfer(IWithdraw from, Account to, decimal amount)
        {
            if (!from.CanWithdraw(amount)) 
            { 
                Console.WriteLine("Transfer failed");
            }
            try
            {
                from.Withdraw(amount);
                to.Deposit(amount);
                Console.WriteLine("Successfully transferred");
            }
            catch
            {
                Console.WriteLine("Transfer failed: insufficient funds");
            }
        }
    }
}
