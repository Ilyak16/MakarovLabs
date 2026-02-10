namespace SOLID_Fundamentals // OCP
{
    public class DiscountCalculator
    {
        public decimal CalculateDiscount(string customerType, decimal orderAmount)
        {
            if (customerType == "Regular")
            {
                return orderAmount * 0.05m;
            }
            else if (customerType == "Premium")
            {
                return orderAmount * 0.10m;
            }
            else if (customerType == "VIP")
            {
                return orderAmount * 0.15m;
            }
            else if (customerType == "Student")
            {
                return orderAmount * 0.08m;
            }
            else if (customerType == "Senior")
            {
                return orderAmount * 0.07m;
            }
            else
            {
                return 0;
            }
        }

        public decimal CalculateShippingCost(string shippingMethod, decimal weight, string destination)
        {
            switch (shippingMethod)
            {
                case "Standard":
                    return 5.00m + (weight * 0.5m);
                case "Express":
                    return 15.00m + (weight * 1.0m);
                case "Overnight":
                    return 25.00m + (weight * 2.0m);
                case "International":
                    if (destination == "USA") return 30.00m;
                    if (destination == "Europe") return 35.00m;
                    if (destination == "Asia") return 40.00m;
                    return 50.00m;
                default:
                    return 0;
            }
        }
    }
}
