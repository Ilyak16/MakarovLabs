using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID_Fundamentals // SRP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OrderProcessor
    {
        private List<Order> orders = new List<Order>();

        public void AddOrder(Order order)
        {
            orders.Add(order);
            Console.WriteLine($"Order {order.Id} added");
        }

        public void ProcessOrder(int orderId)
        {
            var order = orders.FirstOrDefault(o => o.Id == orderId);
            if (order != null)
            {
                Console.WriteLine($"Processing order {orderId}");

                if (order.TotalAmount <= 0)
                    throw new Exception("Invalid order amount");

                ProcessPayment(order.PaymentMethod, order.TotalAmount);
                UpdateInventory(order.Items);
                SendEmail(order.CustomerEmail, $"Order {orderId} processed");
                LogToDatabase($"Order {orderId} processed at {DateTime.Now}");
                GenerateReceipt(order);
            }
        }

        public void GenerateMonthlyReport()
        {
            decimal totalRevenue = orders.Sum(o => o.TotalAmount);
            int totalOrders = orders.Count;
            Console.WriteLine($"Monthly Report: {totalOrders} orders, Revenue: {totalRevenue:C}");
        }

        public void ExportToExcel(string filePath)
        {
            Console.WriteLine($"Exporting orders to {filePath}");
        }

        private void ProcessPayment(string paymentMethod, decimal amount) { }
        private void UpdateInventory(List<string> items) { }
        private void SendEmail(string to, string message) { }
        private void LogToDatabase(string message) { }
        private void GenerateReceipt(Order order) { }
    }
}
