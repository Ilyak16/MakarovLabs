namespace MakarovLab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Тестирование Bank.cs (LSP)
            TestBank();

            // 2. Тестирование DiscountCalculator.cs (OCP) - С ДОБАВЛЕННЫМИ МЕТОДАМИ
            TestDiscountCalculator();

            // 3. Тестирование Order.cs (SRP)
            TestOrder();

            // 4. Тестирование OrderOperations.cs (ISP)
            TestOrderOperations();

            // 5. Тестирование OrderProcessor.cs (SRP)
            TestOrderProcessor();

            // 6. Тестирование Services.cs (DIP)
            TestServices();

            Console.WriteLine("\n===== ТЕСТИРОВАНИЕ ЗАВЕРШЕНО =====");
        }

        static void TestBank()
        {
            Console.WriteLine("\n--- Тест Bank.cs (LSP) ---");

            var bank = new SOLID_Fundamentals.Bank();
            var savings = new SOLID_Fundamentals.SavingsAccount();
            var checking = new SOLID_Fundamentals.CheckingAccount();
            var fixedDeposit = new SOLID_Fundamentals.FixedDepositAccount(DateTime.Now.AddDays(30));

            // Вносим деньги
            savings.Deposit(1000);
            checking.Deposit(500);
            fixedDeposit.Deposit(2000);

            Console.WriteLine("Начальные балансы:");
            Console.WriteLine($"  Savings: {savings.Balance:C}");
            Console.WriteLine($"  Checking: {checking.Balance:C}");
            Console.WriteLine($"  Fixed Deposit: {fixedDeposit.Balance:C}");

            // Тест снятия
            Console.WriteLine("\nТест снятия со сберегательного счета:");
            bank.ProcessWithdrawal(savings, 200);  // Успешно
            bank.ProcessWithdrawal(savings, 800);  // Неудача (ниже минимума)

            // Тест перевода
            Console.WriteLine("\nТест перевода между счетами:");
            bank.Transfer(savings, checking, 300);  // Успешно

            Console.WriteLine("\nБалансы после операций:");
            Console.WriteLine($"  Savings: {savings.Balance:C}");
            Console.WriteLine($"  Checking: {checking.Balance:C}");

            // Тест FixedDepositAccount
            Console.WriteLine("\nТест срочного депозита (дата созревания через 30 дней):");
            bank.ProcessWithdrawal(fixedDeposit, 500);  // Неудача (до maturity date)
        }

        // Новая стратегия для демонстрации OCP
        public class CorporateDiscount : SOLID_Fundamentals.IDiscountStrategy
        {
            public string CustomerType => "Corporate";
            public decimal CalculateDiscount(decimal orderAmount) => orderAmount * 0.12m;
        }

        // Еще одна стратегия для демонстрации
        public class BirthdayDiscount : SOLID_Fundamentals.IDiscountStrategy
        {
            public string CustomerType => "Birthday";
            public decimal CalculateDiscount(decimal orderAmount) => orderAmount * 0.20m;
        }

        // Новая стратегия доставки
        public class DroneShipping : SOLID_Fundamentals.IShippingStrategy
        {
            public string ShippingMethod => "Drone";
            public decimal CalculateShippingCost(decimal weight, string destination)
            {
                // Дроны доставляют только в пределах города
                if (destination == "Local")
                    return 10.00m + (weight * 3.0m);
                return 0; // Не доставляем
            }
        }

        static void TestDiscountCalculator()
        {
            Console.WriteLine("\n--- Тест DiscountCalculator.cs (OCP) ---");

            var calculator = new SOLID_Fundamentals.DiscountCalculator();
            decimal orderAmount = 1000m;

            Console.WriteLine($"Заказ на сумму: {orderAmount:C}");
            Console.WriteLine("Скидки для разных типов клиентов:");
            Console.WriteLine($"  Regular: {calculator.CalculateDiscount("Regular", orderAmount):C}");
            Console.WriteLine($"  Premium: {calculator.CalculateDiscount("Premium", orderAmount):C}");
            Console.WriteLine($"  VIP: {calculator.CalculateDiscount("VIP", orderAmount):C}");
            Console.WriteLine($"  Student: {calculator.CalculateDiscount("Student", orderAmount):C}");
            Console.WriteLine($"  Senior: {calculator.CalculateDiscount("Senior", orderAmount):C}");

            Console.WriteLine("\nСтоимость доставки (вес 2кг):");
            Console.WriteLine($"  Standard (USA): {calculator.CalculateShippingCost("Standard", 2, "USA"):C}");
            Console.WriteLine($"  Express (Europe): {calculator.CalculateShippingCost("Express", 2, "Europe"):C}");
            Console.WriteLine($"  Overnight (Asia): {calculator.CalculateShippingCost("Overnight", 2, "Asia"):C}");
            Console.WriteLine($"  International (USA): {calculator.CalculateShippingCost("International", 2, "USA"):C}");

            // Демонстрация OCP - добавление новой стратегии без изменения существующего кода
            Console.WriteLine("\n--- ДЕМОНСТРАЦИЯ OCP (Open/Closed Principle) ---");
            Console.WriteLine("Добавление новых стратегий без изменения класса DiscountCalculator:\n");

            // Получаем список доступных типов до начала добавления
            Console.WriteLine("Доступные типы скидок до добавления:");
            foreach (var type in calculator.GetAvailableDiscountTypes())
            {
                Console.WriteLine($"  - {type}");
            }

            // Добавляем новую стратегию скидки
            Console.WriteLine("\n1. Добавление новой стратегии скидки 'Corporate' (12%):");
            calculator.AddDiscountStrategy(new CorporateDiscount());
            Console.WriteLine($"  Corporate: {calculator.CalculateDiscount("Corporate", orderAmount):C}");

            // Добавляем еще одну стратегию
            Console.WriteLine("\n2. Добавление новой стратегии скидки 'Birthday' (20%):");
            calculator.AddDiscountStrategy(new BirthdayDiscount());
            Console.WriteLine($"  Birthday: {calculator.CalculateDiscount("Birthday", orderAmount):C}");

            // Добавляем новую стратегию доставки
            Console.WriteLine("\n3. Добавление новой стратегии доставки 'Drone' (для Local):");
            calculator.AddShippingStrategy(new DroneShipping());
            Console.WriteLine($"  Drone (Local): {calculator.CalculateShippingCost("Drone", 2, "Local"):C}");
            Console.WriteLine($"  Drone (Other): {calculator.CalculateShippingCost("Drone", 2, "Other"):C}");

            // Проверяем обновленный список
            Console.WriteLine("\nДоступные типы скидок после добавления:");
            foreach (var type in calculator.GetAvailableDiscountTypes())
            {
                Console.WriteLine($"  - {type}");
            }

            // Демонстрация удаления
            Console.WriteLine("\n4. Удаление стратегии 'Student':");
            calculator.RemoveDiscountStrategy("Student");
            Console.WriteLine($"  Student после удаления: {calculator.CalculateDiscount("Student", orderAmount):C} (0 - стратегия не найдена)");

            Console.WriteLine("\nВАЖНО: Весь этот функционал добавлен БЕЗ изменения существующего кода класса DiscountCalculator!");
            Console.WriteLine("Класс остался закрыт для модификации, но открыт для расширения - это и есть OCP.");
        }

        static void TestOrder()
        {
            Console.WriteLine("\n--- Тест Order.cs (SRP) ---");

            // Создание заказа
            var order = new SOLID_Fundamentals.Order(1, "customer@email.com");
            order.AddItem("Laptop", 1000);
            order.AddItem("Mouse", 50);
            order.SetPaymentMethod("CreditCard");

            Console.WriteLine($"Заказ #{order.Id}");
            Console.WriteLine($"  Email клиента: {order.CustomerEmail}");
            Console.WriteLine($"  Статус: {order.Status}");
            Console.WriteLine($"  Дата создания: {order.CreatedAt}");
            Console.WriteLine($"  Товары: {string.Join(", ", order.Items)}");
            Console.WriteLine($"  Общая сумма: {order.TotalAmount:C}");
            Console.WriteLine($"  Способ оплаты: {order.PaymentMethod}");
            Console.WriteLine($"  Валидность заказа: {order.IsValid()}");

            // Проверка инкапсуляции
            Console.WriteLine("\nПопытка изменить способ оплаты после обработки:");
            try
            {
                // Имитация обработки заказа
                var statusField = typeof(SOLID_Fundamentals.Order).GetProperty("Status");
                statusField?.SetValue(order, "Processed");

                order.SetPaymentMethod("PayPal"); // Должно выбросить исключение
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"  Ошибка: {ex.Message} (как и ожидалось)");
            }
        }

        static void TestOrderOperations()
        {
            Console.WriteLine("\n--- Тест OrderOperations.cs (ISP) ---");

            var portal = new SOLID_Fundamentals.CustomerPortal();
            var order = new SOLID_Fundamentals.Order(2, "portal@user.com");

            Console.WriteLine("Клиентский портал (реализует только IOrderCrud):");
            portal.CreateOrder(order);
            portal.UpdateOrder(order);
            portal.DeleteOrder(2);

            // Проверка, что методы ProcessPayment и другие недоступны
            Console.WriteLine("  (Методы ProcessPayment, ShipOrder и др. отсутствуют - интерфейс разделен)");
        }

        static void TestOrderProcessor()
        {
            Console.WriteLine("\n--- Тест OrderProcessor.cs (SRP) ---");

            var processor = new SOLID_Fundamentals.OrderProcessor();

            // Создание заказов
            var order1 = new SOLID_Fundamentals.Order(3, "test1@email.com");
            order1.AddItem("Book", 25);
            order1.AddItem("Pen", 5);
            order1.SetPaymentMethod("CreditCard");

            var order2 = new SOLID_Fundamentals.Order(4, "test2@email.com");
            order2.AddItem("Monitor", 300);
            order2.SetPaymentMethod("PayPal");

            processor.AddOrder(order1);
            processor.AddOrder(order2);

            Console.WriteLine("Обработка заказа #3:");
            processor.ProcessOrder(3);

            Console.WriteLine("\nГенерация месячного отчета:");
            processor.GenerateMonthlyReport();

            Console.WriteLine("\nЭкспорт в Excel:");
            processor.ExportToExcel("orders.xlsx");
        }

        static void TestServices()
        {
            Console.WriteLine("\n--- Тест Services.cs (DIP) ---");

            // Создание зависимостей
            SOLID_Fundamentals.IEmailService emailService = new SOLID_Fundamentals.EmailService();
            SOLID_Fundamentals.ISmsService smsService = new SOLID_Fundamentals.SmsService();

            // Внедрение зависимостей через конструктор
            var orderService = new SOLID_Fundamentals.OrderService(emailService, smsService);
            var notificationService = new SOLID_Fundamentals.NotificationService(emailService);

            var order = new SOLID_Fundamentals.Order(5, "dip@test.com");
            var phoneField = typeof(SOLID_Fundamentals.Order).GetProperty("CustomerPhone");
            phoneField?.SetValue(order, "+1234567890");

            Console.WriteLine("Размещение заказа (отправка email и SMS):");
            orderService.PlaceOrder(order);

            Console.WriteLine("\nОтправка промо-акции (только email):");
            notificationService.SendPromotion("customer@email.com", "Скидка 20% на все товары!");

            // Демонстрация возможности замены реализации
            Console.WriteLine("\nЗамена реализации EmailService на другую (тестовую):");
            var testEmailService = new TestEmailService();
            var testOrderService = new SOLID_Fundamentals.OrderService(testEmailService, smsService);
            testOrderService.PlaceOrder(order);
        }

        // Тестовая реализация для демонстрации DIP
        public class TestEmailService : SOLID_Fundamentals.IEmailService
        {
            public void SendEmail(string to, string subject, string body)
            {
                Console.WriteLine($"[TEST] Отправка тестового email на {to}: {subject}");
            }
        }
    }
}