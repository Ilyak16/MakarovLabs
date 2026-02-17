namespace SOLID_Fundamentals// ISP
{

    public interface IOrderCrud
    {
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderId);
    }

    public interface IOrderProcessing
    {
        void ProcessPayment(Order order);
        void ShipOrder(Order order);
    }

    public class CustomerPortal : IOrderCrud
    {
        public void CreateOrder(Order order) => Console.WriteLine("Order created by customer");
        public void UpdateOrder(Order order) => Console.WriteLine("Order updated by customer");
        public void DeleteOrder(int orderId) => Console.WriteLine("Order deleted by customer");
    }
}
