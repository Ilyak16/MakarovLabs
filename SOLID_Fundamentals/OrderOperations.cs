namespace SOLID_Fundamentals// ISP
{

    public interface IOrderOperations
    {
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderId);
        void ProcessPayment(Order order);
        void ShipOrder(Order order);
        void GenerateInvoice(Order order);
        void SendNotification(Order order);
        void GenerateReport(DateTime from, DateTime to);
        void ExportToExcel(string filePath);
        void BackupDatabase();
        void RestoreDatabase();
    }

    public class OrderManager : IOrderOperations
    {
        public void CreateOrder(Order order)
        {
            Console.WriteLine("Order created");
        }

        public void UpdateOrder(Order order)
        {
            Console.WriteLine("Order updated");
        }

        public void DeleteOrder(int orderId)
        {
            Console.WriteLine("Order deleted");
        }

        public void ProcessPayment(Order order)
        {
            Console.WriteLine("Payment processed");
        }

        public void ShipOrder(Order order)
        {
            Console.WriteLine("Order shipped");
        }

        public void GenerateInvoice(Order order)
        {
            Console.WriteLine("Invoice generated");
        }

        public void SendNotification(Order order)
        {
            Console.WriteLine("Notification sent");
        }

        public void GenerateReport(DateTime from, DateTime to)
        {
            Console.WriteLine("Report generated");
        }

        public void ExportToExcel(string filePath)
        {
            Console.WriteLine("Exported to Excel");
        }

        public void BackupDatabase()
        {
            Console.WriteLine("Database backed up");
        }

        public void RestoreDatabase()
        {
            Console.WriteLine("Database restored");
        }
    }

    public class CustomerPortal : IOrderOperations
    {
        public void CreateOrder(Order order)
        {
            Console.WriteLine("Order created by customer");
        }

        public void UpdateOrder(Order order)
        {
            Console.WriteLine("Order updated by customer");
        }

        public void DeleteOrder(int orderId)
        {
            Console.WriteLine("Order deleted by customer");
        }

        public void ProcessPayment(Order order)
        {
            throw new NotImplementedException("Customer cannot process payments");
        }

        public void ShipOrder(Order order)
        {
            throw new NotImplementedException("Customer cannot ship orders");
        }

        public void GenerateInvoice(Order order)
        {
            throw new NotImplementedException("Customer cannot generate invoices");
        }

        public void SendNotification(Order order)
        {
            throw new NotImplementedException("Customer cannot send notifications");
        }

        public void GenerateReport(DateTime from, DateTime to)
        {
            throw new NotImplementedException("Customer cannot generate reports");
        }

        public void ExportToExcel(string filePath)
        {
            throw new NotImplementedException("Customer cannot export to Excel");
        }

        public void BackupDatabase()
        {
            throw new NotImplementedException("Customer cannot backup database");
        }

        public void RestoreDatabase()
        {
            throw new NotImplementedException("Customer cannot restore database");
        }
    }
}
