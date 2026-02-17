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
            return amount <= Balance;
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

        public override bool CanWithdraw(decimal amount)
        {
            return Balance - amount >= -OverdraftLimit;
        }

        public override void Withdraw(decimal amount)
        {
            if (!CanWithdraw(amount))
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

        public override bool CanWithdraw(decimal amount)
        {
            if (DateTime.Now < MaturityDate)
            {
                return false;
            }
            return amount <= Balance;
        }

        public override void Withdraw(decimal amount)
        {
            if (!CanWithdraw(amount))
            {
                throw new InvalidOperationException(
                    DateTime.Now < MaturityDate
                        ? "Cannot withdraw before maturity date"
                        : "Insufficient funds");
            }
            Balance -= amount;
        }

        public override decimal CalculateInterest()
        {
            return Balance * 0.05m;
        }
    }

    public interface ITransferService
    {
        bool TryTransfer(ITransferable from, ITransferable to, decimal amount);
        void Transfer(ITransferable from, ITransferable to, decimal amount);
    }

    public class BankTransferService : ITransferService
    {
        public bool TryTransfer(ITransferable from, ITransferable to, decimal amount)
        {
            if (!from.CanWithdraw(amount))
            {
                return false;
            }

            try
            {
                TransferInternal(from, to, amount);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Transfer(ITransferable from, ITransferable to, decimal amount)
        {
            if (!from.CanWithdraw(amount))
            {
                throw new InvalidOperationException($"Cannot withdraw {amount} from source account");
            }

            TransferInternal(from, to, amount);
        }

        private void TransferInternal(ITransferable from, ITransferable to, decimal amount)
        {
            from.Withdraw(amount);
            try
            {
                to.Deposit(amount);
            }
            catch
            {
                from.Deposit(amount);
                throw;
            }
        }
    }

    public class Bank
    {
        private readonly ITransferService _transferService;

        public Bank(ITransferService transferService = null)
        {
            _transferService = transferService ?? new BankTransferService();
        }

        public void ProcessWithdrawal(IWithdrawable account, decimal amount)
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

        public void Transfer(ITransferable from, ITransferable to, decimal amount)
        {
            try
            {
                _transferService.Transfer(from, to, amount);
                Console.WriteLine($"Successfully transferred {amount} from account to account");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Transfer failed: {ex.Message}");
            }
        }

        public bool TryTransfer(ITransferable from, ITransferable to, decimal amount)
        {
            bool success = _transferService.TryTransfer(from, to, amount);
            Console.WriteLine(success
                ? $"Successfully transferred {amount} from account to account"
                : "Transfer failed");
            return success;
        }
    }
}
