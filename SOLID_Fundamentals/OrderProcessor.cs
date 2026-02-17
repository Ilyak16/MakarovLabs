using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID_Fundamentals
{
    public class OrderRepository
    {
        private List<Order> _orders = new();

        public void AddOrder(Order order)
        {
            _orders.Add(order);
            Console.WriteLine($"Order {order.Id} added");
        }

        public Order GetOrder(int orderId) => _orders.FirstOrDefault(o => o.Id == orderId);
        public List<Order> GetAllOrders() => _orders;
    }

    public class PaymentProcessor
    {
        public void ProcessPayment(string paymentMethod, decimal amount)
        {
            if (amount <= 0)
                throw new Exception("Invalid order amount");
            Console.WriteLine($"Processing {paymentMethod} payment of {amount}");
        }
    }

    public class InventoryManager
    {
        public void UpdateInventory(List<string> items)
        {
            Console.WriteLine("Inventory updated");
        }
    }

    public class EmailSender
    {
        public void SendEmail(string to, string message)
        {
            Console.WriteLine($"Email to {to}: {message}");
        }
    }

    public class Logger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }

    public class ReceiptGenerator
    {
        public void GenerateReceipt(Order order)
        {
            Console.WriteLine($"Receipt generated for order {order.Id}");
        }
    }

    public class ReportGenerator
    {
        public void GenerateMonthlyReport(List<Order> orders)
        {
            decimal totalRevenue = orders.Sum(o => o.TotalAmount);
            int totalOrders = orders.Count;
            Console.WriteLine($"Monthly Report: {totalOrders} orders, Revenue: {totalRevenue:C}");
        }

        public void ExportToExcel(string filePath, List<Order> orders)
        {
            Console.WriteLine($"Exporting {orders.Count} orders to {filePath}");
        }
    }
    public class OrderProcessor
    {
        private readonly OrderRepository _repository = new();
        private readonly PaymentProcessor _payment = new();
        private readonly InventoryManager _inventory = new();
        private readonly EmailSender _email = new();
        private readonly Logger _logger = new();
        private readonly ReceiptGenerator _receipt = new();
        private readonly ReportGenerator _report = new();

        public void AddOrder(Order order)
        {
            _repository.AddOrder(order);
        }

        public void ProcessOrder(int orderId)
        {
            var order = _repository.GetOrder(orderId);
            if (order == null) return;

            _payment.ProcessPayment(order.PaymentMethod, order.TotalAmount);
            _inventory.UpdateInventory(order.Items);
            _email.SendEmail(order.CustomerEmail, $"Order {orderId} processed");
            _logger.Log($"Order {orderId} processed at {DateTime.Now}");
            _receipt.GenerateReceipt(order);
        }

        public void GenerateMonthlyReport()
        {
            _report.GenerateMonthlyReport(_repository.GetAllOrders());
        }

        public void ExportToExcel(string filePath)
        {
            _report.ExportToExcel(filePath, _repository.GetAllOrders());
        }
    }
}