namespace SOLID_Fundamentals // SRP
{
    public class Order
    {
        public int Id { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string CustomerEmail { get; private set; }
        public string PaymentMethod { get; private set; }
        public List<string> Items { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Status { get; private set; }
        public string CustomerPhone { get; private set; }
        public Order(int id, string customerEmail)
        {
            Id = id;
            CustomerEmail = customerEmail;
            Items = new List<string>();
            CreatedAt = DateTime.Now;
            Status = "Pending";
        }
        public void AddItem(string item, decimal price)
        {
            Items.Add(item);
            TotalAmount += price;
        }
        public void SetPaymentMethod(string method)
        {
            if (Status != "Pending")
                throw new InvalidOperationException("Cannot change payment method after processing");
        }
        public bool IsValid()
        {
            return TotalAmount > 0 && !string.IsNullOrEmpty(CustomerEmail);
        }

    }
}