namespace SOLID_Fundamentals //DIP
{
    using System;

    public class EmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            Console.WriteLine($"Sending email to {to}: {subject}");
        }
    }

    public class SmsService
    {
        public void SendSms(string phoneNumber, string message)
        {
            Console.WriteLine($"Sending SMS to {phoneNumber}: {message}");
        }
    }

    public class OrderService
    {
        private EmailService _emailService;
        private SmsService _smsService;

        public OrderService()
        {
            _emailService = new EmailService();
            _smsService = new SmsService();
        }

        public void PlaceOrder(Order order)
        {
            _emailService.SendEmail(order.CustomerEmail, "Order Confirmation", "Your order has been placed");
            _smsService.SendSms(order.CustomerPhone, "Your order has been placed");
        }
    }

    public class NotificationService
    {
        private EmailService _emailService;

        public NotificationService()
        {
            _emailService = new EmailService();
        }

        public void SendPromotion(string email, string promotion)
        {
            _emailService.SendEmail(email, "Special Promotion", promotion);
        }
    }
}
