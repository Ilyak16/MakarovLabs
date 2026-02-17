namespace SOLID_Fundamentals // OCP
{
    public interface IDiscountStrategy
    {
        string CustomerType { get; }
        decimal CalculateDiscount(decimal orderAmount);
    }
    public class RegularDiscount : IDiscountStrategy
    {
        public string CustomerType => "Regular";
        public decimal CalculateDiscount(decimal orderAmount) => orderAmount * 0.05m;
    }

    public class PremiumDiscount : IDiscountStrategy
    {
        public string CustomerType => "Premium";
        public decimal CalculateDiscount(decimal orderAmount) => orderAmount * 0.10m;
    }

    public class VIPDiscount : IDiscountStrategy
    {
        public string CustomerType => "VIP";
        public decimal CalculateDiscount(decimal orderAmount) => orderAmount * 0.15m;
    }

    public class StudentDiscount : IDiscountStrategy
    {
        public string CustomerType => "Student";
        public decimal CalculateDiscount(decimal orderAmount) => orderAmount * 0.08m;
    }

    public class SeniorDiscount : IDiscountStrategy
    {
        public string CustomerType => "Senior";
        public decimal CalculateDiscount(decimal orderAmount) => orderAmount * 0.07m;
    }
    public interface IShippingStrategy
    {
        string ShippingMethod { get; }
        decimal CalculateShippingCost(decimal weight, string destination);
    }

    public class StandardShipping : IShippingStrategy
    {
        public string ShippingMethod => "Standard";
        public decimal CalculateShippingCost(decimal weight, string destination)
            => 5.00m + (weight * 0.5m);
    }

    public class ExpressShipping : IShippingStrategy
    {
        public string ShippingMethod => "Express";
        public decimal CalculateShippingCost(decimal weight, string destination)
            => 15.00m + (weight * 1.0m);
    }

    public class OvernightShipping : IShippingStrategy
    {
        public string ShippingMethod => "Overnight";
        public decimal CalculateShippingCost(decimal weight, string destination)
            => 25.00m + (weight * 2.0m);
    }

    public class InternationalShipping : IShippingStrategy
    {
        public string ShippingMethod => "International";
        public decimal CalculateShippingCost(decimal weight, string destination)
        {
            return destination switch
            {
                "USA" => 30.00m,
                "Europe" => 35.00m,
                "Asia" => 40.00m,
                _ => 50.00m
            };
        }
    }
    public class DiscountCalculator
    {
        private readonly Dictionary<string, IDiscountStrategy> _discountStrategies;
        private readonly Dictionary<string, IShippingStrategy> _shippingStrategies;
        
        public DiscountCalculator()
        {
            _discountStrategies = new IDiscountStrategy[]
            {
                new RegularDiscount(),
                new PremiumDiscount(),
                new VIPDiscount(),
                new StudentDiscount(),
                new SeniorDiscount()
            }.ToDictionary(s => s.CustomerType);

            _shippingStrategies = new IShippingStrategy[]
            {
                new StandardShipping(),
                new ExpressShipping(),
                new OvernightShipping(),
                new InternationalShipping()
            }.ToDictionary(s => s.ShippingMethod);
        }
        
        public decimal CalculateDiscount(string customerType, decimal orderAmount)
        {
            return _discountStrategies.TryGetValue(customerType, out var strategy)
                ? strategy.CalculateDiscount(orderAmount)
                : 0;
        }

        public decimal CalculateShippingCost(string shippingMethod, decimal weight, string destination)
        {
            return _shippingStrategies.TryGetValue(shippingMethod, out var strategy)
                ? strategy.CalculateShippingCost(weight, destination)
                : 0;
        }
        
        // МЕТОДЫ ДЛЯ РАСШИРЕНИЯ (ДОБАВЛЕНЫ)
        public void AddDiscountStrategy(IDiscountStrategy strategy)
        {
            if (_discountStrategies.ContainsKey(strategy.CustomerType))
            {
                // Можно либо обновить, либо выбросить исключение
                Console.WriteLine($"Стратегия для {strategy.CustomerType} обновлена");
            }
            _discountStrategies[strategy.CustomerType] = strategy;
        }

        public void AddShippingStrategy(IShippingStrategy strategy)
        {
            if (_shippingStrategies.ContainsKey(strategy.ShippingMethod))
            {
                Console.WriteLine($"Стратегия доставки для {strategy.ShippingMethod} обновлена");
            }
            _shippingStrategies[strategy.ShippingMethod] = strategy;
        }

        // Дополнительно: метод для удаления (опционально)
        public bool RemoveDiscountStrategy(string customerType)
        {
            return _discountStrategies.Remove(customerType);
        }

        public bool RemoveShippingStrategy(string shippingMethod)
        {
            return _shippingStrategies.Remove(shippingMethod);
        }

        // Получение списка доступных стратегий (опционально)
        public IEnumerable<string> GetAvailableDiscountTypes()
        {
            return _discountStrategies.Keys.ToList();
        }

        public IEnumerable<string> GetAvailableShippingMethods()
        {
            return _shippingStrategies.Keys.ToList();
        }
    }
}
